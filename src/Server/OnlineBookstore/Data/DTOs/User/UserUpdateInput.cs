namespace OnlineBookstore.Data.DTOs.User
{
    public record UserUpdateInput
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
