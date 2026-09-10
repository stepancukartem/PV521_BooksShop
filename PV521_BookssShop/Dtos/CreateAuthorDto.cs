namespace PV521_BookssShop.Dtos
{
    public class CreateAuthorDto
    {
        public string Name { get; set; } = string.Empty;

        public string? Biography { get; set; }

        public string? Image { get; set; }

        public string? Country { get; set; }

        public DateTime BirthDate { get; set; }
    }
}