using System;
using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI.ManageUser;

public class ManageUserView
{
    private IUserRepository userInterface;
    private CreateUserView createUserView;
    private ListUsersView listUserView;

    public ManageUserView(IUserRepository userInterface)
    {
        this.userInterface = userInterface;
        this.createUserView =  new CreateUserView(userInterface);
        this.listUserView = new ListUsersView(userInterface);
            }


    public async Task ShowMenuAsync(IUserRepository userRepository)
    {
        while (true)
        {
            Console.WriteLine("\n=== Manage User ===");
            Console.WriteLine("1. Create User");
            Console.WriteLine("2. List Users");
            Console.WriteLine("0. Back");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await createUserView.ShowAsync();
                    break;
                case "2":
                    await listUserView.ShowAsync();
                    break;
                case "0":
                    return;
            }
        }
    }
}
