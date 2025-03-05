using Microsoft.AspNetCore.Mvc;
using Verse_Comment.DTOs;
using Verse_Comment.Services;

namespace Verse_Comment.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        // GET: api/comments?postId=5
        [HttpGet]
        public async Task<IActionResult> GetCommentsByPostId([FromQuery] int postId)
        {
            var comments = await _commentService.GetCommentsByPostIdAsync(postId);
            return Ok(comments);
        }

        // POST: api/comments
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequest request)
        {
            var comment = await _commentService.CreateCommentAsync(request);
            return CreatedAtAction(nameof(GetCommentsByPostId), new { postId = comment.PostId }, comment);
        }

        // PUT: api/comments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentRequest request)
        {
            var comment = await _commentService.UpdateCommentAsync(id, request);
            return Ok(comment);
        }

        // DELETE: api/comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            await _commentService.DeleteCommentAsync(id);
            return NoContent();
        }
    }
}
