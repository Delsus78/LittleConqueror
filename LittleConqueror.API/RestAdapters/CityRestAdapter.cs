using System.Net.Mime;
using LittleConqueror.AppService.Ports;
using Microsoft.AspNetCore.Mvc;

namespace LittleConqueror.API.RestAdapters;

[ApiController]
[Route("api/cities")]
[Produces(MediaTypeNames.Application.Json)]
public class CityRestAdapter(ICityRepository cityRepository) : ControllerBase
{
    private readonly ICityRepository _cityRepository = cityRepository;
    
}