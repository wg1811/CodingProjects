using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("blogs")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BlogController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("allblogs")]
    public async Task<ActionResult<Blog>> AllBlogs()
    {
        //we had to add Include to show author
        var allBlogs = await _context
            .Blogs.Include(b => b.Author)
            .ThenInclude(a => a.Blogs)
            .ToListAsync();
        return Ok(allBlogs);
    }

    [HttpPost("addblog")]
    [Authorize]
    public async Task<ActionResult<Blog>> AddBlog([FromBody] Blog blog)
    {
        var userRoleFromToken = User.FindFirst(ClaimTypes.Role)?.Value;
        //  Allow only Admins (0) and Writers (1) to create blogs
        if (userRoleFromToken != "Admin" && userRoleFromToken != "Writer")
        {
            return StatusCode(403, new { Message = "Only Admins or Writers can create blogs" });
        }

        await _context.Blogs.AddAsync(blog);
        await _context.SaveChangesAsync();
        return Ok(new { Message = "blog added" });
    }

    // update
    [HttpPut("updateblog/{id}")]
    [Authorize(Roles = "Writer")]
    public async Task<ActionResult> UpdateBlog(int id, [FromBody] Blog updatedBlog)
    {
        var existingBlog = await _context.Blogs.FindAsync(id);
        if (existingBlog == null)
        {
            return NotFound(new { Message = $"Blog with id {id} not found" });
        }
        var userIdFromToken = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        // Check if the logged-in user is the author of the blog
        if (existingBlog.AuthorId != userIdFromToken)
        {
            return StatusCode(403, new { Message = "You can only update your own blog." });
        }

        // ✅ Update only provided fields (keep others unchanged)
        existingBlog.Title = updatedBlog.Title ?? existingBlog.Title;
        existingBlog.Content = updatedBlog.Content ?? existingBlog.Content;

        await _context.SaveChangesAsync();
        return Ok(new { Message = $"Blog with id {id} updated successfully" });
    }

    //delete
    [HttpDelete("deleteblog/{id}")]
    [Authorize(Roles = "Admin,Writer")]
    public async Task<ActionResult> DeleteBlog(int id)
    {
        var existingBlog = await _context.Blogs.FindAsync(id);
        if (existingBlog == null)
        {
            return NotFound(new { Message = $"Blog with id {id} not found" });
        }

        // Get the logged-in user ID and role from the token
        var userIdFromToken = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

        // Admins can delete any blog, Writers can only delete their own
        if (userRole != "Admin" && existingBlog.AuthorId != userIdFromToken)
        {
            return StatusCode(403, new { Message = "You can only delete your own blog." });
        }

        _context.Blogs.Remove(existingBlog);
        await _context.SaveChangesAsync();

        return Ok(new { Message = $"Blog with id {id} deleted successfully" });
    }
}
