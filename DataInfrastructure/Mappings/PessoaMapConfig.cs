using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataInfrastructure.Mappings
{
    internal class PessoaMapConfig : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            // NVARCHAR -> VARCHAR      
            builder.Property(p => p.Nome).IsFixedLength().IsUnicode(false).HasMaxLength(60).IsRequired();

            builder.Property(p => p.Descricao).IsFixedLength().IsUnicode(false).HasMaxLength(100).IsRequired();

            builder.Property(p => p.Profissao).IsFixedLength().IsUnicode(false).HasMaxLength(100).IsRequired();

            builder.Property(p => p.Email).IsFixedLength().IsUnicode(false).HasMaxLength(100).IsRequired();

            builder.Property(p => p.Senha).IsFixedLength().IsUnicode(false).HasMaxLength(68).IsRequired();

            builder.Property(p => p.CEP).IsFixedLength().IsUnicode(false).HasMaxLength(8).IsRequired();

            builder.Property(p => p.Nome).IsFixedLength().IsUnicode(false).HasMaxLength(60).IsRequired();

            builder.Property(p => p.Cidade).IsFixedLength().IsUnicode(false).HasMaxLength(60).IsRequired();

            builder.Property(p => p.Telefone).IsFixedLength().IsUnicode(false).HasMaxLength(15).IsRequired();
            builder.Property(p => p.CPF).IsFixedLength().IsUnicode(false).HasMaxLength(15).IsRequired();

            builder.HasIndex(c => c.CPF).IsUnique();
            builder.HasIndex(c => c.Email).IsUnique();

        }
    }
}
