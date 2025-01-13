using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);

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

app.MapGet("/", () => "Hello World!");

// Backend String Functions
app.MapPost("/reversetext", ([FromForm] string anyText) => {
 if (anyText == null)
    {
        return "text cannot be empty";
    }

    char[] textArray = anyText.ToCharArray();
    Array.Reverse(textArray);
    return new string(textArray);
}).DisableAntiforgery();


app.MapPost("/replacechar", ([FromForm] char oldChar, [FromForm] char newChar, [FromForm] string anyText) => {
    string replacedText = anyText.Replace(oldChar, newChar);
    return replacedText;   
}).DisableAntiforgery();

//method not allowed?
app.MapPost("/removespaces", ([FromForm] string anyText) => {
    string result = anyText.Replace(" ", ""); //remove all spaces 
    return result;
}).DisableAntiforgery();


app.MapPost("/createsinustext", ([FromForm] string anyText) => {
    string fancyString = "";
    if (string.IsNullOrWhiteSpace(anyText))
    {
        Console.WriteLine("Text cannot be null or empty.");
        fancyString = "Text cannot be null or empty.";
        return fancyString;
    }
    char[] charTextArray = anyText.ToUpper().ToCharArray();
    int counter = 0;

    foreach (char c in charTextArray)
    {
        double y = Math.Abs(Math.Sin(counter * 0.12) * 20);
        fancyString = new string(' ', (int)y) + c;
        Console.WriteLine(fancyString);
        counter++;
    }
    return fancyString;
}).DisableAntiforgery();


app.MapPost("/findfirstchar", ([FromForm] char anyChar, [FromForm] string anyString) => {
    int index = anyString.IndexOf(anyChar);
    string result = $"The index of the first {anyChar} in the text is {index}.";
    Console.WriteLine($"The index of the first {anyChar} in the text is {index}.");
    return result;
}).DisableAntiforgery();


app.MapPost("/findlastchar", ([FromForm] char anyChar, [FromForm] string anyString) =>
{
    int index = anyString.LastIndexOf(anyChar);
    string result = $"The index of the last {anyChar} in the text is {index}.";
    Console.WriteLine($"The index of the last {anyChar} in the text is {index}.");
    return result;
}).DisableAntiforgery();


app.MapPost("/findallchar", ([FromForm] char anyChar, [FromForm] string anyString) =>
{
    int count = anyString.Count(c => c == anyChar);
    string result = $"The number of {anyChar}'s in the text was {count}";
    Console.WriteLine($"searched char {anyChar}  found {count} times inside the text");
    return result;
}).DisableAntiforgery();


app.MapPost("/createsubstring", ([FromForm] char anyChar, [FromForm] int length, [FromForm] string anyString) =>
{
    int charPos = anyString.IndexOf(anyChar); //we get the first char in the text
    string searchText = anyString.Substring(charPos, length);
    Console.WriteLine(searchText);
    return searchText;
}).DisableAntiforgery();


app.MapPost("/findsubstring", ([FromForm] string searchedText, [FromForm] string anyText) =>
{
    if (anyText.Contains(searchedText))
    {
        Console.WriteLine($"substring {searchedText} exists in the text");
        return true;
    }
    else
    {
        Console.WriteLine($"substring {searchedText} does not exist in the text");
        return false;
    }
}).DisableAntiforgery();

app.Run();





