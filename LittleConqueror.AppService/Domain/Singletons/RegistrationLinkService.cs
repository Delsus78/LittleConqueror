namespace LittleConqueror.AppService.Domain.Singletons;

public interface IRegistrationLinkService
{
    public string CreateRegistrationLink(string role, int firstCityId);
    public (bool valid, string role, int firstCardId) GetLinkRelatedData(string link);
}
public class RegistrationLinkService : IRegistrationLinkService
{
    private readonly Dictionary<string, (string role, int firstCardId)> _validLinks = new();

    public string CreateRegistrationLink(string role, int firstCityId)
    {
        // generate a random string of 20 characters
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var link = new string(Enumerable.Repeat(chars, 20)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        
        _validLinks.Add(link, (role, firstCityId));
        return link;
    }

    public (bool valid, string role, int firstCardId) GetLinkRelatedData(string link) 
        => !_validLinks.TryGetValue(link, out var value) ? (false, "", 0) : (true, value.role, value.firstCardId);
}