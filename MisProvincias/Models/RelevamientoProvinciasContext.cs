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

            entity.HasOne(d => d.ObAnimal).WithMany(p => p.Provincia)
                .HasForeignKey(d => d.IdAnimal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Provincias_Animales");

            entity.HasOne(d => d.ObPlanta).WithMany(p => p.Provincia)
                .HasForeignKey(d => d.IdPlanta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Provincias_Plantas");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
