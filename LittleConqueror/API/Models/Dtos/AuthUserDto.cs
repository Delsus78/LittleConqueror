namespace LittleConqueror.API.Models.Dtos;

public record AuthUserDto(
    long Id, 
    string Username, 
    string Role, 
    long? UserId);