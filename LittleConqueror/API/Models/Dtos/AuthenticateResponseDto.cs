namespace LittleConqueror.API.Models.Dtos;

public record AuthenticateResponseDto(AuthUserDto AuthUser, string Token);