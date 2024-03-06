using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Parking> Parkings { get; set; } = null!;
        public virtual DbSet<Worker> Workers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("EJEMPLO_EF_CORE");

            modelBuilder.Entity<Parking>(entity =>
            {
                entity.ToTable("PARKING");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Numbercarpark)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("NUMBERCARPARK");

                entity.Property(e => e.Numberspace)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("NUMBERSPACE");
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.ToTable("WORKERS");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Age)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("AGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Parkingid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PARKINGID");

                entity.Property(e => e.Rfc)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("RFC");

                entity.HasOne(d => d.Parking)
                    .WithMany(p => p.Workers)
                    .HasForeignKey(d => d.Parkingid)
                    .HasConstraintName("FK_PARKINGID");
            });

            modelBuilder.HasSequence("CATEGORIA_SEQ");

            modelBuilder.HasSequence("PARKING_ID_SEQ");

            modelBuilder.HasSequence("WORKERS_ID_SEQ");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
