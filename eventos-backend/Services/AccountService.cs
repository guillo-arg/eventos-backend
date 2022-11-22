using eventos_backend.DTOs.Account;
using eventos_backend.Models;
using eventos_backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using eventos_backend.Exceptions;

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

        public async Task<string> Login(LoginDTO loginDto)
        {

            if (_userManager.Users.All(x => x.UserName != loginDto.Username))
            {
                throw new AppException(new List<string>() { "No se encontró el usuario" }, 400);
            }

            if (_userManager.Users.Any(x => x.UserName.ToUpper() == loginDto.Username.ToUpper() && !x.Enabled))
            {
                throw new AppException(new List<string>() { "Usuario inactivo" }, 400);
            }


            User user = await _userManager.FindByNameAsync(loginDto.Username);
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (result)
            {
                //generar token
                string token = GenerateJSONWebToken(user);

                return token;
            }
            else
            {
                throw new AppException(new List<string>() { "Credenciales incorrectas" }, 400);                
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

        public async Task<string> Register(RegisterDTO registerDto)
        {
            
            User user = new User()
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                Id = Guid.NewGuid().ToString()
            };

            IdentityResult registerResult = _userManager.CreateAsync(user, registerDto.Password).Result;

            if (registerResult.Succeeded)
            {                
                return "Se registró correctamente el usuario";
            }
            else
            {
                List<string> errors = registerResult.Errors.Select(x => x.Description).ToList();
                throw new AppException(errors, 400);                
            }
        }

        public async Task<string> CreateRole(RoleDTO roleDTO)
        {            
            try
            {

                if (await _roleManager.RoleExistsAsync(roleDTO.Name))
                {
                    throw new AppException(new List<string>() { $"El rol con el nombre: {roleDTO.Name} ya existe" }, 400);
                }

                Role role = new Role()
                {
                    Name = roleDTO.Name,
                    Id = Guid.NewGuid().ToString()
                };

                IdentityResult identityResult = await _roleManager.CreateAsync(role);
                if (identityResult.Succeeded)
                {
                    return $"Se creó el rol {roleDTO.Name}";
                }
                else
                {
                    throw new AppException(new List<string>() { $"No se pudo crear el rol: ${roleDTO.Name}" }, 400);;
                }

            }
            catch (Exception ex)
            {
                throw new AppException(new List<string>() {$"No se pudo crear el rol: {roleDTO.Name}" }, 400); ;
            }
        }

        public async Task<string> AssignRole(AssignRoleDTO assignRoleDTO)
        {
            throw new NotImplementedException();
        }
    }
}
