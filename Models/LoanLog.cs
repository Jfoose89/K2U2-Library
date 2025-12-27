using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace K2U2Library.Models;

[Table("LoanLog")]
public partial class LoanLog
{
    [Key]
    [Column("LogID")]
    public int LogId { get; set; }

    [Column("LoanID")]
    public int? LoanId { get; set; }

    [Column("BookID")]
    public int? BookId { get; set; }

    [Column("MemberID")]
    public int? MemberId { get; set; }

    [StringLength(20)]
    public string? ActionType { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ActionDate { get; set; }
}
