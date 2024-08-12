using FlightsAPI_Simple.Data;
using FlightsAPI_Simple.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightsAPI_Simple.Services
{
    public class FlightService : IFlightService
    {
        private readonly FlightsDbContext _dbContext;
        public FlightService(FlightsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task <Flight> CreateFlight(Flight flight)
        {
            var savedFlight = await _dbContext.Flights.AddAsync(flight);
            await _dbContext.SaveChangesAsync();
            return savedFlight.Entity;
        }

        public async Task<string?> DeleteFlight(int id)
        {
            Flight? savedFlight = await _dbContext.Flights.FindAsync(id);
            if(savedFlight is null)
            {
                return null;
            }
            _dbContext.Flights.Remove(savedFlight);
            await _dbContext.SaveChangesAsync();
            return $"Successfully deleted flight with id: {id}";
        }

        public async Task<List<Flight>> GetAllFlights()
        {
            return await _dbContext.Flights.ToListAsync();
        }

        public async Task<Flight?> GetFlightById(int id)
        {
            var result = await _dbContext.Flights.FindAsync(id);
            if (result is null)
            {
                return null;
            }             
            return result;
        }

        public async Task<Flight?> UpdateFlight(int id, Flight updatedFlight)
        {
            Flight? savedFlight = await _dbContext.Flights.FindAsync(id);
            
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

            await _dbContext.SaveChangesAsync();

            return savedFlight;
        }
    }
}
