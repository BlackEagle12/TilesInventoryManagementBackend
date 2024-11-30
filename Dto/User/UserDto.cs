using Newtonsoft.Json;

namespace Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public string Username { get; set; } = null!;

        [JsonIgnore]
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

        public DateTime? AnniversaryDate { get; set; }

        public int RoleId { get; set; }

        public string? Role { get; set; }

        public int CategoryId { get; set; }

        public string? Category { get; set; }

        public DateTime AddedOn { get; set; }

        public DateTime LastUpdatedOn { get; set; }

        public string? Token { get; set; }

        public UserDto(){}

        [JsonConstructor]
        public UserDto(int id,
                       string email,
                       string username,
                       string password,
                       string firstName,
                       string lastName,
                       string phoneNo,
                       string address1,
                       string address2,
                       int countryId,
                       string country,
                       int stateId,
                       string state,
                       string city,
                       string pincode,
                       string summary,
                       DateTime? birthDate,
                       DateTime? anniversaryDate,
                       int roleId,
                       string role,
                       int categoryId,
                       string category)
        {
            Id = id;
            Email = email;
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            PhoneNo = phoneNo;
            Address1 = address1;
            Address2 = address2;
            CountryId = countryId;
            Country = country;
            StateId = stateId;
            State = state;
            City = city;
            Pincode = pincode;
            Summary = summary;
            BirthDate = birthDate;
            AnniversaryDate = anniversaryDate;
            RoleId = roleId;
            Role = role;
            CategoryId = categoryId;
            Category = category;
        }
    }
}
