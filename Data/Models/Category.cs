using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[Table("categories")]
[Index("CategoryName", Name = "UQ__categori__5189E2553211966E", IsUnique = true)]
public partial class Category
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("category_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string CategoryName { get; set; } = null!;

    [Column("description")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("added_on")]
    public DateTime AddedOn { get; set; }

    [Column("last_updated_on")]
    public DateTime LastUpdatedOn { get; set; }
}
