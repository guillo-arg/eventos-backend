using Microsoft.AspNetCore.Identity;

namespace eventos_backend.Models
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            Enabled = true;
        }

        public bool Enabled  { get; set; }
    }
}
