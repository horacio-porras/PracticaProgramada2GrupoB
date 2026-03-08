using System;
using System.Collections.Generic;
using PracticaProgramada2DAL.Entidades;
using Microsoft.EntityFrameworkCore;

namespace PracticaProgramada2DAL.Data;

public partial class PracticaProgramada2DbContext : DbContext
{
    public PracticaProgramada2DbContext()
    {
    }

    public PracticaProgramada2DbContext(DbContextOptions<PracticaProgramada2DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=C:\\Users\\richa\\Downloads\\DB.Browser.for.SQLite-v3.13.1-win64 (2)\\CarroDB.db");
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.ToTable("Categorias");
            entity.HasIndex(e => e.Nombre).IsUnique();
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.ToTable("Productos");
            entity.HasOne(d => d.Categoria)
                .WithMany()
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
