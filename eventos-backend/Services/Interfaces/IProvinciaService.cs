using eventos_backend.Models;

namespace eventos_backend.Services.Interfaces
{
    public interface IProvinciaService
    {
        Task CreateList(List<Provincia> provincias);
        Task<List<Provincia>> GetAll();
    }
}
