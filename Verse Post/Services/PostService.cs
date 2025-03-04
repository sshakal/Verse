using Verse_Post.DTOs;
using Verse_Post.Models;
using Verse_Post.Repositories;

namespace Verse_Post.Services
{
    public class PostService
    {
        private readonly PostRepository _postRepository;
        private readonly TagRepository _tagRepository;

        public PostService(PostRepository postRepository, TagRepository tagRepository)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
        }

        public async Task<PostResponse> CreatePostAsync(CreatePostRequest request, int authorId)
        {
            var post = new Post
            {
                Title = request.Title,
                Content = request.Content,
                AuthorId = authorId,
                CommunityId = request.CommunityId,
                Tags = new List<Tag>()
            };

            foreach (var tagName in request.Tags)
            {
                var tag = await _tagRepository.GetOrCreateTagAsync(tagName);
                post.Tags.Add(tag);
            }

            await _postRepository.CreateAsync(post);

            return new PostResponse
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                AuthorId = post.AuthorId,
                CommunityId = post.CommunityId,
                CreatedAt = post.CreatedAt,
                Tags = post.Tags.Select(t => t.Name).ToList()
            };
        }

        public async Task<PostResponse> UpdatePostAsync(int postId, UpdatePostRequest request)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null)
            {
                throw new Exception("Post not found");
            }

            post.Title = request.Title;
            post.Content = request.Content;
            post.UpdatedAt = DateTime.UtcNow;

            post.Tags.Clear();
            foreach (var tagName in request.Tags)
            {
                var tag = await _tagRepository.GetOrCreateTagAsync(tagName);
                post.Tags.Add(tag);
            }

            await _postRepository.UpdateAsync(post);

            return new PostResponse
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                AuthorId = post.AuthorId,
                CommunityId = post.CommunityId,
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt,
                Tags = post.Tags.Select(t => t.Name).ToList()
            };
        }

        public async Task DeletePostAsync(int postId)
        {
            await _postRepository.DeleteAsync(postId);
        }
    }
}
