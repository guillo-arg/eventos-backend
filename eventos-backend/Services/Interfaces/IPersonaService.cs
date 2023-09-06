using eventos_backend.DTOs.Persona;
using eventos_backend.Models;

namespace eventos_backend.Services.Interfaces
{
    public interface IPersonaService
    {
        Task<PostPersonaDTO> CreatePersona(PostPersonaDTO postPersonaDTO);
        Task<string> DeletePersona(int id);
        Task<List<Persona>> GetAll();
        Task<Persona> GetById(int id);
        Task<PostPersonaDTO> UpdatePersona(PostPersonaDTO postPersonaDTO);
    }
}
