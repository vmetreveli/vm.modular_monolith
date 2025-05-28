using Carter;
using Catalog.Module;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using vm.modular.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCatalogModule(builder.Configuration);
builder.Services.AddSerilogServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();

//common services: carter, mediatr, fluentvalidation, masstransit
var catalogAssembly = typeof(Catalog.Module.DependencyInjection).Assembly;
// var basketAssembly = typeof(BasketModule).Assembly;
// var orderingAssembly = typeof(OrderingModule).Assembly;

builder.Services
    .AddCarterWithAssemblies(catalogAssembly);

builder.Services.AddFramework(builder.Configuration, typeof(Program).Assembly);

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
    //app.ApplyMigration();
    app.UseDeveloperExceptionPage();

app.UseErrorHandling();

app.MapCarter();


app.Run();