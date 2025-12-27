USE K2U2Library;
GO

-- Reset CopiesAvailable for testing
UPDATE Book
Set CopiesAvailable = CopiesTotal;

PRINT 'Test 1: Normal loan insert (should succeed)';
EXEC usp_RegisterLoan @BookID = 8, @MemberID = 1; -- Dune
SELECT BookID, Title, CopiesAvailable FROM Book WHERE BookID = 8;

PRINT 'Test 2: Borrow same book until no copies left';

-- First, set copies to 0
UPDATE Book SET CopiesAvailable = 0 WHERE BookID = 30; -- Halo: Fall of Reach

-- Try to borrow it
BEGIN TRY
	EXEC usp_RegisterLoan @BookID = 30, @MemberID = 2;
END TRY
BEGIN CATCH
	PRINT 'Expected error caught: ' + ERROR_MESSAGE();
END CATCH

-- Confirm CopiesAvailable did not change
SELECT BookID, Title, CopiesAvailable FROM Book WHERE BookID = 30;
