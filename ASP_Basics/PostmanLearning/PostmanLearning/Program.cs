using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/calculate", async (HttpContext context) =>
{
    var form = context.Request.Form;

    //checking for valid input
    if (!form.ContainsKey("operation") || !form.ContainsKey("num1") || !form.ContainsKey("num2"))
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Missing input(s).");
        return;
    }

    //calculating the numbers
    string operation = form["operation" ];
    if (!double.TryParse("num1", out double num1) || !double.TryParse("num2", out double num2))
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Invalid number(s) provided.");
        return;
    }

    double result = operation switch
    {
        "add" => num1 + num2,
        "subtract" => num1 - num2,
        "multiply" => num1 * num2,
        "divide" when num2 != 0 => num1 / num2,
        _ => double.NaN
    };

    if(double.IsNaN(result))
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Invalid division by zero. Silly.");
        return;
    }

await context.Response.WriteAsync($"Result: {result}");


});

app.Run();
