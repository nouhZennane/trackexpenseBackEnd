using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackExences.Models;

namespace TrackExences.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        UserConfig(modelBuilder);
        ExpensesConfig(modelBuilder);
    }

    private void UserConfig(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<User> userbuilder = modelBuilder.Entity<User>();
        
        userbuilder
            .HasKey(u => u.Id);
        userbuilder.Property(u => u.Id).ValueGeneratedOnAdd();
        
        userbuilder
            .Property(u => u.nickname).IsRequired().HasMaxLength(255);
        userbuilder
            .Property(u => u.Email).IsRequired().HasMaxLength(255);
        userbuilder
            .Property(u => u.Password).IsRequired().HasMaxLength(255);
        userbuilder.HasIndex(u => u.Email).IsUnique();
    }

    private void ExpensesConfig(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<Expense> expenseBuilder = modelBuilder.Entity<Expense>();
        
        expenseBuilder.HasKey(e => e.Id);
        expenseBuilder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        expenseBuilder.Property(e => e.Amount).IsRequired().HasMaxLength(255);
        expenseBuilder.Property(e => e.Date).IsRequired().HasMaxLength(255);
        expenseBuilder.Property(e => e.Description).IsRequired(false).HasMaxLength(255);

        expenseBuilder
            .HasOne<User>(e => e.User)
            .WithMany(u => u.Expenses)
            .HasForeignKey(e => e.UserId);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Expense> Expenses { get; set; }
}