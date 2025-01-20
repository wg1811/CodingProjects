using Microsoft.AspNetCore.Mvc;
using System.Linq;


var builder = WebApplication.CreateBuilder(args);

// Add services for OpenAPI
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors();

// Enable Swagger middleware
// app.UseSwagger();
// app.UseSwaggerUI();

// app.UseStaticFiles();

// app.MapPost("/calculate", async (HttpContext context) =>
// {
//     var form = context.Request.Form;

//     //checking for valid input
//     if (!form.ContainsKey("operation") || !form.ContainsKey("num1") || !form.ContainsKey("num2"))
//     {
//         context.Response.StatusCode = 400;
//         await context.Response.WriteAsync("Missing input(s).");
//         return;
//     }

//     //calculating the numbers
//     string operation = form["operation" ];
//     if (!double.TryParse("num1", out double num1) || !double.TryParse("num2", out double num2))
//     {
//         context.Response.StatusCode = 400;
//         await context.Response.WriteAsync("Invalid number(s) provided.");
//         return;
//     }

//     double result = operation switch
//     {
//         "add" => num1 + num2,
//         "subtract" => num1 - num2,
//         "multiply" => num1 * num2,
//         "divide" when num2 != 0 => num1 / num2,
//         _ => double.NaN
//     };

//     if(double.IsNaN(result))
//     {
//         context.Response.StatusCode = 400;
//         await context.Response.WriteAsync("Invalid division by zero. Silly.");
//         return;
//     }

// await context.Response.WriteAsync($"Result: {result}");


app.MapPost("/sumallnumbers", ([FromBody] NumRange range) =>
{
    int sumNumbers = 0; 
    for(int i = range.Min; i <= range.Max; i++)
    {
        sumNumbers += i;
    }
    return Results.Ok(new { Message = $"The sum of all numbers from {range.Min} to {range.Max} (inclusive) is {sumNumbers}."});

});

app.MapPost("/numisodd", ([FromBody] NumInput input) =>
{
    if(input.Num % 2 == 0)
    {
        return Results.Ok(new { Message = false });
    }
    return Results.Ok(new { Message = true });
});

app.Run();

public class NumRange
{
    public int Min { get; set; }
    public int Max { get; set; }
}

public class NumInput
{
    public int Num { get; set; }
}
