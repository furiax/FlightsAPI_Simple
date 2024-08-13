using FlightsAPI_Simple.Dtos;
using FlightsAPI_Simple.Models;

namespace FlightsAPI_Simple.Services
{
    public interface IFlightService
    {
        public Task <List<Flight>> GetAllFlights();
        public Task <Flight?> GetFlightById(int id);
        public Task <Flight> CreateFlight(FlightApiRequestDto flight);
        public Task <Flight?> UpdateFlight(int id, FlightApiRequestDto updatedFlight);
        public Task <string?> DeleteFlight(int id);
    }
}
