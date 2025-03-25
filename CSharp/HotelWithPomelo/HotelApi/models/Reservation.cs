using System.ComponentModel.DataAnnotations;

namespace hotel1.Model
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int RoomId { get; set; }
        public Room? Room { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
