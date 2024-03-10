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
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Userstype> Userstypes { get; set; } = null!;
        public virtual DbSet<Worker> Workers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("User Id=ejemplo_ef_core;Password=123456;Data Source=localhost:1521/XE;");
            }
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

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Datecreated)
                    .HasColumnType("DATE")
                    .HasColumnName("DATECREATED");

                entity.Property(e => e.Dateupdated)
                    .HasColumnType("DATE")
                    .HasColumnName("DATEUPDATED");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Password)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Username)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.Property(e => e.Usertypeid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERTYPEID");

                entity.HasOne(d => d.Usertype)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Usertypeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USERTYPEID");
            });

            modelBuilder.Entity<Userstype>(entity =>
            {
                entity.ToTable("USERSTYPE");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Datecreated)
                    .HasColumnType("DATE")
                    .HasColumnName("DATECREATED");

                entity.Property(e => e.Dateupdated)
                    .HasColumnType("DATE")
                    .HasColumnName("DATEUPDATED");

                entity.Property(e => e.Endpoints)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ENDPOINTS");

                entity.Property(e => e.Typeuser)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TYPEUSER");
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

            modelBuilder.HasSequence("USERS_ID_SEQ");

            modelBuilder.HasSequence("USERSTYPE_ID_SEQ");

            modelBuilder.HasSequence("WORKERS_ID_SEQ");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
