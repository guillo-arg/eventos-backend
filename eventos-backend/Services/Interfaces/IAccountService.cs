using eventos_backend.DTOs.Account;

namespace eventos_backend.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ServiceResponse<string>> Register(RegisterDTO registerDto);
        Task<ServiceResponse<string>> Login(LoginDTO loginDto);
        Task<ServiceResponse<string>> CreateRole(RoleDTO roleDTO);
        Task<ServiceResponse<string>> AssignRole(AssignRoleDTO assignRoleDTO);
    }
}
