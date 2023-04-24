using Microsoft.Azure.Cosmos;

using ReviewApi.Entities;

namespace ReviewApi.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly Container _container;

        public ReviewRepository(CosmosClient cosmosClient, IConfiguration configuration)
        {
            _container = cosmosClient.GetContainer(configuration["reviewDatabaseID"], configuration["reviewCollectionId"]);
        }


        public async Task<Review> AddReviewAsync(Review review, CancellationToken cancellationToken)
        {
            await _container.CreateItemAsync(review, new PartitionKey(review.ToiletId.ToString()), cancellationToken: cancellationToken);
            return review;
        }

        public async Task<IEnumerable<Review>> GetReviewsAsync(string toiletId, CancellationToken cancellationToken)
        {

            try
            {
                var feedIterator = _container.GetItemQueryIterator<Review>("SELECT * FROM t", requestOptions: new QueryRequestOptions()
                {
                    PartitionKey = new PartitionKey(toiletId)

                });
                var list = new List<Review>();

                while (feedIterator.HasMoreResults)
                {
                    list.AddRange(await feedIterator.ReadNextAsync(cancellationToken));
                }

                return list;
            }
            catch (CosmosException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return Enumerable.Empty<Review>();
                }
                throw;
            }

        }

    }
}
