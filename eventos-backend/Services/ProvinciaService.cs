using eventos_backend.Models;
using eventos_backend.Services.Interfaces;

namespace eventos_backend.Services
{
    public class ProvinciaService : IProvinciaService
    {
        private readonly AppDataContext _appDataContext;
        public ProvinciaService(AppDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }
        public async Task CreateList(List<Provincia> provincias)
        {
            _appDataContext.Provincias.AddRange(provincias);
            _appDataContext.SaveChanges();
        }

        public async Task<List<Provincia>> GetAll()
        {
            return _appDataContext.Provincias.ToList();
        }
    }
}
