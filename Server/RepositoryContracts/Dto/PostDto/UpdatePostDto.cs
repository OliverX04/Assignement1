using System;

namespace RepositoryContracts.Dto.PostDto;

public class UpdatePostDto
{
    public string Title { get; set; } = string.Empty;
     public string Body { get; set; } = string.Empty;
}
