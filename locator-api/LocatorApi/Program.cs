using Dapr.Client;
using LocatorApi.Configuration;
using LocatorApi.Repository;

using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDaprClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(async x =>
{
    var daprClient = x.GetRequiredService<DaprClient>();
    var connectionStr = await daprClient.GetSecretAsync("commonsecrets", "Cosmos-ConnectionString");
    return new CosmosClient(connectionStr.First().Value, new CosmosClientOptions
    {
        SerializerOptions = new CosmosSerializationOptions()
        {
            PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
        }
    });
});
builder.Services.AddSingleton(async x =>
{
    var configurationKeyList = new List<string>() { "locatorDatabaseId", "locatorCollectionId" };
    var daprClient = x.GetRequiredService<DaprClient>();
    var configurationList = await daprClient.GetConfiguration("configstore", configurationKeyList);
    return new DatabaseOption()
    {
        CollectionName = configurationList.Items["locatorCollectionId"].Value,
        DatabaseName = configurationList.Items["locatorDatabaseId"].Value,
    };
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
