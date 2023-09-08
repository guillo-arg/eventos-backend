using eventos_backend.Models;

namespace eventos_backend.DTOs.Comedor
{
    public class PostComedorDto
    {
        public int Id { get; set; }
        public decimal Precio { get; set; }
        public bool TienePrecioPorDia { get; set; }
    }
}
