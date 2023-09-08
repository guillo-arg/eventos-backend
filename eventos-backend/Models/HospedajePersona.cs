namespace eventos_backend.Models
{
    public class HospedajePersona
    {
        public int Id { get; set; }
        public Hospedaje Hospedaje { get; set; }
        public int HospedajeId { get; set; }
        public Persona Persona { get; set; }
        public int PersonaId { get; set; }
    }
}
