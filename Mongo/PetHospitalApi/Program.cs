using Microsoft.AspNetCore.Mvc;
using PetHospitalApi.Models;
using PetHospitalApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MongoDbService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//get all
app.MapGet(
    "/pets",
    async (MongoDbService db) =>
    {
        var pets = await db.GetAllPetsAsync();
        return Results.Ok(pets);
    }
);

app.MapPost(
    "/pets",
    async ([FromBody] Pet pet, MongoDbService db) =>
    {
        await db.AddPetAsync(pet);
        Console.WriteLine($"Added pet: {pet.Name}");
        return Results.Created($"/pets/{pet.Id}", pet);
    }
);

app.Run();
