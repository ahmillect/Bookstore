namespace OnlineBookstore.Data.DTOs
{
    public record UserLoginInput
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
