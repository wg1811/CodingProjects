using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//creating a list of Persons
List<Person> people = new List<Person>()
{
    new Person(1, "Joe", 32, "joe@gmail.com"),
    new Person(2, "Jane", 24, "jane@gmail.com"),
    new Person(3, "Tom", 47, "tom@gmail.com")
};

app.MapGet("/", () => "Hello World!");



app.MapGet("/people", () => 
{
    return Results.Ok(people);
});

//add a new person (post it)
app.MapPost("/addperson", ([FromForm] string name, [FromForm] int age, [FromForm] string email) => {
    if (string.IsNullOrEmpty(name) || age == 0)
    {
        return Results.BadRequest("Name or Age can't be empty");
    }
    //Ternary Operation is short version of if,else
    int giveIndex = people.Any() ? people.Max(p=>p.Id)+1 : 1;

    Person newPerson = new Person(giveIndex, name, age, email);
    people.Add(newPerson);
    return Results.Ok($"New person named {newPerson.Name} added");
    
}).DisableAntiforgery();

app.Run();

record Person(int Id, string Name, int Age, string Email);
