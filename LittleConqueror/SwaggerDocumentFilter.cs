using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LittleConqueror;

internal class SwaggerDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var apiDescription in context.ApiDescriptions)
        {
            var controllerActionDescriptor = (ControllerActionDescriptor)apiDescription.ActionDescriptor;

            // If the namespace of the controller DOES NOT start with..
            if (!controllerActionDescriptor.ControllerTypeInfo.FullName.StartsWith("MyAssemblyNamespace"))
            {
                var key = "/" + apiDescription.RelativePath.TrimEnd('/');
                swaggerDoc.Paths.Remove(key); // Hides the Api
            }
        }
    }
}