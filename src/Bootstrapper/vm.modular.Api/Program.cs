using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Basket.Module;
using Carter;
using Catalog.Module;
using Meadow_Framework.Core.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Ordering.Module;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using vm.modular.Api;
using vm.modular.Api.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureOpenTelemetry();

builder.Services.AddCatalogModule(builder.Configuration);
builder.Services.AddOrderingModule(builder.Configuration);
builder.Services.AddBasketModule(builder.Configuration);

builder.Services.AddSerilogServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();

//common services: carter, mediatr, fluentvalidation, masstransit
Assembly catalogAssembly = typeof(Catalog.Module.DependencyInjection).Assembly;
Assembly basketAssembly = typeof(Basket.Module.DependencyInjection).Assembly;
Assembly orderingAssembly = typeof(Ordering.Module.DependencyInjection).Assembly;

builder.Services.AddCarterWithAssemblies(orderingAssembly, basketAssembly, catalogAssembly);


builder.Services.AddFramework(
    builder.Configuration,
    true,
    typeof(Basket.Application.DependencyInjection).Assembly,
    typeof(Ordering.Application.DependencyInjection).Assembly,
    typeof(Catalog.Application.DependencyInjection).Assembly);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.OperationFilter<SwaggerDefaultValues>();
});


builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });


builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    // Add a custom operation filter which sets default values
    options.OperationFilter<SwaggerDefaultValues>();
});


builder.Logging.AddSerilog();
var app = builder.Build();
//app.MapHealthChecks("_health");

// Apply the CORS policy globally

// app.UseCors(options =>
// {
//     options.AllowAnyOrigin()
//         .AllowAnyHeader()
//         .AllowAnyMethod();
// });

//if (app.Environment.IsDevelopment())
{
    app.MapPrometheusScrapingEndpoint();
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        IReadOnlyList<ApiVersionDescription> descriptions = app.DescribeApiVersions();

        // Build a swagger endpoint for each discovered API version
        foreach (ApiVersionDescription description in descriptions)
        {
            string url = $"/swagger/{description.GroupName}/swagger.json";
            string name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });
    app.ApplyMigration(
    [typeof(Catalog.Infrastructure.DependencyInjection).Assembly,
        typeof(Basket.Infrastructure.DependencyInjection).Assembly,
        typeof(Ordering.Infrastructure.DependencyInjection).Assembly]);
}

// app.UseAuthentication();
// app.UseAuthorization();
app.MapGet("/", () => Task.FromResult(DateTime.UtcNow));
app.MapCarter();


app.Run();