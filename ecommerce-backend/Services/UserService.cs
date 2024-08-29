using AutoMapper;
using ecommerce_backend.Data;
using ecommerce_backend.Dto;
using ecommerce_backend.Exceptions;
using ecommerce_backend.Helpers;
using ecommerce_backend.Interfaces;
using ecommerce_backend.Models;
using Microsoft.EntityFrameworkCore;

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

        // READ
        public async Task<ReadUserDto> GetUserById(long id)
        {
            _logger.ReadDb<User>(nameof(id), id);
            var user = await _db.User.FindAsync(id);
            if (user == null)
            {
                throw new NotFoundException<User>(nameof(id), id);
            }
            return _mapper.Map<ReadUserDto>(user);
        }

        public async Task<ReadUserDto> GetUserByUsername(string username)
        {
            _logger.ReadDb<User>(nameof(username), username);
            var user = await _db.User.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                throw new NotFoundException<User>(nameof(username), username);
            }
            return _mapper.Map<ReadUserDto>(user);
        }

        // CREATE
        public async Task CreateUser(CreateUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
            {
                throw new IncompleteException<User>();
            }

            var existingUser = await _db.User.FirstOrDefaultAsync(u =>  u.Username == dto.Username);

            if (existingUser != null)
            {
                throw new AlreadyExistsException<User>(nameof(dto.Username), dto.Username);
            }

            var newUser = _mapper.Map<User>(dto);

            _logger.CreateDb<User>(nameof(newUser.Username), newUser.Username);
            _db.User.Add(newUser);
            await _db.SaveChangesAsync();
        }
    }
}