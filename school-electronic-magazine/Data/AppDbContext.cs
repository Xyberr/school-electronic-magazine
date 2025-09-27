using Microsoft.EntityFrameworkCore;
using school_electronic_magazine.Models;

namespace school_electronic_magazine.Data;

public class AppDbContext : DbContext
{
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Student> StudentDto { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<UserCredentials> UserCredentials { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Первичный ключ
        modelBuilder.Entity<Teacher>().HasKey(t => t.Id);
        modelBuilder.Entity<Student>().HasKey(s => s.Id);
        modelBuilder.Entity<Admin>().HasKey(a => a.Id);
        modelBuilder.Entity<UserCredentials>().HasKey(c => c.Id);

        // Таблицы
        modelBuilder.Entity<Teacher>().ToTable("Teachers");
        modelBuilder.Entity<Student>().ToTable("Students");
        modelBuilder.Entity<Admin>().ToTable("Admins");
        modelBuilder.Entity<UserCredentials>().ToTable("UserCredentials");

        // связь Teacher с UserCredentials
        modelBuilder.Entity<Teacher>()
            .HasOne(t => t.Credential)
            .WithOne(c => c.Teacher)
            .HasForeignKey<UserCredentials>(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);

        // связь Student с UserCredentials
        modelBuilder.Entity<Student>()
            .HasOne(s => s.Credentials)
            .WithOne(c => c.Student)
            .HasForeignKey<UserCredentials>(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);

        // связь Admin с UserCredentials
        modelBuilder.Entity<Admin>()
            .HasOne(a => a.Credentials)
            .WithOne(c => c.Admin)
            .HasForeignKey<UserCredentials>(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}