using AutoMapper;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.Infrastructure.Entities.DatabaseEntities;
using LittleConqueror.Models.Dtos;

namespace LittleConqueror.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<City, CityDto>().ReverseMap();
        CreateMap<Geojson, GeojsonDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Territory, TerritoryDto>().ReverseMap();
        
        CreateMap<User, UserEntity>().ReverseMap();
        CreateMap<City, CityEntity>().ReverseMap();
        CreateMap<Territory, TerritoryEntity>().ReverseMap();
    }
}