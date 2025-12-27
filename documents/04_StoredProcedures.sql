USE K2U2Library;

CREATE PROCEDURE usp_RegisterLoan
	@BookID INT,
	@MemberID INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRANSACTION;

	IF EXISTS (
		SELECT 1
		FROM Book
		WHERE BookID = @BookID
			AND CopiesAvailable > 0
	)
	BEGIN
		INSERT INTO Loan (BookID, MemberID, LoanDate, DueDate)
		VALUES (
			@BookID,
			@MemberID,
			GETDATE(),
			DATEADD(DAY, 14, GETDATE())
		);

		UPDATE Book
		SET CopiesAvailable = CopiesAvailable -1
		WHERE BookID = @BookID;

		COMMIT TRANSACTION;
	END
	ELSE
	BEGIN
		ROLLBACK TRANSACTION;
		THROW 50001, 'No copies available', 1;
	END
END;
