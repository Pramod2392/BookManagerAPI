CREATE PROCEDURE [AddBookUserMap]
	@bookId int,
	@userId uniqueidentifier
AS
	BEGIN
		INSERT INTO BookUserMap (BookId, UserId) VALUES (@bookId, @userId);
	END
RETURN 0
