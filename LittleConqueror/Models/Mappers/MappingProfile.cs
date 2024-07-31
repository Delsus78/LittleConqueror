using AutoMapper;
using LittleConqueror.AppService.DomainEntities;
using LittleConqueror.Models.Dtos;

namespace LittleConqueror.Models.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<City, CityDto>().ReverseMap();
        CreateMap<Geojson, GeojsonDto>().ReverseMap();
    }
}