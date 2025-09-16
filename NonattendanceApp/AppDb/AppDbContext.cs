using Microsoft.EntityFrameworkCore;

namespace NonattendanceApp.AppDb;

public class AppDbContext : DbContext
{
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Class> Classes { get; set; } = null!;
    public DbSet<Attendance> Attendances { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Attendance ilişkileri
        modelBuilder.Entity<Attendance>()
            .HasOne(a => a.Student)
            .WithMany()
            .HasForeignKey(a => a.StudentId);

        modelBuilder.Entity<Attendance>()
            .HasOne(a => a.Class)
            .WithMany()
            .HasForeignKey(a => a.ClassId);
    }
}