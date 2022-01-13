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