using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BD_Films.Models;

public partial class DbFilmsContext : DbContext
{
    public DbFilmsContext()
    {
    }

    public DbFilmsContext(DbContextOptions<DbFilmsContext> options)
        : base(options)
    {
        //this.LogTo(Console.WriteLine);
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<FilmsActor> FilmsActors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlite("Data Source=C:\\Work\\C_Sharp\\Bing\\BD_Films\\BD_Films\\BD_Films\\wwwroot\\DB_Films.sqlite3");
        => optionsBuilder.UseSqlite(Program.connectionString)
           .LogTo(Console.WriteLine);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.Property(e => e.Id)
                //.ValueGeneratedNever()
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
        });

        modelBuilder.Entity<FilmsActor>(entity =>
        {
            entity.ToTable("Films_Actors");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IdActor).HasColumnName("idActor");
            entity.Property(e => e.IdFilm).HasColumnName("idFilm");

            entity.HasOne(d => d.IdActorNavigation).WithMany(p => p.FilmsActors).HasForeignKey(d => d.IdActor);

            entity.HasOne(d => d.IdFilmNavigation).WithMany(p => p.FilmsActors).HasForeignKey(d => d.IdFilm);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
