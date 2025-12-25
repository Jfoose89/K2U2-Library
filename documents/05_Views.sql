USE K2U2Library;
GO

CREATE OR ALTER VIEW vw_ActiveLoans
AS
SELECT
    l.LoanID,
    l.BookID,
    b.Title AS BookTitle,
    b.Author AS BookAuthor,
    l.MemberID,
    m.FirstName + ' ' + m.LastName AS MemberName,
    m.Email AS MemberEmail,
    l.LoanDate,
    l.DueDate
FROM Loan l
INNER JOIN Book b
    ON l.BookID = b.BookID
INNER JOIN Member m
    ON l.MemberID = m.MemberID
WHERE l.ReturnDate IS NULL;
GO
