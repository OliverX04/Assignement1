using System;
using System.Reflection.Metadata;
using Entities;
using RepositoryContracts;

namespace InMemoryRepository;

public class UserInMemoryRepository : IUserRepository
{
    public List<User>? users { get; set; } = new List<User>();
    public Task<User> AddAsync(User user)
    {
        user.Id = users.Any()
            ? users.Max(p => p.Id) + 1
            : 1;
        users.Add(user);
        return Task.FromResult(user);
    }
    public Task UpdateAsync(User user)
    {
        User? existingUser = users.SingleOrDefault(p => p.Id == user.Id);
        if (existingUser is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{user.Id}' not found");
        }
        users.Remove(existingUser);

        users.Add(user);

        return Task.CompletedTask;
    }
    public Task Delete(int id)
    {
        User? userToRemove = users.SingleOrDefault(p => p.Id == id);
        if (userToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }
        users.Remove(userToRemove);

        return Task.CompletedTask;
    }
    public Task<User> GetSingleAsync(int id)
    {
           User? givenUser = users.SingleOrDefault(p => p.Id == id);
        if (givenUser is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }
        return Task.FromResult(givenUser);
    }
    public IQueryable<User> GetManyAsync()
    {
        return users.AsQueryable();
    }
    
}
