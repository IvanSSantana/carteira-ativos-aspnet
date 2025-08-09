using CarteiraAtivos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarteiraAtivos.Data.Map
{
    public class AtivoMap : IEntityTypeConfiguration<AtivoModel>
    {
        public void Configure(EntityTypeBuilder<AtivoModel> builder)
        {
            // Chave primária
            builder.HasKey(x => x.Id);

            // Cria o relacionamento um para muitos com LoginUsuario
            // Um usuario pode ter muitos ativos
            builder.HasOne(a => a.LoginUsuario);
        }
    }
}
