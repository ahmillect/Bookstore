namespace OnlineBookstore.Data.DTOs.User
{
    public record UserInput
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
