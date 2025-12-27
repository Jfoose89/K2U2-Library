using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace K2U2Library.Models;

[Keyless]
public partial class VwMemberBorrowingHistory
{
    [Column("LoanID")]
    public int LoanId { get; set; }

    [Column("MemberID")]
    public int MemberId { get; set; }

    [StringLength(201)]
    public string MemberName { get; set; } = null!;

    [Column("BookID")]
    public int BookId { get; set; }

    [StringLength(255)]
    public string Title { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime LoanDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DueDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReturnDate { get; set; }
}
