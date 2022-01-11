Use BookStoreDB

--===================================================================================
--Creating Table of Books Details
--===================================================================================
Create Table Book_Details_Table (
BookId int Identity(1,1) Not Null Primary Key,
BookName VARCHAR(255) NOT NULL Unique,
AuthorName VARCHAR(255) NOT NULL,
DiscountPrice INT NOT NULL,
OriginalPrice INT NOT NULL,
BookDescription VARCHAR(255) NOT NULL,
Rating Int,
Reviewer Int,
Image VARCHAR(255) NOT NULL,
BookCount Int
)
CREATE PROC spAddBook
	@BookName VARCHAR(255),
	@AuthorName VARCHAR(255),
	@DiscountPrice INT,
	@OriginalPrice INT,
	@BookDescription VARCHAR(255),
	@Rating INT,
	@Reviewer Int,
	@Image VARCHAR(255),
	@BookCount Int
AS
BEGIN
	BEGIN TRY
	BEGIN TRANSACTION;
		INSERT INTO Book_Details_Table VALUES (@BookName, @AuthorName,@DiscountPrice, @OriginalPrice, @BookDescription, @Rating, @Reviewer, @Image,@BookCount)
	COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
			Rollback TRANSACTION;
			Select
				ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
End