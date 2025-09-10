using System;
using System.Reflection.Metadata;
using Entities;

namespace RepositoryContracts;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task Delete(int id);
    Task<User> GetSingleAsync(int id);
    IQueryable<User> GetManyAsync();
}
