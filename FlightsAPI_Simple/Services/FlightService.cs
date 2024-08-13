using AutoMapper;
using FlightsAPI_Simple.Data;
using FlightsAPI_Simple.Dtos;
using FlightsAPI_Simple.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightsAPI_Simple.Services
{
    public class FlightService : IFlightService
    {
        private readonly FlightsDbContext _dbContext;
        private readonly IMapper _mapper;

        public FlightService(FlightsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task <Flight> CreateFlight(FlightApiRequestDto flight)
        {
            Flight newFlight = _mapper.Map<Flight>(flight);
            var savedFlight = await _dbContext.Flights.AddAsync(newFlight);
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

        public async Task<Flight?> UpdateFlight(int id, FlightApiRequestDto updatedFlight)
        {
            Flight? savedFlight = await _dbContext.Flights.FindAsync(id);
            
            if (savedFlight is null) {
                return null;
            }

            savedFlight = _mapper.Map(updatedFlight, savedFlight);
            savedFlight.Id = id;

            await _dbContext.SaveChangesAsync();

            return savedFlight;
        }
    }
}
