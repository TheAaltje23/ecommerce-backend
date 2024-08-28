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

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetUserById(long id)
        {
            _logger.ReceiveHttpRequest<User>(nameof(GetUserById));
            var user = await _service.GetUserById(id);
            _logger.ReturnHttpResponse<User>(nameof(GetUserById));
            return Ok(user);
        }
    }
}