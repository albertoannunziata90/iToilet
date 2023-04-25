using Dapr.Client;
using Dapr.Extensions.Configuration;

using LocatorApi.Repository;

using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureAppConfiguration(config =>
{
    var configurationList = new List<string>() { "locatorDatabaseId", "locatorCollectionId" };
    var daprClient = new DaprClientBuilder().Build();
    config.AddDaprSecretStore("commonsecrets", daprClient, TimeSpan.FromSeconds(10));
    config.AddDaprConfigurationStore("configstore", configurationList, daprClient, TimeSpan.FromSeconds(10));
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDaprClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(x =>
{
    var configuration = x.GetRequiredService<IConfiguration>();
    var connectionStr = configuration["Cosmos-ConnectionString"];
    return new CosmosClient(connectionStr, new CosmosClientOptions
    {
        SerializerOptions = new CosmosSerializationOptions()
        {
            PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
        }
    });
});
builder.Services.AddSingleton<IToiletRepository, ToiletRepository>();
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
