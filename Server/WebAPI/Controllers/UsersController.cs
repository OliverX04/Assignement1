using Entities;
using FileRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using RepositoryContracts.Dto.UserDto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepo;

        public UsersController(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }
        [HttpGet]
        public ActionResult<IQueryable<User>> GetAllUsers() => Ok(userRepo.GetManyAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
        var user = await userRepo.GetSingleAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] CreateUserDto request)
        {
            await VerifyUserNameIsAvailableAsync(request.Username);
            User user = new()
            {
                Username = request.Username,
                Password = request.Password
            };
            User created = await userRepo.AddAsync(user);
            User dto = new()
            {
                Id = created.Id,
                Username = created.Username
            };
            return Created($"/users/{dto.Id}", created);
        }
            [HttpPut("{id}")]
    public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] UpdateUserDto request)
    {
        try
        {
            VerifyUserNameIsAvailableAsync(request.Username!);
            var existingUser = await userRepo.GetSingleAsync(id);
            if (existingUser is null)
            {
                return NotFound();
            }
            User user = new()
            {
                Id = existingUser.Id,
                Username = request.Username,
                //Password = request.Password,
            };
            if (id != user.Id)
            {
                return BadRequest("ID in URL does not match ID in body");
            }
            await userRepo.UpdateAsync(user);
            UserDto dto = new()
            { Id = user.Id, Username = user.Username };
            return Ok(dto);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500);
        }
    }

    // update : only username (use patch)
    [HttpPatch("updateusername/{id}")]
    public async Task<ActionResult<User>> UpdateUserName(int id, [FromBody] UpdateUserNameDto request)
    {
        try
        {
            VerifyUserNameIsAvailableAsync(request.Username!);
            var existingUser = await userRepo.GetSingleAsync(id);
            if (existingUser is null)
            {
                return NotFound();
            }
            User user = new()
            {
                Id = existingUser.Id,
                Username = request.Username,
                Password = existingUser.Password,
            };
            if (id != user.Id)
            {
                return BadRequest("ID in URL does not match ID in body");
            }
            await userRepo.UpdateAsync(user);
            UserDto dto = new()
            { Id = user.Id, Username = user.Username };
            return Ok(dto);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500);
        }
    }

    [HttpPatch("updatepass/{id}")]
    public async Task<ActionResult<User>> UpdateUserPassword(int id, [FromBody] UpdatePasswordDto request)
    {
        try
        {
            // TODO: verify old password is correct (not implement yet)
            var existingUser = await userRepo.GetSingleAsync(id);
            if (existingUser is null)
            {
                return NotFound();
            }
            User user = new()
            {
                Id = existingUser.Id,
                Username = existingUser.Username,
                //Password = request.NewPassword,
            };
            if (id != user.Id)
            {
                return BadRequest("ID in URL does not match ID in body");
            }
            await userRepo.UpdateAsync(user);
            UserDto dto = new()
            { Id = user.Id, Username = user.Username };
            return Ok(dto);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500);
        }
    }

    //Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult<User>> DeleteUser(int id)
    {
        try
        {
           await userRepo.Delete(id);
           return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500);
        }
    }


        private async Task VerifyUserNameIsAvailableAsync(string userName)
        {
            var users = userRepo.GetManyAsync();
        if (users.Any(u => u.Username!.Equals(userName) && !string.IsNullOrWhiteSpace(u.Username)))
        {
            throw new Exception($"Username: {userName} is already exist");
        }
        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new Exception("Username cannot be empty");
        }
        }
    }
 
}
