namespace LittleConqueror.AppService.Domain.Models.Entities.Base;

public interface IEntity<out TId>
{
    TId Id { get; }
}