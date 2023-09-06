using eventos_backend.DTOs.Account;
using eventos_backend.Models;
using eventos_backend.Services.Interfaces;

namespace eventos_backend.Startup
{
    public static class DataLoader
    {
        public static async Task Seed(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            IAccountService accountService = scope.ServiceProvider.GetService<IAccountService>();

            RegisterDTO registerDTO = new RegisterDTO();
            registerDTO.Username = "admin";
            registerDTO.Password = "123456";
            registerDTO.Password2 = "123456";
            registerDTO.Email = "admin@gmail.com";

            await accountService.Register(registerDTO);

            IProvinciaService provinciaService = scope.ServiceProvider.GetService<IProvinciaService>();

            List<Provincia> provincias = new List<Provincia>() { 
            
                new Provincia()
                {
                    Id = 1,
                    Nombre="Buenos Aires"
                },
                new Provincia()
                {
                    Id = 2,
                    Nombre="CABA"
                },
                new Provincia()
                {
                    Id = 3,
                    Nombre="Catamarca"
                },
                new Provincia()
                {
                    Id = 4,
                    Nombre="Chaco"
                },
                new Provincia()
                {
                    Id = 5,
                    Nombre="Chubut"
                },
                new Provincia()
                {
                    Id = 6,
                    Nombre="Córdoba"
                },
                new Provincia()
                {
                    Id = 7,
                    Nombre="Corrientes"
                },
                new Provincia()
                {
                    Id = 8,
                    Nombre="Entre Ríos"
                },
                new Provincia()
                {
                    Id = 9,
                    Nombre="Formosa"
                },
                new Provincia()
                {
                    Id = 10,
                    Nombre="Jujuy"
                },
                new Provincia()
                {
                    Id = 11,
                    Nombre="La Pampa"
                },
                new Provincia()
                {
                    Id = 12,
                    Nombre="La Rioja"
                },
                new Provincia()
                {
                    Id = 13,
                    Nombre="Mendoza"
                },
                new Provincia()
                {
                    Id = 14,
                    Nombre="Misiones"
                },
                new Provincia()
                {
                    Id = 15,
                    Nombre="Neuquén"
                },
                new Provincia()
                {
                    Id = 16,
                    Nombre="Río Negro"
                },
                new Provincia()
                {
                    Id = 17,
                    Nombre="Salta"
                },
                new Provincia()
                {
                    Id = 18,
                    Nombre="San Juan"
                },
                new Provincia()
                {
                    Id = 19,
                    Nombre="San Luis"
                },
                new Provincia()
                {
                    Id = 20,
                    Nombre="Santa Cruz"
                },
                new Provincia()
                {
                    Id = 21,
                    Nombre="Santa Fe"
                },
                new Provincia()
                {
                    Id = 22,
                    Nombre="Santiago del Estero"
                },
                new Provincia()
                {
                    Id = 23,
                    Nombre="Tierra del Fuego"
                },
                new Provincia()
                {
                    Id = 24,
                    Nombre="Tucumán"
                }

            };

            await provinciaService.CreateList(provincias);

            ICategoriaService categoriaService = scope.ServiceProvider.GetService<ICategoriaService>();

            List<Categoria> categorias = new List<Categoria>()
            {
                new Categoria()
                {
                    Id= 1,
                    Nombre="Pastor/a"
                },
                new Categoria()
                {
                    Id=2,
                    Nombre="Colaborador/a"
                },
                new Categoria()
                {
                    Id=3,
                    Nombre="Hijo/a de Pastor"
                },
                new Categoria()
                {
                    Id=4,
                    Nombre="Mienbro de congregación"
                },
                new Categoria()
                {
                    Id=5,
                    Nombre="Discipulo/a"
                }
            };

            await categoriaService.CreateList(categorias);
        }
    }
}
