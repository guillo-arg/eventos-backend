using eventos_backend.Exceptions;
using eventos_backend.Models;
using eventos_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eventos_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;
        public EventoController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Evento> eventos = await _eventoService.GetAll();
            return Ok(eventos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Evento evento = await _eventoService.GetById(id);
            return Ok(evento);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Evento evento)
        {
            ValidaleModel();
            evento = await _eventoService.CreateEvento(evento);
            return Ok(evento);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Evento evento)
        {
            ValidaleModel();
            evento = await _eventoService.UpdateEvento(evento);

            return Ok(evento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ValidaleModel();
            string msg = await _eventoService.DeleteEvento(id);
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
