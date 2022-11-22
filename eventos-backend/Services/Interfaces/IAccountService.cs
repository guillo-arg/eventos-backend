using eventos_backend.DTOs.Account;

namespace eventos_backend.Services.Interfaces
{
    public interface IAccountService
    {
        Task<string> Register(RegisterDTO registerDto);
        Task<string> Login(LoginDTO loginDto);
        Task<string> CreateRole(RoleDTO roleDTO);
        Task<string> AssignRole(AssignRoleDTO assignRoleDTO);
    }
}
