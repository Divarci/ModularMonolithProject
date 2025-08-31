using System.Reflection;
using Futions.CRM.Common.Application;
using Futions.CRM.Common.Infrastructure;
using Futions.CRM.Modules.Catalogue.Infrastructure;
using Futions.CRM.Common.Presentation.Endpoints;
using Serilog;
using Futions.CRM.API.Presentation.Extensions;
using Futions.CRM.API.Presentation.Middleware;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Futions.CRM.Modules.Deals.Infrastructure;
using Futions.CRM.Modules.Users.Infrastructure;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, LoggerConfiguration) =>
    LoggerConfiguration
        .ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Configuration.AddModuleConfiguration(["catalogue", "deals", "users"]);

builder.Services.AddOpenApi();

builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();

Assembly[] moduleApplicationAssemblies = [
    Futions.CRM.Modules.Catalogue.Application.AssemblyReference.Assembly,
    Futions.CRM.Modules.Deals.Application.AssemblyReference.Assembly,
    Futions.CRM.Modules.Users.Application.AssemblyReference.Assembly];

builder.Services.AddApplication(moduleApplicationAssemblies);
builder.Services.AddInfrastructure(
    [
        DealsModule.ConfigureConsumers,
        CatalogueModule.ConfigureConsumers
    ]);

string connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddCatalogueModule(connectionString!);
builder.Services.AddDealModule(connectionString!);
builder.Services.AddUsersModule(connectionString!, builder.Configuration);

builder.Services.AddHealthChecks()
    .AddSqlServer(connectionString!)
    .AddUrlGroup(new Uri(builder.Configuration.GetValue<string>("KeyCloak:HealthUrl")!), HttpMethod.Get, "keycloack");

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapEndpoints();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization(); 

app.Run();
