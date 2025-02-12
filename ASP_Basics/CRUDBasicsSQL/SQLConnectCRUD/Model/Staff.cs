using System.ComponentModel.DataAnnotations;
using System.Text.Json;

public class Staff
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; } = "";

    [Required]
    public string LastName { get; set; } = "";

    [Required]
    public string? Gender { get; set; } = "";
    public string Email { get; set; } = "";

    [Required]
    public string Department { get; set; } = "";
    public double Salary { get; set; }
    public string City { get; set; } = "";

    public async Task<Staff?> CreateStaff(int numStaff, ApplicationDbContext context)
    {
        try
        {
            using var client = new HttpClient();
            var apiUrl = $"https://randomuser.me/api/?results={numStaff}";

            var response = await client.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"API request failed with status code: {response.StatusCode}");
                return null;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Raw API response: {jsonResponse}"); // Is it working?

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var newStaffMembersJSON = JsonSerializer.Deserialize<Staff>(jsonResponse, options);

            if (newStaffMembersJSON == null)
            {
                Console.WriteLine("Failed to deserialize API response");
                return null;
            }
            var newStaffMembersList = newStaffMembersJSON
                .Results.Select(r => new Staff
                {
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    Gender = r.Gender,
                    Email = r.Email,
                    Department = r.Department,
                    Salary = r.Salary,
                    City = r.City,
                })
                .ToList();
            await context.Staff.AddRangeAsync(newStaffMembersList);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }

        return Results.Ok($"Inserted {staffList.Count} staff members into the database.");
    }
}
