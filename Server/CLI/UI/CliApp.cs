using System;
using CLI.UI.ManageUser;
using CLI.UI.ManagePosts;
using CLI.UI.ManageComments;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    public IUserRepository userRepository { get; set; }
    public ICommentRepository commentRepository { get; set; }
    public IPostRepository postRepository { get; set; }
    public ManageUserView manageUserView;
    public ManagePostsView managePostsView;

    public ManageCommentsView manageCommentsView;
    public CliApp(IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository)
    {
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
        manageUserView = new ManageUserView(userRepository);
        managePostsView = new ManagePostsView(postRepository);
        manageCommentsView = new ManageCommentsView(commentRepository);
    }

    internal async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("\n=== Main Menu ===");
            Console.WriteLine("1. Manage User");
            Console.WriteLine("2. Manage Post");
            Console.WriteLine("3. Manage Comment");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await manageUserView.ShowMenuAsync(userRepository);
                    break;
                case "2":
                    await managePostsView.ShowMenuAsync(postRepository);
                    break;
                case "3":
                    await manageCommentsView.ShowMenuAsync(commentRepository);
                    break;
            }
        }
    }

    
}
