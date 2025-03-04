using Verse_Post.Models;

namespace Verse_Post.Repositories
{
    public class PostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _context.Posts
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Post>> GetByCommunityIdAsync(int communityId)
        {
            return await _context.Posts
                .Where(p => p.CommunityId == communityId)
                .Include(p => p.Tags)
                .ToListAsync();
        }

        public async Task CreateAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                post.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
