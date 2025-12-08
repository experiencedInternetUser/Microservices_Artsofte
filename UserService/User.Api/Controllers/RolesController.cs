using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using User.Application.Services;
using User.Core.DTOs;

namespace User.Api.Controllers
{
    [ApiController]
    [Route("api/v1/roles")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _svc;
        public RolesController(IRoleService svc) => _svc = svc;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleRequest req)
        {
            var created = await _svc.CreateAsync(req);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var r = await _svc.GetAsync(id);
            if (r == null) return NotFound();
            return Ok(r);
        }

        [HttpGet]
        public async Task<IActionResult> List() => Ok(await _svc.ListAsync());

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoleRequest req)
        {
            await _svc.UpdateAsync(id, req);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _svc.DeleteAsync(id);
            return NoContent();
        }
    }
}
