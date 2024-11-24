using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[Table("users")]
[Index("Email", Name = "UQ__users__AB6E6164DB1CCD15", IsUnique = true)]
[Index("PhoneNo", Name = "UQ__users__E6BE36DCEFB2259A", IsUnique = true)]
[Index("Username", Name = "UQ__users__F3DBC5728C2D5A53", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("email")]
    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("username")]
    [StringLength(100)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [Column("password")]
    [StringLength(50)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [Column("first_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [Column("phone_no")]
    [StringLength(20)]
    [Unicode(false)]
    public string PhoneNo { get; set; } = null!;

    [Column("address1")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Address1 { get; set; } = null!;

    [Column("address2")]
    [StringLength(1000)]
    [Unicode(false)]
    public string Address2 { get; set; } = null!;

    [Column("country_id")]
    public int CountryId { get; set; }

    [Column("state_id")]
    public int StateId { get; set; }

    [Column("city")]
    [StringLength(500)]
    [Unicode(false)]
    public string City { get; set; } = null!;

    [Column("pincode")]
    [StringLength(20)]
    [Unicode(false)]
    public string Pincode { get; set; } = null!;

    [Column("summary")]
    [StringLength(2000)]
    [Unicode(false)]
    public string? Summary { get; set; }

    [Column("birth_date")]
    public DateTime? BirthDate { get; set; }

    [Column("aniversary_date")]
    public DateTime? AniversaryDate { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("added_on")]
    public DateTime AddedOn { get; set; }

    [Column("last_updated_on")]
    public DateTime LastUpdatedOn { get; set; }
}
