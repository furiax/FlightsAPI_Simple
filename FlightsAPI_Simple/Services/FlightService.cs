using AutoMapper;
using FlightsAPI_Simple.Data;
using FlightsAPI_Simple.Dtos;
using FlightsAPI_Simple.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
        public async Task <ApiResponseDto<Flight>> CreateFlight(FlightApiRequestDto flight)
        {
            Flight newFlight = _mapper.Map<Flight>(flight);
            var savedFlight = await _dbContext.Flights.AddAsync(newFlight);
            await _dbContext.SaveChangesAsync();
            return new ApiResponseDto<Flight> { Data = savedFlight.Entity,
                    ResponseCode = HttpStatusCode.Created};
        }

        public async Task<ApiResponseDto<string?>> DeleteFlight(int id)
        {
            Flight? savedFlight = await _dbContext.Flights.FindAsync(id);
            if(savedFlight is null)
            {
                return new ApiResponseDto<string?>
                {
                    RequestFailed = true,
                    Data = null,
                    ResponseCode = HttpStatusCode.NotFound,
                    ErrorMessage = $"Resource with id {id} was not found."
                };
            }
            _dbContext.Flights.Remove(savedFlight);
            await _dbContext.SaveChangesAsync();

            return new ApiResponseDto<string?>
            {
                Data = $"Successfully deleted flight with id: {id}",
                ResponseCode = HttpStatusCode.NoContent
            };
        }

        public async Task<ApiResponseDto<List<Flight>>> GetAllFlights()
        {
            var flights = await _dbContext.Flights.ToListAsync();
            return new ApiResponseDto<List<Flight>>
            {
                Data = flights,
                ResponseCode = HttpStatusCode.OK,
            }; 
        }

        public async Task<ApiResponseDto<Flight?>> GetFlightById(int id)
        {
            var result = await _dbContext.Flights.FindAsync(id);
            if (result is null)
            {
                return new ApiResponseDto<Flight?>
                {
                    RequestFailed = true,
                    Data = null,
                    ResponseCode = HttpStatusCode.NotFound,
                    ErrorMessage = $"Resource with id {id} was not found."
                };
            }
            return new ApiResponseDto<Flight?>
            {
                Data = result,
                ResponseCode = HttpStatusCode.OK,
            };
        }

        public async Task<ApiResponseDto<Flight?>> UpdateFlight(int id, FlightApiRequestDto updatedFlight)
        {
            Flight? savedFlight = await _dbContext.Flights.FindAsync(id);
            
            if (savedFlight is null) {
                return new ApiResponseDto<Flight?>
                {
                    RequestFailed = true,
                    Data = null,
                    ResponseCode = HttpStatusCode.NotFound,
                    ErrorMessage = $"Resource with id {id} was not found."
                };
            }

            savedFlight = _mapper.Map(updatedFlight, savedFlight);
            savedFlight.Id = id;

            await _dbContext.SaveChangesAsync();

            return new ApiResponseDto<Flight?>
            {
                Data = savedFlight,
                ResponseCode = HttpStatusCode.OK,
            };
        }
    }
}
