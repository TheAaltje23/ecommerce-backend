using ecommerce_backend.Dto;

namespace ecommerce_backend.Interfaces
{
    public interface IUserService
    {
        Task<ReadUserDto> GetUserById(long id);
        Task<PaginationDto<ReadUserDto>> Search(SearchUserDto dto);
        Task CreateUser(CreateUserDto dto);
        Task RegisterUser(RegisterUserDto dto);
        Task<string> LogIn(LogInDto dto);
        Task UpdateUser(UpdateUserDto dto, long id);
        Task UpdateUserInfo(UpdateUserInfoDto dto, long jwtId);
        Task DeleteUser(long id);
    }
}