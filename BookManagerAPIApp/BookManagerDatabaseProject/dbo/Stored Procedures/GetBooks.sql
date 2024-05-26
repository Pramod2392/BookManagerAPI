CREATE PROCEDURE [GetBooks]
	@userId uniqueidentifier
AS
BEGIN
	SELECT * FROM Books WHERE Books.Id IN (SELECT BookUserMap.BookId FROM BookUserMap WHERE BookUserMap.UserId = @userId)
END

