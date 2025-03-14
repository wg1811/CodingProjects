using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("Jwt");

var secretKey = jwtSettings["Blog_Key"] ?? ""; // Fetch the secret
var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

// var secretKeyString = configuration["JwtSettings:Secret"];
// if (string.IsNullOrEmpty(secretKeyString))
// {
//     Console.WriteLine("⚠️ JWT Secret Key is missing! Check appsettings.json.");
// }
// else
// {
//     Console.WriteLine($"JWT Secret Key: {secretKeyString}");
// }

Console.WriteLine($"JWT Secret Key: {secretKey}"); // Just for debugging (remove in production)

//register db context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

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
            .IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true; // Pretty JSON output
    });

//3- add Authentication service
builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"],
            ValidateLifetime = true,
        };
    });

//4- enable [Authorize] attribute
builder.Services.AddAuthorization();

var app = builder.Build();

//5- we enable authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
