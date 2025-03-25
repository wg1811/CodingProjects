using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Book
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Title { get; set; }

    [Required]
    public string? Author { get; set; }

    [Required]
    public string? ISBN { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public DateTime PublishedDate { get; set; }

    [Required]
    public DateTime AddedDate { get; set; }

    [Required]
    public DateTime ModifiedDate { get; set; }

    [Required]
    public DateTime PurchaseDate { get; set; }

    [Required]
    public int Pages { get; set; }

    [Required]
    public string? Publisher { get; set; }

    [Required]
    public string? Language { get; set; }

    [Required]
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }

    [Required]
    [ForeignKey("Category")]
    public int CategoryId { get; set; }

    [Required]
    public Category? Category { get; set; }

    [Required]
    public bool IsAvailable { get; set; }

    [ForeignKey("Library")]
    public int LibraryId { get; set; }

    [JsonIgnore]
    public Library? Library { get; set; }
}
