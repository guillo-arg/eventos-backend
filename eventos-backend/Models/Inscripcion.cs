namespace eventos_backend.Models
{
    public class Inscripcion
    {
        public int Id { get; set; }
        public Persona Persona { get; set; }
        public int PersonaId { get; set; }
        public Evento Evento { get; set; }
        public int EventoId { get; set;}
        public DateTime FechaDeIngreso { get; set; }
        
    }
}
