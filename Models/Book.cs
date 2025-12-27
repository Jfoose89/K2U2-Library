using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace K2U2Library.Models;

[Table("Book")]
[Index("CopiesAvailable", Name = "IX_Book_CopiesAvailable")]
public partial class Book
{
    [Key]
    [Column("BookID")]
    public int BookId { get; set; }

    [Column("ISBN")]
    [StringLength(20)]
    [Unicode(false)]
    public string Isbn { get; set; } = null!;

    [StringLength(255)]
    public string Title { get; set; } = null!;

    [StringLength(255)]
    public string Author { get; set; } = null!;

    public int PublishedYear { get; set; }

    public int CopiesTotal { get; set; }

    public int CopiesAvailable { get; set; }

    [InverseProperty("Book")]
    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
