namespace TP_PW.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class eMuseuContext : DbContext
    {
        public eMuseuContext()
            : base("name=eMuseuContext")
        {
        }

        public virtual DbSet<Artigos> Artigos { get; set; }
        public virtual DbSet<ArtigosEmprestimos> ArtigosEmprestimos { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Emprestimos> Emprestimos { get; set; }
        public virtual DbSet<EstadoEmprestimo> EstadoEmprestimo { get; set; }
        public virtual DbSet<Mensagens> Mensagens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artigos>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<Artigos>()
                .HasMany(e => e.ArtigosEmprestimos)
                .WithRequired(e => e.Artigos)
                .HasForeignKey(e => e.IdArtigo);

            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Emprestimos)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.IdUtilizador)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Mensagens)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.IdRecetor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Mensagens1)
                .WithRequired(e => e.AspNetUsers1)
                .HasForeignKey(e => e.IdRemetente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Emprestimos>()
                .HasMany(e => e.ArtigosEmprestimos)
                .WithRequired(e => e.Emprestimos)
                .HasForeignKey(e => e.IdEmprestimo);

            modelBuilder.Entity<EstadoEmprestimo>()
                .HasMany(e => e.Emprestimos)
                .WithRequired(e => e.EstadoEmprestimo)
                .HasForeignKey(e => e.IdEstado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Mensagens>()
                .Property(e => e.Mensagem)
                .IsUnicode(false);
        }
    }
}
