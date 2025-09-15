using System;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    public IPostRepository postInterface;

    public ListPostsView(IPostRepository postInterface)
    {
        this.postInterface = postInterface;
    }

    internal async Task ShowAsync()
     {
        var posts = postInterface.GetManyAsync();
        Console.WriteLine("\n=== All Post ===");
        foreach (var post in posts)
        {
            Console.WriteLine($"Title: {post.Title}, UserId: {post.UserId}");
        }
    }
}
