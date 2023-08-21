using BiblioTech.Api.Extensions;
using BiblioTech.Application.Mapper;
using BiblioTech.Infra.DataConnector;
using BiblioTech.Interfaces.Repositories.DataConnector;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Core));
builder.Services.AddControllers();

string connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddScoped<IDbConnector>(db => new SqlConnector(connectionString));

builder.Services.RegisterIoC();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.SwaggerConfiguration();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
