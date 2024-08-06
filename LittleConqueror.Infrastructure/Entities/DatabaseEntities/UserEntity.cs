namespace LittleConqueror.Infrastructure.Entities.DatabaseEntities;

public class UserEntity
{
    public int Id { get; init; }
    public string Name { get; init; }
    public TerritoryEntity Territory { get; init; }
    public AuthUserEntity AuthUser { get; init; }
}