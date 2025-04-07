using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var random = new Random();
app.MapGet("/", () => "Hello World!");

//create random number between 0-500
app.MapGet(
    "/createrandomprime",
    () =>
    {
        var rnd = new Random();
        static int GetRandomPrime(Random rnd)
        {
            List<int> primes = [2, 3, 5, 7, 11, 13, 17, 19, 23];
            return primes[rnd.Next(primes.Count)];
        }

        int primeNumber = GetRandomPrime(rnd);
        return Results.Ok(new { primeNumber = primeNumber });
    }
);
app.Run();
