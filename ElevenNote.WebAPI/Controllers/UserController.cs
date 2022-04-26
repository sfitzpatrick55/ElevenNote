using Microsoft.AspNetCore.Mvc;
using ElevenNote.Services.User;
using ElevenNote.Models.User;

namespace ElevenNote.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegister model)
    {
        if (!modelState.IsValid)
        {
            return BadRequest(modelState);
        }

        var registerResult = await _service.RegisterUserAsync(model);
        if (registerResult)
        {
            return Ok("User was registered.");
        }
        return BadRequest("User could not be registered");
    }
}
