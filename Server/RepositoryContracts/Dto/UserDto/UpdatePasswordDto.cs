using System;

namespace RepositoryContracts.Dto.UserDto;

public class UpdatePasswordDto
{
    public required string OldPassword { get; set; }
    public required string NewPassword { get; set; }
}
