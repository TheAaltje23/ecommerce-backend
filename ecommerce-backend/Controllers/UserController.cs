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

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            _logger.ReceiveHttpRequest<User>(nameof(GetUserByUsername));
            var user = await _service.GetUserByUsername(username);
            _logger.ReturnHttpResponse<User>(nameof(GetUserByUsername));
            return Ok(user);
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
            return Ok();
        }
    }
}