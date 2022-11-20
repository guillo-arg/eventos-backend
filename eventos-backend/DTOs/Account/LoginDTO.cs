using System.ComponentModel.DataAnnotations;

namespace eventos_backend.DTOs.Account
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [StringLength(255, ErrorMessage = "El usuario debe tener como mínimo 4 caracteres", MinimumLength = 4)]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(255, ErrorMessage = "La contraseña debe tener como mínimo 6 caracteres", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
