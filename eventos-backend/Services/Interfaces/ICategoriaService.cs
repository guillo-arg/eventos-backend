using eventos_backend.Models;

namespace eventos_backend.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<Categoria> CreateCategoria(Categoria categoria);
        Task CreateList(List<Categoria> categorias);
        Task<string> DeleteCategoria(int id);
        Task<List<Categoria>> GetAll();
        Task<Categoria> GetById(int id);
        Task<Categoria> UpdateCategoria(Categoria categoria);
    }
}
