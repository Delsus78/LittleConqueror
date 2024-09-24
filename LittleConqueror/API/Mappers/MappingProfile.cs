using AutoMapper;
using LittleConqueror.API.Models.Dtos;
using LittleConqueror.API.Models.Dtos.ActionsDtos;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;
using LittleConqueror.AppService.Domain.Models.TechResearches;
using LittleConqueror.AppService.Domain.Models.TechResearches.Configs;
using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

namespace LittleConqueror.API.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<City, CityDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Resources, ResourcesDto>()
            .ForMember(dto => dto.Food,
                opt => opt.MapFrom(resources => resources.FoodData))
            .ForMember(dto => dto.Wood,
                opt => opt.MapFrom(resources => resources.WoodData))
            .ForMember(dto => dto.Stone, 
                opt => opt.MapFrom(resources => resources.StoneData))
            .ForMember(dto => dto.Iron, 
                opt => opt.MapFrom(resources => resources.IronData))
            .ForMember(dto => dto.Gold,
                opt => opt.MapFrom(resources => resources.GoldData))
            .ForMember(dto => dto.Diamond,
                opt => opt.MapFrom(resources => resources.DiamondData))
            .ForMember(dto => dto.Petrol,
                opt => opt.MapFrom(resources => resources.PetrolData))
            .ReverseMap();
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
        
        CreateMap<Technologique, ActionTechnologiqueDto>()
            .ForMember(dto => dto.ActionType, 
                opt => opt.MapFrom(_ => ActionType.Technologique))
            .ForMember(dto => dto.SciencePoints,
                opt => opt.MapFrom(action => action.SciencePoints))
            .ForMember(dto => dto.TechnologiqueEfficiency,
                opt => opt.MapFrom(action => 0.5))
            .ForMember(dto => dto.TechResearchCategory, 
                opt => opt.MapFrom(action => action.TechResearchCategory));
            
        
        // TechResearches
        CreateMap<TechResearchData, TechResearchDataDto>()
            .ForMember(dto => dto.StartSearchingDate,
                opt => opt.MapFrom(data =>
                    data.StartSearchingDate.HasValue
                        ? data.StartSearchingDate.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffffff")
                        : null))
            .ForMember(dto => dto.EndSearchingDate,
                opt => opt.MapFrom(data =>
                    data.EndSearchingDate.HasValue
                        ? data.EndSearchingDate.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffffff")
                        : null));

        CreateMap<TechConfigDto, TechConfig>()
            .ForMember(dest => dest.PreReqs,
                opt => opt.MapFrom(src => src.PreReqs.Select(Enum.Parse<TechResearchType>).ToList()))
            .ForMember(dest => dest.Type,
                opt => opt.MapFrom(src => Enum.Parse<TechResearchType>(src.Type)))
            .ForMember(dest => dest.Category,
                opt => opt.MapFrom(src => Enum.Parse<TechResearchCategory>(src.Category)))
            .ForMember(dest => dest.ResearchTime,
                opt => opt.MapFrom(src => TimeSpan.FromSeconds(src.ResearchTime)));
    }
}