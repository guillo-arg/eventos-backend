using eventos_backend.Models;
using eventos_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eventos_backend.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ProvinciaController : ControllerBase
    {
        private readonly IProvinciaService _provinciaService;
        public ProvinciaController(IProvinciaService provinciaService)
        {
            _provinciaService = provinciaService;
        }

        [HttpGet] public async Task<IActionResult> GetAll()
        {
            List<Provincia> provincias = await _provinciaService.GetAll();
            return Ok(provincias);
        }
    }
}
