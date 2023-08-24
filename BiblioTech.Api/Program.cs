using BiblioTech.Api.Extensions;
using BiblioTech.Application.Mapper;
using BiblioTech.Application.Models;
using BiblioTech.Infra.DataConnector;
using BiblioTech.Interfaces.Repositories.DataConnector;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Core));

builder.Services.AddControllers();

// Connection Database
string connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddScoped<IDbConnector>(db => new SqlConnector(connectionString));

// Settings JWT
var authSettingsSection = builder.Configuration.GetSection("AuthSettings");
builder.Services.Configure<AuthSettings>(authSettingsSection);

var authSettings = authSettingsSection.Get<AuthSettings>();
SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Secret));

builder.Services.JWT(key);

// Registrar Ioc
builder.Services.RegisterIoC();

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.SwaggerConfiguration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setup =>
    {
        setup.RoutePrefix = "swagger";
        setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Documentation");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
