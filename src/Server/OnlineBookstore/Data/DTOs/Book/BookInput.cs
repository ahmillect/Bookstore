namespace OnlineBookstore.Data.DTOs.Book
{
    public record BookInput
    {
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
