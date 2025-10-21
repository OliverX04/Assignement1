using System;

namespace RepositoryContracts.Dto.UserDto;

public class CreateUserDto
{
    public required string Username { get; set; }
    public required int Password { get; set; }
}
