using AutoMapper;
using eventos_backend.DTOs.Account;
using eventos_backend.DTOs.Persona;
using eventos_backend.Exceptions;
using eventos_backend.Helpers;
using eventos_backend.Models;
using eventos_backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eventos_backend.Services
{
    public class PersonaService : IPersonaService
    {
        private readonly AppDataContext _appDataContext;        
        private readonly IConfiguration _configuration;
        private readonly IMapper _autoMapper;
        private readonly ILogger<PersonaService> _logger;

        public PersonaService(AppDataContext appDataContext, IConfiguration configuration, ILogger<PersonaService> logger)
        {
            _appDataContext = appDataContext;
            _configuration = configuration;
            _autoMapper = AutomapperConfiguration.Instance().CreateMapper();
            _logger = logger;

        }

        public async Task<PostPersonaDTO> CreatePersona(PostPersonaDTO postPersonaDTO)
        {

            Persona persona = _autoMapper.Map<Persona>(postPersonaDTO);

            ValidateIncludes(postPersonaDTO, persona);


            try
            {
                _appDataContext.Personas.Add(persona);
                _appDataContext.SaveChanges();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new AppException(new List<string>() { $"No se pudo crear la persona : {postPersonaDTO.Nombre}" }, 400);

            }

            postPersonaDTO.Id = persona.Id;

            return postPersonaDTO;
        }


        public async Task<string> DeletePersona(int id)
        {
            Persona persona = ValidateExists(id);

            try
            {
                _appDataContext.Personas.Remove(persona);
                _appDataContext.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new AppException(new List<string>() { $"No se pudo crear la persona : {persona.Nombre}" }, 400);
            }

            return $"La persona {persona.Nombre} fue eliminada";

        }

        public async Task<List<Persona>> GetAll()
        {
            List<Persona> personas = _appDataContext.Personas.Include(x => x.Categoria).Include(x => x.Provincia).ToList();
            return personas;

        }

        public async Task<Persona> GetById(int id)
        {
            Persona persona = ValidateExists(id);

            persona.Categoria = _appDataContext.Categorias.FirstOrDefault(x => x.Id == persona.CategoriaId);
            persona.Provincia = _appDataContext.Provincias.FirstOrDefault(x => x.Id == persona.ProvinciaId);

            return persona;
        }


        public async Task<PostPersonaDTO> UpdatePersona(PostPersonaDTO postPersonaDTO)
        {
            Persona persona = ValidateExists(postPersonaDTO.Id);
            ValidateIncludes(postPersonaDTO, persona);
            MapUpdate(postPersonaDTO, persona);

            try
            {
                _appDataContext.Personas.Update(persona);
                _appDataContext.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw new AppException(new List<string> { $"No se pudo actualizar la persona con ID: {postPersonaDTO.Id}" }, 400);
            }

            return postPersonaDTO;

        }
        private void ValidateIncludes(PostPersonaDTO postPersonaDTO, Persona persona)
        {
            if (postPersonaDTO.Id != null)
            {
                Categoria categoria = _appDataContext.Categorias.FirstOrDefault(x => x.Id == postPersonaDTO.CategoriaId);
                if (categoria == null)
                {
                    throw new AppException(new List<string>() { $"No existe la categoria con ID : {postPersonaDTO.CategoriaId}" }, 400);
                }


            }

            if (postPersonaDTO.ProvinciaId != null)
            {
                Provincia provincia = _appDataContext.Provincias.FirstOrDefault(x => x.Id == postPersonaDTO.ProvinciaId);
                if (provincia == null)
                {
                    throw new AppException(new List<string>() { $"No existe la provincia con ID : {postPersonaDTO.ProvinciaId}" }, 400);
                }
            }
        }

        private static void MapUpdate(PostPersonaDTO postPersonaDTO, Persona persona)
        {
            persona.CategoriaId = postPersonaDTO.CategoriaId;
            persona.Ciudad = postPersonaDTO.Ciudad;
            persona.Dni = postPersonaDTO.Dni;
            persona.Domicilio = postPersonaDTO.Domicilio;
            persona.Email = postPersonaDTO.Email;
            persona.Iglesia = postPersonaDTO.Iglesia;
            persona.Nombre = postPersonaDTO.Nombre;
            persona.ProvinciaId = postPersonaDTO.ProvinciaId;
            persona.Telefono = postPersonaDTO.Telefono;
        }
        private Persona ValidateExists(int id)
        {
            Persona persona = _appDataContext.Personas.FirstOrDefault(x => x.Id == id);
            if (persona == null)
            {
                throw new AppException(new List<string>() { $"No se pudo encontrar a la persona con ID: {id}" }, 404);
            }

            return persona;
        }
    }
}
