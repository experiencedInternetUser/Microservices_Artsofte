using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLib.DTOs;
using CoreLib.Entities;
using CoreLib.Interfaces;
using Logic.Http;
using System.Net.Http;
using Logic.Trace;

namespace Logic.Services
{
    public interface ITaskService
    {
        Task<TaskDto> CreateAsync(CreateTaskRequest req);
        Task<TaskDto?> GetAsync(Guid id);
        Task<IEnumerable<TaskDto>> ListAsync(Guid? projectId = null);
        Task UpdateAsync(Guid id, UpdateTaskRequest req);
        Task DeleteAsync(Guid id);
    }

    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepo;
        private readonly ILabelRepository _labelRepo;
        private readonly IHttpRequestService _httpRequestService;
        private readonly ITraceWriter _traceWriter;

        public TaskService(ITaskRepository taskRepo, ILabelRepository labelRepo, IHttpRequestService httpRequestService, ITraceWriter traceWriter)
        {
            _taskRepo = taskRepo;
            _labelRepo = labelRepo;
            _httpRequestService = httpRequestService;
            _traceWriter = traceWriter;
        }

        public async Task<TaskDto> CreateAsync(CreateTaskRequest req)
        {
            var task = new TaskEntity
            {
                ProjectId = req.ProjectId,
                Title = req.Title,
                Description = req.Description,
                ReporterId = req.ReporterId,
                Priority = req.Priority,
                DueDate = req.DueDate,
                Estimate = req.Estimate
            };

            // assignments
            if (req.AssigneeIds != null)
            {
                foreach (var uid in req.AssigneeIds)
                {
                    task.Assignments.Add(new TaskAssignment { TaskId = task.Id, UserId = uid });
                }
            }

            // labels: validate existence
            if (req.LabelIds != null)
            {
                foreach (var lid in req.LabelIds)
                {
                    var label = await _labelRepo.GetAsync(lid);
                    if (label != null)
                    {
                        task.TaskLabels.Add(new TaskLabel { TaskId = task.Id, LabelId = lid });
                    }
                }
            }

            // --- ASYNC CALL TO UserService (service A -> user-api) ---
            if (req.ReporterId.HasValue)
            {
                try
                {
                    var uri = new Uri($"http://user-api/api/v1/profiles/by-user/{req.ReporterId.Value}");
                    var httpReq = new HttpRequestData
                    {
                        Method = HttpMethod.Get,
                        Uri = uri
                    };

                    // send request; the request will include TraceId header automatically
                    var resp = await _httpRequestService.SendRequestAsync<System.Text.Json.JsonElement>(httpReq);

                    if (resp.IsSuccessStatusCode && resp.Body.ValueKind != System.Text.Json.JsonValueKind.Undefined)
                    {
                        // optionally process profile data; here — just log basic info to console for demo
                        try
                        {
                            string? fullName = null;
                            if (resp.Body.TryGetProperty("fullName", out var fnProp) && fnProp.ValueKind == System.Text.Json.JsonValueKind.String)
                                fullName = fnProp.GetString();

                            Console.WriteLine($"[TaskService] fetched reporter profile for {req.ReporterId}: fullName={fullName}; trace={_traceWriter.GetValue()}");
                            // Optionally: store snapshot into task.Description or a dedicated Audit store
                            if (!string.IsNullOrEmpty(fullName) && string.IsNullOrWhiteSpace(task.Description))
                            {
                                task.Description = $"Reporter name snapshot: {fullName}";
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("[TaskService] failed to parse profile response: " + ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"[TaskService] profile not found or request failed for reporter {req.ReporterId}. Status: {resp.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    // network errors shouldn't break task creation for MVP (depends on requirements)
                    Console.WriteLine("[TaskService] error calling user-api: " + ex.Message);
                }
            }

            var created = await _taskRepo.AddAsync(task);

            return new TaskDto(created.Id, created.ProjectId, created.Title, created.Description, created.Status, created.Priority, created.ReporterId, created.CreatedAt, created.UpdatedAt, created.DueDate, created.Estimate, created.Assignments.Select(a => a.UserId), created.TaskLabels.Select(tl => tl.LabelId));
        }

        public async Task DeleteAsync(Guid id) => await _taskRepo.DeleteAsync(id);

        public async Task<TaskDto?> GetAsync(Guid id)
        {
            var t = await _taskRepo.GetAsync(id);
            if (t == null) return null;
            return new TaskDto(t.Id, t.ProjectId, t.Title, t.Description, t.Status, t.Priority, t.ReporterId, t.CreatedAt, t.UpdatedAt, t.DueDate, t.Estimate, t.Assignments.Select(a => a.UserId), t.TaskLabels.Select(tl => tl.LabelId));
        }

        public async Task<IEnumerable<TaskDto>> ListAsync(Guid? projectId = null)
        {
            var list = await _taskRepo.ListAsync(projectId);
            return list.Select(t => new TaskDto(t.Id, t.ProjectId, t.Title, t.Description, t.Status, t.Priority, t.ReporterId, t.CreatedAt, t.UpdatedAt, t.DueDate, t.Estimate, t.Assignments.Select(a => a.UserId), t.TaskLabels.Select(tl => tl.LabelId)));
        }

        public async Task UpdateAsync(Guid id, UpdateTaskRequest req)
        {
            var existing = await _taskRepo.GetAsync(id);
            if (existing == null) throw new KeyNotFoundException("Task not found");
            if (req.Title != null) existing.Title = req.Title;
            if (req.Description != null) existing.Description = req.Description;
            if (req.Status != null) existing.Status = req.Status;
            if (req.Priority != null) existing.Priority = req.Priority;
            if (req.DueDate.HasValue) existing.DueDate = req.DueDate;
            if (req.Estimate.HasValue) existing.Estimate = req.Estimate;
            await _taskRepo.UpdateAsync(existing);
        }
    }
}
