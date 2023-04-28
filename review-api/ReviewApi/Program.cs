using Dapr.Client;
using Dapr.Extensions.Configuration;

using Microsoft.Azure.Cosmos;

using ReviewApi.Repository;
using ReviewApi.Utils;

StartupHelper.WaitForDapr();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.ConfigureAppConfiguration((hostcontext, config) =>
{
    var configurationList = new List<string>() { "reviewDatabaseID", "reviewCollectionId" };
    var daprClient = new DaprClientBuilder()
    .Build();
    config.AddDaprSecretStore("commonsecrets", daprClient, TimeSpan.FromSeconds(30));
    config.AddDaprConfigurationStore("configstore", configurationList, daprClient, TimeSpan.FromSeconds(30));

});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDaprClient();
builder.Services.AddHttpContextAccessor();

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
builder.Services.AddSingleton<IReviewRepository, ReviewRepository>(); ;

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



