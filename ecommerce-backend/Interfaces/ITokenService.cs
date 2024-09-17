using ecommerce_backend.Models;

namespace ecommerce_backend.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}