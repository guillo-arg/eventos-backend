using Azure.Core;
using eventos_backend.DTOs.Persona;
using eventos_backend.Exceptions;
using eventos_backend.Models;
using eventos_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace eventos_backend.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class PersonaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IPersonaService _personaService;
        public PersonaController(IPersonaService personaService)
        {
            _personaService = personaService;
                
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Persona> personas = await _personaService.GetAll();

            return Ok(personas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Persona persona = await _personaService.GetById(id);

            return Ok(persona);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]PostPersonaDTO postPersonaDTO)
        {
            ValidaleModel();

            PostPersonaDTO response = await _personaService.CreatePersona(postPersonaDTO);

            return Ok(postPersonaDTO);
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody]PostPersonaDTO postPersonaDTO)
        {
            ValidaleModel();

            PostPersonaDTO response = await _personaService.UpdatePersona(postPersonaDTO);

            return Ok(postPersonaDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            ValidaleModel();

            string msg =  await _personaService.DeletePersona(id);

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
