CREATE PROCEDURE [AddUser]
	@userId uniqueidentifier,
	@firstName varchar(50),
	@lastName varchar(50),
	@displayName varchar(50),
	@createdDate datetime,
	@emailId varchar(50)
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM Users WHERE user_id = @userId)
	BEGIN
		INSERT INTO Users (user_id, first_name, last_name, display_name, created_date, email_id)
		VALUES (@userId, @firstName, @lastName, @displayName, GETUTCDATE(), @emailId)
	END
END	
