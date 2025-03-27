using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

var choices = new List<string> { "Rock", "Paper", "Scissors", "Lizard", "Spock" };
var random = new Random();

app.MapGet(
    "/api/game/play",
    (string playerChoice) =>
    {
        if (!choices.Contains(playerChoice))
        {
            return Results.BadRequest(
                "Invalid choice. Choose from: Rock, Paper, Scissors, Lizard, Spock."
            );
        }

        string computerChoice = choices[random.Next(choices.Count)];
        string result = PlayRound(playerChoice, computerChoice);

        return Results.Json(
            new
            {
                PlayerChoice = playerChoice,
                ComputerChoice = computerChoice,
                Result = result,
            }
        );
    }
);

app.Run();

static string PlayRound(string playerChoice, string computerChoice)
{
    var defeats = new Dictionary<string, List<string>>
    {
        {
            "Rock",
            new() { "Lizard", "Scissors" }
        },
        {
            "Paper",
            new() { "Rock", "Spock" }
        },
        {
            "Scissors",
            new() { "Paper", "Lizard" }
        },
        {
            "Lizard",
            new() { "Spock", "Paper" }
        },
        {
            "Spock",
            new() { "Scissors", "Rock" }
        },
    };

    if (playerChoice == computerChoice)
        return "It's a tie!";

    return defeats[playerChoice].Contains(computerChoice)
        ? $"You win! {playerChoice} beats {computerChoice}."
        : $"You lose! {computerChoice} beats {playerChoice}.";
}
