using Dapr.Client;

using Microsoft.Azure.Cosmos;
using ReviewApi.Configuration;
using ReviewApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddDapr();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

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
    var configurationKeyList = new List<string>() { "reviewDatabaseID", "reviewCollectionId" };
    var daprClient = x.GetRequiredService<DaprClient>();
    var configurationList = await daprClient.GetConfiguration("configstore", configurationKeyList);
    return new DatabaseOption()
    {
        CollectionName = configurationList.Items["reviewCollectionId"].Value,
        DatabaseName = configurationList.Items["reviewDatabaseID"].Value,
    };
});

builder.Services.AddSingleton<IReviewRepository, ReviewRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();



