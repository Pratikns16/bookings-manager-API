using BookingsManagerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingsManagerAPI.Data
{
    public class BookingsManagerAPIDbContext : DbContext
    {
        public BookingsManagerAPIDbContext(DbContextOptions options) : base(options) 
        {
        }

        public DbSet<Booking> Bookings { get; set; }
    }
}

