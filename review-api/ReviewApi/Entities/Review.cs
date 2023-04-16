namespace ReviewApi.Entities
{
    public class Review
    {
        public Guid Id { get; set; }

        public string? AuthorName { get; set; }

        public string? ReviewMessage { get; set; }

        public int Star { get; set; }

        public Guid ToiletId { get; set; }

    }
}
