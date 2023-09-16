namespace OnlineBookstore.Data.DTOs.User
{
    public record UserLoginInput
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
