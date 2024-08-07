using LittleConqueror.AppService.Domain.Models;

namespace LittleConqueror.AppService.Domain.Singletons;

public interface IRegistrationLinkService
{
    public string CreateRegistrationLink(RegistrationLinkData data);
    public RegistrationLinkData ConsumeLinkRelatedData(string link);
}
public class RegistrationLinkService : IRegistrationLinkService
{
    private readonly Dictionary<string, RegistrationLinkData> _validLinks = new();

    public string CreateRegistrationLink(RegistrationLinkData data)
    {
        // generate a random string of 20 characters
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var link = new string(Enumerable.Repeat(chars, 20)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        
        data.Valid = true;
        
        _validLinks.Add(link, data);
        return link;
    }

    public RegistrationLinkData ConsumeLinkRelatedData(string link)
    {
        var linkData = !_validLinks.TryGetValue(link, out var value) ? new RegistrationLinkData { Valid = false } : value;
        _validLinks.Remove(link);
        return linkData;
    }
}