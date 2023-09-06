using eventos_backend.DTOs.Persona;
using eventos_backend.Exceptions;
using eventos_backend.Models;
using eventos_backend.Services.Interfaces;
using System.Reflection.Metadata;

namespace eventos_backend.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly AppDataContext _appDataContext;
        private readonly ILogger<CategoriaService> _logger;
        public CategoriaService(AppDataContext appDataContext, ILogger<CategoriaService> logger)
        {
            _appDataContext = appDataContext;
            _logger = logger;
        }

        public async Task<Categoria> CreateCategoria(Categoria categoria)
        {
            categoria.Id = 0;

            try
            {
                _appDataContext.Categorias.Add(categoria);
                _appDataContext.SaveChanges();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new AppException(new List<string>() { $"No se pudo crear la categoría : {categoria.Nombre}" }, 400);
            }

            return categoria;
        }

        public async Task CreateList(List<Categoria> categorias)
        {
            _appDataContext.Categorias.AddRange(categorias);
            _appDataContext.SaveChanges();

        }

        public async Task<string> DeleteCategoria(int id)
        {
            Categoria categoria = ValidateExists(id);

            try
            {
                _appDataContext.Categorias.Remove(categoria);
                _appDataContext.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new AppException(new List<string>() { $"No se pudo crear la categoría : {categoria.Nombre}" }, 400);
            }

            return $"La categoría {categoria.Nombre} fue eliminada";
        }

        public async Task<List<Categoria>> GetAll()
        {
            return _appDataContext.Categorias.ToList();
        }

        public async Task<Categoria> GetById(int id)
        {
            Categoria categoria = ValidateExists(id);

            return categoria;
        }

        public async Task<Categoria> UpdateCategoria(Categoria categoria)
        {
            Categoria categoriaDB = ValidateExists(categoria.Id);
            MapUpdate(categoria, categoriaDB);

            try
            {
                _appDataContext.Categorias.Update(categoriaDB);
                _appDataContext.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw new AppException(new List<string> { $"No se pudo actualizar la categoría con ID: {categoria.Id}" }, 400);
            }

            return categoria;
        }

        private void MapUpdate(Categoria categoria, Categoria categoriaDB)
        {
            categoriaDB.Nombre = categoria.Nombre;
        }

        private Categoria ValidateExists(int id)
        {
            Categoria categoria = _appDataContext.Categorias.FirstOrDefault(x => x.Id == id);
            if (categoria == null)
            {
                throw new AppException(new List<string>() { $"No se pudo encontrar a la categoría con ID: {id}" }, 404);
            }

            return categoria;
        }
    }
}
