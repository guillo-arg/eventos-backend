using eventos_backend.DTOs.Account;
using eventos_backend.Models;
using eventos_backend.Services;
using eventos_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eventos_backend.Controllers
{
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
                return BadRequest(ModelState);
            }

            ServiceResponse<string> serviceResponse = await _accountService.Register(registerDto);

            if (serviceResponse.Success)
            {
                return Ok(serviceResponse.Data);
            }
            else
            {
                return BadRequest(serviceResponse.Data);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServiceResponse<string> serviceResponse = await _accountService.Login(loginDto);

            if (serviceResponse.Success)
            {
                return Ok(serviceResponse.Data);
            }
            else
            {
                return BadRequest(serviceResponse.Data);
            }
        }
    }
}
