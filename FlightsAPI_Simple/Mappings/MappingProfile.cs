using AutoMapper;
using FlightsAPI_Simple.Dtos;
using FlightsAPI_Simple.Models;

namespace FlightsAPI_Simple.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Flight, FlightApiRequestDto>();
            CreateMap<FlightApiRequestDto, Flight>();
        }
    }
}
