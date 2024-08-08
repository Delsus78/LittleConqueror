using AutoMapper;
using LittleConqueror.API.Models.Dtos;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Entities;

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
        CreateMap<AuthUser, AuthUserDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ReverseMap();

        CreateMap<(AuthUser AuthUser, string Token), AuthenticateResponseDto>()
            .ConstructUsing((src, context) => new AuthenticateResponseDto(
                context.Mapper.Map<AuthUserDto>(src.AuthUser), src.Token))
            .ReverseMap();
    }
}