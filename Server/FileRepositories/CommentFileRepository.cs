using System;
using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

 public class CommentFileRepository : ICommentRepository

{
    private readonly string filePath = "comments.json";

    public CommentFileRepository()

    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    public async Task<Comment> AddAsync(Comment comment)

    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List < Comment > comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        int maxId = (int)(comments.Count > 0 ? comments.Max(c => c.Id) : 1);
        comment.Id = maxId + 1;
        comments.Add(comment);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
        return comment;
    }

    public  async Task Delete(int id)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List < Comment > comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        Comment? commentToRemove = comments.SingleOrDefault(p => p.Id == id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found");
        }
        comments.Remove(commentToRemove);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }

    public IQueryable<Comment> GetManyAsync()
    {
        string commentsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        return comments.AsQueryable();
    } 

    public async Task<Comment> GetSingleAsync(int id)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List < Comment > comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        return comments.SingleOrDefault(p => p.Id == id);
    }

    public async Task UpdateAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        int index = comments.FindIndex(c => c.Id == comment.Id);
        if (index == -1)
        {
            throw new InvalidOperationException($"Comment with Id {comment.Id} not found");
        }
        comments[index] = comment;
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    } 
}
