using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using User.Application.Services;
using User.Core.DTOs;

namespace User.Api.Controllers
{
    [ApiController]
    [Route("api/v1/profiles")]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfileService _svc;
        public ProfilesController(IProfileService svc) => _svc = svc;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProfileRequest req)
        {
            var created = await _svc.CreateAsync(req);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var p = await _svc.GetAsync(id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpGet("by-user/{userId:guid}")]
        public async Task<IActionResult> GetByUser(Guid userId)
        {
            var p = await _svc.GetByUserIdAsync(userId);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpGet]
        public async Task<IActionResult> List() => Ok(await _svc.ListAsync());

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProfileRequest req)
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
