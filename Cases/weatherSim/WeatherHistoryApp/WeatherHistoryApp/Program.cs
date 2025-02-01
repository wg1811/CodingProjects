using System.IO;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherHistoryApp;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();

var apiMapKey = builder.Configuration["GoogleMaps:ApiKey"];

// Add services for OpenAPI :  Don't need this yet.
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

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
// app.UseSwagger();
// app.UseSwaggerUI();
app.UseStaticFiles();

app.MapGet("/", () => "Hello World");

app.MapGet(
    "/api/getweather",
    async (double lat, double lon, string start_date, string end_date) =>
    {
        // Console.WriteLine(
        //     $"The lat is {lat}, the long is {lon}.\nThe start date is {start_date}, the end date is {end_date}."
        // );
        WeatherService weatherService = new();
        var weather = await weatherService.GetHistoricalWeather(lat, lon, start_date, end_date);
        return weather != null
            ? Results.Json(weather)
            : Results.Problem("Weather data not fetched.");
    }
);

app.MapGet(
    "/api/testweather",
    async (double lat, double lon, string start_date, string end_date) =>
    {
        WeatherService weatherService = new();
        var weather = await weatherService.TestWeather(lat, lon, start_date, end_date);
        return weather != null
            ? Results.Json(weather)
            : Results.Problem("Weather data not fetched.");
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
