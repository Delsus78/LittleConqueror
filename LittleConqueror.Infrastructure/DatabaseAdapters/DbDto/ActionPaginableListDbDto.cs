using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;
namespace LittleConqueror.Infrastructure.DatabaseAdapters.DbDto;

public record ActionPaginableListDbDto(int TotalActions, IEnumerable<ActionEntities.Action> Actions);