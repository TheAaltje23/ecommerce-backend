using AutoMapper;
using ecommerce_backend.Data;
using ecommerce_backend.Dto;
using ecommerce_backend.Exceptions;
using ecommerce_backend.Helpers;
using ecommerce_backend.Interfaces;
using ecommerce_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_backend.Services
{
    public class UserService : IUserService
    {
        private readonly LoggingHelper _logger;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenService _tokenService;

        public UserService(ILogger<UserService> logger, ApplicationDbContext db, IMapper mapper, IPasswordHasher<User> passwordhasher, ITokenService tokenService)
        {
            _logger = new LoggingHelper(logger);
            _db = db;
            _mapper = mapper;
            _passwordHasher = passwordhasher;
            _tokenService = tokenService;
        }

        // READ
        public async Task<ReadUserDto> GetUserById(long id)
        {
            var user = await _db.User.FindAsync(id) ?? throw new NotFoundException<User>(nameof(id), id);
            _logger.ReadDb<User>(nameof(id), id);
            return _mapper.Map<ReadUserDto>(user);
        }

        public async Task<PaginationDto<ReadUserDto>> Search(SearchUserDto dto)
        {
            IQueryable<User> query = _db.User;

            // Filtering
            if (dto.Id != null)
            {
                query = query.Where(u => u.Id == dto.Id);
            }

            if (!string.IsNullOrEmpty(dto.Username))
            {
                query = query.Where(u => u.Username != null && u.Username.Contains(dto.Username));
            }

            if (!string.IsNullOrEmpty(dto.FirstName))
            {
                query = query.Where(u => u.FirstName != null && u.FirstName.Contains(dto.FirstName));
            }

            if (!string.IsNullOrEmpty(dto.LastName))
            {
                query = query.Where(u => u.LastName != null && u.LastName.Contains(dto.LastName));
            }

            if (!string.IsNullOrEmpty(dto.Email))
            {
                query = query.Where(u => u.Email != null && u.Email.Contains(dto.Email));
            }

            if (!string.IsNullOrEmpty(dto.PhoneNumber))
            {
                query = query.Where(u => u.PhoneNumber != null && u.PhoneNumber.Contains(dto.PhoneNumber));
            }

            if (dto.UserRole != null)
            {
                query = query.Where(u => u.UserRole == dto.UserRole);
            }

            int totalItems = await query.CountAsync();

            // Sorting
            query = dto.SortOrder.ToLower() == "asc"
                ? query.OrderBy(SortHelper.Sort<User>(dto.SortBy))
                : query.OrderByDescending(SortHelper.Sort<User>(dto.SortBy));

            // Pagination
            query = query.Skip((dto.Page - 1) * dto.PageSize).Take(dto.PageSize);

            _logger.SearchDb<User>(totalItems);
            var users = await query.ToListAsync();
            var mappedUsers = _mapper.Map<IEnumerable<ReadUserDto>>(users);

            return new PaginationDto<ReadUserDto>
            {
                Items = mappedUsers,
                Page = dto.Page,
                PageSize = dto.PageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling((double)totalItems / dto.PageSize)
            };
        }

        // CREATE
        public async Task RegisterUser(RegisterUserDto dto)
        {
            var existingUser = await _db.User.FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (existingUser != null)
            {
                throw new AlreadyExistsException<User>(nameof(dto.Username), dto.Username);
            }

            var newUser = _mapper.Map<User>(dto);

            newUser.Password = _passwordHasher.HashPassword(newUser, dto.Password);

            _logger.CreateDb<User>(nameof(newUser.Username), newUser.Username);
            _db.User.Add(newUser);
            await _db.SaveChangesAsync();
        }

        public async Task<string> LogIn(LogInDto dto)
        {
            var existingUser = await _db.User.FirstOrDefaultAsync(u => u.Username == dto.Username) ?? throw new UnauthorizedAccessException("Invalid username or password.");
            var verificationResult = _passwordHasher.VerifyHashedPassword(existingUser, existingUser.Password, dto.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            _logger.LogIn<User>(nameof(dto.Username), dto.Username);
            return _tokenService.GenerateToken(existingUser);
        }

        public async Task CreateUser(CreateUserDto dto)
        {
            var existingUser = await _db.User.FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (existingUser != null)
            {
                throw new AlreadyExistsException<User>(nameof(dto.Username), dto.Username);
            }

            var newUser = _mapper.Map<User>(dto);

            newUser.Password = _passwordHasher.HashPassword(newUser, dto.Password);

            _logger.CreateDb<User>(nameof(newUser.Username), newUser.Username);
            _db.User.Add(newUser);
            await _db.SaveChangesAsync();
        }

        // UPDATE
        public async Task UpdateUser(UpdateUserDto dto, long id)
        {
            var existingUser = await _db.User.FindAsync(id) ?? throw new NotFoundException<User>(nameof(id), id);

            existingUser.Username = dto.Username ?? existingUser.Username;
            existingUser.Password = dto.Password != null ? _passwordHasher.HashPassword(existingUser, dto.Password) : existingUser.Password;
            existingUser.FirstName = dto.FirstName ?? existingUser.FirstName;
            existingUser.LastName = dto.LastName ?? existingUser.LastName;
            existingUser.Email = dto.Email ?? existingUser.Email;
            existingUser.PhoneNumber = dto.PhoneNumber ?? existingUser.PhoneNumber;

            if (existingUser.UserRole != dto.UserRole)
            {
                existingUser.UserRole = dto.UserRole;
            }

            _logger.UpdateDb<User>(nameof(id), id);
            _db.User.Update(existingUser);
            await _db.SaveChangesAsync();
        }

        // DELETE
        public async Task DeleteUser(long id)
        {
            var existingUser = await _db.User.FindAsync(id) ?? throw new NotFoundException<User>(nameof(id), id);
            _logger.DeleteDb<User>(nameof(id), id);
            _db.User.Remove(existingUser);
            await _db.SaveChangesAsync();
        }
    }
}