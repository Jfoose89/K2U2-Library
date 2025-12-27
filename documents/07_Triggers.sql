CREATE TABLE LoanLog (
    LogID INT IDENTITY(1,1) PRIMARY KEY,
    LoanID INT,
    BookID INT,
    MemberID INT,
    ActionType NVARCHAR(20), -- 'Loan' or 'Return'
    ActionDate DATETIME DEFAULT GETDATE()
);
GO

-- Decrease CopiesAvailable on Loan insert
CREATE OR ALTER TRIGGER trg_DecreaseCopiesOnLoan
ON Loan
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE b
    SET b.CopiesAvailable = b.CopiesAvailable - x.LoanCount
    FROM Book b
    INNER JOIN (
        SELECT BookID, COUNT(*) AS LoanCount
        FROM inserted
        GROUP BY BookID
    ) x ON b.BookID = x.BookID;

    IF EXISTS (SELECT 1 FROM Book WHERE CopiesAvailable < 0)
    BEGIN
        THROW 50002, 'Cannot loan book: no copies available', 1;
    END
END;
GO

-- Increase CopiesAvailable on Return
CREATE OR ALTER TRIGGER trg_IncreaseCopiesOnReturn
ON Loan
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE b
    SET b.CopiesAvailable = b.CopiesAvailable + 1
    FROM Book b
    INNER JOIN inserted i ON b.BookID = i.BookID
    INNER JOIN deleted d ON i.LoanID = d.LoanID
    WHERE d.ReturnDate IS NULL
      AND i.ReturnDate IS NOT NULL;
END;
GO

-- Log Loans and Returns
CREATE OR ALTER TRIGGER trg_LogLoanActions
ON Loan
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Log new loans
    INSERT INTO LoanLog (LoanID, BookID, MemberID, ActionType)
    SELECT LoanID, BookID, MemberID, 'Loan'
    FROM inserted i
    WHERE NOT EXISTS (
        SELECT 1 FROM deleted d WHERE d.LoanID = i.LoanID
    );

    -- Log returns
    INSERT INTO LoanLog (LoanID, BookID, MemberID, ActionType)
    SELECT i.LoanID, i.BookID, i.MemberID, 'Return'
    FROM inserted i
    INNER JOIN deleted d ON i.LoanID = d.LoanID
    WHERE d.ReturnDate IS NULL AND i.ReturnDate IS NOT NULL;
END;
GO
