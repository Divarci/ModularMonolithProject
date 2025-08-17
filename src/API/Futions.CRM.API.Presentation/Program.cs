using System.Reflection;
using Futions.CRM.Common.Application;
using Futions.CRM.Common.Infrastructure;
using Futions.CRM.Modules.Catalogue.Infrastructure;
using Futions.CRM.Common.Presentation.Endpoints;
using Serilog;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, LoggerConfiguration) =>
    LoggerConfiguration
        .ReadFrom.Configuration(context.Configuration));

builder.Services.AddOpenApi();

builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();

Assembly[] moduleApplicationAssemblies = [
    Futions.CRM.Modules.Catalogue.Application.AssemblyReference.Assembly];

builder.Services.AddApplication(moduleApplicationAssemblies);

builder.Services.AddInfrastructure();

builder.Services.AddCatalogueModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapEndpoints();

app.UseSerilogRequestLogging();

app.Run();
