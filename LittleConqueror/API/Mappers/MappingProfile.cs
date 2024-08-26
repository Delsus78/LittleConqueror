using AutoMapper;
using LittleConqueror.API.Models.Dtos;
using LittleConqueror.API.Models.Dtos.ActionsDtos;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;
using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

namespace LittleConqueror.API.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<City, CityDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Resources, ResourcesDto>().ReverseMap();
        CreateMap<Territory, TerritoryDto>().ReverseMap();
        CreateMap<UserInformations, UserInformationsDto>().ReverseMap();
        CreateMap<AuthUser, AuthUserDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ReverseMap();

        CreateMap<(AuthUser AuthUser, string Token), AuthenticateResponseDto>()
            .ConstructUsing((src, context) => new AuthenticateResponseDto(
                context.Mapper.Map<AuthUserDto>(src.AuthUser), src.Token))
            .ReverseMap();

        CreateMap<City, FeatureDto>()
            .ForMember(dto => dto.Id, 
                opt => opt.MapFrom(city => city.Id.ToString()))
            .ForMember(dto => dto.Geometry, 
                opt => opt.MapFrom(city => city.Geojson))
            .ForMember(dto => dto.Properties,
                opt => opt.MapFrom(city => city))
            .ReverseMap();
        
        CreateMap<City, CityPropertiesDto>()
            .ReverseMap();

        CreateMap<City, LowDataCityDto>()
            .ReverseMap();
        
        CreateMap<List<City>, FullCitiesDataDto>()
            .ForMember(dto => dto.Features, 
                opt => opt.MapFrom(cities => cities.Select(city => city).ToList()))
            .ReverseMap();
        
        // Actions
        CreateMap<(int total, List<ActionEntities.Action> actions), ActionsListDto>()
            .ForMember(dto => dto.Actions, 
                opt => opt.MapFrom(src => src.actions))
            .ForMember(dto => dto.TotalActions, 
                opt => opt.MapFrom(src => src.total))
            .ReverseMap();

        CreateMap<ActionEntities.Action, ActionWithCityDto<ActionDto>>()
            .ForMember(dto => dto.Action,
                opt => opt.MapFrom(action => action))
            .ForMember(dto => dto.City,
                opt => opt.MapFrom(action => action.City));
        
        CreateMap<ActionEntities.Action, ActionDto>()
            .IncludeAllDerived();

        CreateMap<Agricole, ActionAgricoleDto>()
            .ForMember(dto => dto.ActionType, 
                opt => opt.MapFrom(_ => ActionType.Agricole))
            .ForMember(dto => dto.FoodProduction, 
                opt => opt.MapFrom(action => action.FoodProduction))
            .ForMember(dto => dto.AgriculturalFertility, 
                opt => opt.MapFrom(action => action.AgriculturalFertility));
        
        CreateMap<Miniere, ActionMiniereDto>()
            .ForMember(dto => dto.ActionType, 
                opt => opt.MapFrom(_ => ActionType.Miniere));
            
    }
}