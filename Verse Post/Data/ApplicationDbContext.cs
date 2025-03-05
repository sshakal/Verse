using Microsoft.EntityFrameworkCore;
using Verse_Post.Models;

namespace Verse_Post.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Tags)
                .WithMany(t => t.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "PostTags", // Имя промежуточной таблицы
                    j => j
                        .HasOne<Tag>()
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade), // Внешний ключ для Tag
                    j => j
                        .HasOne<Post>()
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade), // Внешний ключ для Post
                    j =>
                    {
                        j.HasKey("PostId", "TagId"); // Составной ключ
                    });
        }
    }
}
