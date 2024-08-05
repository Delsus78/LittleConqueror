using System.ComponentModel.DataAnnotations;

namespace LittleConqueror.API.Models.Dtos;

public record UserDto([Required] string Name);