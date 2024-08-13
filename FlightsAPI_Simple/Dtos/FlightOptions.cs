﻿using Microsoft.AspNetCore.Mvc;

namespace FlightsAPI_Simple.Dtos
{
    public class FlightOptions
    {
        [FromQuery(Name = "airline_name")]
        public string AirlineName { get; set; } = string.Empty;
        [FromQuery(Name = "departure_airport_code")]
        public string DepartureAirportCode { get; set; } = string.Empty;
        [FromQuery(Name = "arrival_airport_code")]
        public string ArrivalAirportCode { get; set; } = string.Empty;
        [FromQuery(Name = "departure_date_time")]
        public DateTime? DepartureDateTime { get; set; }
        [FromQuery(Name = "arrival_date_time")]
        public DateTime? ArrivalDateTime { get; set; }

        [FromQuery(Name = "sort_by")]
        public string SortBy { get; set; } = "id";
        [FromQuery(Name = "sort_order")]
        public string SortOrder { get; set; } = "ASC";

        [FromQuery(Name = "search")]
        public string Search { get; set; } = string.Empty;
    }
}
