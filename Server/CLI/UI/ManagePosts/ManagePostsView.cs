using System;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private IPostRepository postInterface;
    private CreatePostView createPostView;
    private ListPostsView listPostView;
    private SinglePostView singlePostView;

    public ManagePostsView(IPostRepository postInterface)
    {
        this.postInterface = postInterface;
        this.createPostView = new CreatePostView(postInterface);
        this.listPostView = new ListPostsView(postInterface);
        singlePostView = new SinglePostView(postInterface);
    }


    public async Task ShowMenuAsync(IPostRepository postRepository)
    {
        while (true)
        {
            Console.WriteLine("\n=== Manage Posts ===");
            Console.WriteLine("1. Create Post");
            Console.WriteLine("2. List Posts");
            Console.WriteLine("3. View Single Post");
            Console.WriteLine("0. Back");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await createPostView.ShowAsync();
                    break;
                case "2":
                    await listPostView.ShowAsync();
                    break;
                case "3":
                    await singlePostView.ShowAsync();
                    break;
                case "0":
                    return;
            }
        }
    }
}
