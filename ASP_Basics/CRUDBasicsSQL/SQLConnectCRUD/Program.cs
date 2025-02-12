using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "allowAll",
        policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        }
    );
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();
app.UseCors("allowAll");

// Endpoints
app.MapGet("/", () => "Hello World!");



app.MapGet(
    "/getstaff",
    async (ApplicationDbContext db) =>
    {
        var allStaff = await db.Staffs.ToListAsync();
        return allStaff.Any()
            ? Results.Ok(allStaff)
            : Results.Ok(new { message = "No staff found." });
    }
);



// // Create staff member from API trying to call a method in the class
// app.MapGet(
//     "/createstaffapi",
//     async (int numStaff) =>
//     {
//         Staff staff = new();
//         var staffMembers = await staff.CreateStaff(numStaff);
//         return staffMembers != null
//             ? Results.Json(staffMembers)
//             : Results.Problem("Staff member data not fetched.");
//     };
//     staff.SendToDb(staffMembers);
// );

// Add staff member
app.MapPost(
    "/addStaff",
    async (ApplicationDbContext db, [FromBody] Staff staff) =>
    {
        var existingStaff = await db.Staffs.FindAsync(staff.Id);
        if (existingStaff != null)
            return Results.Ok(new { Message = "This person already exists" });

        db.Staffs.Add(staff);
        await db.SaveChangesAsync();
        return Results.Created(
            $"/staff/{staff.Id}",
            new { Message = $"New staff created: {staff.FirstName}." }
        );
    }
);

// Update staff member info
app.MapPut(
    "/updatestaff",
    async (ApplicationDbContext db, int id, [FromBody] Staff updatedStaff) =>
    {
        var existingStaff = await db.Staffs.FindAsync(id);
        if (existingStaff == null)
            return Results.NotFound(new { message = "Id not found." });

        existingStaff.FirstName = updatedStaff.FirstName;
        existingStaff.LastName = updatedStaff.LastName;
        existingStaff.Email = updatedStaff.Email;
        existingStaff.Department = updatedStaff.Department;
        existingStaff.Salary = updatedStaff.Salary;
        existingStaff.City = updatedStaff.City;

        await db.SaveChangesAsync();
        return Results.Ok(existingStaff);
    }
);

// Delete staff member record
app.MapDelete(
    "/deletestaff",
    async (ApplicationDbContext db, [FromBody] int deleteId) =>
    {
        var existingStaff = await db.Staffs.FindAsync(deleteId);
        if (existingStaff == null)
            return Results.NotFound(new { Message = "Staff not found" });

        db.Staffs.Remove(existingStaff);
        await db.SaveChangesAsync();
        return Results.Ok(new { Message = "Staff deleted successfully" });
    }
);

app.MapGet(
    "/getstaffmember",
    async (ApplicationDbContext db, [FromBody] int staffId) =>
    {
        var existingStaff = await db.Staffs.FindAsync(staffId);
        if (existingStaff == null)
            return Results.NotFound(new { Message = "Staff id not found" });
        return Results.Ok(existingStaff);
    }
);

app.MapGet(
    "/staffmemberemail",
    async (ApplicationDbContext db, [FromBody] string staffMemberEmail) =>
    {
        var existingStaff = await db.Staffs.FirstOrDefaultAsync(s => s.Email == staffMemberEmail);
        if (existingStaff == null)
            return Results.NotFound(new { Message = "Staff email not found" });
        return Results.Ok(existingStaff);
    }
);

app.Run();
