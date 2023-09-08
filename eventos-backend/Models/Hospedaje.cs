namespace eventos_backend.Models
{
    public class Hospedaje
    {
        public int Id { get; set; }
        public Evento Evento { get; set; }
        public int EventoId { get; set; }
        public decimal Precio { get; set; }
        public string Tipo { get; set; }
    }
}
