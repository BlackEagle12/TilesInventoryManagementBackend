using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[Table("countries")]
[Index("CountryCode", Name = "UQ__countrie__3436E9A5615F7750", IsUnique = true)]
[Index("CountryName", Name = "UQ__countrie__F7018894DEF2D316", IsUnique = true)]
public partial class Country
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("country_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string CountryName { get; set; } = null!;

    [Column("country_code")]
    [StringLength(10)]
    [Unicode(false)]
    public string CountryCode { get; set; } = null!;

    [Column("description")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("added_on")]
    public DateTime AddedOn { get; set; }

    [Column("last_updated_on")]
    public DateTime LastUpdatedOn { get; set; }
}
