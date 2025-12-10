using Microsoft.EntityFrameworkCore;
using school_electronic_magazine.Models;

namespace school_electronic_magazine.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> users { get; set; }
    public DbSet<SchoolClass> SchoolClasses { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .ToTable("Users")
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity(j => j.ToTable("UserRoles"));

        modelBuilder.Entity<User>()
            .HasMany(u => u.ContactInfos)
            .WithOne(ci => ci.User)
            .HasForeignKey(ci => ci.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Student>()
            .ToTable("Students")
            .HasOne(s => s.Group)
            .WithMany(g => g.Students)
            .HasForeignKey(s => s.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SchoolClass>()
            .HasOne(sc => sc.Group)
            .WithMany(g => g.SchoolClasses)
            .HasForeignKey(sc => sc.ClassId);

        modelBuilder.Entity<Grade>()
            .HasOne(g => g.User)
            .WithMany(u => u.Grades)
            .HasForeignKey(g => g.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Lesson>()
            .ToTable("Lessons")
            .HasOne(l => l.Subject)
            .WithMany(s => s.Lesson)
            .HasForeignKey(l => l.SubjectId)
            .IsRequired();

        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Student)
            .WithMany() 
            .HasForeignKey(l => l.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Teacher)
            .WithMany()
            .HasForeignKey(l => l.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}