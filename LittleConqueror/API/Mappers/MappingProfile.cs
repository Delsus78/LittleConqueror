using AutoMapper;
using LittleConqueror.API.Models.Dtos;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.Infrastructure.Entities.DatabaseEntities;

namespace LittleConqueror.API.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<City, CityDto>().ReverseMap();
        CreateMap<Geojson, GeojsonDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Territory, TerritoryDto>().ReverseMap();
        CreateMap<UserInformations, UserInformationsDto>().ReverseMap();
        
        CreateMap<User, UserEntity>().ReverseMap();
        CreateMap<City, CityEntity>().ReverseMap();
        CreateMap<Territory, TerritoryEntity>().ReverseMap();
    }
}