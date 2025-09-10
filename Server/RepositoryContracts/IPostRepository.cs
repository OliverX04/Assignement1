using System;
using Entities;

namespace RepositoryContracts;

public interface IPostRepository
{
    Task<Post> AddAsync(Post post);
    Task UpdateAsync(Post post);
    Task Delete(int id);
    Task<Post> GetSingleAsync(int id);
    IQueryable<Post> GetManyAsync();
}
