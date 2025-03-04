using Verse_Post.Models;

namespace Verse_Post.Repositories
{
    public class TagRepository
    {
        private readonly ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tag> GetOrCreateTagAsync(string name)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == name);
            if (tag == null)
            {
                tag = new Tag { Name = name };
                _context.Tags.Add(tag);
                await _context.SaveChangesAsync();
            }
            return tag;
        }
    }
}
