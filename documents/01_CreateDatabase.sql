-- Create the database
IF DB_ID('K2U2Library') IS NOT NULL
    DROP DATABASE K2U2Library;
GO

CREATE DATABASE K2U2Library;
GO

USE K2U2Library;
GO

-- Create Book table
CREATE TABLE Book (
    BookID INT IDENTITY(1,1) PRIMARY KEY,
    ISBN VARCHAR(20) NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    Author NVARCHAR(255) NOT NULL,
    PublishedYear INT NOT NULL,
    CopiesTotal INT NOT NULL,
    CopiesAvailable INT NOT NULL
);
GO

-- Create Member table
CREATE TABLE Member (
    MemberID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Phone NVARCHAR(50) NOT NULL
);
GO

-- Create Loan table
CREATE TABLE Loan (
    LoanID INT IDENTITY(1,1) PRIMARY KEY,
    BookID INT NOT NULL,
    MemberID INT NOT NULL,
    LoanDate DATETIME NOT NULL DEFAULT GETDATE(),
    DueDate DATETIME NOT NULL,
    ReturnDate DATETIME NULL,
    CONSTRAINT FK_Loan_Book FOREIGN KEY (BookID) REFERENCES Book(BookID),
    CONSTRAINT FK_Loan_Member FOREIGN KEY (MemberID) REFERENCES Member(MemberID)
);
GO

-- Verify
SELECT * FROM Book ORDER BY BookID;
SELECT * FROM Member ORDER BY MemberID;
