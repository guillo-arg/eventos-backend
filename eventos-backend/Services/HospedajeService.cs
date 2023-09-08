using AutoMapper;
using eventos_backend.DTOs.Hospedaje;
using eventos_backend.Exceptions;
using eventos_backend.Helpers;
using eventos_backend.Models;
using eventos_backend.Services.Interfaces;

namespace eventos_backend.Services
{
    public class HospedajeService : IHospedajeService
    {
        private readonly ILogger<HospedajeService> _logger;
        private readonly AppDataContext _appDataContext;
        private readonly IMapper _autoMapper;
        public HospedajeService(ILogger<HospedajeService> logger, AppDataContext appDataContext)
        {
            _appDataContext = appDataContext;
            _logger = logger;
            _autoMapper = AutomapperConfiguration.Instance().CreateMapper();

        }
        public async Task<PostHospedajeDTO> CreateHospedaje(int eventoId, PostHospedajeDTO postHospedajeDto)
        {
            Evento evento = ValidateExistsEvento(eventoId);
            Hospedaje hospedaje = _autoMapper.Map<Hospedaje>(postHospedajeDto);
            hospedaje.Id = 0;
            hospedaje.EventoId = eventoId;
            hospedaje.Evento = evento;

            try
            {
                _appDataContext.Hospedajes.Add(hospedaje);
                _appDataContext.SaveChanges();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new AppException(new List<string>() { $"No se pudo crear el hospedaje" }, 400);
            }

            return postHospedajeDto;
        }

        public async Task<string> DeleteHospedaje(int eventoId, int id)
        {
            Evento evento = ValidateExistsEvento(eventoId);
            Hospedaje hospedaje = ValidateExists(eventoId,id);

            try
            {
                _appDataContext.Hospedajes.Remove(hospedaje);
                _appDataContext.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new AppException(new List<string>() { $"No se pudo eliminar el hospedaje con ID: {hospedaje.Id}" }, 400);
            }

            return $"El hospedaje fue eliminado";

        }


        public async Task<List<Hospedaje>> GetAllByEvento(int eventoId)
        {
            Evento evento = ValidateExistsEvento(eventoId);
            List<Hospedaje> hospedajes = _appDataContext.Hospedajes.Where(x => x.EventoId == eventoId).ToList();

            return hospedajes;
        }

        public async Task<Hospedaje> GetById(int eventoId, int id)
        {
            Evento evento = ValidateExistsEvento(eventoId);
            Hospedaje hospedaje = ValidateExists(eventoId, id);

            return hospedaje;

        }

        public async Task<PostHospedajeDTO> UpdateHospedaje(int eventoId, PostHospedajeDTO postHospedajeDto)
        {
            Evento evento = ValidateExistsEvento(eventoId);
            Hospedaje hospedajeDb = ValidateExists(eventoId, postHospedajeDto.Id);
            MapUpdate(hospedajeDb, postHospedajeDto);

            try
            {
                _appDataContext.Hospedajes.Update(hospedajeDb);
                _appDataContext.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw new AppException(new List<string> { $"No se pudo actualizar el hospedaje con ID: {postHospedajeDto.Id}" }, 400);
            }

            return postHospedajeDto;
        }

        private void MapUpdate(Hospedaje hospedajeDb, PostHospedajeDTO postHospedajeDto)
        {
            hospedajeDb.Precio = postHospedajeDto.Precio;
            hospedajeDb.Tipo = postHospedajeDto.Tipo;
            
        }

        private Evento ValidateExistsEvento(int eventoId)
        {
            Evento evento = _appDataContext.Eventos.FirstOrDefault(x => x.Id == eventoId);
            if (evento == null)
            {
                throw new AppException(new List<string>() { $"No se pudo encontrar el evento con ID: {eventoId}" }, 404);
            }

            return evento;
        }

        private Hospedaje ValidateExists(int eventoId, int id)
        {
            Hospedaje hospedaje = _appDataContext.Hospedajes.FirstOrDefault(x => x.Id == id && x.EventoId == eventoId);
            if (hospedaje == null)
            {
                throw new AppException(new List<string>() { $"No se pudo encontrar el hospedaje con ID: {id}" }, 404);
            }

            return hospedaje;
        }
    }
}
