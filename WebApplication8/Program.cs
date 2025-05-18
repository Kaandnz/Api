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
builder.Services.AddOpenApi();

var app = builder.Build();


    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "api");
    });


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();