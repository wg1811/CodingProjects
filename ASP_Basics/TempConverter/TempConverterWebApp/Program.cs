using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Collections.Generic;


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

app.UseDefaultFiles();
app.UseStaticFiles();


app.MapGet("/", () => "Hello World!");

app.MapPost("/tempconverter", ([FromBody] ConvertTempRequest convert) => {
    var temp = convert.TempToConvert;
    Console.WriteLine($"From {convert.From} to {convert.To} for {temp}");
    if (temp < -1000 || temp > 1000000000) // Outside reality
    {
        return Results.BadRequest(new { Message = "Invalid input. Please provide a valid temperature greater than absolute zero (-459.67°F)." });
    }
    var convertedTemp = TempConverterModel.ConvertTemp(convert.From, convert.To, temp);
    
    return Results.Ok( new { Message = $"Converted Tempurature: {convertedTemp}"});

}).DisableAntiforgery();


app.Run();

class ConvertTempRequest
{
    public required string From {get; set;}
    public required string To {get; set;}
    public required double TempToConvert {get; set;}
}

class TempConverterModel
{
    private static double FToCConstant { get; set; }= 5.0 / 9.0;
    private static double CToFConstant { get; set; } = 9.0 / 5.0;
    private static double CToKelvin { get; set; } = 273.15;

    private static readonly Dictionary<(string, string), Func<double, double>> conversions = new Dictionary<(string, string), Func<double, double>>
    {
        { ("Celsius", "Fahrenheit"), temp => Math.Round(temp * CToFConstant + 32, 2) },
        { ("Celsius", "Kelvin"), temp => temp + CToKelvin },
        { ("Fahrenheit", "Celsius"), temp => Math.Round((temp - 32) * FToCConstant, 2) },
        { ("Fahrenheit", "Kelvin"), temp => Math.Round((temp - 32) * FToCConstant, 2) + CToKelvin },
        { ("Kelvin", "Celsius"), temp => Math.Round(temp - CToKelvin, 2) }, 
        { ("Kelvin", "Fahrenheit"), temp => Math.Round((temp-CToKelvin) * CToFConstant + 32, 2)}
    };

    public static double ConvertTemp(string from, string to, double temp)
    {
        var key = (from, to);
        if (conversions.ContainsKey(key))
        {
            return conversions[key](temp);
        } else 
            {
            throw new ArgumentException("Invalid temperature units.");
            }
    }
}