using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//creating a list of Persons
List<Person> people = new List<Person>()
{
    new Person(1, "Joe", 32, "joe@gmail.com", 184, 80),
    new Person(2, "Jane", 24, "jane@gmail.com", 184, 80),
    new Person(3, "Tom", 47, "tom@gmail.com", 184, 80)
};

app.MapGet("/", () => "Hello World!");

app.MapPost("/calculatebmi", ([FromBody] Person person) => 
{
    double persWeight = person.Weight;
    double persHeight = person.Height;
    double persBmi = Math.Round((persWeight / Math.Pow(persHeight, 2)), 2);
    person.Bmi = persBmi;
});



app.MapGet("/people", () => 
{
    return Results.Ok(people);
});

//add a new person (post it)
app.MapPost("/addperson", ([FromForm] string name, [FromForm] int age, [FromForm] string email, [FromForm] double height, [FromForm] double weight) => {
    if (string.IsNullOrEmpty(name) || age == 0)
    {
        return Results.BadRequest("Name or Age can't be empty");
    }
    //Ternary Operation is short version of if,else
    int giveIndex = people.Any() ? people.Max(p=>p.Id)+1 : 1;

    Person newPerson = new Person(giveIndex, name, age, email, height, weight);
    people.Add(newPerson);
    return Results.Ok($"New person named {newPerson.Name} added");
    
}).DisableAntiforgery();

app.Run();

class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public double Bmi { get; set; }
    public string BmiType { get; set; }

    public Person(int id, string name, int age, string email, double height, double weight)
    {
        Id = id;
        Name = name;
        Age = age;
        Email = email;
        Height = height;
        Weight = weight;
        Bmi = 0;
        BmiType = "";
    }
}