using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("Jwt");

var secretKey = jwtSettings["Blog_Key"] ?? ""; // Fetch the secret
var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

void SeedAdminUser(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Check if the database is available before seeding
        if (context.Database.CanConnect() && !context.Users.Any(u => u.UserRole == UserRole.Admin))
        {
            var passwordHasher = new PasswordHasher<User>();
            var adminUser = new User
            {
                UserRole = UserRole.Admin, // Role = Admin
                UserName = "admin",
                Email = "admin@blog.com",
                Password = passwordHasher.HashPassword(null, "1234"), // Secure hashed password
            };

            context.Users.Add(adminUser);
            context.SaveChanges();
            Console.WriteLine(" Admin user created successfully!"); // Log success
        }
    }
}

// Console.WriteLine($"JWT Secret Key: {secretKey}"); // Just for debugging (remove in production)

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

SeedAdminUser(app);

//5- we enable authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
