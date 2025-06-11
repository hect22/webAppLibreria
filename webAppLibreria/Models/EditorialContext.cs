using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace webAppLibreria.Models;

public partial class EditorialContext : DbContext
{
    public EditorialContext()
    {
    }

    public EditorialContext(DbContextOptions<EditorialContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Editorial> Editorials { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Editorial;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Editorial>(entity =>
        {
            entity.HasKey(e => e.Ideditorial).HasName("PK__Editoria__0903B8214810E224");

            entity.ToTable("Editorial");

            entity.Property(e => e.Ideditorial).HasColumnName("IDEditorial");
            entity.Property(e => e.Ciudad).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.Editorial1)
                .HasMaxLength(100)
                .HasColumnName("Editorial");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.NombreContacto).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.Idinventario).HasName("PK__Inventar__D69EA8804549C2CE");

            entity.ToTable("Inventario");

            entity.Property(e => e.Idinventario).HasColumnName("IDInventario");
            entity.Property(e => e.Idlibro).HasColumnName("IDLibro");
            entity.Property(e => e.Idsucursal).HasColumnName("IDSucursal");

            entity.HasOne(d => d.IdlibroNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.Idlibro)
                .HasConstraintName("FK__Inventari__IDLib__2C3393D0");

            entity.HasOne(d => d.IdsucursalNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.Idsucursal)
                .HasConstraintName("FK__Inventari__IDSuc__2D27B809");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Idlibro).HasName("PK__Libro__3FE8EB6F4CBB0F60");

            entity.ToTable("Libro");

            entity.HasIndex(e => e.Isbn, "UQ__Libro__447D36EA54080393").IsUnique();

            entity.Property(e => e.Idlibro).HasColumnName("IDLibro");
            entity.Property(e => e.Autor).HasMaxLength(100);
            entity.Property(e => e.Ideditorial).HasColumnName("IDEditorial");
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("ISBN");
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Titulo).HasMaxLength(200);

            entity.HasOne(d => d.IdeditorialNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.Ideditorial)
                .HasConstraintName("FK__Libro__IDEditori__276EDEB3");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.Idsucursal).HasName("PK__Sucursal__696BA61040E126C2");

            entity.ToTable("Sucursal");

            entity.Property(e => e.Idsucursal).HasColumnName("IDSucursal");
            entity.Property(e => e.Ciudad).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.NombreEncargado).HasMaxLength(100);
            entity.Property(e => e.Sucursal1)
                .HasMaxLength(100)
                .HasColumnName("Sucursal");
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
