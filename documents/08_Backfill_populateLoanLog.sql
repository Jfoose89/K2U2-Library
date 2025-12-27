-- Log all existing active loans
INSERT INTO LoanLog (LoanID, BookID, MemberID, ActionType, ActionDate)
SELECT LoanID, BookID, MemberID, 
       CASE WHEN ReturnDate IS NULL THEN 'Loan' ELSE 'Return' END,
       GETDATE()
FROM Loan;
GO
