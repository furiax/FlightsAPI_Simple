using FlightsAPI_Simple.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightsAPI_Simple.Data
{
    public class FlightsDbContext : DbContext
    {
        public FlightsDbContext(DbContextOptions options) : base(options)
        {
                
        }

        public DbSet<Flight> Flights { get; set; }
    }
}
