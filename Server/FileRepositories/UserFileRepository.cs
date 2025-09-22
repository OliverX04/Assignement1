using System;
using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "comments.json";

    public UserFileRepository()

    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    public async Task<User> AddAsync(User user)

    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List < User > users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        int maxId = (int)(users.Count > 0 ? users.Max(c => c.Id) : 1);
        user.Id = maxId + 1;
        users.Add(user);
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
        return user;
    }

    public  async Task Delete(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List < User > users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        User? userToRemove = users.SingleOrDefault(p => p.Id == id);
        if (userToRemove is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }
        users.Remove(userToRemove);
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }

    public IQueryable<User> GetManyAsync()
    {
       string usersAsJson =  File.ReadAllTextAsync(filePath).Result;
        List < User > users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        return users.AsQueryable();
    } 

    public async Task<User> GetSingleAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List < User > users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        return users.SingleOrDefault(p => p.Id == id);
    }

    public async Task UpdateAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List < User > users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        int index = users.FindIndex(c => c.Id == user.Id);
        if (index == -1)
        {
            throw new InvalidOperationException($"User with Id {user.Id} not found");
        }
        users[index] = user;
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    } 
}
