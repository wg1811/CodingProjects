using hotel1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hotel1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("make")]
        public async Task<IActionResult> MakeReservation(int customerId)
        {
            // 1. Find available room
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Availability);
            if (room == null)
                return BadRequest("No available rooms.");

            // 2. Get customer
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
                return NotFound("Customer not found.");

            // 3. Create reservation
            var reservation = new Reservation
            {
                CustomerId = customerId,
                RoomId = room.Id,
                CheckInDate = DateTime.Now,
                IsActive = true,
            };

            // 4. Update room and customer
            room.Availability = false;
            customer.RoomId = room.Id;

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return Ok($"Room {room.RoomNumber} reserved for {customer.Name}");
        }
    }
}
