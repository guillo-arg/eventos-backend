namespace eventos_backend.Models
{
    public class ComedorPersona
    {
        public int Id { get; set; }
        public Comedor Comedor{ get; set; }
        public int ComedorId { get; set; }
        public Persona Persona { get; set; }
        public int PersonaId { get; set;}
        public int CantidadDeDias { get; set; }
    }
}
