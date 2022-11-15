using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace nummerplade_api.Model;

public partial class NummerpladeApiContext : DbContext
{
    public NummerpladeApiContext()
    {
    }

    public NummerpladeApiContext(DbContextOptions<NummerpladeApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Policecar> Policecars { get; set; }

    public virtual DbSet<Spotting> Spottings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=jbgaard.xyz;Database=nummerplade-api;User Id=nummerplade-api-usr;Password=NummerpladeScanner2022;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Policecar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__policeca__3213E83FEF2840B5");

            entity.ToTable("policecars");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("added_date");
            entity.Property(e => e.Nrplade)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("nrplade");
        });

        modelBuilder.Entity<Spotting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__spotting__3213E83FC9AA8466");

            entity.ToTable("spottings");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("added_date");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.Nrplade)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("nrplade");
            entity.Property(e => e.ValidUntilDate)
                .HasColumnType("datetime")
                .HasColumnName("validUntil_date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
