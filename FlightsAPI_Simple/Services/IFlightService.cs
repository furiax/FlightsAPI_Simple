using FlightsAPI_Simple.Dtos;
using FlightsAPI_Simple.Models;

namespace FlightsAPI_Simple.Services
{
    public interface IFlightService
    {
        public Task <ApiResponseDto<List<Flight>>> GetAllFlights(FlightOptions flightOptions);
        public Task <ApiResponseDto<Flight?>> GetFlightById(int id);
        public Task <ApiResponseDto<Flight>> CreateFlight(FlightApiRequestDto flight);
        public Task <ApiResponseDto<Flight?>> UpdateFlight(int id, FlightApiRequestDto updatedFlight);
        public Task <ApiResponseDto<string?>> DeleteFlight(int id);
    }
}
