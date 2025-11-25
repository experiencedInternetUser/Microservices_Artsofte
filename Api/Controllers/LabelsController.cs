using System;
using System.Threading.Tasks;
using Logic.Services;
using Microsoft.AspNetCore.Mvc;
using CoreLib.DTOs;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/labels")]
    public class LabelsController : ControllerBase
    {
        private readonly ILabelService _service;
        public LabelsController(ILabelService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLabelRequest req)
        {
            var created = await _service.CreateAsync(req);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var l = await _service.GetAsync(id);
            if (l == null) return NotFound();
            return Ok(l);
        }

        [HttpGet]
        public async Task<IActionResult> List() => Ok(await _service.ListAsync());

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLabelRequest req)
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
