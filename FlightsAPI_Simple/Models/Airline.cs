using System.ComponentModel.DataAnnotations;

namespace FlightsAPI_Simple.Models
{
    public class Airline
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int FlightId { get; set; }
        public virtual Flight? Flight { get; set; }
    }
}
