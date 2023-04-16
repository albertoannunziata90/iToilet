using Dapr.Client;
using Dapr.Extensions.Configuration;

using Microsoft.Azure.Cosmos;

using ReviewApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.ConfigureAppConfiguration(config =>
{
    var configurationList = new List<string>() { "reviewDatabaseID", "reviewCollectionId" };
    var daprClient = new DaprClientBuilder()
    .Build();
    config.AddDaprSecretStore("commonsecrets", daprClient);
    config.AddDaprConfigurationStore("configstore", configurationList, daprClient, TimeSpan.FromSeconds(10));

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
    var connectionStr = configuration["Cosmos_ConnectionString"];

    return new CosmosClient(connectionStr);
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
