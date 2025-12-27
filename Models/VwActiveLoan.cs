using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace K2U2Library.Models;

[Keyless]
public partial class VwActiveLoan
{
    [Column("LoanID")]
    public int LoanId { get; set; }

    [StringLength(255)]
    public string BookTitle { get; set; } = null!;

    [StringLength(255)]
    public string Author { get; set; } = null!;

    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    public string LastName { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime LoanDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DueDate { get; set; }
}
