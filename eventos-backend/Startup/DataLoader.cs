using eventos_backend.DTOs.Account;
using eventos_backend.Services.Interfaces;

namespace eventos_backend.Startup
{
    public static class DataLoader
    {
        public static async Task Seed(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            IAccountService accountService = scope.ServiceProvider.GetService<IAccountService>();

            RegisterDTO registerDTO = new RegisterDTO();
            registerDTO.Username = "admin";
            registerDTO.Password = "123456";
            registerDTO.Password2 = "123456";
            registerDTO.Email = "admin@gmail.com";

            await accountService.Register(registerDTO);


        }
    }
}
