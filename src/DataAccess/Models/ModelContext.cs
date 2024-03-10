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
        public virtual DbSet<Resource> Resources { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Userresource> Userresources { get; set; } = null!;
        public virtual DbSet<Worker> Workers { get; set; } = null!;

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

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.ToTable("RESOURCES");

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

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
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

                entity.Property(e => e.Usertype)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USERTYPE");
            });

            modelBuilder.Entity<Userresource>(entity =>
            {
                entity.ToTable("USERRESOURCE");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Actcreate)
                    .HasPrecision(1)
                    .HasColumnName("ACTCREATE");

                entity.Property(e => e.Actdelete)
                    .HasPrecision(1)
                    .HasColumnName("ACTDELETE");

                entity.Property(e => e.Actread)
                    .HasPrecision(1)
                    .HasColumnName("ACTREAD");

                entity.Property(e => e.Actupdate)
                    .HasPrecision(1)
                    .HasColumnName("ACTUPDATE");

                entity.Property(e => e.Datecreated)
                    .HasColumnType("DATE")
                    .HasColumnName("DATECREATED");

                entity.Property(e => e.Dateupdated)
                    .HasColumnType("DATE")
                    .HasColumnName("DATEUPDATED");

                entity.Property(e => e.Resourceid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("RESOURCEID");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.Userresources)
                    .HasForeignKey(d => d.Resourceid)
                    .HasConstraintName("FK_RESOURCEID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userresources)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK_USERID");
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

            modelBuilder.HasSequence("PARKING_ID_SEQ");

            modelBuilder.HasSequence("RESOURCES_ID_SEQ");

            modelBuilder.HasSequence("USERRESOURCE_ID_SEQ");

            modelBuilder.HasSequence("USERS_ID_SEQ");

            modelBuilder.HasSequence("WORKERS_ID_SEQ");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
