using IntelligentTaskAgent.Repositories.Configurations;
using IntelligentTaskAgent.Repositories.Extensions;
using IntelligentTaskAgent.Core.Extensions;
using IntelligentTaskAgent.Infrastructure.Extensions;
using IntelligentTaskAgent.Application.Extensions;
using IntelligentTaskAgent.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ConfigureServices(builder.Configuration);
//added service extension for core and repositoroes
//builder.Services.AddCoreServices();
//builder.Services.AddRepositoryServices(builder.Configuration);
//builder.Services.AddAppServices();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

// added service extension IOptions
//builder.Services.Configure<DatabaseOptions>(
//    builder.Configuration.GetSection(DatabaseOptions.SectionName));

//builder.Services.Configure<OllamaOptions>(
//    builder.Configuration.GetSection(OllamaOptions.SectionName)); 

// builder.Services.AddInfrastructure(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
