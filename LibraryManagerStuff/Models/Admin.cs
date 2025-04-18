using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Admin
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

    [JsonIgnore]
    public List<Library>? Libraries { get; set; }
}
