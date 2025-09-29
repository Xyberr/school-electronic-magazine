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
        /*modelBuilder.Entity<User>()
            .HasMany(user => user.Roles)
            .WithMany(role => role.Users)
            .UsingEntity("user_roles");
*/
        modelBuilder.Entity<SchoolClass>()
            .HasMany(sc => sc.Students)
            .WithOne(u => u.SchoolClass)
            .HasForeignKey(u => u.ClassId);
            
        
        base.OnModelCreating(modelBuilder);
    }
}