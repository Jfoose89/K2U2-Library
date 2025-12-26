USE K2U2Library;
GO

-- Loan table indexes
CREATE NONCLUSTERED INDEX IX_Loan_MemberID_ReturnDate
ON dbo.Loan (MemberID)
WHERE ReturnDate IS NULL;

CREATE NONCLUSTERED INDEX IX_Loan_BookID
ON dbo.Loan (BookID);

CREATE NONCLUSTERED INDEX IX_Loan_DueDate_ReturnDate
ON dbo.Loan (DueDate)
WHERE ReturnDate IS NULL;

-- Book table index 
CREATE NONCLUSTERED INDEX IX_Book_CopiesAvailable
ON dbo.Book (CopiesAvailable);

-- Member table index
CREATE NONCLUSTERED INDEX IX_Member_MemberID
ON dbo.Member (MemberID);

SELECT name, type_desc
FROM sys.indexes
WHERE object_id = OBJECT_ID('dbo.Loan');

SELECT name, type_desc
FROM sys.indexes
WHERE object_id = OBJECT_ID('dbo.Book');

SELECT name, type_desc
FROM sys.indexes
WHERE object_id = OBJECT_ID('dbo.Member');
