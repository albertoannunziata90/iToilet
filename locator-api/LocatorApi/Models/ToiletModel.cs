namespace LocatorApi.Models
{
    public class ToiletModel
    {
        public ToiletModel()
        {
            Point = new GeoPoint();
        }

        public Guid Id { get; set; }

        public string? Address { get; set; }

        public string? Name { get; set; }

        public string? Photo { get; set; }

        public string? City { get; set; }

        public GeoPoint Point { get; set; }

    }

    public class GeoPoint
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
