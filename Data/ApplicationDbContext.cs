using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EDIITechincalInterview.Models;

namespace EDIITechincalInterview.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Biodata>()
                .HasOne(b => b.User)
                .WithOne()
                .HasForeignKey<Biodata>(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Biodata>()
                .HasIndex(b => b.UserId)
                .IsUnique();

            modelBuilder.Entity<PendidikanTerakhir>()
                .HasOne(p => p.Biodata)
                .WithMany(b => b.PendidikanTerakhir)
                .HasForeignKey(p => p.BiodataId);

            modelBuilder.Entity<RiwayatPelatihan>()
                .HasOne(r => r.Biodata)
                .WithMany(b => b.RiwayatPelatihan)
                .HasForeignKey(r => r.BiodataId);

            modelBuilder.Entity<RiwayatPekerjaan>()
                .HasOne(r => r.Biodata)
                .WithMany(b => b.RiwayatPekerjaan)
                .HasForeignKey(r => r.BiodataId);
        }

        public DbSet<Biodata> Biodatas { get; set; } = null!;
        public DbSet<PendidikanTerakhir> PendidikanTerakhirs { get; set; } = null!;
        public DbSet<RiwayatPelatihan> RiwayatPelatihans { get; set; } = null!;
        public DbSet<RiwayatPekerjaan> RiwayatPekerjaans { get; set; } = null!;
    }
}