using CarteiraAtivos.Models;
using Microsoft.EntityFrameworkCore;

namespace CarteiraAtivos.Data
{
    public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<LoginUsuarioModel> LoginUsuarios { get; set; }
    }
}
