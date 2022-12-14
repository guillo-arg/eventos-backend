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
    [Route("[Controller]")]
    public class AccountController : Controller
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
            if (!ModelState.IsValid)
            {
                List<string> errors = ModelState.Select(x => x.Value.Errors.FirstOrDefault().ErrorMessage).ToList();
                throw new AppException(errors, 400);
            }

            string response = await _accountService.Register(registerDto);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {           
            if (!ModelState.IsValid)
            {
                List<string> errors = ModelState.Select(x => x.Value.Errors.FirstOrDefault().ErrorMessage).ToList();
                throw new AppException(errors, 400);
            }

            string response = await _accountService.Login(loginDto);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        [Route("Role")]
        public async Task<IActionResult> CreateRole([FromBody] RoleDTO roleDTO)
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = ModelState.Select(x => x.Value.Errors.FirstOrDefault().ErrorMessage).ToList();
                throw new AppException(errors, 400);
            }

            string response = await _accountService.CreateRole(roleDTO);
            return Ok(response);

        }

        [Authorize]
        [HttpPost]
        [Route("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody]AssignRoleDTO assignRoleDTO)
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = ModelState.Select(x => x.Value.Errors.FirstOrDefault().ErrorMessage).ToList();
                throw new AppException(errors, 400);
            }

            string response = await _accountService.AssignRole(assignRoleDTO);

            return Ok(response);
        }

        //[Authorize]
        //[HttpGet]
        //[Route("User")]
        //public async Task<IActionResult> GetUsers()
        //{
        //    ServiceResponse<UserDTO> serviceResponse = _
        //}
    }
}
