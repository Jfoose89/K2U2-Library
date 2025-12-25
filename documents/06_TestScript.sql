USE K2U2Library;
GO

-- Reset CopiesAvailable for all books
UPDATE Book
SET CopiesAvailable = CopiesTotal;

PRINT 'Book availability reset.';
SELECT BookID, Title, CopiesAvailable, CopiesTotal
FROM Book
ORDER BY Title;
GO

-- Test 1: Insert multiple active loans
PRINT 'Test 1: Insert multiple active loans';
EXEC usp_RegisterLoan @BookID = 8, @MemberID = 1; -- Dune
EXEC usp_RegisterLoan @BookID = 1, @MemberID = 6; -- Philosopher's Stone
EXEC usp_RegisterLoan @BookID = 20, @MemberID = 11; -- Storm Front
EXEC usp_RegisterLoan @BookID = 47, @MemberID = 4; -- Guards! Guards!
EXEC usp_RegisterLoan @BookID = 30, @MemberID = 9; -- Halo: Fall of Reach

-- Verify active loans and CopiesAvailable
SELECT * FROM vw_ActiveLoans ORDER BY LoanDate;
SELECT BookID, Title, CopiesAvailable, CopiesTotal
FROM Book
ORDER BY TITLE; 
GO

-- Test 2: Insert a returned loan

PRINT 'Test 2: Insert a returned loan (simulated past loan)';

-- Insert manually with ReturnDate
INSERT INTO Loan (BookID, MemberID, LoanDate, DueDate, ReturnDate)
VALUES (14, 13, DATEADD(DAY, -30, GETDATE()), DATEADD(DAY, -16, GETDATE()), DATEADD(DAY, -18, GETDATE()));

-- Restore availability manually
UPDATE Book
SET CopiesAvailable = CopiesAvailable + 1
WHERE BookID = 14;

-- Verify returned loan
SELECT * FROM vw_ActiveLoans ORDER BY LoanDate;
SELECT BookID, Title, CopiesAvailable, CopiesTotal
FROM Book
ORDER BY Title;
GO

-- Test 3: Attempt loan with no copies available (rollback)
PRINT 'Test 3: Borrow book with no copies (should rollback)';

-- Force CopiesAvailable to 0
UPDATE BOOK SET CopiesAvailable = 0 WHERE BookID = 26; -- Dead Beat

BEGIN TRY
	EXEC usp_RegisterLoan @BookID = 26, @MemberID = 2;
END TRY
BEGIN CATCH
	PRINT 'Expected error caught: ' + ERROR_MESSAGE();
END CATCH

-- Verify no change
SELECT BookID, Title, CopiesAvailable, CopiesTotal
FROM Book
WHERE BookID = 26;
GO

-- Final verification
PRINT 'Final active loans and book availabílity';

SELECT * FROM vw_ActiveLoans ORDER BY LoanDate;
SELECT BookID, Title, CopiesAvailable, CopiesTotal
FROM Book
ORDER BY Title;
GO
