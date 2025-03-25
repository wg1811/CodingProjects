using hotel1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hotel1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoomController(AppDbContext context)
        {
            _context = context;
        }

        //  Create a new room for a hotel
        [HttpPost]
        public async Task<IActionResult> AddRoom([FromBody] Room room)
        {
            var hotel = await _context.Hotels.FindAsync(room.HotelId);
            if (hotel == null)
                return NotFound("Hotel not found.");

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return Ok(room);
        }

        //  Get all rooms (optionally by hotel)
        [HttpGet]
        public async Task<IActionResult> GetRooms([FromQuery] int? hotelId)
        {
            var rooms =
                hotelId == null
                    ? await _context.Rooms.Include(r => r.Hotel).ToListAsync()
                    : await _context
                        .Rooms.Where(r => r.HotelId == hotelId)
                        .Include(r => r.Hotel)
                        .ToListAsync();

            return Ok(rooms);
        }

        //  Update availability of a room
        [HttpPut("{id}/availability")]
        public async Task<IActionResult> UpdateAvailability(int id, [FromQuery] bool available)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return NotFound("Room not found.");

            room.Availability = available;
            await _context.SaveChangesAsync();

            return Ok($"Room {room.RoomNumber} availability set to {available}");
        }
    }
}
