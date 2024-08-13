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

        public void SeedData()
        {
            Flights.RemoveRange(Flights);
            Flights.AddRange(
                new Flight { FlightNumber = "AA-101", AirlineName = "Airline A", DepartureAirportCode = "NYC", ArrivalAirportCode = "LON", DepartureDateTime = new DateTime(2024,5,24,21,40,0) , ArrivalDateTime = new DateTime(2024, 5, 25, 04, 00, 0), PassengerCapacity = 100 },
                new Flight { FlightNumber = "AB-202", AirlineName = "Airline B", DepartureAirportCode = "PAR", ArrivalAirportCode = "TKY", DepartureDateTime = new DateTime(2024, 5, 24, 21, 40, 0), ArrivalDateTime = new DateTime(2024, 5, 25, 04, 00, 0), PassengerCapacity = 120 },
                new Flight { FlightNumber = "AC-303", AirlineName = "Airline C", DepartureAirportCode = "SYD", ArrivalAirportCode = "LAX", DepartureDateTime = new DateTime(2024, 5, 24, 21, 40, 0), ArrivalDateTime = new DateTime(2024, 5, 25, 04, 00, 0), PassengerCapacity = 95 }
                );
            SaveChanges();
        }
    }
}
