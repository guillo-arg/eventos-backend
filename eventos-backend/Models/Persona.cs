﻿namespace eventos_backend.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string Ciudad { get; set; }
        public Provincia? Provincia { get; set; }
        public int? ProvinciaId { get; set; }
        public string Email { get; set; }
        public Categoria? Categoria { get; set; }
        public int? CategoriaId { get; set; }
        public string Iglesia { get; set; }
        public long? Dni { get; set; }
    }
}
