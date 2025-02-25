namespace API.Models
{
    public class User
    {
        public int Id { get; set; }
        public required String Username { get; set; }
        public required Byte[] Password { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
