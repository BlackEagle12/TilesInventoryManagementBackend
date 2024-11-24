using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

[Table("roles")]
[Index("RoleName", Name = "UQ__roles__783254B171EF73C7", IsUnique = true)]
public partial class Role
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("role_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string RoleName { get; set; } = null!;

    [Column("description")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("is_default")]
    public bool IsDefault { get; set; }

    [Column("added_on")]
    public DateTime AddedOn { get; set; }

    [Column("last_updated_on")]
    public DateTime LastUpdatedOn { get; set; }
}
