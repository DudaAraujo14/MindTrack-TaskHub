using Microsoft.EntityFrameworkCore;
using MindTrack.Domain.Entities;

namespace MindTrack.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<TaskItem> Tasks => Set<TaskItem>();
        public DbSet<FocusRecord> FocusRecords => Set<FocusRecord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<User>(e =>
            {
                e.Property(p => p.Name)
                    .HasMaxLength(120)
                    .IsRequired();

                e.Property(p => p.Email)
                    .HasMaxLength(160)
                    .IsRequired();
            });

          
            modelBuilder.Entity<TaskItem>(e =>
            {
                e.Property(p => p.Title)
                    .HasMaxLength(160)
                    .IsRequired();

                e.Property(p => p.Description)
                    .HasMaxLength(1000);

                e.HasOne(t => t.User!)
                    .WithMany(u => u.Tasks!)
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            
            modelBuilder.Entity<FocusRecord>(e =>
            {
                e.HasOne(f => f.User!)
                    .WithMany(u => u.FocusRecords!)
                    .HasForeignKey(f => f.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
