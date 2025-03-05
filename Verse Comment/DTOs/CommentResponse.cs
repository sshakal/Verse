namespace Verse_Comment.DTOs
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public int PostId { get; set; }
        public int? ParentCommentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<CommentResponse> Replies { get; set; } = new List<CommentResponse>();
    }
}
