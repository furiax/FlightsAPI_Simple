using FlightsAPI_Simple.Data;
using FlightsAPI_Simple.Models;

namespace FlightsAPI_Simple.Services
{
    public class FlightService : IFlightService
    {
        private readonly FlightsDbContext _dbContext;
        public FlightService(FlightsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Flight CreateFlight(Flight flight)
        {
            var savedFlight = _dbContext.Flights.Add(flight);
            _dbContext.SaveChanges();
            return savedFlight.Entity;
        }

        public string? DeleteFlight(int id)
        {
            Flight? savedFlight = _dbContext.Flights.Find(id);
            if(savedFlight is null)
            {
                return null;
            }
            _dbContext.Flights.Remove(savedFlight);
            _dbContext.SaveChanges();
            return $"Successfully deleted flight with id: {id}";
        }

        public List<Flight> GetAllFlights()
        {
            return _dbContext.Flights.ToList();
        }

        public Flight? GetFlightById(int id)
        {
            var result = _dbContext.Flights.Find(id);
            if (result is null)
            {
                return null;
            }             
            return result;
        }

        public Flight? UpdateFlight(int id, Flight updatedFlight)
        {
            Flight? savedFlight = _dbContext.Flights.Find(id);
            
            if (savedFlight is null) {
                return null;
            }

            savedFlight.Id = updatedFlight.Id;
            savedFlight.FlightNumber = updatedFlight.FlightNumber;
            savedFlight.AirlineName = updatedFlight.AirlineName;
            savedFlight.DepartureAirportCode = updatedFlight.DepartureAirportCode;
            savedFlight.ArrivalAirportCode = updatedFlight.ArrivalAirportCode;
            savedFlight.DepartureDateTime = updatedFlight.DepartureDateTime;
            savedFlight.ArrivalDateTime = updatedFlight.ArrivalDateTime;
            savedFlight.PassengerCapacity = updatedFlight.PassengerCapacity;

            _dbContext.SaveChanges();

            return savedFlight;
        }
    }
}
