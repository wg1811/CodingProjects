using System.IO;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherMapSimApp;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();

var apiMapKey = builder.Configuration["GoogleMaps:ApiKey"];

// Add services for OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding Dependency Injection Singleton
builder.Services.AddSingleton<HttpClient>();

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
    "/api/getweather",
    () =>
    {
        Weather weather = new Weather();
        weather.GetWeather(0); // Need to get last id and feed it here.
        Console.WriteLine(weather.ToString());
        return Results.Ok();
    }
);

app.MapGet(
    "/api/getweathersystem",
    () =>
    {
        WeatherSystem weatherSystem = new WeatherSystem();
        weatherSystem.CreateWeatherSystem();
        //weatherSystem.ConsoleWeatherList();
        return Results.Ok(weatherSystem);
    }
);

app.MapGet(
    "/api/getMapsApiKey",
    (IConfiguration config) =>
    {
        var apiKey = config["GoogleMaps:ApiKey"];
        return Results.Json(new { apiKey });
    }
);

// Define the minimal API route
app.MapGet(
    "/api/getCoordinates",
    async (string address, HttpClient httpClient) =>
    {
        var apiKey = apiMapKey; // Your Google Maps API Key
        var url =
            $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={apiKey}";
        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return Results.BadRequest("Failed to get coordinates.");
        }

        var json = await response.Content.ReadAsStringAsync();
        var jsonDocument = JsonDocument.Parse(json);
        var results = jsonDocument.RootElement.GetProperty("results");

        if (results.GetArrayLength() == 0)
        {
            return Results.NotFound("No results found for the given address.");
        }

        var location = results[0].GetProperty("geometry").GetProperty("location");
        var lat = location.GetProperty("lat").GetDouble();
        var lng = location.GetProperty("lng").GetDouble();

        return Results.Ok(new { lat, lng });
    }
);

app.Run();
