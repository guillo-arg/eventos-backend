using eventos_backend.DTOs.Persona;
using eventos_backend.Exceptions;
using eventos_backend.Models;
using eventos_backend.Services.Interfaces;

namespace eventos_backend.Services
{
    public class EventoService : IEventoService
    {
        private readonly AppDataContext _appDataContext;
        private readonly ILogger<EventoService> _logger;
        public EventoService(AppDataContext appDataContext, ILogger<EventoService> logger)
        {
            _logger = logger;
            _appDataContext = appDataContext;

        }
        public async Task<Evento> CreateEvento(Evento evento)
        {
            evento.Id = 0;

            try
            {
                _appDataContext.Eventos.Add(evento);
                _appDataContext.SaveChanges();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new AppException(new List<string>() { $"No se pudo crear el evento : {evento.Nombre}" }, 400);
            }

            return evento;
        }

        public async Task<string> DeleteEvento(int id)
        {
            Evento evento = ValidateExists(id);

            try
            {
                _appDataContext.Eventos.Remove(evento);
                _appDataContext.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new AppException(new List<string>() { $"No se pudo crear el evento : {evento.Nombre}" }, 400);
            }

            return $"El evento {evento.Nombre} fue eliminado";
        }

        public async Task<List<Evento>> GetAll()
        {
            List<Evento> eventos = _appDataContext.Eventos.ToList();

            return eventos;
        }

        public async Task<Evento> GetById(int id)
        {
            Evento evento = ValidateExists(id);

            return evento;
        }

        public async Task<Evento> UpdateEvento(Evento evento)
        {
            Evento eventoDb = ValidateExists(evento.Id);
            MapUpdate(evento, eventoDb);

            try
            {
                _appDataContext.Eventos.Update(eventoDb);
                _appDataContext.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw new AppException(new List<string> { $"No se pudo actualizar el evento con ID: {evento.Id}" }, 400);
            }

            return evento;
        }

        private void MapUpdate(Evento evento, Evento eventoDb)
        {
            eventoDb.Descripcion = evento.Descripcion;
            eventoDb.FechaFin = evento.FechaFin;
            eventoDb.FechaInicio = evento.FechaInicio;
            eventoDb.Lugar = evento.Lugar;
            eventoDb.Nombre = evento.Nombre;            
        }

        private Evento ValidateExists(int id)
        {
            Evento evento = _appDataContext.Eventos.FirstOrDefault(x => x.Id == id);
            if (evento == null)
            {
                throw new AppException(new List<string>() { $"No se pudo encontrar el evento con ID: {id}" }, 404);
            }

            return evento;
        }
    }
}
