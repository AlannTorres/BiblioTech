using BiblioTech.Api.Extensions;
using BiblioTech.Application.Mapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterIoC(builder);

builder.Services.AddAutoMapper(typeof(Core));
builder.Services.AddControllers();

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
