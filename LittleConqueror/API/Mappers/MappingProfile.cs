using AutoMapper;
using LittleConqueror.API.Models.Dtos;
using LittleConqueror.API.Models.Dtos.ActionsDtos;
using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;
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
        
        // Actions
        CreateMap<ActionEntities.Action, ActionDto>()
            .IncludeAllDerived();

        CreateMap<Agricole, ActionAgricoleDto>()
            .ForMember(dto => dto.ActionType, 
                opt => opt.MapFrom(_ => ActionType.Agricole));
    }
}