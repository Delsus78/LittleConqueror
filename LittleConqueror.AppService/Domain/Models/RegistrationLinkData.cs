namespace LittleConqueror.AppService.Domain.Models;

public class RegistrationLinkData
{
    public bool Valid { get; set; }
    public string Role { get; set; }
    public int FirstOsmId { get; set; }
    public char FirstOsmType { get; set; }
}