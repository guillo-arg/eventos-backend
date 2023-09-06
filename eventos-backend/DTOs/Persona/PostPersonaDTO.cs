using eventos_backend.Models;

namespace eventos_backend.DTOs.Persona
{
    public class PostPersonaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string Ciudad { get; set; }
        public int? ProvinciaId { get; set; }
        public string Email { get; set; }
        public int? CategoriaId { get; set; }
        public string Iglesia { get; set; }
        public long? Dni { get; set; }
    }
}
