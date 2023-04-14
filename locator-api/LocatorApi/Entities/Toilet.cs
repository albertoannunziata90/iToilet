using Microsoft.Azure.Cosmos.Spatial;

namespace LocatorApi.Entities
{
    public class Toilet
    {
        public Guid Id { get; set; }

        public string? Address { get; set; }

        public string? Name { get; set; }

        public string? Photo { get; set; }

        public string? City { get; set; }

        public Point? Point { get; set; }
    }
}
