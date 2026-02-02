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

    
    modelBuilder.Entity<User>(entity =>
    {
        entity.ToTable("Users");

        entity.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserRoles",
                r => r.HasOne<Role>()
                      .WithMany()
                      .HasForeignKey("RoleId")
                      .OnDelete(DeleteBehavior.Cascade),
                l => l.HasOne<User>()
                      .WithMany()
                      .HasForeignKey("UserId")
                      .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("UserId", "RoleId");

                    j.Property<long>("UserId");
                    j.Property<long>("RoleId");

                    j.ToTable("UserRoles");
                });

        entity.HasMany(u => u.ContactInfos)
            .WithOne(ci => ci.User)
            .HasForeignKey(ci => ci.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(u => u.RefreshTokens)
            .WithOne(rt => rt.User)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(u => u.Lessons)
            .WithMany(l => l.Teachers)
            .UsingEntity<Dictionary<string, object>>(
                "LessonUser",
                r => r.HasOne<Lesson>()
                      .WithMany()
                      .HasForeignKey("LessonId")
                      .OnDelete(DeleteBehavior.Cascade),
                l => l.HasOne<User>()
                      .WithMany()
                      .HasForeignKey("UserId")
                      .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("LessonId", "UserId");

                    j.Property<long>("LessonId");
                    j.Property<long>("UserId");

                    j.ToTable("LessonUser");
                });
    });


    modelBuilder.Entity<Role>()
        .ToTable("Roles");
    
    
    modelBuilder.Entity<ContactType>()
        .ToTable("ContactTypes");

    modelBuilder.Entity<ContactInfo>(entity =>
    {
        entity.ToTable("ContactInfos");

        entity.HasOne(ci => ci.ContactType)
            .WithMany(ct => ct.ContactInfos)
            .HasForeignKey(ci => ci.ContactTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    });


    modelBuilder.Entity<Group>(entity =>
    {
        entity.ToTable("Groups");

        entity.HasMany(g => g.Students)
            .WithOne(s => s.Group)
            .HasForeignKey(s => s.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(g => g.SchoolClasses)
            .WithOne(sc => sc.Group)
            .HasForeignKey(sc => sc.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
    });


    modelBuilder.Entity<Student>(entity =>
    {
        entity.ToTable("Students");
        entity.HasBaseType<User>();
    });


    modelBuilder.Entity<SchoolClass>(entity =>
    {
        entity.ToTable("SchoolClasses");

        
        entity.HasMany(sc => sc.Lessons)
            .WithMany(l => l.SchoolClasses)
            .UsingEntity<Dictionary<string, object>>(
                "LessonSchoolClass",
                r => r.HasOne<Lesson>()
                      .WithMany()
                      .HasForeignKey("LessonId")
                      .OnDelete(DeleteBehavior.Cascade),
                l => l.HasOne<SchoolClass>()
                      .WithMany()
                      .HasForeignKey("SchoolClassId")
                      .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("LessonId", "SchoolClassId");

                    j.Property<long>("LessonId");
                    j.Property<long>("SchoolClassId");

                    j.ToTable("LessonSchoolClass");
                });
    });


    modelBuilder.Entity<Lesson>(entity =>
    {
        entity.ToTable("Lessons");

        entity.HasOne(l => l.Subject)
            .WithMany()
            .HasForeignKey(l => l.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(l => l.Grades)
            .WithOne()
            .HasForeignKey(g => g.LessonId)
            .OnDelete(DeleteBehavior.Cascade);
    });


    modelBuilder.Entity<Subject>(entity =>
    {
        entity.ToTable("Subjects");

        entity.HasMany(s => s.Teachers)
            .WithMany(u => u.TeacherSubjects)
            .UsingEntity<Dictionary<string, object>>(
                "SubjectUser",
                r => r.HasOne<User>()
                      .WithMany()
                      .HasForeignKey("UserId")
                      .OnDelete(DeleteBehavior.Cascade),
                l => l.HasOne<Subject>()
                      .WithMany()
                      .HasForeignKey("SubjectId")
                      .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("SubjectId", "UserId");

                    j.Property<long>("SubjectId");
                    j.Property<long>("UserId");

                    j.ToTable("SubjectUser");
                });
    });


    modelBuilder.Entity<Grade>(entity =>
    {
        entity.ToTable("Grades");

        entity.HasOne(g => g.Student)
            .WithMany(s => s.Grades)
            .HasForeignKey(g => g.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    });


    modelBuilder.Entity<RefreshToken>()
        .ToTable("RefreshTokens");

    base.OnModelCreating(modelBuilder);
}

}