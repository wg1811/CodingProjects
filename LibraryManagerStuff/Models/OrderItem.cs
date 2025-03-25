using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class OrderItem // This is so we can add cds, dvds, etc. in the future
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }

    [Required]
    [JsonIgnore]
    public Order? Order { get; set; }

    [ForeignKey("Book")]
    public int BookId { get; set; }

    [JsonIgnore]
    public Book? Book { get; set; }

    [Required]
    public string? Status { get; set; }
}
