using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PizzaApplication.Models;

public partial class PizzaDbContext : DbContext
{
    public PizzaDbContext()
    {
    }

    public PizzaDbContext(DbContextOptions<PizzaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<OrderPlaced> OrderPlaceds { get; set; }

    public virtual DbSet<OrderPlacing> OrderPlacings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:newserver3058.database.windows.net,1433;Initial Catalog=PizzaDb;User ID=admin123;Password=Vasanth@123;Encrypt=True;TrustServerCertificate=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderPlaced>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__OrderPla__C3905BCF29586D74");

            entity.ToTable("OrderPlaced");

            entity.Property(e => e.CustomerName).HasMaxLength(50);
            entity.Property(e => e.ProductName).HasMaxLength(100);
        });

        modelBuilder.Entity<OrderPlacing>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__OrderPla__A4AE64D87D02D1AC");

            entity.ToTable("OrderPlacing", tb => tb.HasTrigger("trg_InsertOrderPlaced"));

            entity.Property(e => e.CustomerId).ValueGeneratedNever();
            entity.Property(e => e.CustomerName).HasMaxLength(50);
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.PhoneNumber).HasMaxLength(10);
            entity.Property(e => e.ProductName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
