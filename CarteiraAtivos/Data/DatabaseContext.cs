using CarteiraAtivos.Data.Map;
using CarteiraAtivos.Models;
using Microsoft.EntityFrameworkCore;

namespace CarteiraAtivos.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        // Criação das entidades no banco de dados
        public DbSet<LoginUsuarioModel> LoginUsuarios { get; set; }
        public DbSet<AtivoModel> Ativos { get; set; }   
        public DbSet<RedefinicaoSenhaModel> RedefinicoesSenha { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AtivoMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
