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
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity(j => j.ToTable("UserRoles"));

        modelBuilder.Entity<User>()
            .HasMany(u => u.ContactInfos)
            .WithOne(ci => ci.User)
            .HasForeignKey(ci => ci.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Grades)
            .WithOne(g => g.User)
            .HasForeignKey(ci => ci.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Groups)
            .WithOne(g => g.User)
            .HasForeignKey(g => g.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SchoolClass>()
            .HasMany(u => u.Lesson)
            .WithMany(sc => sc.SchoolClass)
            .UsingEntity(j => j.ToTable("LessonSchoolClass"));

        modelBuilder.Entity<User>()
            .HasMany(u => u.Subjects)
            .WithMany(s => s.TeacherId)
            .UsingEntity(j => j.ToTable("TeacherSubjects"));

        modelBuilder.Entity<SchoolClass>()
            .HasOne(sc => sc.Group)
            .WithMany(g => g.SchoolClasses)
            .HasForeignKey(sc => sc.GroupId);

        modelBuilder.Entity<Grade>()
            .HasOne(g => g.SchoolClass)
            .WithMany(sc => sc.Grade)
            .HasForeignKey(g => g.SchoolClassId);

        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Subject)
            .WithMany(s => s.Lesson)
            .HasForeignKey(l => l.SubjectId)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}