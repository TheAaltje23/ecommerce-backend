using AutoMapper;
using ecommerce_backend.Data;
using ecommerce_backend.Dto;
using ecommerce_backend.Exceptions;
using ecommerce_backend.Helpers;
using ecommerce_backend.Interfaces;
using ecommerce_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
        public async Task CreateUser(CreateUserDto dto)
        {
            var existingUser = await _db.User.FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (existingUser != null)
            {
                throw new AlreadyExistsException<User>(nameof(dto.Username), dto.Username);
            }

            var newUser = _mapper.Map<User>(dto);

            _logger.CreateDb<User>(nameof(newUser.Username), newUser.Username);
            _db.User.Add(newUser);
            await _db.SaveChangesAsync();
        }

        // UPDATE
        public async Task UpdateUser(UpdateUserDto dto, long id)
        {
            var existingUser = await _db.User.FindAsync(id) ?? throw new NotFoundException<User>(nameof(id), id);

            existingUser.Username = dto.Username ?? existingUser.Username;
            existingUser.Password = dto.Password ?? existingUser.Password;
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