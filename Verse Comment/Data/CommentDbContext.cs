using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse_Comment.Models;

namespace Verse_Comment.Data
{
    public class CommentDbContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }

        public CommentDbContext(DbContextOptions<CommentDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
