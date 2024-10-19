CREATE PROCEDURE [AddBook]
	@name nvarchar(50),
	@purchasedDate date,
	@price real,
	@imageBlobURL nvarchar(max),
	@categoryId int,
	@languageId int
AS
BEGIN

	IF EXISTS(SELECT * FROM Category where Category.Id = @categoryId)
	BEGIN

		INSERT INTO Books ([Name], [PurchasedDate], [Price], [ImageBlobURL], [CategoryId], [LanguageId])
		VALUES (@name, @purchasedDate, @price, @imageBlobURL, @categoryId, @languageId);

		SELECT * from Books WHERE Id = SCOPE_IDENTITY();

		RETURN;
	END
END
