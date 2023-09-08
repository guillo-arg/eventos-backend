using eventos_backend.DTOs.Comedor;
using eventos_backend.Models;

namespace eventos_backend.Services.Interfaces
{
    public interface IComedorService
    {
        Task<PostComedorDto> CreateComedor(int eventoId, PostComedorDto postComedorDto);
        Task<string> DeleteComedor(int eventoId, int id);
        Task<List<Comedor>> GetAllByEventoId(int eventoId);
        Task<Comedor> GetById(int eventoId, int id);
        Task<PostComedorDto> UpdateComedor(int eventoId, PostComedorDto postComedorDto);
    }
}
