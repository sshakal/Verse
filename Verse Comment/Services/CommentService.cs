using System.Security.Claims;
using Verse_Comment.DTOs;
using Verse_Comment.Models;
using Verse_Comment.Repositories;

namespace Verse_Comment.Services
{
    public class CommentService
    {
        private readonly CommentRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentService(
            CommentRepository repository,
            IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<CommentResponse>> GetCommentsByPostIdAsync(int postId)
        {
            var comments = await _repository.GetCommentsByPostIdAsync(postId);
            return MapCommentsToResponse(comments);
        }

        public async Task<CommentResponse> CreateCommentAsync(CreateCommentRequest request)
        {
            var authorId = GetCurrentUserId();

            var comment = new Comment
            {
                Content = request.Content,
                PostId = request.PostId,
                ParentCommentId = request.ParentCommentId,
                AuthorId = authorId
            };

            var createdComment = await _repository.CreateAsync(comment);
            return MapToResponse(createdComment);
        }

        public async Task<CommentResponse> UpdateCommentAsync(int id, UpdateCommentRequest request)
        {
            var comment = await _repository.GetByIdAsync(id);
            if (comment == null || comment.IsDeleted)
                throw new Exception("Comment not found");

            comment.Content = request.Content;
            var updatedComment = await _repository.UpdateAsync(comment);
            return MapToResponse(updatedComment);
        }

        public async Task DeleteCommentAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        private List<CommentResponse> MapCommentsToResponse(List<Comment> comments)
        {
            return comments.Select(c => new CommentResponse
            {
                Id = c.Id,
                Content = c.Content,
                AuthorId = c.AuthorId,
                PostId = c.PostId,
                ParentCommentId = c.ParentCommentId,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                Replies = MapCommentsToResponse(c.Replies)
            }).ToList();
        }
        private CommentResponse MapToResponse(Comment comment)
        {
            return new CommentResponse
            {
                Id = comment.Id,
                Content = comment.Content,
                AuthorId = comment.AuthorId,
                PostId = comment.PostId,
                ParentCommentId = comment.ParentCommentId,
                CreatedAt = comment.CreatedAt,
                UpdatedAt = comment.UpdatedAt,
                Replies = comment.Replies.Select(MapToResponse).ToList() 
            };
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim == null ? throw new Exception("User not authenticated") : int.Parse(userIdClaim.Value);
        }
    }
}
