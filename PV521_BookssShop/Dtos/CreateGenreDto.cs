namespace PV521_BookssShop.Dtos
{
    public class CreateGenreDto
    {
        public string Name { get; set; } = string.Empty;

        public List<int> BookIds { get; set; } = [];
    }
}