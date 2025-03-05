namespace Verse_Post.DTOs
{
    public class CreatePostRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int CommunityId { get; set; }
        public List<string> Tags { get; set; } // Список тегов
    }
}
