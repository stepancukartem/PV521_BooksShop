namespace PV521_BookssShop.Dtos
{
    public class GenreDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<int> BookIds { get; set; } = [];
    }
}