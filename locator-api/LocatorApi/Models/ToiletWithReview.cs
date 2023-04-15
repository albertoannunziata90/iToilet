using LocatorApi.Entities;

namespace LocatorApi.Models
{
    public class ToiletWithReview : Toilet
    {
        public ToiletWithReview()
        {
            Reviews = Enumerable.Empty<Review>();
        }
        public ToiletWithReview(Toilet toilet)
        {
            Reviews = Enumerable.Empty<Review>();
            Id = toilet.Id;
            Name = toilet.Name;
            Point = toilet.Point;
            Photo = toilet.Photo;
            Address = toilet.Address;

        }

        public IEnumerable<Review>? Reviews { get; set; }


    }
}
