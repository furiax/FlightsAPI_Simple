using System.ComponentModel.DataAnnotations;

namespace FlightsAPI_Simple.Models
{
    public class Seat
    {
        [Key]
        public int Id { get; set; }
        public string SeatNumber { get; set; } = string.Empty;
        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; }
    }
}
