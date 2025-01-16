using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

app.MapGet("/", () => "Hello World!");

app.MapGet("/palindrome", content => 
{
    content.Response.Redirect("/palindrome.html");
    return Task.CompletedTask;
});

app.MapPost("/palindromebody", ([FromBody] string palText) =>
{
    string cleaned = new(palText.Where(char.IsLetter).ToArray());
    char[] cleanPalArray = cleaned.ToLower().Replace(" ", "").ToCharArray();
    char[] reversedPalArray = cleanPalArray.Reverse().ToArray();
    
    int arraySize = cleanPalArray.Length;

    for(int i = 0; i > arraySize; i++)
    {
        if (cleanPalArray[i] != reversedPalArray[i])
        {
            return Results.Ok(new { Message = $"{palText} is not a palindrome."});
        }
    }
    return Results.Ok(new { Message = $"{palText} is a palindrome."});

});

app.Run();
