using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("allowAll", policy => 
    {
        policy.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});

var app = builder.Build();
app.UseCors("allowAll");

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


app.MapPost("/palindromeobject", ([FromBody] PalindromeRequest palindromeRequest) =>
{
    var palText = palindromeRequest.PalText;
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

//Takes everything. Is bad strategy and can be used for Phishing, etc.
app.MapPost("/palindromestream", async context =>
{
    //create a stream reader to read body 
    using var reader = new StreamReader(context.Request.Body);
    //get text 
    var palText = await reader.ReadToEndAsync();

    string cleaned = new(palText.Where(char.IsLetter).ToArray());
    char[] cleanPalArray = cleaned.ToLower().Replace(" ", "").ToCharArray();
    char[] reversedPalArray = cleanPalArray.Reverse().ToArray();
    
    int arraySize = cleanPalArray.Length;

    for(int i = 0; i > arraySize; i++)
    {
        if (cleanPalArray[i] != reversedPalArray[i])
        {
            // Respond with a message if not a palindrome
            await context.Response.WriteAsJsonAsync(new { Message = $"{palText} is not a palindrome."});
            return;
        }
    }
    await context.Response.WriteAsJsonAsync(new { Message = $"{palText} is a palindrome."});
    return;
});


app.MapPost("/palindromeform", ([FromForm] string palText) =>
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


public class PalindromeRequest
{
    public string PalText { get; set; } = "";
}

 