using System;
using System.Reflection;
using System.Threading.Tasks;
using Asp.Versioning;
using Basket.Module;
using Carter;
using Catalog.Module;
using Discount.Module;
using Framework.Infrastructure;
using Meadow_Framework.Core.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
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
builder.Services.AddDiscountModule(builder.Configuration);

// Configure Serilog early
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console();

    if (context.Configuration.GetValue<bool>("Elastic:Enabled"))
    {
        configuration.WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(
            new Uri(context.Configuration["Elastic:Uri"]))
        {
            AutoRegisterTemplate = true,
            IndexFormat = context.Configuration["Elastic:Index"]
        });
    }
});



builder.Services.AddSerilogServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();

//common services: carter, mediatr, fluentvalidation, masstransit
var catalogAssembly = typeof(Catalog.Module.DependencyInjection).Assembly;
var basketAssembly = typeof(Basket.Module.DependencyInjection).Assembly;
var orderingAssembly = typeof(Ordering.Module.DependencyInjection).Assembly;
var discountAssembly = typeof(Discount.Module.DependencyInjection).Assembly;

builder.Services.AddCarterWithAssemblies(orderingAssembly, basketAssembly, catalogAssembly, discountAssembly);


builder.Services.AddFramework(
    builder.Configuration,
    true,
    typeof(Basket.Application.DependencyInjection).Assembly,
    typeof(Ordering.Application.DependencyInjection).Assembly,
    typeof(Discount.Application.DependencyInjection).Assembly,
    typeof(Catalog.Application.DependencyInjection).Assembly);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    // options.OperationFilter<SwaggerDefaultValues>();
    options.CustomSchemaIds(type => type.FullName);
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

if (app.Environment.IsDevelopment())
{
    app.MapPrometheusScrapingEndpoint();
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();

        // Build a swagger endpoint for each discovered API version
        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });
    app.ApplyMigration();
}
app.UseErrorHandling();
// app.UseAuthentication();
// app.UseAuthorization();
app.MapGet("/", () => Task.FromResult(DateTime.UtcNow));
app.MapCarter();


app.Run();