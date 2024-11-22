using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[Table("states")]
[Index("StateName", "CountryId", Name = "uq_states", IsUnique = true)]
public partial class State
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("state_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string StateName { get; set; } = null!;

    [Column("country_id")]
    public int CountryId { get; set; }

    [Column("description")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("added_on")]
    public DateTime AddedOn { get; set; }

    [Column("last_updated_on")]
    public DateTime LastUpdatedOn { get; set; }
}
