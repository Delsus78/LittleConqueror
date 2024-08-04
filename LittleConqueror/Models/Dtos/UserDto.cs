using System.ComponentModel.DataAnnotations;

namespace LittleConqueror.Models.Dtos;

public record UserDto([Required] string Name);