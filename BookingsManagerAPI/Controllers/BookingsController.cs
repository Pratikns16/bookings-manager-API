using BookingsManagerAPI.Data;
using BookingsManagerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingsManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : Controller
    {
        private readonly BookingsManagerAPIDbContext _bookingsManagerAPIDbContext;

        public BookingsController(BookingsManagerAPIDbContext bookingsManagerAPIDbContext)
        {
            _bookingsManagerAPIDbContext = bookingsManagerAPIDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            return Ok(await _bookingsManagerAPIDbContext.Bookings.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking(AddBookingRequest addBookingRequest)
        {
            var newBooking = new Booking()
            {
                Id = Guid.NewGuid(),
                GuestId = addBookingRequest.GuestId,
                CheckInDate = addBookingRequest.CheckInDate,
                CheckOutDate = addBookingRequest.CheckOutDate,
                TotalAmount = addBookingRequest.TotalAmount
            };

            await _bookingsManagerAPIDbContext.Bookings.AddAsync(newBooking);
            await _bookingsManagerAPIDbContext.SaveChangesAsync();

            return Ok(newBooking);
        } 
    }
}

