﻿using LocatorApi.Entities;

using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Spatial;

namespace LocatorApi.Repository
{
    public class ToiletRepository : IToiletRepository
    {
        private readonly Container container;
        private const int Distance = 1000;

        public ToiletRepository(CosmosClient cosmosClient, IConfiguration configuration)
        {
            container = cosmosClient.GetContainer(configuration["Cosmos:DatabaseId"], configuration["Cosmos:ContainerId"]);
        }

        public Task AddAsync(Toilet toilet, CancellationToken cancellationToken)
        {
            toilet.Id = Guid.NewGuid();
            return container.CreateItemAsync(toilet, cancellationToken: cancellationToken);
        }

        public Task<IEnumerable<Toilet>> GetNearestAsync(string city, double lat, double lon, CancellationToken cancellationToken)
        {
            try
            {
                var ordered = container.GetItemLinqQueryable<Toilet>(true, requestOptions: new QueryRequestOptions() { PartitionKey = new PartitionKey(city) })
                .Where(x => x.Point != null
                && x.Point.Distance(new Point(lon, lat)) < Distance
                )
                .Take(100);

                return Task.FromResult(ordered.ToList().AsEnumerable());
            }
            catch (CosmosException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return Task.FromResult(Enumerable.Empty<Toilet>());
                }
                throw;
            }
        }
    }
}