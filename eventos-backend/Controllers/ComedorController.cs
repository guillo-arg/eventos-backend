using eventos_backend.DTOs.Comedor;
using eventos_backend.Exceptions;
using eventos_backend.Models;
using eventos_backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eventos_backend.Controllers
{
    [Route("api/evento/{eventoId}/[controller]")]
    [ApiController]
    public class ComedorController : ControllerBase
    {
        private readonly IComedorService _comedorService;
        public ComedorController(IComedorService comedorService)
        {
            _comedorService = comedorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int eventoId)
        {
            List<Comedor> comedores = await _comedorService.GetAllByEventoId(eventoId);
            
            return Ok(comedores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int eventoId, int id)
        {
            Comedor comedor = await _comedorService.GetById(eventoId, id);
            return Ok(comedor);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int eventoId, PostComedorDto postComedorDto)
        {
            ValidaleModel();
            postComedorDto = await _comedorService.CreateComedor(eventoId, postComedorDto);

            return Ok(postComedorDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int eventoId, PostComedorDto postComedorDto)
        {
            ValidaleModel();
            postComedorDto = await _comedorService.UpdateComedor(eventoId, postComedorDto);

            return Ok(postComedorDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int eventoId, int id)
        {
            ValidaleModel();
            string msg = await _comedorService.DeleteComedor(eventoId, id);

            return Ok(msg);
        }

        private void ValidaleModel()
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = ModelState.Select(x => x.Value.Errors.FirstOrDefault().ErrorMessage).ToList();
                throw new AppException(errors, 400);
            }
        }
    }
}
