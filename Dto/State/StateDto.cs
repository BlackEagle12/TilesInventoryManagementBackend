
namespace Dto
{
    public class StateDto
    {
        public int Id { get; set; }

        public string StateName { get; set; } = null!;

        public int CountryId { get; set; }

        public string? Description { get; set; }

        public DateTime AddedOn { get; set; }

        public DateTime LastUpdatedOn { get; set; }
    }
}
