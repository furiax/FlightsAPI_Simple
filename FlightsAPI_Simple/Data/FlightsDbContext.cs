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
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Seat> Seats { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>()
                .HasOne(f => f.Airline)
                .WithOne(a => a.Flight)
                .HasForeignKey<Airline>(A => A.FlightId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Flight)
                .WithMany(f => f.Seats)
                .HasForeignKey(s => s.FlightId);
        }


        public void SeedData()
        {
            Airlines.RemoveRange(Airlines);
            var airlines = new List<Airline>
            {
                new Airline { Name = "Airline A" },
                new Airline { Name = "Airline B" },
                new Airline { Name = "Airline C" }
            };

            Airlines.AddRange(airlines);

            Seats.RemoveRange(Seats);
            var seatsA = new List<Seat>
            {
                new Seat {SeatNumber = "A1"},
                new Seat {SeatNumber = "A2"},
                new Seat {SeatNumber = "A3"},
                new Seat {SeatNumber = "A4"},
                new Seat {SeatNumber = "A5"},
                new Seat {SeatNumber = "A6"}
            };
            var seatsB = new List<Seat>
            {
                new Seat {SeatNumber = "B1"},
                new Seat {SeatNumber = "B2"},
                new Seat {SeatNumber = "B3"},
                new Seat {SeatNumber = "B4"},
                new Seat {SeatNumber = "B5"},
                new Seat {SeatNumber = "B6"}
            };
            var seatsC = new List<Seat>
            {
                new Seat {SeatNumber = "C1"},
                new Seat {SeatNumber = "C2"},
                new Seat {SeatNumber = "C3"},
                new Seat {SeatNumber = "C4"},
                new Seat {SeatNumber = "C5"},
                new Seat {SeatNumber = "C6"}
            };
            Seats.AddRange(seatsA);
            Seats.AddRange(seatsB);
            Seats.AddRange(seatsC);

            Flights.RemoveRange(Flights);
            Flights.AddRange(
                new Flight { FlightNumber = "AA-101", DepartureAirportCode = "NYC", ArrivalAirportCode = "LON", DepartureDateTime = new DateTime(2024,5,24,21,40,0) , ArrivalDateTime = new DateTime(2024, 5, 25, 04, 00, 0), PassengerCapacity = 100, Airline = airlines[0], Seats = seatsA },
                new Flight { FlightNumber = "AB-202", DepartureAirportCode = "PAR", ArrivalAirportCode = "TKY", DepartureDateTime = new DateTime(2024, 5, 27, 22, 40, 0), ArrivalDateTime = new DateTime(2024, 5, 28, 05, 13, 0), PassengerCapacity = 120, Airline = airlines[1], Seats = seatsB },
                new Flight { FlightNumber = "AC-303", DepartureAirportCode = "SYD", ArrivalAirportCode = "LAX", DepartureDateTime = new DateTime(2024, 5, 28, 10, 22, 0), ArrivalDateTime = new DateTime(2024, 5, 28, 20, 50, 0), PassengerCapacity = 95, Airline = airlines[2], Seats = seatsC }
                );
            SaveChanges();
        }
    }
}
