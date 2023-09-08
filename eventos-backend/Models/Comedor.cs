namespace eventos_backend.Models
{
    public class Comedor
    {
        public int Id { get; set; }
        public Evento Evento { get; set; }
        public int EventoId { get; set; }
        public decimal Precio { get; set; }
        public bool TienePrecioPorDia { get; set; }
        
    }
}
