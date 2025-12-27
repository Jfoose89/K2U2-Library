using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace K2U2Library.Models;

public partial class K2U2LibraryContext : DbContext
{
    public K2U2LibraryContext()
    {
    }

    public K2U2LibraryContext(DbContextOptions<K2U2LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<LoanBackup> LoanBackups { get; set; }

    public virtual DbSet<LoanLog> LoanLogs { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<VwActiveLoan> VwActiveLoans { get; set; }

    public virtual DbSet<VwMemberBorrowingHistory> VwMemberBorrowingHistories { get; set; }

    public virtual DbSet<VwMostFrequentBook> VwMostFrequentBooks { get; set; }

    public virtual DbSet<VwOverdueLoan> VwOverdueLoans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=K2U2Library;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Book__3DE0C227E3671279");
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.LoanId).HasName("PK__Loan__4F5AD4378BE118F3");

            entity.ToTable("Loan", tb =>
                {
                    tb.HasTrigger("trg_DecreaseCopiesOnLoan");
                    tb.HasTrigger("trg_IncreaseCopiesOnReturn");
                    tb.HasTrigger("trg_LogLoanActions");
                });

            entity.HasIndex(e => e.DueDate, "IX_Loan_DueDate_ReturnDate").HasFilter("([ReturnDate] IS NULL)");

            entity.HasIndex(e => e.MemberId, "IX_Loan_MemberID_ReturnDate").HasFilter("([ReturnDate] IS NULL)");

            entity.Property(e => e.LoanDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Book).WithMany(p => p.Loans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Loan_Book");

            entity.HasOne(d => d.Member).WithMany(p => p.Loans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Loan_Member");
        });

        modelBuilder.Entity<LoanBackup>(entity =>
        {
            entity.Property(e => e.LoanId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<LoanLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__LoanLog__5E5499A81F9CCF52");

            entity.Property(e => e.ActionDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Member__0CF04B3899FAB10E");
        });

        modelBuilder.Entity<VwActiveLoan>(entity =>
        {
            entity.ToView("vw_ActiveLoans");
        });

        modelBuilder.Entity<VwMemberBorrowingHistory>(entity =>
        {
            entity.ToView("vw_MemberBorrowingHistory");
        });

        modelBuilder.Entity<VwMostFrequentBook>(entity =>
        {
            entity.ToView("vw_MostFrequentBooks");
        });

        modelBuilder.Entity<VwOverdueLoan>(entity =>
        {
            entity.ToView("vw_OverdueLoans");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
