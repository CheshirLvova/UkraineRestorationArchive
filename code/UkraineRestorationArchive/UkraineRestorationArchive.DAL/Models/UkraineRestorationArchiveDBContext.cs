using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace UkraineRestorationArchive.DAL.Models
{
    public partial class UkraineRestorationArchiveDBContext : DbContext
    {
        public UkraineRestorationArchiveDBContext()
        {
        }

        public UkraineRestorationArchiveDBContext(DbContextOptions<UkraineRestorationArchiveDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=UkraineRestorationArchiveDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.Image1)
                    .IsRequired()
                    .HasColumnType("image")
                    .HasColumnName("Image");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(900);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Images)
                    .HasPrincipalKey(p => p.Username)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Scores_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username, "UQ__Users__536C85E449F93A1B")
                    .IsUnique();

                entity.Property(e => e.EmailAddres).IsRequired();

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasDefaultValueSql("('User')")
                    .IsFixedLength(true);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(900);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
