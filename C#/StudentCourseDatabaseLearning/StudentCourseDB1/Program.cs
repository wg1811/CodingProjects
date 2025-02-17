using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//register db context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//JSON CYCLE


//builder.Services.AddControllers(); //first version


//inorder to stop json cyle
builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System
            .Text
            .Json
            .Serialization
            .ReferenceHandler
            .Preserve;
        options.JsonSerializerOptions.WriteIndented = true; // Pretty JSON output
    });

var app = builder.Build();

//register controller
app.MapControllers();

//can be added a view page


app.MapGet("/", () => "Hello World!");

app.Run();
