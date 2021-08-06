using System.Threading.Tasks;
using FlashOrder.DTOs;

namespace FlashOrder.Services.Auth
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginDTO loginDto);
        Task<string> CreateToken();
    }
}