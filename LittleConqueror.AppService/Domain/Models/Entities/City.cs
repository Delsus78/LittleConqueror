using LittleConqueror.AppService.Domain.Logic;
using LittleConqueror.AppService.Domain.Logic.ActionsHelpers;
using LittleConqueror.AppService.Domain.Models.Entities.Base;
using Newtonsoft.Json.Linq;

namespace LittleConqueror.AppService.Domain.Models.Entities;

public class City : Entity
{
    public required char OsmType { get; set; } = 'R';
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? AddressType { get; init; }
    
    public JToken Geojson { get; set; }
    public int Population { get; set; }
    
    // n:1 relationship
    public long? TerritoryId { get; set; }
    public Territory? Territory { get; set; }
    
    // 1:1 relationship
    public virtual ActionEntities.Action? Action { get; set; }

    public double? AgriculturalFertility
        => AgricoleExpressions.GetAgriculturalFertilityExpression(GeoProceduralConfigs.BaseFertility).Compile()
            .Invoke(this);

}
