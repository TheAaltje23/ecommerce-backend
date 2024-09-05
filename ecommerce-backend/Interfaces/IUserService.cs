using ecommerce_backend.Dto;

namespace ecommerce_backend.Interfaces
{
    public interface IUserService
    {
        // Read single
        Task<ReadUserDto> GetUserById(long id);
        Task<ReadUserDto> GetUserByUsername(string username);
        //Task<User> GetUserByLastName(string lastName);
        //Task<User> GetUserByPhoneNumber(string phoneNumber);
        //Task<User> GetUserByEmail(string email);

        //// Read multiple
        //Task<IEnumerable<User>> GetAllUsers();
        //Task<IEnumerable<User>> GetAllUsersByRole(User.Role role);

        //// CUD
        Task CreateUser(CreateUserDto dto);
        Task UpdateUser(UpdateUserDto dto, long id);
        //Task DeleteUser(long id);
    }
}