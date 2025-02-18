using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("students")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public StudentController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("getallstudents")]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        var students = await _context.Students.ToListAsync();
        return Ok(students);
    }

    [HttpGet("getstudent/{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
            return NotFound();
        return Ok(student);
    }

    [HttpPost("addstudent")]
    public async Task<ActionResult<Student>> AddStudent([FromBody] Student student)
    {
        var existedStudent = await _context.Students.FirstOrDefaultAsync(s =>
            s.Email == student.Email
        );
        if (existedStudent != null)
            return BadRequest(new { Message = "This student already exists" });
        //add
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
        return Ok(new { Message = "Student Added" });
    }

    [HttpPost("{studentId}/addcourse/{courseId}")]
    public async Task<ActionResult> AddCourseToStudent(int studentId, int courseId)
    {
        var student = await _context
            .Students.Include(s => s.Courses) //include course
            .FirstOrDefaultAsync(s => s.Id == studentId);

        var course = await _context.Courses.FindAsync(courseId);

        if (student == null || course == null)
            return NotFound(new { Message = "Student or course not found" });
        //prevent duplicate assignment
        if (!student.Courses.Contains(course))
        {
            student.Courses.Add(course);
            await _context.SaveChangesAsync();
        }
        ;
        return Ok(new { Message = "Course assigned to student" });
    }

    [HttpDelete("deletestudent/{id}")]
    public async Task<ActionResult<Student>> DeleteStudent(int id)
    {
        var existedStudent = await _context.Students.FindAsync(id);
        if (existedStudent == null)
            return NotFound(new { Message = "Student does not exist." });

        _context.Students.Remove(existedStudent);
        await _context.SaveChangesAsync();
        return Ok(new { Message = "Student Deleted ", Student = existedStudent });
    }

    [HttpPut("updatestudent/{id}")]
    public async Task<ActionResult<Student>> UpdateStudent(
        int id,
        [FromBody] Student updatedStudent
    ) //  Include `id` from URL
    {
        if (id != updatedStudent.Id)
            return BadRequest(new { Message = "ID mismatch between URL and body" });

        var existedStudent = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        if (existedStudent == null)
            return NotFound(new { Message = "Student does not exist" });

        existedStudent.Name = updatedStudent.Name ?? existedStudent.Name;
        existedStudent.Email = updatedStudent.Email ?? existedStudent.Email;
        existedStudent.Age = updatedStudent.Age ?? existedStudent.Age;

        await _context.SaveChangesAsync();
        return Ok(new { Message = "Student updated", Student = existedStudent });
    }
}
