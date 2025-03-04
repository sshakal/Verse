namespace Verse_Post.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; } // ID пользователя, создавшего пост
        public int CommunityId { get; set; } // ID сообщества, к которому относится пост
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<Tag> Tags { get; set; } = new List<Tag>(); // Связь с тегами
    }
}
