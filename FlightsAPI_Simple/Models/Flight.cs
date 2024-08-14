using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace FlightsAPI_Simple.Models
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        public string FlightNumber { get; set; } = string.Empty;
        public string DepartureAirportCode { get; set; } = string.Empty;
        public string ArrivalAirportCode { get; set; } = string.Empty;
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public int PassengerCapacity { get; set; }
        public virtual  Airline? Airline { get; set; }
    }
}
