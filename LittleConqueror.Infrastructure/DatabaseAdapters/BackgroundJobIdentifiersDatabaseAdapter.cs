using LittleConqueror.AppService.Domain.Logic;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.Infrastructure.DatabaseAdapters.DbDto;
using LittleConqueror.Infrastructure.Repositories;

namespace LittleConqueror.Infrastructure.DatabaseAdapters;

public class BackgroundJobIdentifiersDatabaseAdapter(BackgroundJobIdentifierRepository backgroundJobIdentifierRepository) 
    : IBackgroundJobIdentifiersDatabasePort
{
    public async Task<string?> GetBackgroundJobIdentifierForNaturalId(string naturalId)
        => (await GetBackgroundJobIdentifierEntityForNaturalId(naturalId))?.JobIdentifier;

    public async Task SetBackgroundJobIdentifierForNaturalId(string naturalId, string identifier)
        => await backgroundJobIdentifierRepository.CreateAsync(
                new BackgroundJobIdentifierDbDto(identifier)
                { Id = MathHelpers.StringToLong(naturalId) });

    public async Task RemoveBackgroundJobIdentifierForNaturalId(string naturalId)
    {
        var entity = await GetBackgroundJobIdentifierEntityForNaturalId(naturalId);
        if (entity is not null)
            await backgroundJobIdentifierRepository.RemoveAsync(entity);
    }

    private async Task<BackgroundJobIdentifierDbDto?> GetBackgroundJobIdentifierEntityForNaturalId(string naturalId)
        => await backgroundJobIdentifierRepository.GetByIdAsync(MathHelpers.StringToLong(naturalId));
}