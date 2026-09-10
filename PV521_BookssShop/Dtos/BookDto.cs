namespace PV521_BookssShop.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Image { get; set; }

        public int Rating { get; set; }

        public decimal Price { get; set; }

        public int Pages { get; set; }

        public int Year { get; set; }
    }
}