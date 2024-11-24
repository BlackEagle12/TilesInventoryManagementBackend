using Dto;

namespace Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string PhoneNo { get; set; } = null!;

        public string Address1 { get; set; } = null!;

        public string Address2 { get; set; } = null!;

        public int CountryId { get; set; }

        public string? Country { get; set; }

        public int StateId { get; set; }

        public string? State { get; set; }

        public string City { get; set; } = null!;

        public string Pincode { get; set; } = null!;

        public string? Summary { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? AniversaryDate { get; set; }

        public int RoleId { get; set; }

        public string? Role { get; set; }

        public int CategoryId { get; set; }
        
        public string? Category { get; set; }

        public DateTime AddedOn { get; set; }

        public DateTime LastUpdatedOn { get; set; }
    }
}
