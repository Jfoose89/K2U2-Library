USE K2U2Library;
GO

SELECT 
    b.BookID,
    b.Title,
    b.CopiesTotal,
    b.CopiesAvailable,
    COUNT(l.LoanID) AS CopiesLoanedOut
FROM Book b
LEFT JOIN Loan l 
    ON b.BookID = l.BookID
    AND l.ReturnDate IS NULL -- only count active loans
GROUP BY 
    b.BookID, b.Title, b.CopiesTotal, b.CopiesAvailable
ORDER BY b.BookID;
