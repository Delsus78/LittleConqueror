using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Handlers.AuthHandlers;
using LittleConqueror.AppService.Domain.Handlers.ConfigsHandlers;
using LittleConqueror.AppService.Domain.Models.TechResearches;
using LittleConqueror.AppService.Domain.Models.TechResearches.Configs;
using LittleConqueror.Infrastructure.JwtAdapters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LittleConqueror.API.RestAdapters;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/Authenticate")]
public class AdminRestAdapter(
    ICreateRegistrationLinkHandler createRegistrationLinkHandler,
    ICreateForgetPasswordLinkHandler createForgetPasswordLinkHandler,
    ISetTechResearchConfigHandler setTechResearchConfigHandler,
    ITokenManagerService tokenBlacklist) : ControllerBase
{
    
    [HttpPut("desactivateToken/{userId}")]
    public ActionResult DesactivateTokenJTI(long userId)
    {
        tokenBlacklist.DesactivateTokenJTI(userId);
        return Ok();
    }
    
    [HttpPut("createForgetPasswordLink")]
    public string CreateForgetPasswordLink([FromBody] CreateForgetPasswordLinkCommand model)
        => createForgetPasswordLinkHandler.Handle(model);
    

    [HttpPut("createLink")]
    public string CreateLink(CreateRegistrationLinkCommand model)
        => createRegistrationLinkHandler.Handle(model);
    
    [HttpPut("reloadTechResearchConfigs")]
    public async Task ReloadTechResearchConfigs()
    {
        var techConfigsHardcoded =
            TechResearchesDataDictionariesDeprecated.Values.Select(x => new TechConfig()
            {
                Description = x.Value.description,
                Name = x.Value.name,
                Type = x.Key,
                Category = x.Value.category,
                Cost = x.Value.cost,
                ResearchTime = x.Value.researchTime,
                PreReqs = x.Value.preReqs
            }).ToList();

        await setTechResearchConfigHandler.Handle(new SetTechResearchConfigCommand
        {
            TechConfigs = techConfigsHardcoded
        });
    }
}