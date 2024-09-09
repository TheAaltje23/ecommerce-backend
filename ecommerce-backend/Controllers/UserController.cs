using ecommerce_backend.Dto;
using ecommerce_backend.Interfaces;
using ecommerce_backend.Helpers;
using ecommerce_backend.Models;
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
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetUserById(long id)
        {
            _logger.ReceiveHttpRequest<User>(nameof(GetUserById));
            var user = await _service.GetUserById(id);
            _logger.ReturnHttpResponse<User>(nameof(GetUserById));
            return Ok(user);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            _logger.ReceiveHttpRequest<User>(nameof(GetAllUsers));
            var users = await _service.GetAllUsers();
            _logger.ReturnHttpResponse<User>(nameof(GetAllUsers));
            return Ok(users);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchUserDto dto)
        {
            _logger.ReceiveHttpRequest<User>(nameof(Search));
            var users = await _service.Search(dto);
            _logger.ReturnHttpResponse<User>(nameof(Search));
            return Ok(users);
        }

        // POST
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            _logger.ReceiveHttpRequest<User>(nameof(CreateUser));
            await _service.CreateUser(dto);
            _logger.ReturnHttpResponse<User>(nameof(CreateUser));
            return Created();
        }

        // PUT
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto dto, long id)
        {
            _logger.ReceiveHttpRequest<User>(nameof(UpdateUser));
            await _service.UpdateUser(dto, id);
            _logger.ReturnHttpResponse<User>(nameof(UpdateUser));
            return NoContent();
        }

        // DELETE
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