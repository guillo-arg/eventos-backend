using eventos_backend.Models;

namespace eventos_backend.Services.Interfaces
{
    public interface IEventoService
    {
        Task<Evento> CreateEvento(Evento evento);
        Task<string> DeleteEvento(int id);
        Task<List<Evento>> GetAll();
        Task<Evento> GetById(int id);
        Task<Evento> UpdateEvento(Evento evento);
    }
}
