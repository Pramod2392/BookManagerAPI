CREATE TABLE [dbo].[Users]
(
	[user_id] BIGINT NOT NULL PRIMARY KEY, 
    [first_name] VARCHAR(50) NOT NULL,
    [last_name] VARCHAR(50) NULL,
    [display_name] VARCHAR(50) NULL, 
    [created_date] DATETIME NOT NULL, 
    [email_id] NVARCHAR(50) NOT NULL
)
