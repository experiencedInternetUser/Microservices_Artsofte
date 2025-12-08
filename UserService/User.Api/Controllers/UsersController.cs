using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using User.Application.Services;
using User.Core.DTOs;

namespace User.Api.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _svc;
        public UsersController(IUserService svc) => _svc = svc;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest req)
        {
            var created = await _svc.CreateAsync(req);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var u = await _svc.GetAsync(id);
            if (u == null) return NotFound();
            return Ok(u);
        }

        [HttpGet]
        public async Task<IActionResult> List() => Ok(await _svc.ListAsync());

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest req)
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

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest login)
        {
            var u = await _svc.AuthenticateAsync(login.Email, login.Password);
            if (u == null) return Unauthorized();
            return Ok(u);
        }
    }

    public record LoginRequest(string Email, string Password);
}
