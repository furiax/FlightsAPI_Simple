using FlightsAPI_Simple.Models;
using FlightsAPI_Simple.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightsAPI_Simple.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly FlightService _flightService;
        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public ActionResult<List<Flight>> GetAllFLights()
        {
            return Ok(_flightService.GetAllFlights());
        }

        [HttpGet("{id}")]
        public ActionResult<Flight> GetFlightById(int id)
        {
            return Ok(_flightService.GetFlightById(id));
        }

        [HttpPost]
        public ActionResult<Flight> CreateFlight(Flight flight) 
        {
            return Ok(_flightService.CreateFlight(flight));
        }

        [HttpPut("{id}")]
        public ActionResult<Flight> UpdateFlight(int id, Flight updatedFlight)
        {
            return Ok(_flightService.UpdateFlight(id, updatedFlight));
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteFlight(int id)
        {
            return Ok(_flightService.DeleteFlight(id));
        }
    }
}
