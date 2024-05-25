CREATE TABLE [dbo].[BookUserMap]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [BookId] INT NOT NULL, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_BookUserMap_Books] FOREIGN KEY (BookId) REFERENCES [Books](Id), 
    CONSTRAINT [FK_BookUserMap_Users] FOREIGN KEY (UserId) REFERENCES [Users](user_id)
)
