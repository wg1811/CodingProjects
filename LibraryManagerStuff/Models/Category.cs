using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [JsonIgnore]
    public List<Book>? Books { get; set; } // Not sure if this is necessary
}
