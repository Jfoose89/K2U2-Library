using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace K2U2Library.Models;

[Keyless]
public partial class VwMostFrequentBook
{
    [Column("BookID")]
    public int BookId { get; set; }

    [StringLength(255)]
    public string Title { get; set; } = null!;

    public int? TimesBorrowed { get; set; }
}
