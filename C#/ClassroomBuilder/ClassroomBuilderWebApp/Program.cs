using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Classroom? classroom = null;

//template to add html index file later

app.MapGet("/", () => "Hello World!");

//create classroom if it doesn't exist

app.MapPost("/createclassroom", ([FromBody] Classroom newClassroom) =>
{
    if (classroom != null)
    {
        return Results.Ok(new { Message = $"Classroom with name {classroom.Name} already exists."});
    }

    classroom = newClassroom;

    return Results.Ok(new { Message = $"Classroom '{newClassroom.Name}' is created."});
});

// app.MapPost("/updateclassroom", () => 
// {
//     if (classroom != null)
//     {
//     return Results.Ok(new { Message = $"Classroom with name {classroom.Name} already exists."});
//     }
//     classroom.Name = newName;
//     return classroom.Name;
// });


app.MapGet("/classroom", () => 
{
    if (classroom == null)
    {
        return Results.Ok(new { Message = $"Classroom not created."});
    }
    return Results.Ok(classroom.students);
});

app.MapPost("/addstudent", ([FromBody] Student student) =>
{
    if (classroom == null)
    {
        return Results.BadRequest(new { Message = "You must create a class first."});
    }
    var existingStudent = classroom.students.FirstOrDefault(s => s.Id == student.Id);
    if (existingStudent == null)
    {
        return Results.BadRequest(new { Message = $"Studend with ID: {student.Id} already exists."});
    }

    //check for empty
    if (string.IsNullOrWhiteSpace(student.Name))
    {
        return Results.BadRequest(new { Message = "Student name is required."});
    }

    //get correct ID number
    int newId = classroom.students.Any() ? classroom.students.Max(p => p.Id) + 1 : 1;
    //create a student or update
    student.Id = newId;

    //add to class
    classroom.AddStudent(student);
    return Results.Ok(new { Message = $"Student with ID {student.Id} added."});
});

//update by ID
app.MapPut("/updatestudent/{id:int}", ([FromRoute] int id, [FromBody] Student updatedStudent) =>
{
    if(classroom == null)
    {
        return Results.BadRequest(new { Message = "You must create a classroom first."});
    }
    var existingStudent = classroom.students.FirstOrDefault(s => s.Id == id);
    if (existingStudent == null)
    {
        return Results.NotFound(new { Message = $"Student with Id {id} not found."});
    }
    //check for empty
     if (!string.IsNullOrWhiteSpace(updatedStudent.Name))
    {
        existingStudent.Name = updatedStudent.Name;
    }

    existingStudent.BirthDay = updatedStudent.BirthDay;

    return Results.Ok(new { Message = $"Student named {updatedStudent.Name} and id {id} is updated."});
});

app.Run();
