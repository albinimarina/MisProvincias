using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MisProvincias.Models;

public partial class RelevamientoProvinciasContext : DbContext
{
    public RelevamientoProvinciasContext()
    {
    }

    public RelevamientoProvinciasContext(DbContextOptions<RelevamientoProvinciasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Animale> Animales { get; set; }

    public virtual DbSet<Planta> Plantas { get; set; }

    public virtual DbSet<Provincia> Provincias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; DataBase=Relevamiento-Provincias;Integrated Security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animale>(entity =>
        {
            entity.HasKey(e => e.IdAnimal).HasName("PK__Animales__951092F09A70F96E");

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Planta>(entity =>
        {
            entity.HasKey(e => e.IdPlanta).HasName("PK__Plantas__12FEC12463094C38");

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.IdProvincia).HasName("PK__Provinci__EED744558BB305AA");

            entity.Property(e => e.Capital)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAnimalNavigation).WithMany(p => p.Provincia)
                .HasForeignKey(d => d.IdAnimal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Provincias_Animales");

            entity.HasOne(d => d.IdPlantaNavigation).WithMany(p => p.Provincia)
                .HasForeignKey(d => d.IdPlanta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Provincias_Plantas");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
