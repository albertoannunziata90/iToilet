namespace NotificationHandler.Entities
{
    public class Users
    {

        public Users()
        {
            Toilets = new List<Guid>();
        }

        public string? Email { get; set; }

        public List<Guid> Toilets { get; set; }

    }
}
