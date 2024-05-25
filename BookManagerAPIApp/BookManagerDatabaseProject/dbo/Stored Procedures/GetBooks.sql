CREATE PROCEDURE [GetBooks]
	@userId bigint
AS
BEGIN
	SELECT * FROM Books WHERE user
END

