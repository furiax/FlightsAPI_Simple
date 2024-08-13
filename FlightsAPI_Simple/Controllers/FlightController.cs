using FlightsAPI_Simple.Dtos;
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
        public async Task<ActionResult<ApiResponseDto<List<Flight>>>> GetAllFLights()
        {
            return Ok(await _flightService.GetAllFlights());
        }

        [HttpGet("{id}")]
        public async Task <ActionResult<ApiResponseDto<Flight>>> GetFlightById(int id)
        {
            var result = await _flightService.GetFlightById(id);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponseDto<Flight>>> CreateFlight(FlightApiRequestDto flight) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return new ObjectResult(await _flightService.CreateFlight(flight)) { StatusCode = 201 };
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseDto<Flight>>> UpdateFlight(int id, FlightApiRequestDto updatedFlight)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _flightService.UpdateFlight(id, updatedFlight);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task <ActionResult> DeleteFlight(int id)
        {
            var result = await _flightService.DeleteFlight(id);

            if(result is null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
