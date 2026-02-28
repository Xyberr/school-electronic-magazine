using Microsoft.EntityFrameworkCore;
using school_electronic_magazine.Models;

namespace school_electronic_magazine.Data;

    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Lesson> Lessons { get; set; } = null!;
        public DbSet<Subject> Subjects { get; set; } = null!;
        public DbSet<Grade> Grades { get; set; } = null!;
        public DbSet<ContactInfo> ContactInfos { get; set; } = null!;
        public DbSet<ContactType> ContactTypes { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public DbSet<SchoolClass> SchoolClasses { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            
            base.OnModelCreating(modelBuilder);
        }
    }