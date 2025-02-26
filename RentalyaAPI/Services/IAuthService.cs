using RentalyaAPI.Models;

namespace RentalyaAPI.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterUser(RegisterModel model);
    }
} 