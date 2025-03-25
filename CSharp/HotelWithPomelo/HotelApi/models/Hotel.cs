using System.ComponentModel.DataAnnotations;

namespace hotel1.Model
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public List<Room> Rooms { get; set; } = new List<Room>();
        public List<Customer> Customers { get; set; } = new List<Customer>();
    }
}
