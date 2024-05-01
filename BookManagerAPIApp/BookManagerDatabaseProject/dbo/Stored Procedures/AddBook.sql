CREATE PROCEDURE [AddBook]
	@name nvarchar(50),
	@purchasedDate date,
	@price real,
	@imageBlobURL nvarchar(max),
	@categoryId int
AS
BEGIN

	IF EXISTS(SELECT * FROM Category where Category.Id = @categoryId)
	BEGIN

		INSERT INTO Books ([Name], [PurchasedDate], [Price], [ImageBlobURL], [CategoryId])
		VALUES (@name, @purchasedDate, @price, @imageBlobURL, @categoryId)	
	END
END
