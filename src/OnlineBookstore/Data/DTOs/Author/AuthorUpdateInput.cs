namespace OnlineBookstore.Data.DTOs.Author
{
    public record AuthorUpdateInput
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
    }
}
