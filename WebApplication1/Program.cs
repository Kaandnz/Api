using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using WebApplication8.Models;
using WebApplication8.Repository;
using WebApplication8.Settings;

var builder = WebApplication.CreateBuilder(args);


var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
builder.Services.AddSingleton(mongoDbSettings);
builder.Services.AddSingleton<IMongoClient>(new MongoClient(mongoDbSettings.ConnectionString));
builder.Services.AddScoped<IIcecekRepository, IcecekRepository>();
builder.Services.AddScoped<IYemekRepository, YemekRepository>();
builder.Services.AddScoped<IGarsonRepository, GarsonRepository>();
builder.Services.AddScoped<IGenericRepository<Garson>, GenericRepository<Garson>>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseAuthorization();
app.MapControllers();
app.Run();

