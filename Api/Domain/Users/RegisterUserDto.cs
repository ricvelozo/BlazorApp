namespace Api.Domain.Users
{
    public class RegisterUserDto
    {
        public required String Username { get; set; }
        public required String Password { get; set; }
    }
}
