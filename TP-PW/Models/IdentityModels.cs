using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TP_PW.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string PrimeiroNome { get; set; }

        [Required]
        public string Apelido { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }


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
        public virtual DbSet<Mensagen> Mensagens { get; set; }
        public virtual DbSet<Tratamento> Tratamentos { get; set; }

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

            modelBuilder.Entity<Artigo>()
                .HasMany(e => e.Tratamentos)
                .WithRequired(e => e.Artigo)
                .HasForeignKey(e => e.IdArtigo);
                

            modelBuilder.Entity<Emprestimo>()
                .HasMany(e => e.ArtigosEmprestimos)
                .WithRequired(e => e.Emprestimo)
                .HasForeignKey(e => e.IdEmprestimo);

            modelBuilder.Entity<Mensagen>()
                .Property(e => e.Mensagem)
                .IsUnicode(false);

            modelBuilder.Entity<Mensagen>()
                .Property(e => e.HoraEnvio)
                .HasPrecision(0);
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}