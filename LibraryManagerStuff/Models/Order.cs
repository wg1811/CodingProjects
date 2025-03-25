using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Order
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }

    [JsonIgnore]
    public User? User { get; set; }

    [JsonIgnore]
    public List<OrderItem>? OrderItems { get; set; }
    public DateTime OrderDate { get; set; }

    [Required]
    public int Quantity { get; set; } // To limit the number of items in the order
    public string? Status { get; set; }
}
