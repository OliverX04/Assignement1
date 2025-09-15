using System;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ManageCommentsView
{

    private ICommentRepository commentInterface;
    private CreateCommentView createCommentView;
    

    public ManageCommentsView(ICommentRepository commentInterface)
    {
        this.commentInterface = commentInterface;
        this.createCommentView =  new CreateCommentView(commentInterface);
            }


    public async Task ShowMenuAsync(ICommentRepository commentRepository)
    {
        while (true)
        {
            Console.WriteLine("\n=== Manage comment ===");
            Console.WriteLine("1. Create comment");
            Console.WriteLine("0. Back");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await createCommentView.ShowAsync();
                    break;
                case "0":
                    return;
            }
        }
    }
}
