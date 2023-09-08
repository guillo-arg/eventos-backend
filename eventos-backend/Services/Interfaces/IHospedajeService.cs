using eventos_backend.DTOs.Hospedaje;
using eventos_backend.Models;

namespace eventos_backend.Services.Interfaces
{
    public interface IHospedajeService
    {
        Task<PostHospedajeDTO> CreateHospedaje(int eventoId, PostHospedajeDTO postHospedajeDTO);
        Task<string> DeleteHospedaje(int eventoId, int id);
        Task<List<Hospedaje>> GetAllByEvento(int eventoId);
        Task<Hospedaje> GetById(int eventoId, int id);
        Task<PostHospedajeDTO> UpdateHospedaje(int eventoId, PostHospedajeDTO postHospedajeDTO);
    }
}
