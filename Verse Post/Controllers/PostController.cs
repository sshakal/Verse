using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Verse_Post.DTOs;
using Verse_Post.Services;

namespace Verse_Post.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostRequest request)
        {
            var authorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var post = await _postService.CreatePostAsync(request, authorId);
            return Ok(post);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, UpdatePostRequest request)
        {
            var post = await _postService.UpdatePostAsync(id, request);
            return Ok(post);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.DeletePostAsync(id);
            return NoContent();
        }
    }
}
