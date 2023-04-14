using ReviewApi.Entities;

namespace ReviewApi.Repository
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetReviewsAsync(string toiletId, CancellationToken cancellationToken);
       
        Task<Review> AddReviewAsync(Review review, CancellationToken cancellationToken);
    }
}
