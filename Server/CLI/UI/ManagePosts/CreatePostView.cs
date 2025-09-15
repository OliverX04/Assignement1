using System;
using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepo;

    public CreatePostView(IPostRepository postRepo)
    {
        this.postRepo = postRepo;
    }

    public async Task ShowAsync()
    {
        System.Console.WriteLine("write Post Title here");
        string? TitleInput = Console.ReadLine();
        System.Console.WriteLine("write body here");
        string? BodyInput = Console.ReadLine();
        System.Console.WriteLine("write UserId for this post: ");

        string? UserIdInput = Console.ReadLine();
         if (!int.TryParse(UserIdInput, out int Id))
        {
            Console.WriteLine("Invalid password. Please enter digits only.");
            return;
        }
        var post = new Post
        {
            Title = TitleInput,
            Body = BodyInput,
            UserId = Id
        };
        Post newpost = await postRepo.AddAsync(post);
        System.Console.WriteLine("Created Post");

    }
}
