using BiblioTech.Application.Applications;
using BiblioTech.Application.Interfaces;
using BiblioTech.Domain.Interface.Services;
using BiblioTech.Domain.Interfaces.Repositories;
using BiblioTech.Domain.Services;
using BiblioTech.Interfaces.Repositories;
using BiblioTech.Interfaces.Repositories.DataConnector;
using BiblioTech.Infra.DataConnector;
using BiblioTech.Infra.Repositories;
using BiblioTech.Application.Mapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Core));
builder.Services.AddControllers();

string connectionString = builder.Configuration.GetConnectionString("default");

builder.Services.AddScoped<IDbConnector>(db => new SqlConnector(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IUserApplication, UserApplication>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
