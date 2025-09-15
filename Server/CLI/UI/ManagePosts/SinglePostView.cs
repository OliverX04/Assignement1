using System;
using Entities;
using RepositoryContracts;
namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    public IPostRepository postInterface;
    public ICommentRepository commentInterface;

    public SinglePostView(IPostRepository postInterface)
    {
        this.postInterface = postInterface;
        this.commentInterface = commentInterface;
    }
    public async Task ShowAsync()
    {
        Console.WriteLine("Enter Post Id: ");
        int PostIdInput = Convert.ToInt32(Console.ReadLine());
        Post? post = await postInterface.GetSingleAsync(PostIdInput);
        if (post != null)
        {
            System.Console.WriteLine($"ID: {post.Id}, Title: {post.Title}, Body: {post.Body}, UserID: {post.UserId}");
            IQueryable<Comment> comments = commentInterface.GetManyAsync().Where(c => c.PostId == PostIdInput);
            Console.WriteLine($"__Comment of post : {post.Title}");
              foreach (var comment in comments)
              {
                  Console.WriteLine($"\nUser ID: {comment.UserId}" +
                      $"\n => {comment.Body}");
              }

        }
        else
        {
            Console.WriteLine("Post not found");
        }
        
    }
}