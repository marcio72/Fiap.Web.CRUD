using Fiap.Web.alunosII.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.alunosII.Data
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<RepresentanteModel> Representantes { get; set; }
        public virtual DbSet<ClienteModel> Clientes { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        protected DatabaseContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RepresentanteModel>(entity =>
            {
                entity.ToTable("Tbl_Representante");
                entity.HasKey(e => e.RepresentanteId);
                entity.Property(e => e.NomeRepresentante).IsRequired();
                entity.HasIndex(e => e.Cpf).IsUnique();
            });

            modelBuilder.Entity<ClienteModel>(entity =>
            {
                entity.ToTable("Tbl_Cliente");
                entity.HasKey(e => e.ClienteId);
                entity.Property(e => e.Nome).IsRequired();
                entity.Property(e => e.Email).IsRequired();

                //Especifica o tipo de Dado para DataNascimento
                entity.Property(e => e.DataNascimento).HasColumnType("date");
                entity.Property(e => e.Observacao).HasMaxLength(500);

                entity.HasOne(e => e.Representante)
                .WithMany()
                .HasForeignKey(e => e.RepresentanteId)
                .IsRequired();

            });



            base.OnModelCreating(modelBuilder);
        }

    }
}
