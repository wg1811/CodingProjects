using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Zip { get; set; }

    [Required]
    public string? Role { get; set; }

    [JsonIgnore]
    public List<Library>? Libraries { get; set; } // Does this need [jsonignore]? Not necessary if we can get library from Orders in Orderlist?

    [Required]
    public DateTime DateRegistered { get; set; }
    public List<Order>? Orders { get; set; }
    public string? Status { get; set; }
}
