using System.IO;
using BetterWeatherSimApp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
        weatherSystem.ConsoleWeatherList();
        return Results.Ok(weatherSystem);
    }
);

app.MapGet(
    "/getgeofiles",
    (IWebHostEnvironment env) =>
    {
        try
        {
            string dataFolderPath = Path.Combine(env.WebRootPath, "data");
            Console.WriteLine($"Resolved data folder path: {dataFolderPath}");

            if (!Directory.Exists(dataFolderPath))
            {
                Console.WriteLine($"Data folder not found at {dataFolderPath}");
                return Results.NotFound("The 'data' file is not there.");
            }

            var dirInfo = new DirectoryInfo(dataFolderPath);
            var filesInfo = dirInfo.GetFiles();
            Console.WriteLine($"DirectoryInfo found {filesInfo.Length} total files");
            foreach (var file in filesInfo)
            {
                Console.WriteLine($"Found file: {file.Name} | Extension: {file.Extension}");
            }

            // Method 2: Directory.EnumerateFiles (might handle permissions differently)
            Console.WriteLine("\nTrying Directory.EnumerateFiles:");
            var allFiles = Directory.EnumerateFiles(dataFolderPath).ToList();
            Console.WriteLine($"EnumerateFiles found {allFiles.Count} total files");
            foreach (var file in allFiles)
            {
                Console.WriteLine($"Found file: {Path.GetFileName(file)}");
            }

            // Method 3: Original .geojson search
            var geojsonFiles = Directory.GetFiles(dataFolderPath, "*.geojson");
            Console.WriteLine($"\nSpecifically searching for .geojson files:");
            Console.WriteLine($"Found {geojsonFiles.Length} .geojson files");

            var files = Directory.GetFiles(dataFolderPath, "*.geojson");
            Console.WriteLine($"Found {files.Length} files");
            foreach (var file in files)
            {
                Console.WriteLine($"File found: {file}");
            }

            var relativePaths = files
                .Select(f => Path.GetRelativePath(env.WebRootPath, f))
                .ToArray();

            // Generate full URLs for the files
            // var baseUrl = $"{request.Scheme}://{request.Host}";
            // var fileUrls = files
            //      .Select(path => Path.Combine("data", Path.GetFileName(path)))
            //      .Select(relativePath => $"{baseUrl}/{relativePath}")
            //      .ToList();

            // Console.WriteLine($"File URLs: {string.Join(", ", fileUrls)}");

            // return Results.Ok(new { filePaths = fileUrls });
            return Results.Ok(relativePaths);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return Results.Problem($"Error accessing files: {ex.Message}");
        }
    }
);

app.Run();




// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

// app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");

// app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
