using Microsoft.EntityFrameworkCore;

namespace DevExpress.AI.Samples.Blazor.Data {
    public class IssuesContext : DbContext {
        public IssuesContext(DbContextOptions<IssuesContext> options)
            : base(options) {
        }

        public DbSet<Issue> Items { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Issue>(entity => {
                entity.HasKey(t => new { t.ID });
                // Properties
                entity.Property(t => t.ID);
                entity.Property(t => t.Name)
                    .HasMaxLength(50);
                entity.Property(t => t.Description)
                    .HasMaxLength(2147483647);
                entity.Property(t => t.Resolution)
                    .HasMaxLength(2147483647);
            });
            modelBuilder.Entity<Project>(entity => {
                entity.HasKey(t => t.ID);
                // Properties
                entity.Property(t => t.ID);
                entity.Property(t => t.Name)
                    .HasMaxLength(100);
            });
            modelBuilder.Entity<User>(entity => {
                entity.HasKey(t => t.ID);
                // Properties
                entity.Property(t => t.ID);
                entity.Property(t => t.FirstName)
                    .HasMaxLength(25);
                entity.Property(t => t.LastName)
                    .HasMaxLength(25);
                entity.Property(t => t.Country)
                    .HasMaxLength(15);
                entity.Property(t => t.City)
                    .HasMaxLength(15);
                entity.Property(t => t.Address)
                    .HasMaxLength(60);
                entity.Property(t => t.Phone)
                    .HasMaxLength(24);
                entity.Property(t => t.Email)
                    .HasMaxLength(50);
            });
        }
    }
}
