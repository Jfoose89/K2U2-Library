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

	UPDATE Book
	SET CopiesAvailable = CopiesAvailable -1
	WHERE BookID = @BookID;
END;

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

	UPDATE Book
	SET CopiesAvailable = CopiesAvailable - 1
	WHERE BookID = @BookID;
END;

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

    UPDATE Book
    SET CopiesAvailable = CopiesAvailable - 1
    WHERE BookID = @BookID;
END;

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

    UPDATE Book
    SET CopiesAvailable = CopiesAvailable - 1
    WHERE BookID = @BookID;
END;

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

    UPDATE Book
    SET CopiesAvailable = CopiesAvailable - 1
    WHERE BookID = @BookID;
END;

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

UPDATE Book
SET CopiesAvailable = CopiesAvailable + 1
WHERE BookID = @BookID;

SELECT *
FROM vw_ActiveLoans
ORDER BY DueDate;

SELECT BookID, Title, CopiesAvailable
FROM Book
WHERE BookID IN (8, 1, 20, 47, 30, 14);
