using CourseManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Assigment> Assigments { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(u => u.OwnCourses);

            builder.Entity<UserCourse>().HasKey(sc => new { sc.UserId, sc.CourseId });

            builder.Entity<UserCourse>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.UserCourses)
                .HasForeignKey(sc => sc.UserId);


            builder.Entity<UserCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(s => s.UserCourses)
                .HasForeignKey(sc => sc.CourseId);

            builder.Entity<Assigment>()
                .HasOne(a => a.Course)
                .WithMany(c => c.Assigments);   

            base.OnModelCreating(builder);
        }
    }
}
