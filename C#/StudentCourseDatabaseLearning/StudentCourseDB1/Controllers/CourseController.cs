using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("courses")]
[ApiController]
//CONTROLLER CLASS
public class CourseController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CourseController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("getallcourses")]
    public async Task<ActionResult<List<Course>>> GetCourses()
    {
        var courses = await _context.Courses.ToListAsync();
        if (courses.Count() == 0)
            return NotFound(new { Message = "No Course Found " });
        return Ok(courses);
    }

    [HttpPost("addcourse")]
    public async Task<ActionResult<Course>> AddCourse([FromBody] Course course)
    {
        var existedCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Name == course.Name);
        if (existedCourse != null)
            return BadRequest(new { Message = "This course already exists" });

        //add
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
        return Ok(new { Message = "Course added" });
    }

    [HttpDelete("deletecourse/{id}")]
    public async Task<ActionResult<Course>> DeleteCourse(int id)
    {
        var existedCourse = await _context.Courses.FindAsync(id);
        if (existedCourse == null)
            return NotFound(new { Message = "course not exists" });

        _context.Courses.Remove(existedCourse);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Course deleted", Course = existedCourse });
    }

    [HttpPut("updatecourse/{id}")]
    public async Task<ActionResult<Course>> UpdateCourse(int id, [FromBody] Course updatedCourse)
    {
        if (id != updatedCourse.Id)
            return BadRequest(new { Message = "wrong Id" });

        var existedCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
        if (existedCourse == null)
            return NotFound(new { Message = "Course does not exist" });

        existedCourse.Name = updatedCourse.Name ?? existedCourse.Name;
        await _context.SaveChangesAsync();
        return Ok(new { Message = "Course Updated", Course = existedCourse });
    }
}
