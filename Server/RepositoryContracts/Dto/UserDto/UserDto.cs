using System;

namespace RepositoryContracts.Dto.UserDto;

public class UserDto
{
    public required int? Id { get; set; }
    public string? Username { get; set; }
}
