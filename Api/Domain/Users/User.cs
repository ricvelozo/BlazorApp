namespace Api.Domain.Users
{
    public class User
    {
        public int Id { get; set; }
        public required String Username { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
