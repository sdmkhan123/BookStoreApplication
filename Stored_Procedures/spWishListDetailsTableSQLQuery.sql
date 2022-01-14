Use BookStoreDB

--************************Implementing CURD Operations************************

--===================================================================================
--Creating Table of WishLish Details
--===================================================================================
create table WishlistTable
(
	WishlistId int NOT NULL Identity(1,1) Primary Key,
	UserId int NOT NULL FOREIGN KEY REFERENCES User_SignUp_Table(UserId),
	BookId int NOT NULL FOREIGN KEY REFERENCES Book_Details_Table(BookId),	
)
--===================================================================================
--1.Creating Stored Procedure for adding new Book in Cart_Details_Table
--===================================================================================
Create PROCEDURE spCreateWishlist
	@UserId INT,
	@BookId INT
AS
BEGIN
	Begin Try
	Begin Transaction
		IF (EXISTS(SELECT * FROM Book_Details_Table WHERE BookId=@BookId))		
		begin
			INSERT INTO WishlistTable VALUES (@UserId,@BookId)
		end
	COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
			Rollback TRANSACTION;
			Select
				ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END
--===================================================================================
--2.Creating Stored Procedure for Delelting a wishlist from WishlistTable
--===================================================================================
CREATE PROCEDURE spDeleteWishlist
	@WishlistId INT
AS
BEGIN
	Begin Try
	Begin Transaction
		DELETE FROM WishlistTable WHERE WishlistId = @WishlistId
	COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
			Rollback TRANSACTION;
			Select
				ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END
--===================================================================================
--To Retreive All Books
--===================================================================================
Create procedure SpGetBooksFromWishList
(
    @UserId varchar(50)        
)
AS
BEGIN
	Begin Try
	Begin Transaction                    
		Select Book_Details_Table.BookId, Book_Details_Table.BookName, Book_Details_Table.AuthorName, 
		Book_Details_Table.BookDescription, Book_Details_Table.DiscountPrice, Book_Details_Table.OriginalPrice,
		Book_Details_Table.Rating, Book_Details_Table.BookCount, Book_Details_Table.Image, WishlistTable.WishlistId,
		WishlistTable.UserId,WishlistTable.BookId
		From Book_Details_Table
		Inner Join WishlistTable On WishlistTable.BookId=Book_Details_Table.BookId 
		Where WishlistTable.UserId=@UserId
	COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
			Rollback TRANSACTION;
			Select
				ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END