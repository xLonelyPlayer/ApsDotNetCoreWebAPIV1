using SuperHeroAPI;
using SuperHeroAPI.Data;
using SuperHeroAPI.Repositories;
using SuperHeroAPI.Repositories.Interfaces;
using SuperHeroAPI.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<SuperHeroDatabaseSettings>(builder.Configuration.GetSection("SuperHeroDatabaseSettings"));
builder.Services.AddSingleton<SuperHero>();
builder.Services.AddScoped<ISuperHeroRespository, SuperHeroRepositoryMongoDB>();
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
