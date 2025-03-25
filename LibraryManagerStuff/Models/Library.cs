using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Library
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Address { get; set; }

    [Required]
    public string? City { get; set; }

    [Required]
    public string? State { get; set; }

    [Required]
    public string? Zip { get; set; }

    [Required]
    public string? Phone { get; set; }

    [Required]
    public string? Website { get; set; }

    [JsonIgnore]
    public List<Book>? Books { get; set; } // Not sure if this is necessary

    [JsonIgnore]
    public List<User>? Users { get; set; } // Is this good enough for Admin / Librarian rights?
}
