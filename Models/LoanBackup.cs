using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace K2U2Library.Models;

[Keyless]
[Table("LoanBackup")]
public partial class LoanBackup
{
    [Column("LoanID")]
    public int LoanId { get; set; }

    [Column("BookID")]
    public int BookId { get; set; }

    [Column("MemberID")]
    public int MemberId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LoanDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DueDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReturnDate { get; set; }
}
