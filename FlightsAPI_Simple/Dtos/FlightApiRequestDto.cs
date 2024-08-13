using System.ComponentModel.DataAnnotations;

namespace FlightsAPI_Simple.Dtos
{
    public class FlightApiRequestDto
    {
        [Required]
        [MaxLength(8, ErrorMessage="FlightNumber must have 8 characters or less.")]
        [MinLength(6, ErrorMessage="FlightNumber must have at least 6 characters.")]
        public string FlightNumber { get; set; } = string.Empty;
        [Required]
        public string AirlineName { get; set; }
        [Required]
        public string DepartureAirportCode { get; set; }
        [Required]
        public string ArrivalAirportCode { get; set; }
        [Required]
        public DateTime DepartureDateTime { get; set; }
        [Required]
        public DateTime ArrivalDateTime { get; set; }
        [Required]
        [Range(90,140)]
        public int PassengerCapacity { get; set; }
    }
}
