using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

Questions answers = new() { Question1 = "b", Question2 = "b" };

//app.MapGet("/", () => "Hello World!");

app.MapGet(
    "/",
    context =>
    {
        context.Response.Redirect("./testform.html");
        return Task.CompletedTask;
    }
);

app.MapPost(
    "/test",
    ([FromBody] Questions questions) =>
    {
        int score = 0;
        if (questions.Question1 == answers.Question1)
            score++;
        if (questions.Question2 == answers.Question2)
            score++;

        var testResult = new
        {
            totalQuestions = 2,
            totalScore = score,
            situation = score >= 1 ? "Passed" : "Failed",
        };

        return Results.Json(testResult);
    }
);

app.Run();
