using System;
using System.Threading.Tasks;
using System.Linq;
using Logic.Services;
using Microsoft.AspNetCore.Mvc;
using CoreLib.DTOs;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;
        public TasksController(ITaskService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskRequest req)
        {
            var created = await _service.CreateAsync(req);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var t = await _service.GetAsync(id);
            if (t == null) return NotFound();
            return Ok(t);
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] Guid? projectId)
        {
            var list = await _service.ListAsync(projectId);
            return Ok(list);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskRequest req)
        {
            await _service.UpdateAsync(id, req);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
