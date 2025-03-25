using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Add static html page
    [HttpGet]
    public async Task<ActionResult> RegisterUser()
    {
        return Redirect("/register.html");
    }

    [HttpGet("allusers")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<User>> AllUsers()
    {
        var allUsers = await _context.Users.ToListAsync();
        return Ok(allUsers);
    }

    [HttpPost("adduser")]
    public async Task<ActionResult<User>> AddUser([FromBody] User user)
    {
        var existedUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        if (existedUser != null)
            return BadRequest(new { Message = "User already exists" });

        // Hash the password before saving it to the database
        var passwordHasher = new PasswordHasher<User>();
        user.Password = passwordHasher.HashPassword(user, user.Password);

        //add
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return Ok(new { Message = "User Added" });
    }

    [HttpPut("updateuser/{id}")]
    [Authorize]
    public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] User updatedUser)
    {
        var existingUser = await _context.Users.FindAsync(id);
        if (existingUser == null)
        {
            return NotFound(new { Message = $"User with id {id} not found" });
        }
        var userIdFromToken = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value ?? "0");
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

        //admin can update all users but also user can update their own account
        if (userIdFromToken != id && userRole != "Admin")
        {
            return StatusCode(403, new { Message = "You cannot update other people's account" });
        }
        // Only update fields if new values are provided (keep old values otherwise)
        existingUser.UserName = updatedUser.UserName ?? existingUser.UserName;
        existingUser.Email = updatedUser.Email ?? existingUser.Email;
        existingUser.Password = updatedUser.Password ?? existingUser.Password;

        // Check if a new password is provided, if so, hash it before updating
        if (!string.IsNullOrEmpty(updatedUser.Password))
        {
            var passwordHasher = new PasswordHasher<User>();
            existingUser.Password = passwordHasher.HashPassword(existingUser, updatedUser.Password);
        }

        await _context.SaveChangesAsync();
        return Ok(new { Message = $"User with id {id} updated successfully" });
    }

    [HttpDelete("deleteuser/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<User>> DeleteUser(int id)
    {
        var existingUser = await _context.Users.FindAsync(id);
        if (existingUser == null)
        {
            return NotFound(new { Message = $"User with id {id} not found" });
        }
        var userIdFromToken = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value ?? "0");
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

        //admin can update all users but also user can update their own account
        if (userIdFromToken != id && userRole != "Admin")
        {
            return StatusCode(403, new { Message = "You cannot delete other people's account" });
        }
        _context.Users.Remove(existingUser);
        await _context.SaveChangesAsync();
        return Ok(new { Message = "User deleted" });
    }
}
