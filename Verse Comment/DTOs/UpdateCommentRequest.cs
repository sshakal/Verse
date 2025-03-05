using System.ComponentModel.DataAnnotations;

namespace Verse_Comment.DTOs
{
    public class UpdateCommentRequest
    {
        [Required]
        public string Content { get; set; }
    }
}
