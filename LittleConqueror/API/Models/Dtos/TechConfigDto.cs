namespace LittleConqueror.API.Models.Dtos;

public record TechConfigDto
{
    public string Name { get; init; }
    public string Description { get; init; }
    public int Cost { get; init; }
    public int ResearchTime { get; init; }
    public string Category { get; init; }
    public List<string> PreReqs { get; init; }
    public string Type { get; init; }
}