using eventos_backend.DTOs.Account;
using eventos_backend.Exceptions;
using eventos_backend.Models;
using eventos_backend.Services;
using eventos_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eventos_backend.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;
        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration configuration, IAccountService accountService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            ValidateModel();

            string response = await _accountService.Register(registerDto);
            return Ok(response);
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            ValidateModel();
            string response = await _accountService.Login(loginDto);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        [Route("Role")]
        public async Task<IActionResult> CreateRole([FromBody] RoleDTO roleDTO)
        {
            ValidateModel();

            string response = await _accountService.CreateRole(roleDTO);
            return Ok(response);

        }

        [Authorize]
        [HttpPost]
        [Route("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody]AssignRoleDTO assignRoleDTO)
        {
            ValidateModel();

            string response = await _accountService.AssignRole(assignRoleDTO);

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [Route("Users")]
        public async Task<IActionResult> GetUsers()
        {
            List<UserDTO> userDTOs = await _accountService.GetAllUsers();

            return Ok(userDTOs);
        }

        [Authorize]
        [HttpGet]
        [Route("Users/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            UserDTO userDTO = await _accountService.GetUserById(id);

            return Ok(userDTO);

        }

        [Authorize]
        [HttpGet]
        [Route("Roles")]
        public async Task<IActionResult> GetRoles()
        {
            List<RoleDTO> roleDTOs = await _accountService.GetAllRoles();

            return Ok(roleDTOs);
        }


        private void ValidateModel()
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = ModelState.Select(x => x.Value.Errors.FirstOrDefault().ErrorMessage).ToList();
                throw new AppException(errors, 400);
            }
        }

    }
}
