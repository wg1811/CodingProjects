using hotel1.DTOs;
using hotel1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hotel1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HotelController(AppDbContext context)
        {
            _context = context;
        }

        //  Create a new hotel
        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("Hotel name is required.");
            if (request.RoomCount <= 0)
                return BadRequest("RoomCount must be greater than 0.");
            Console.WriteLine($"Hotel '{request.Name}' created with {request.RoomCount} rooms.");
            var hotel = new Hotel { Name = request.Name, Rooms = new List<Room>() };
            int i = request.RoomCount;
            for (i = 1; i <= request.RoomCount; i++)
            {
                hotel.Rooms.Add(new Room { RoomNumber = i, Availability = true });
            }

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            return Ok(hotel);
        }

        //  Get all hotels
        [HttpGet]
        public async Task<ActionResult<List<Hotel>>> GetHotels()
        {
            var hotels = await _context.Hotels.ToListAsync();
            return Ok(hotels);
        }

        //  Get hotel by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
                return NotFound("Hotel not found.");

            return Ok(hotel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
                return NotFound("Hotel not found.");

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return Ok($"Hotel with ID {id} deleted successfully.");
        }

        // Update hotel by ID (new)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] UpdateHotelRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("Hotel name is required.");

            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
                return NotFound("Hotel not found.");

            // Update hotel properties
            hotel.Name = request.Name;

            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();

            return Ok(hotel);
        }
    }
}
