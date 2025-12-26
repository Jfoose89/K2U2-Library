-- Loan Insert Query
SELECT BookID, Title, CopiesAvailable
FROM Book
WHERE CopiesAvailable > 0
ORDER BY Title;

SELECT MemberID, FirstName, LastName
FROM Member
ORDER BY LastName;

USE K2U2Library;

-- Loan 1
DECLARE @BookID INT = 8; -- Dune
DECLARE @MemberID INT = 1; -- Ethan Cole

IF EXISTS (
	SELECT 1 FROM Book
	WHERE BookID = @BookID AND CopiesAvailable > 0
)
BEGIN
	INSERT INTO Loan (BookID, MemberID, LoanDate, DueDate)
	VALUES (
		@BookID,
		@MemberID,
		DATEADD(DAY, -3, GETDATE()),
		DATEADD(DAY, 11, GETDATE())
	);
END;
GO

-- Loan 2
DECLARE @BookID INT = 1; -- Philosopher's Stone
DECLARE @MemberID INT = 6; -- Ella Fors

IF EXISTS (
	SELECT 1 FROM Book
	WHERE BookID = @BookID AND CopiesAvailable > 0
)
BEGIN
	INSERT INTO Loan (BookID, MemberID, LoanDate, DueDate)
	VALUES (
		@BookID,
		@MemberID,
		DATEADD(DAY, -1, GETDATE()),
		DATEADD(DAY, 13, GETDATE())
	);
END;
GO

-- Loan 3
DECLARE @BookID INT = 20;  -- Storm Front
DECLARE @MemberID INT = 11; -- William Åkesson

IF EXISTS (
    SELECT 1 FROM Book
    WHERE BookID = @BookID AND CopiesAvailable > 0
)
BEGIN
    INSERT INTO Loan (BookID, MemberID, LoanDate, DueDate)
    VALUES (
        @BookID,
        @MemberID,
        DATEADD(DAY, -6, GETDATE()),
        DATEADD(DAY, 8, GETDATE())
    );
END;
GO

-- Loan 4
DECLARE @BookID INT = 47;  -- Guards! Guards!
DECLARE @MemberID INT = 4; -- Sofia Holm

IF EXISTS (
    SELECT 1 FROM Book
    WHERE BookID = @BookID AND CopiesAvailable > 0
)
BEGIN
    INSERT INTO Loan (BookID, MemberID, LoanDate, DueDate)
    VALUES (
        @BookID,
        @MemberID,
        DATEADD(DAY, -4, GETDATE()),
        DATEADD(DAY, 10, GETDATE())
    );
END;
GO

-- Loan 5
DECLARE @BookID INT = 30;  -- Halo: The Fall of Reach
DECLARE @MemberID INT = 9; -- Oliver Sjöberg

IF EXISTS (
    SELECT 1 FROM Book
    WHERE BookID = @BookID AND CopiesAvailable > 0
)
BEGIN
    INSERT INTO Loan (BookID, MemberID, LoanDate, DueDate)
    VALUES (
        @BookID,
        @MemberID,
        DATEADD(DAY, -2, GETDATE()),
        DATEADD(DAY, 12, GETDATE())
    );
END;
GO

-- Info not  in view
DECLARE @BookID INT = 14;  -- Fellowship of the Ring
DECLARE @MemberID INT = 2; -- Mila Berg

INSERT INTO Loan (
    BookID,
    MemberID,
    LoanDate,
    DueDate,
    ReturnDate
)
VALUES (
    @BookID,
    @MemberID,
    DATEADD(DAY, -35, GETDATE()),
    DATEADD(DAY, -21, GETDATE()),
    DATEADD(DAY, -25, GETDATE())
);

SELECT *
FROM vw_ActiveLoans
ORDER BY DueDate;

SELECT BookID, Title, CopiesAvailable
FROM Book
WHERE BookID IN (8, 1, 20, 47, 30, 14);

-- Active Loan Query
SELECT
    name,
    database_id,
    create_date
FROM sys.databases
WHERE name = 'K2U2Library';

USE K2U2Library;
GO

SELECT name FROM sys.tables;
SELECT name FROM sys.views;

-- Create Views
CREATE VIEW vw_MostFrequentBooks AS
SELECT
	b.BookID,
	b.Title,
	COUNT(l.LoanID) AS TimesBorrowed
FROM Loan l
JOIN Book b ON l.BookID = b.BookID
GROUP BY b.BookID, b.Title;

SELECT *
FROM vw_MostFrequentBooks
ORDER BY TimesBorrowed DESC;

CREATE VIEW vw_OverdueLoans AS
SELECT
	l.LoanID,
	m.MemberID,
	CONCAT(m.FirstName, '', m.LastName) AS MemberName,
	b.BookID,
	b.Title,
	l.LoanDate,
	l.DueDate
FROM Loan AS l
INNER JOIN Member m ON l.MemberID = m.MemberID
INNER JOIN Book b ON l.BookID = b.BookID
WHERE l.ReturnDate IS NULL
	AND l.DueDate < GETDATE();

SELECT *
FROM vw_OverdueLoans
ORDER BY DueDate;

UPDATE dbo.Loan
SET DueDate = DATEADD(day, -5, GETDATE())
WHERE LoanID = 1;

USE K2U2Library;
GO

CREATE VIEW vw_MemberBorrowingHistory AS
SELECT
	l.LoanID,
	m.MemberID,
	CONCAT(m.FirstName, '', m.LastName) AS MemberName,
	b.BookID,
	b.Title,
	l.LoanDate,
	l.DueDate,
	l.ReturnDate
FROM dbo.Loan AS l
INNER JOIN dbo.Member AS m ON l.MemberID = m.MemberID
INNER JOIN dbo.Book AS b ON l.BookID = b.BookID;

-- Member Borrowing History Query

-- See all loans for all members
SELECT *
FROM vw_MemberBorrowingHistory;

-- See loans for a specific member
SELECT *
FROM vw_MemberBorrowingHistory
WHERE MemberID = 1
ORDER BY LoanDate DESC;

-- See only returned loans
SELECT *
FROM vw_MemberBorrowingHistory
WHERE MemberID = 1
ORDER BY LoanDate DESC;

-- See only returned loans
SELECT *
FROM vw_MemberBorrowingHistory
WHERE ReturnDate IS NOT NULL
ORDER BY ReturnDate DESC;

-- See only active loans
SELECT *
FROM vw_MemberBorrowingHistory
WHERE ReturnDate IS NULL
ORDER BY DueDate ASC;
