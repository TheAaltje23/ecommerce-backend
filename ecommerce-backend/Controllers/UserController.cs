using ecommerce_backend.Dto;
using ecommerce_backend.Interfaces;
using ecommerce_backend.Helpers;
using ecommerce_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly LoggingHelper _logger;
        private readonly IUserService _service;

        public UserController(ILogger<UserController> logger, IUserService service)
        {
            _logger = new LoggingHelper(logger);
            _service = service;
        }

        // GET
        [Authorize(Roles = "Admin")]
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetUserById(long id)
        {
            _logger.ReceiveHttpRequest<User>(nameof(GetUserById));
            var user = await _service.GetUserById(id);
            _logger.ReturnHttpResponse<User>(nameof(GetUserById));
            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchUserDto dto)
        {
            _logger.ReceiveHttpRequest<User>(nameof(Search));
            var users = await _service.Search(dto);
            _logger.ReturnHttpResponse<User>(nameof(Search));
            return Ok(users);
        }

        // POST
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            _logger.ReceiveHttpRequest<User>(nameof(RegisterUser));
            await _service.RegisterUser(dto);
            _logger.ReturnHttpResponse<User>(nameof(RegisterUser));
            return Created();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] LogInDto dto)
        {
            _logger.ReceiveHttpRequest<User>(nameof(LogIn));
            var token = await _service.LogIn(dto);
            _logger.ReturnHttpResponse<User>(nameof(LogIn));
            return Ok(new { token });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            _logger.ReceiveHttpRequest<User>(nameof(CreateUser));
            await _service.CreateUser(dto);
            _logger.ReturnHttpResponse<User>(nameof(CreateUser));
            return Created();
        }

        // PUT
        [Authorize(Roles = "Admin")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto dto, long id)
        {
            _logger.ReceiveHttpRequest<User>(nameof(UpdateUser));
            await _service.UpdateUser(dto, id);
            _logger.ReturnHttpResponse<User>(nameof(UpdateUser));
            return NoContent();
        }

        // DELETE
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            _logger.ReceiveHttpRequest<User>(nameof(DeleteUser));
            await _service.DeleteUser(id);
            _logger.ReturnHttpResponse<User>(nameof(DeleteUser));
            return NoContent();
        }
    }
}