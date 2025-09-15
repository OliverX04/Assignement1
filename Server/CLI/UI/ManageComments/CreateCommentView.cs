using System;
using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CreateCommentView
{
    private readonly ICommentRepository commentRepo;

    public CreateCommentView(ICommentRepository commentRepo)
    {
        this.commentRepo = commentRepo;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("write User Id for this comment: ");
        int? UserIdInput = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("write Post Id to comment on: ");
        int? PostIdInput = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("write body here");
        string? BodyInput = Console.ReadLine();
        var comment = new Comment
        {
            PostId = PostIdInput,
            Body = BodyInput,
            UserId = UserIdInput
        };
        Comment newcomment = await commentRepo.AddAsync(comment);
        Console.WriteLine("Created comment");

    }
}
