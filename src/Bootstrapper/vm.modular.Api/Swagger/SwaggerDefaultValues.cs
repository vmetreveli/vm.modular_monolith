using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace vm.modular.Api.Swagger;

public class SwaggerDefaultValues : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var apiDescription = context.ApiDescription;

        foreach (var responseType in context.ApiDescription.SupportedResponseTypes)
        {
            var responseKey = responseType.IsDefaultResponse ? "default" : responseType.StatusCode.ToString();
            var response = operation.Responses[responseKey];

            foreach (var contentType in response.Content.Keys)
                if (responseType.ApiResponseFormats.All(x => x.MediaType != contentType))
                    response.Content.Remove(contentType);
        }

        if (operation.Parameters == null) return;

        foreach (var openApiParameter in operation.Parameters)
        {
            var parameter = (OpenApiParameter)openApiParameter;
            var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

            parameter.Description ??= description.ModelMetadata?.Description;

            if (parameter.Schema is OpenApiSchema concreteSchema &&
                concreteSchema.Default == null &&
                description.DefaultValue != null &&
                description.DefaultValue is not DBNull &&
                description.ModelMetadata is ModelMetadata modelMetadata)
            {
                var json = JsonSerializer.Serialize(description.DefaultValue, modelMetadata.ModelType);
                concreteSchema.Default = JsonNode.Parse(json);
            }

            parameter.Required |= description.IsRequired;
        }
    }
}