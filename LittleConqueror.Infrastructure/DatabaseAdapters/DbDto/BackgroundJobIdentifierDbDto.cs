using LittleConqueror.AppService.Domain.Models.Entities.Base;

namespace LittleConqueror.Infrastructure.DatabaseAdapters.DbDto;

public class BackgroundJobIdentifierDbDto(string jobIdentifier) : Entity
{
    public string JobIdentifier { get; init; } = jobIdentifier;
}