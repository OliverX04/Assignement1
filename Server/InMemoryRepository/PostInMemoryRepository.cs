using System;
using Entities;
using RepositoryContracts;

namespace InMemoryRepository;

public class PostInMemoryRepository : IPostRepository
{

    public List<Post>? posts { get; set; } = new List<Post>();
    public Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Any()
        ? posts.Max(p => p.Id) + 1
        : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task Delete(int id)
    {
        Post? postToRemove = posts.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found");
        }
        posts.Remove(postToRemove);
        return Task.CompletedTask;
    }

    public IQueryable<Post> GetManyAsync()
    {
        return posts.AsQueryable();
    }

    public Task<Post> GetSingleAsync(int id)
    {
        Post? givenPost = posts.SingleOrDefault(p => p.Id == id);
        if (givenPost is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found");
        }
        return Task.FromResult(givenPost);
    }

    public Task UpdateAsync(Post post)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException($"Post with ID '{post.Id} ain't found");
        }
        posts.Remove(existingPost);
        posts.Add(post);
        return Task.CompletedTask;
    }
}
