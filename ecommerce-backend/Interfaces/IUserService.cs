using ecommerce_backend.Dto;

namespace ecommerce_backend.Interfaces
{
    public interface IUserService
    {
        Task<ReadUserDto> GetUserById(long id);
        Task<PaginationDto<ReadUserDto>> Search(SearchUserDto dto);
        Task RegisterUser(RegisterUserDto dto);
        Task<string> LogIn(LogInDto dto);
        //Task LogOut(LogOutDto dto);
        Task CreateUser(CreateUserDto dto);
        Task UpdateUser(UpdateUserDto dto, long id);
        Task DeleteUser(long id);
    }
}