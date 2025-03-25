using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace hotel1.Model
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        // Foreign key to Hotel
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        // One-to-one with Room
        [JsonIgnore]
        public int? RoomId { get; set; }
        public Room? Room { get; set; }
    }
}
