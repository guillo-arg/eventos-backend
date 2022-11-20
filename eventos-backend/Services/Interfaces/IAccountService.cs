using eventos_backend.DTOs.Account;

namespace eventos_backend.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ServiceResponse<string>> Register(RegisterDTO registerDto);
        Task<ServiceResponse<string>> Login(LoginDTO loginDto);
    }
}
