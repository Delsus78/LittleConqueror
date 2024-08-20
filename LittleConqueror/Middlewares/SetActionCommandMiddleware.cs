using System.Text;
using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;
using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands.Agricole;
using LittleConqueror.AppService.Exceptions;
using Newtonsoft.Json;

namespace LittleConqueror.Middlewares;

public class SetActionCommandMiddleware
{
    private readonly RequestDelegate _next;

    public SetActionCommandMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/api/Cities/setAction"))
        {
            var actionTypeStr = context.Request.Query["actionType"][0];
            if (!Enum.TryParse<ActionType>(actionTypeStr, out var actionType))
                throw new AppException("Unsupported action type", 400);
            
            var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();

            SetActionToCityCommand? command = actionType switch
            {
                ActionType.Agricole => JsonConvert.DeserializeObject<SetActionAgricoleToCityCommand>(requestBody),
                ActionType.Miniere => throw new NotImplementedException("Implement Action in SetActionCommandMiddleware"),
                ActionType.Militaire => throw new NotImplementedException("Implement Action in SetActionCommandMiddleware"),
                ActionType.Espionnage => throw new NotImplementedException("Implement Action in SetActionCommandMiddleware"),
                ActionType.Diplomatique => throw new NotImplementedException("Implement Action in SetActionCommandMiddleware"),
                ActionType.Technologique => throw new NotImplementedException("Implement Action in SetActionCommandMiddleware"),
                _ => throw new AppException("Unsupported action type", 400)
            };

            context.Items["DeserializedCommand"] = command;
            context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(requestBody)); // Réinitialiser le stream du body
        }

        await _next(context);
    }
}