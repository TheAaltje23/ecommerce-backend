using AutoMapper;
using ecommerce_backend.Data;
using ecommerce_backend.Dto;
using ecommerce_backend.Exceptions;
using ecommerce_backend.Helpers;
using ecommerce_backend.Interfaces;
using ecommerce_backend.Models;

namespace ecommerce_backend.Services
{
    public class UserService : IUserService
    {
        private readonly LoggingHelper _logger;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UserService(ILogger<UserService> logger, ApplicationDbContext db, IMapper mapper)
        {
            _logger = new LoggingHelper(logger);
            _db = db;
            _mapper = mapper;
        }

        public async Task<ReadUserDto> GetUserById(long id)
        {
            _logger.FetchDb<User>(nameof(id), id);
            var user = await _db.User.FindAsync(id);
            if (user == null)
            {
                _logger.NotFoundDb<User>(nameof(id), id);
                throw new NotFoundException<User>(nameof(id), id);
            }
            var userDto = _mapper.Map<ReadUserDto>(user);
            return userDto;
        }
    }
}