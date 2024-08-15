using LittleConqueror.AppService.Domain.Models;

namespace LittleConqueror.AppService.Domain.Singletons;

public interface ITemporaryCodeService
{
    public string CreateRegistrationLink(RegistrationLinkData data);
    public string CreateForgetPasswordLink(ForgetPasswordLinkData data);
    public T? ConsumeLinkRelatedData<T>(string link) where T : class;
}
public class TemporaryCodeService : ITemporaryCodeService
{
    private readonly Dictionary<string, RegistrationLinkData> _registrationLinks = new();
    private readonly Dictionary<string, ForgetPasswordLinkData> _forgetPasswordLinks = new();

    public string CreateRegistrationLink(RegistrationLinkData data)
    {
        var link = GenerateLink();
        data.Valid = true;
        
        _registrationLinks.Add(link, data);
        return link;
    }
    public string CreateForgetPasswordLink(ForgetPasswordLinkData data)
    {
        var link = GenerateLink();
        data.Valid = true;
        
        _forgetPasswordLinks.Add(link, data);
        return link;
    }

    public T? ConsumeLinkRelatedData<T>(string link) where T : class
    {
        T? linkData;
        switch (typeof(T))
        {
            case { } t when t == typeof(RegistrationLinkData):
            {
                linkData = (!_registrationLinks.TryGetValue(link, out var value)
                    ? new RegistrationLinkData { Valid = false }
                    : value) as T;
                _registrationLinks.Remove(link);
                break;
            }
            case { } t when t == typeof(ForgetPasswordLinkData):
            {
                linkData = (!_forgetPasswordLinks.TryGetValue(link, out var value)
                    ? new ForgetPasswordLinkData() { Valid = false }
                    : value) as T;
                _forgetPasswordLinks.Remove(link);
                break;
            }
            default:
                throw new ArgumentException("Invalid type");
        }
        
        return linkData;
    }
    
    private string GenerateLink()
    {
        // generate a random string of 20 characters
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, 20)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}