using AutoMapper;
using ecommerce_backend.Data;
using ecommerce_backend.Dto;
using ecommerce_backend.Helpers;
using ecommerce_backend.Interfaces;
using ecommerce_backend.Models;
using ecommerce_backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;

namespace ecommerce_backend.Tests.UserTests
{
    public class UserServiceTests
    {
        private readonly Mock<ILogger<UserService>> _loggerMock;
        private readonly Mock<ApplicationDbContext> _dbMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IPasswordHasher<User>> _passwordHasherMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _loggerMock = new Mock<ILogger<UserService>>();
            _dbMock = new Mock<ApplicationDbContext>();
            _mapperMock = new Mock<IMapper>();
            _passwordHasherMock = new Mock<IPasswordHasher<User>>();
            _tokenServiceMock = new Mock<ITokenService>();

            var loggingHelper = new LoggingHelper(_loggerMock.Object);
            _userService = new UserService(_loggerMock.Object, _dbMock.Object, _mapperMock.Object, _passwordHasherMock.Object, _tokenServiceMock.Object);
        }

        [Fact]
        public void UserService_GetUserById_ReturnReadUserDto()
        {
            // Arrange
            var userId = 1L;
            var user = new User { Id = userId, Username = "testuser", Password = "Wachtwoord1!" };
            var userDto = new ReadUserDto { Id = userId, Username = "testuser" };
            _dbMock.Setup(db => db.User.FindAsync(userId)).ReturnsAsync(user);
            _mapperMock.Setup(map => map.Map<ReadUserDto>(user)).Returns(userDto);

            // Act
            var result = _userService.GetUserById(userId);

            // Assert

        }
    }
}
