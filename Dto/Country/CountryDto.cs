
namespace Dto
{
    public class CountryDto
    {
        public int Id { get; set; }

        public string CountryName { get; set; } = null!;

        public string CountryCode { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime AddedOn { get; set; }

        public DateTime LastUpdatedOn { get; set; }
    }
}
