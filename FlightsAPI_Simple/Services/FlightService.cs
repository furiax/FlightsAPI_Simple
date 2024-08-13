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

        public async Task<ApiResponseDto<List<Flight>>> GetAllFlights(FlightFilterOptions filterOptions)
        {
            var query = _dbContext.Flights.AsQueryable();

            if(!string.IsNullOrEmpty(filterOptions.AirlineName))
            {
                query = query.Where(f => f.AirlineName == filterOptions.AirlineName);
            }

            if (!string.IsNullOrEmpty(filterOptions.DepartureAirportCode))
            {
                query = query.Where(f => f.DepartureAirportCode == filterOptions.DepartureAirportCode);
            }

            if (!string.IsNullOrEmpty(filterOptions.ArrivalAirportCode))
            {
                query = query.Where(f => f.ArrivalAirportCode == filterOptions.ArrivalAirportCode);
            }

            if (filterOptions.DepartureDateTime.HasValue)
            {
                query = query.Where(f => f.DepartureDateTime <= filterOptions.DepartureDateTime);
            }

            if(filterOptions.ArrivalDateTime.HasValue)
            {
                query = query.Where(f => f.ArrivalDateTime <= filterOptions.ArrivalDateTime);
            }

            if(filterOptions.SortBy == "id" || !string.IsNullOrEmpty(filterOptions.SortBy))
            {
                switch(filterOptions.SortBy)
                {
                    case "airline_name": 
                        query = filterOptions.SortOrder.ToUpper() == "ASC" ? 
                            query.OrderBy(f => f.AirlineName) :
                            query.OrderByDescending(f => f.AirlineName); 
                        break;
                    case "flight_number":
                        query = filterOptions.SortOrder.ToUpper() == "ASC" ?
                            query.OrderBy(f => f.FlightNumber) :
                            query.OrderByDescending(f => f.FlightNumber);
                        break;
                    case "departure_airport_code":
                        query = filterOptions.SortOrder.ToUpper() == "ASC" ?
                            query.OrderBy(f => f.DepartureAirportCode) :
                            query.OrderByDescending(f => f.DepartureAirportCode);
                        break;
                    case "arrival_airport_code":
                        query = filterOptions.SortOrder.ToUpper() == "ASC" ?
                            query.OrderBy(f => f.ArrivalAirportCode) :
                            query.OrderByDescending(f => f.ArrivalAirportCode);
                        break;
                    case "departure_date_time":
                        query = filterOptions.SortOrder.ToUpper() == "ASC" ?
                            query.OrderBy(f => f.DepartureDateTime) :
                            query.OrderByDescending(f => f.DepartureDateTime);
                        break;
                    case "arrival_date_time":
                        query = filterOptions.SortOrder.ToUpper() == "ASC" ?
                            query.OrderBy(f => f.ArrivalDateTime) :
                            query.OrderByDescending(f => f.ArrivalDateTime);
                        break;
                    case "passenger_capacity":
                        query = filterOptions.SortOrder.ToUpper() == "ASC" ?
                            query.OrderBy(f => f.PassengerCapacity) :
                            query.OrderByDescending(f => f.PassengerCapacity);
                        break;
                    default:
                        query = filterOptions.SortOrder.ToUpper() == "ASC" ?
                            query.OrderBy(f => f.Id) :
                            query.OrderByDescending(f => f.Id);
                        break;
                }
            }
            var flights = await query.ToListAsync();

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
