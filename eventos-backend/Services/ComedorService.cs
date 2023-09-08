using AutoMapper;
using eventos_backend.DTOs.Comedor;
using eventos_backend.DTOs.Hospedaje;
using eventos_backend.Exceptions;
using eventos_backend.Helpers;
using eventos_backend.Models;
using eventos_backend.Services.Interfaces;

namespace eventos_backend.Services
{
    public class ComedorService : IComedorService
    {
        private readonly AppDataContext _appDataContext;
        private readonly ILogger<ComedorService> _logger;
        private readonly IMapper _autoMapper;
        private readonly IEventoService _eventoService;
        public ComedorService(AppDataContext appDataContext, ILogger<ComedorService> logger, IEventoService eventoService)
        {
            _eventoService = eventoService;
            _logger = logger;
            _appDataContext = appDataContext;
            _autoMapper = AutomapperConfiguration.Instance().CreateMapper();

        }
        public async Task<PostComedorDto> CreateComedor(int eventoId, PostComedorDto postComedorDto)
        {
            Evento evento = await _eventoService.GetById(eventoId);
            Comedor comedorDb = _appDataContext.Comedores.FirstOrDefault(x => x.EventoId == eventoId);
            if (comedorDb != null)
            {
                throw new AppException(new List<string>() { $"Ya existe un comedor creado para este evento" }, 400);
            }
            Comedor comedor = _autoMapper.Map<Comedor>(postComedorDto);
            comedor.Id = 0;
            comedor.EventoId = eventoId;
            comedor.Evento = evento;

            try
            {
                _appDataContext.Comedores.Add(comedor);
                _appDataContext.SaveChanges();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new AppException(new List<string>() { $"No se pudo crear el comedor" }, 400);
            }

            return postComedorDto;
        }

        public async Task<string> DeleteComedor(int eventoId, int id)
        {
            Evento evento = await _eventoService.GetById(eventoId);
            Comedor comedor = ValidateExists(eventoId, id);

            try
            {
                _appDataContext.Comedores.Remove(comedor);
                _appDataContext.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new AppException(new List<string>() { $"No se pudo eliminar el comedor con ID: {comedor.Id}" }, 400);
            }

            return $"El comedor fue eliminado";
        }
        public async Task<List<Comedor>> GetAllByEventoId(int eventoId)
        {
            Evento evento = await _eventoService.GetById(eventoId);
            List<Comedor> comedores = _appDataContext.Comedores.Where(x => x.EventoId == eventoId).ToList();

            return comedores;
        }

        public async Task<Comedor> GetById(int eventoId, int id)
        {
            Evento evento = await _eventoService.GetById(eventoId);
            Comedor comedor = ValidateExists(eventoId, id);

            return comedor;
        }

        public async Task<PostComedorDto> UpdateComedor(int eventoId, PostComedorDto postComedorDto)
        {
            Evento evento = await _eventoService.GetById(eventoId);
            Comedor comedorDb = ValidateExists(eventoId, postComedorDto.Id);
            MapUpdate(comedorDb, postComedorDto);

            try
            {
                _appDataContext.Comedores.Update(comedorDb);
                _appDataContext.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw new AppException(new List<string> { $"No se pudo actualizar el comedor con ID: {postComedorDto.Id}" }, 400);
            }

            return postComedorDto;
        }

        private void MapUpdate(Comedor comedorDb, PostComedorDto postComedorDto)
        {
            comedorDb.Precio = postComedorDto.Precio;
            comedorDb.TienePrecioPorDia = postComedorDto.TienePrecioPorDia;
        }

        private Comedor ValidateExists(int eventoId, int id)
        {
            Comedor comedor = _appDataContext.Comedores.FirstOrDefault(x => x.Id == id && x.EventoId == eventoId);
            if (comedor == null)
            {
                throw new AppException(new List<string>() { $"No se pudo encontrar el comedor con ID: {id}" }, 404);
            }

            return comedor;
        }
    }

    
}
