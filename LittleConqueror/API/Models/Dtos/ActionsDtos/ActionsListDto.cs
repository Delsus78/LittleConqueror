namespace LittleConqueror.API.Models.Dtos.ActionsDtos;

public class ActionsListDto
{
    public int TotalActions { get; set; }
    public IEnumerable<ActionWithCityDto<ActionDto>> Actions { get; set; }
}
