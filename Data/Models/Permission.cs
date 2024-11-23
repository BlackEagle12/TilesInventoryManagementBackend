using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

[Table("permissions")]
[Index("PermissionName", Name = "UQ__permissi__81C0F5A20AD36F3A", IsUnique = true)]
public partial class Permission
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("permission_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string PermissionName { get; set; } = null!;

    [Column("description")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("added_on")]
    public DateTime AddedOn { get; set; }

    [Column("last_updated_on")]
    public DateTime LastUpdatedOn { get; set; }
}
