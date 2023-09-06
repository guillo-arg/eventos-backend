using eventos_backend.Models;

namespace eventos_backend.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task CreateList(List<Categoria> categorias);
        Task<List<Categoria>> GetAll();
    }
}
