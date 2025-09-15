using System;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ListUsersView
{
    public IUserRepository userInterface;

    public ListUsersView(IUserRepository userInterface)
    {
        this.userInterface = userInterface;
    }

    internal async Task ShowAsync()
     {
        var users = userInterface.GetManyAsync();
        Console.WriteLine("\n=== All Users ===");
        foreach (var user in users)
        {
            Console.WriteLine($"ID: {user.Id}, Username: {user.Username}");
        }
    }
}
