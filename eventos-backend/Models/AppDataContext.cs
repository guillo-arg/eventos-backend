using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eventos_backend.Models
{
    public class AppDataContext : IdentityDbContext<User, Role, string>
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {

        }
    }
}
