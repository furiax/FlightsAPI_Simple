using FlightsAPI_Simple.Models;

namespace FlightsAPI_Simple.Services
{
    public interface IFlightService
    {
        public List<Flight> GetAllFlights();
        public Flight? GetFlightById(int id);
        public Flight CreateFlight(Flight flight);
        public Flight? UpdateFlight(int id, Flight updatedFlight);
        public string? DeleteFlight(int id);
    }
}
