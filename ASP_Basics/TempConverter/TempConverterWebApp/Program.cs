using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
var builder = WebApplication.CreateBuilder(args);

// Add services for OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");

app.MapPost("/tempconverter", ([FromBody] double temp) => {
    if (double.IsNaN(temp) || temp < -459.67) // Absolute zero in Fahrenheit
    {
        return Results.BadRequest(new { Message = "Invalid input. Please provide a valid temperature greater than absolute zero (-459.67°F)." });
    }
    double fToCConstant = 5.0 / 9.0;
    double convertedToC = Math.Round((temp - 32) * fToCConstant, 2);
    return Results.Ok(convertedToC);
}).DisableAntiforgery();


app.Run();
