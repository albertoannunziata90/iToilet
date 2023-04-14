using LocatorApi.Entities;

namespace LocatorApi.Repository
{
    public interface IToiletRepository
    {
        Task AddAsync(Toilet toilet, CancellationToken cancellationToken);

        Task<IEnumerable<Toilet>> GetNearestAsync(string city, double lat, double lon, CancellationToken cancellationToken);

    }
}
