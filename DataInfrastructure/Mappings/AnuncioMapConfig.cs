using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DataInfrastructure.Mappings
{
    internal class AnuncioMapConfig : IEntityTypeConfiguration<Anuncio>
    {
        public void Configure(EntityTypeBuilder<Anuncio> builder)
        {
            builder.Property(a => a.Nome).IsFixedLength().IsUnicode(false).HasMaxLength(60).IsRequired();

            builder.Property(a => a.Descricao).IsUnicode(false).HasMaxLength(200).IsRequired(); //

            builder.HasOne(c => c.Pessoa).WithMany(c => c.Anuncios).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(c => c.PessoasInteressadas).WithMany(c => c.Interesses).UsingEntity<Dictionary<string, object>>("AnuncioInteresses",
              j => j.HasOne<Pessoa>().WithMany().HasForeignKey("AdotanteID").HasConstraintName("FK_AnuncioInteresses_AdotanteID").OnDelete(DeleteBehavior.NoAction),
              j => j.HasOne<Anuncio>().WithMany().HasForeignKey("AnuncioID").HasConstraintName("FK_AnuncioInteresses_AnuncioID").OnDelete(DeleteBehavior.NoAction));
        }
    }
}
