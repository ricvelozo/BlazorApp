namespace SimpleServer.Domain.Users
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
