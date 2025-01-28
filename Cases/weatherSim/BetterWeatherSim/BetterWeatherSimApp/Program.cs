using System.IO;
using BetterWeatherSimApp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

// Add services for OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
    );
});

var app = builder.Build();
app.UseCors("AllowAllOrigins");

// Enable Swagger middleware
app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();

app.MapGet("/", () => "Hello World");

app.MapGet(
    "/getweather",
    () =>
    {
        Weather weather = new Weather();
        weather.GetWeather(0); // Need to get last id and feed it here.
        Console.WriteLine(weather.ToString());
        return Results.Ok();
    }
);

app.MapGet(
    "/getweathersystem",
    () =>
    {
        WeatherSystem weatherSystem = new WeatherSystem();
        weatherSystem.CreateWeatherSystem();
        //weatherSystem.ConsoleWeatherList();
        return Results.Ok(weatherSystem);
    }
);

app.Run();
