namespace LocatorApi.Entities
{
    public class Review
    {
        public Review()
        {

        }

        public Guid Id { get; set; }

        public string? AuthorName { get; set; }

        public string? ReviewMessage { get; set; }

        public int Star { get; set; }


    }
}
