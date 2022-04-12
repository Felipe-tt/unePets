using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataInfrastructure.Mappings
{
    internal class MensagemMapConfig : IEntityTypeConfiguration<Mensagem>
    {
        public void Configure(EntityTypeBuilder<Mensagem> builder)
        {
            builder.Property(c => c.Corpo).IsRequired().HasMaxLength(200).IsUnicode(false);
            builder.HasOne(c => c.Remetente).WithMany(c => c.MensagensEnviadas).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(c => c.Destinatario).WithMany(c => c.MensagensRecebidas).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
