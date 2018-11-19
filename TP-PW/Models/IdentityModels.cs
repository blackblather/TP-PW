using System.Data.Entity;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TP_PW.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public string Nome { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {

            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Artigo> Artigos { get; set; }
        public virtual DbSet<ArtigosEmprestimo> ArtigosEmprestimos { get; set; }
        public virtual DbSet<Emprestimo> Emprestimos { get; set; }
        public virtual DbSet<EstadoEmprestimo> EstadoEmprestimoes { get; set; }
        public virtual DbSet<Mensagem> Mensagens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Artigo>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<Artigo>()
                .HasMany(e => e.ArtigosEmprestimos)
                .WithRequired(e => e.Artigo)
                .HasForeignKey(e => e.IdArtigo);

            modelBuilder.Entity<Emprestimo>()
                .HasMany(e => e.ArtigosEmprestimos)
                .WithRequired(e => e.Emprestimo)
                .HasForeignKey(e => e.IdEmprestimo);

            modelBuilder.Entity<EstadoEmprestimo>()
                .HasMany(e => e.Emprestimos)
                .WithRequired(e => e.EstadoEmprestimo)
                .HasForeignKey(e => e.IdEstado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Mensagem>()
                .Property(e => e.Texto)
                .IsUnicode(false);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}