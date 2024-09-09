using ecommerce_backend.Dto;

namespace ecommerce_backend.Interfaces
{
    public interface IUserService
    {
        Task<ReadUserDto> GetUserById(long id);
        Task<IEnumerable<ReadUserDto>> GetAllUsers();
        Task<IEnumerable<ReadUserDto>> Search(SearchUserDto dto);
        Task CreateUser(CreateUserDto dto);
        Task UpdateUser(UpdateUserDto dto, long id);
        Task DeleteUser(long id);
    }
}