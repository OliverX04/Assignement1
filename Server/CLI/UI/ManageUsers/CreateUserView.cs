using System;
using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository userRepo;

    public CreateUserView(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }

    public async Task ShowAsync()
    {
        System.Console.WriteLine("write username here");
        string? UsernameInput = Console.ReadLine();
        System.Console.WriteLine("write your Password");
        string? PasswordnameInput = Console.ReadLine();
        if (!int.TryParse(PasswordnameInput, out int password))
        {
            Console.WriteLine("Invalid password. Please enter digits only.");
            return;
        }
        var user = new User
        {
            Username = UsernameInput,
            Password = password
        };
        User newuser = await userRepo.AddAsync(user);
        System.Console.WriteLine("Created User");
    }

}
