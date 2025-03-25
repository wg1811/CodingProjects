using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace hotel1.Model
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        public int RoomNumber { get; set; }

        public bool Availability { get; set; } = true;

        // Foreign key to Hotel
        public int HotelId { get; set; }

        [JsonIgnore]
        public Hotel? Hotel { get; set; }

        // One-to-one with Customer
        public Customer? Customer { get; set; }
    }
}
