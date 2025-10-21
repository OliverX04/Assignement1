using System;

namespace RepositoryContracts.Dto.UserDto;

public class UpdateUserDto
{
    public string? Username { get; set; } = "";
    public string? Password { get; set; } = "";
}
