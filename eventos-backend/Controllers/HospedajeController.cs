using eventos_backend.DTOs.Hospedaje;
using eventos_backend.Exceptions;
using eventos_backend.Models;
using eventos_backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eventos_backend.Controllers
{
    [Route("api/evento/{eventoId}/[controller]")]
    [ApiController]
    public class HospedajeController : ControllerBase
    {
        private readonly IHospedajeService _hospedajeService;
        public HospedajeController(IHospedajeService hospedajeService)
        {
            _hospedajeService = hospedajeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int eventoId)
        {
            List<Hospedaje> hospedajes = await _hospedajeService.GetAllByEvento(eventoId);
            
            return Ok(hospedajes);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int eventoId, int id)
        {
            Hospedaje hospedaje = await _hospedajeService.GetById(eventoId, id);

            return Ok(hospedaje);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int eventoId, PostHospedajeDTO postHospejadeDto)
        {
            ValidaleModel();
            postHospejadeDto = await _hospedajeService.CreateHospedaje(eventoId, postHospejadeDto);

            return Ok(postHospejadeDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int eventoId, PostHospedajeDTO postHospedajeDto)
        {
            ValidaleModel();
            postHospedajeDto = await _hospedajeService.UpdateHospedaje(eventoId, postHospedajeDto);

            return Ok(postHospedajeDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int eventoId, int id)
        {
            ValidaleModel();
            string msg = await _hospedajeService.DeleteHospedaje(eventoId, id);

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
