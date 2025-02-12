using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddHttpClient();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet(
    "/populateproducts",
    async (HttpClient httpClient, ApplicationDbContext dbContext) =>
    {
        Console.WriteLine("This worked so far");
        var url = "https://fakestoreapi.com/products";
        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return Results.BadRequest("Failed to get products.");
        }

        var json = await response.Content.ReadAsStringAsync();
        // Console.WriteLine(json);
        var products = JsonSerializer.Deserialize<List<Product>>(
            json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true } //what does this mean "cache and reuse instances?"
        );
        dbContext.Products.AddRange(products!);
        await dbContext.SaveChangesAsync();

        return Results.Ok("Products populated");
    }
);

// app.MapGet(
//     "/getstaff",
//     async (ApplicationDbContext db) =>
//     {
//         var allStaff = await db.Staffs.ToListAsync();
//         return allStaff.Any()
//             ? Results.Ok(allStaff)
//             : Results.Ok(new { message = "No staff found." });
//     }
// );)

app.Run();
