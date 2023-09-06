using eventos_backend.Models;
using eventos_backend.Services.Interfaces;

namespace eventos_backend.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly AppDataContext _appDataContext;
        public CategoriaService(AppDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }
        public async Task CreateList(List<Categoria> categorias)
        {
            _appDataContext.Categorias.AddRange(categorias);
            _appDataContext.SaveChanges();

        }

        public async Task<List<Categoria>> GetAll()
        {
            return _appDataContext.Categorias.ToList();
        }
    }
}
