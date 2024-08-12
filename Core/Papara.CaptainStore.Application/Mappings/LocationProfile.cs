using AutoMapper;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.LocationEntities;

namespace Papara.CaptainStore.Application.Mappings
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<DistrictListDTO, District>().ReverseMap();
            CreateMap<CityListDTO, City>().ReverseMap();
            CreateMap<CountryListDTO, Country>().ReverseMap();
        }
    }
}
