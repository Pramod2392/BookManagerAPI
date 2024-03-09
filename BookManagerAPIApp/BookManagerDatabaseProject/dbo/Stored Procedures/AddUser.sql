CREATE PROCEDURE [dbo].[AddUser]
	@userId bigint,
	@firstName varchar(50),
	@lastName varchar(50),
	@displayName varchar(50),
	@createdDate datetime
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM Users WHERE user_id = @userId)
	BEGIN
		INSERT INTO Users (user_id, first_name, last_name, display_name, created_date)
		VALUES (@userId, @firstName, @lastName, @displayName, GETUTCDATE())
	END
END	
