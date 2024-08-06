namespace LittleConqueror.API.Models.Dtos;

public record AuthUserDto(
    int Id, 
    string Username, 
    string Role, 
    int? UserId);