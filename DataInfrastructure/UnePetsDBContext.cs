using Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataInfrastructure
{
    public class UnePetsDBContext : DbContext
    {
        //
        //List<Anuncio> anuncios = db.Anuncios.Where(c => c.Status != Domain.Enum.Status.Adotado).ToList();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\OneDrive\Documentos\UnePetsDBContext.mdf;Integrated Security=True;Connect Timeout=5", x => x.EnableRetryOnFailure(5));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // procura as config de mapping dentro do projeto 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // retorna um projeto que contem o metodo em execução
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Anuncio> Anuncios { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }
    }
}
