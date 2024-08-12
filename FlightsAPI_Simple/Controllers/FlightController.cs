using FlightsAPI_Simple.Models;
using FlightsAPI_Simple.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightsAPI_Simple.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;
        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Flight>>> GetAllFLights()
        {
            return Ok(_flightService.GetAllFlights());
        }

        [HttpGet("{id}")]
        public async Task <ActionResult<Flight>> GetFlightById(int id)
        {
            var result = await _flightService.GetFlightById(id);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Flight>> CreateFlight(Flight flight) 
        {
            var createdFlight = await _flightService.CreateFlight(flight);
            return new ObjectResult(createdFlight) { StatusCode = 201 };
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Flight>> UpdateFlight(int id, Flight updatedFlight)
        {
            var result = await _flightService.UpdateFlight(id, updatedFlight);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task <ActionResult<string>> DeleteFlight(int id)
        {
            var result = await _flightService.DeleteFlight(id);

            if(result is null)
            {
                return NotFound();
            }
            return new ObjectResult(result) { StatusCode = 204 };
        }
    }
}
