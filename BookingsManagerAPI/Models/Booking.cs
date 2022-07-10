using System;
namespace BookingsManagerAPI.Models
{
    public class Booking
    {
        public Guid Id { get; set; }

        public int GuestId { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int TotalAmount { get; set; }
    }
}

