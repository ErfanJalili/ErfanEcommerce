using Product.Entities.Users;
using Product.Services;
using System.Threading.Tasks;

namespace Product.Services.Services
{
    public interface IJwtService
    {
        Task<AccessToken> GenerateAsync(User user);
    }
}