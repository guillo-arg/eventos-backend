using eventos_backend.DTOs.Account;
using eventos_backend.Models;
using eventos_backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace eventos_backend.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDataContext _appDataContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountService(AppDataContext appDataContext, UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration configuration)
        {
            _appDataContext = appDataContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> Login(LoginDTO loginDto)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            if (_userManager.Users.All(x => x.UserName != loginDto.Username))
            {
                response.Success = false;
                response.Data = "No se encontró el usuario";

                return response;
            }

            if (_userManager.Users.Any(x => x.UserName.ToUpper() == loginDto.Username.ToUpper() && !x.Enabled))
            {
                response.Success = false;
                response.Data = "Usuario inactivo";

                return response;
            }


            User user = await _userManager.FindByNameAsync(loginDto.Username);
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (result)
            {
                //generar token
                string token = GenerateJSONWebToken(user);

                response.Success = true;
                response.Data = token;

                return response;
            }
            else
            {
                response.Success = false;
                response.Data = "Credenciales incorrectas";

                return response;
            }

        }

        private string GenerateJSONWebToken(User user)
        {
            string secretKey = _configuration["SECRET_KEY"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ServiceResponse<string>> Register(RegisterDTO registerDto)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            User user = new User()
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                Id = Guid.NewGuid().ToString()
            };

            IdentityResult registerResult = _userManager.CreateAsync(user, registerDto.Password).Result;

            if (registerResult.Succeeded)
            {
                response.Success = true;
                response.Data = "Se registró correctamente el usuario";
            }
            else
            {
                response.Success = false;
                response.Data = "No se pudo registrar el usuario";
            }

            return response;
        }

        public async Task<ServiceResponse<string>> CreateRole(RoleDTO roleDTO)
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
            try
            {

                if (await _roleManager.RoleExistsAsync(roleDTO.Name))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Data = $"El rol con el nombre: {roleDTO.Name} ya existe";
                    return serviceResponse;
                }

                Role role = new Role()
                {
                    Name = roleDTO.Name,
                    Id = Guid.NewGuid().ToString()
                };

                IdentityResult identityResult = await _roleManager.CreateAsync(role);
                if (identityResult.Succeeded)
                {
                    serviceResponse.Success = true;
                    serviceResponse.Data = $"Se creó el rol {roleDTO.Name}";
                    return serviceResponse;
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Data = $"No se pudo crear el rol: ${roleDTO.Name}";
                    return serviceResponse;
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Data = "No se pudo crear el rol: ${roleDTO.Name}";
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<string>> AssignRole(AssignRoleDTO assignRoleDTO)
        {
            throw new NotImplementedException();
        }
    }
}
