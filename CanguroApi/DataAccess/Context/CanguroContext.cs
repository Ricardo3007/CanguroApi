using System;
using System.Collections.Generic;
using CanguroApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CanguroApi.DataAccess.Context;

public partial class CanguroContext : DbContext
{
    public CanguroContext()
    {
    }

    public CanguroContext(DbContextOptions<CanguroContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Moneda> Moneda { get; set; }

    public virtual DbSet<MovCanguro> MovCanguro { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Moneda>(entity =>
        {
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombre).HasMaxLength(200);
        });

        modelBuilder.Entity<MovCanguro>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__MovCangu__06370DAD9B37A257");

            entity.Property(e => e.Codigo).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).HasMaxLength(250);
            entity.Property(e => e.Direccion).HasMaxLength(250);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.Identificacion).HasMaxLength(50);

            entity.HasOne(d => d.MonedaNavigation).WithMany(p => p.MovCanguro)
                .HasForeignKey(d => d.Moneda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MovCangur__Moned__48CFD27E");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Password).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
