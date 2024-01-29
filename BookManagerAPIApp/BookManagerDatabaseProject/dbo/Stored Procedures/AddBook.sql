CREATE PROCEDURE [dbo].[spo.AddBook]
	@name nvarchar(50),
	@purchasedDate date,
	@price real,
	@imageBlobURL nvarchar(max),
	@categoryId int
AS
BEGIN

	IF EXISTS(SELECT * FROM dbo.Category where Category.Id = @categoryId)
	BEGIN

		INSERT INTO dbo.Books ([Name], [PurchasedDate], [Price], [ImageBlobURL], [CategoryId])
		VALUES (@name, @purchasedDate, @price, @imageBlobURL, @categoryId)	
	END
END
