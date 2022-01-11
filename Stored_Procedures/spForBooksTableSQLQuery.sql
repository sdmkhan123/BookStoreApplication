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
--===================================================================================
--Creating Stored Procedure for adding new Book in Book_Detail_Table
--===================================================================================
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
--===================================================================================
--Creating Stored Procedure for Updating Book in Book_Detail_Table
--===================================================================================
Create procedure sp_UpdateBooks   
( 
	@BookId int,
    @BookName VARCHAR(255),
    @AuthorName varchar(255),
    @DiscountPrice Int,
	@OriginalPrice  Int,
	@BookDescription nvarchar(255),
    @Rating float ,
    @Reviewer int  ,
    @Image varchar(255),
	@BookCount int
)
as
Begin 
	Begin try   
		BEGIN TRANSACTION;
		IF Exists(select * from Book_Details_Table where BookId = @BookId) 
		begin
			update Book_Details_Table
			set 
				BookName= @BookName ,
				AuthorName=@AuthorName,
				DiscountPrice=@DiscountPrice,
				OriginalPrice=@OriginalPrice,
				BookDescription=@BookDescription,
				Rating=@Rating,
				Reviewer=@Reviewer,
				Image=@Image,
				BookCount=@BookCount
			where BookId = @BookId;
		end
		else
		begin
			Select 0;
		end
		COMMIT TRANSACTION; 
	End try
	Begin catch
		SELECT  ERROR_MESSAGE() AS ErrorMessage;  
		ROLLBACK TRANSACTION;  
	End catch  
End
--===================================================================================
--Creating Stored Procedure for books a Book in Book_Detail_Table
--===================================================================================
create Procedure spRetieveBookDetails
(
	@BookId int
)

AS
BEGIN
 Begin try
     IF(EXISTS(SELECT * FROM Book_Details_Table WHERE BookId=@BookId))
	 begin
	   SELECT * FROM Book_Details_Table WHERE BookId=@BookId;
   	 end
 End try
 Begin catch
		SELECT  ERROR_MESSAGE() AS ErrorMessage;
 End catch
End