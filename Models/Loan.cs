using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace K2U2Library.Models;

[Table("Loan")]
[Index("BookId", Name = "IX_Loan_BookID")]
public partial class Loan
{
    [Key]
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

    [ForeignKey("BookId")]
    [InverseProperty("Loans")]
    public virtual Book Book { get; set; } = null!;

    [ForeignKey("MemberId")]
    [InverseProperty("Loans")]
    public virtual Member Member { get; set; } = null!;
}
