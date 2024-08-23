using LittleConqueror.AppService.Exceptions;
namespace LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

public class Miniere : Action
{
    private ResourceType _resourceType;
    public ResourceType ResourceType
    {
        get => _resourceType;
        set
        {
            if (value == ResourceType.Food)
                throw new AppException("Mined resource type cannot be food", 400);
            _resourceType = value;
        }
    }
}
