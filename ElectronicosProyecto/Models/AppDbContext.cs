using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ElectronicosProyecto.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asignacion> Asignacions { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<MovimientoEquipo> MovimientoEquipos { get; set; }

    public virtual DbSet<Ubicacion> Ubicacion { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-8S23DTV;Database=EmpresaElectronicos;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asignacion>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__Asignaci__3213E83FDFC130BD");

            entity.ToTable("Asignacion", "Administracion");

            entity.Property(e => e.FechaAsignacion).HasColumnType("datetime");
            entity.Property(e => e.FechaLiberacion).HasColumnType("datetime");
            entity.Property(e => e.descripcion)
                .HasMaxLength(180)
                .IsUnicode(false)
                .HasDefaultValue("");
            entity.Property(e => e.fecha_registro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.responsable)
                .HasMaxLength(180)
                .IsUnicode(false);
            entity.Property(e => e.sis_status).HasDefaultValue(true);

            entity.HasOne(d => d.fk_empresa).WithMany(p => p.Asignacions)
                .HasForeignKey(d => d.fk_empresa_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Asignacio__fk_em__5BE2A6F2");

            entity.HasOne(d => d.fk_equipo).WithMany(p => p.Asignacions)
                .HasForeignKey(d => d.fk_equipo_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Asignacio__fk_eq__5CD6CB2B");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__Categori__3213E83F333A1195");

            entity.ToTable("Categoria", "Clasificacion");

            entity.Property(e => e.fecha_registro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.nombre)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.sis_status).HasDefaultValue(true);

            entity.HasOne(d => d.fk_empresa).WithMany(p => p.Categoria)
                .HasForeignKey(d => d.fk_empresa_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Categoria__fk_em__46E78A0C");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__Empresa__3213E83F8C062279");

            entity.ToTable("Empresa", "Configuracion");

            entity.Property(e => e.fecha_registro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.nombre)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.sis_status).HasDefaultValue(true);
        });

        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__Equipo__3213E83F64325417");

            entity.ToTable("Equipo", "Administracion");

            entity.HasIndex(e => new { e.fk_empresa_id, e.num_serie }, "UX_Equipo_Empresa_NumSerie").IsUnique();

            entity.Property(e => e.descripcion)
                .HasMaxLength(180)
                .IsUnicode(false)
                .HasDefaultValue("");
            entity.Property(e => e.fecha_registro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.num_serie)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.rowver)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.fk_categoria).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.fk_categoria_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Equipo__fk_categ__4CA06362");

            entity.HasOne(d => d.fk_empresa).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.fk_empresa_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Equipo__fk_empre__4BAC3F29");

            entity.HasOne(d => d.fk_status).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.fk_status_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Equipo__fk_statu__4E88ABD4");

            entity.HasOne(d => d.fk_ubicacion).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.fk_ubicacion_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Equipo__fk_ubica__4D94879B");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__Estado__3213E83FE98A71D8");

            entity.ToTable("Estado", "Clasificacion");

            entity.Property(e => e.descripcion)
                .HasMaxLength(180)
                .IsUnicode(false)
                .HasDefaultValue("");
            entity.Property(e => e.fecha_registro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.nombre)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.sis_status).HasDefaultValue(true);
        });

        modelBuilder.Entity<MovimientoEquipo>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__Movimien__3213E83FA991B150");

            entity.ToTable("MovimientoEquipo", "Administracion");

            entity.Property(e => e.descripcion)
                .HasMaxLength(180)
                .IsUnicode(false)
                .HasDefaultValue("");
            entity.Property(e => e.fecha_registro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.responsable)
                .HasMaxLength(180)
                .IsUnicode(false);
            entity.Property(e => e.tipo_movimiento)
                .HasMaxLength(120)
                .IsUnicode(false);

            entity.HasOne(d => d.fk_empresa).WithMany(p => p.MovimientoEquipos)
                .HasForeignKey(d => d.fk_empresa_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movimient__fk_em__534D60F1");

            entity.HasOne(d => d.fk_equipo).WithMany(p => p.MovimientoEquipos)
                .HasForeignKey(d => d.fk_equipo_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movimient__fk_eq__5441852A");

            entity.HasOne(d => d.fk_ubicacion_final).WithMany(p => p.MovimientoEquipofk_ubicacion_finals)
                .HasForeignKey(d => d.fk_ubicacion_final_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movimient__fk_ub__5629CD9C");

            entity.HasOne(d => d.fk_ubicacion_origen).WithMany(p => p.MovimientoEquipofk_ubicacion_origens)
                .HasForeignKey(d => d.fk_ubicacion_origen_id)
                .HasConstraintName("FK__Movimient__fk_ub__5535A963");
        });

        modelBuilder.Entity<Ubicacion>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__Ubicacio__3213E83F6853811C");

            entity.ToTable("Ubicacion", "Administracion");

            entity.Property(e => e.fecha_registro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.nombre)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.sis_status).HasDefaultValue(true);

            entity.HasOne(d => d.fk_empresa).WithMany(p => p.Ubicacions)
                .HasForeignKey(d => d.fk_empresa_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ubicacion__fk_em__4222D4EF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
