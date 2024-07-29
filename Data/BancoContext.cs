using Microsoft.EntityFrameworkCore;
using ControleCandidaturas.Models;

namespace ControleCandidaturas.Data
{
    public class BancoContext: DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidatura>()
                .Property(c => c.Status)
                .HasConversion<int>();

            modelBuilder.Entity<Candidatura>()
                .Property(c => c.Grau)
                .HasConversion<int>();

            modelBuilder.Entity<Candidatura>()
                .Property(c => c.Modo)
                .HasConversion<int>();

            modelBuilder.Entity<Candidatura>()
                .Property(c => c.DataSubmissao)
                .HasDefaultValueSql("NOW()");

            modelBuilder.Entity<Relatorio>()
                .Property(r => r.DataCadastro)
                .HasDefaultValueSql("NOW()");

            modelBuilder.Entity<Candidatura>()
                .HasMany(e => e.Relatorios)
                .WithOne(e => e.Candidatura)
                .HasForeignKey(e => e.CandidaturaId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Candidatura> Candidaturas { get; set; }

        public DbSet<Relatorio> Relatorios { get; set; }
    }
}
