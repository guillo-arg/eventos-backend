using eventos_backend.Models;
using eventos_backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eventos_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaService) { 
        
            _categoriaService = categoriaService;
        }

        [HttpGet] public async Task<IActionResult> GetAll()
        {
            List<Categoria> categorias = await _categoriaService.GetAll();
            return Ok(categorias);
        }
    }
}
