using System;
using System.Collections.Generic;
using CursosOnline.Domains;
using Microsoft.EntityFrameworkCore;

namespace CursosOnline.Contexts;

public partial class CursosOnlineContext : DbContext
{
    public CursosOnlineContext()
    {
    }

    public CursosOnlineContext(DbContextOptions<CursosOnlineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aluno> Aluno { get; set; }

    public virtual DbSet<Curso> Curso { get; set; }

    public virtual DbSet<Instrutor> Instrutor { get; set; }

    public virtual DbSet<Matricula> Matricula { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CursosOnline;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Aluno__3214EC0799277E8A");

            entity.HasIndex(e => e.Email, "UQ__Aluno__A9D10534F4E2FEA8").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Nome).HasMaxLength(150);
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Curso__3214EC07F646ABC9");

            entity.Property(e => e.Nome).HasMaxLength(150);
            entity.Property(e => e.StatusCurso).HasDefaultValue(true);

            entity.HasOne(d => d.Instrutor).WithMany(p => p.Curso)
                .HasForeignKey(d => d.InstrutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cursos_Instrutor");
        });

        modelBuilder.Entity<Instrutor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Instruto__3214EC07FAB9E8AB");

            entity.HasIndex(e => e.Email, "UQ__Instruto__A9D105346F8607FC").IsUnique();

            entity.Property(e => e.AreaEspecializacao).HasMaxLength(150);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Nome).HasMaxLength(150);
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Matricul__3214EC0794C4AB87");

            entity.HasIndex(e => new { e.AlunoId, e.CursoId }, "UQ_Aluno_Curso").IsUnique();

            entity.HasOne(d => d.Aluno).WithMany(p => p.Matricula)
                .HasForeignKey(d => d.AlunoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Matricula_Aluno");

            entity.HasOne(d => d.Curso).WithMany(p => p.Matricula)
                .HasForeignKey(d => d.CursoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Matricula_Curso");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
