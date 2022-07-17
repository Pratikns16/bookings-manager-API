using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingsManagerAPI.Data;
using BookingsManagerAPI.Entities;
using BookingsManagerAPI.Models;
using BookingsManagerAPI.Helpers;

namespace BookingsManagerAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : Controller
    {
        private readonly BookingsManagerAPIDbContext _bookingsManagerAPIDbContext;
        private const string bookingNotFoundErrorMessage = "Booking with the given Id does not exists";

        public BookingsController(BookingsManagerAPIDbContext bookingsManagerAPIDbContext)
        {
            _bookingsManagerAPIDbContext = bookingsManagerAPIDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            return Ok(await _bookingsManagerAPIDbContext.Bookings.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetBooking([FromRoute] Guid id)
        {
            var booking = await _bookingsManagerAPIDbContext.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound(bookingNotFoundErrorMessage);
            }
            return Ok(booking);
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

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateBooking([FromRoute] Guid id, UpdateBookingRequest updateBookingRequest)
        {
            var booking = await _bookingsManagerAPIDbContext.Bookings.FindAsync(id);

            if(booking == null)
            {
                return NotFound(bookingNotFoundErrorMessage);
            }

            booking.GuestId = updateBookingRequest.GuestId;
            booking.CheckInDate = updateBookingRequest.CheckInDate;
            booking.CheckOutDate = updateBookingRequest.CheckOutDate;
            booking.TotalAmount = updateBookingRequest.TotalAmount;

            await _bookingsManagerAPIDbContext.SaveChangesAsync();

            return Ok(booking);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteBooking([FromRoute] Guid id)
        {
            var booking = await _bookingsManagerAPIDbContext.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound(bookingNotFoundErrorMessage);
            }

            _bookingsManagerAPIDbContext.Bookings.Remove(booking);
            await _bookingsManagerAPIDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}

