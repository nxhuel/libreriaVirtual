using libreriaVirtual.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace libreriaVirtual.Context
{
    public class AppDbContext : IdentityDbContext<UserModel>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserModel> User { get; set; }
    }
}
