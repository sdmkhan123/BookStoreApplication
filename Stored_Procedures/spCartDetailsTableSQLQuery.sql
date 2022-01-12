Use BookStoreDB

--************************Implementing CURD Operations************************

--===================================================================================
--Creating Table of Cart Details
--===================================================================================
Create Table Cart_Details_Table
(
	CartID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	UserId int NOT NULL FOREIGN KEY REFERENCES User_SignUp_Table(UserId),
	BookId int NOT NULL FOREIGN KEY REFERENCES Book_Details_Table(BookId),	
	OrderQuantity int default 1
);
--===================================================================================
--1.Creating Stored Procedure for adding new Book in Cart_Details_Table
--===================================================================================
Create Procedure spAddingCart
(
	@UserId int,
	@BookId int
)
AS
BEGIN
	Begin Try
	Begin Transaction
	IF (EXISTS(SELECT * FROM Book_Details_Table WHERE BookId=@BookId))		
		begin
			INSERT INTO Cart_Details_Table( UserId,BookId)
			VALUES (@UserId,@BookId)
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
--2.Creating Stored Procedure for Update Quantity of a book in Cart_Details_Table
--===================================================================================
CREATE PROC spUpdateQuantity
	@CartID int,
	@OrderQuantity int
AS
BEGIN
	Begin Try
	Begin Transaction
	IF (EXISTS(SELECT * FROM Cart_Details_Table WHERE CartID = @CartID))
		BEGIN
			UPDATE Cart_Details_Table
			SET OrderQuantity = @OrderQuantity
			WHERE CartID = @CartID;
		END
	COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		Rollback TRANSACTION;
		Select
			ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END
--===================================================================================
--3.Creating Stored Procedure for retrievving all book from Cart_Details_Table
--===================================================================================
CREATE PROCEDURE spGetCartDetails
	@UserId INT
AS
BEGIN
	Begin Try
	Begin Transaction
	SELECT
		Cart_Details_Table.CartID,
		Cart_Details_Table.UserId,
		Cart_Details_Table.BookId,
		Cart_Details_Table.OrderQuantity,
		Book_Details_Table.BookName,
		Book_Details_Table.AuthorName,
		Book_Details_Table.DiscountPrice,
		Book_Details_Table.OriginalPrice
	FROM Cart_Details_Table
	Inner JOIN Book_Details_Table ON Cart_Details_Table.BookId = Book_Details_Table.BookId
	WHERE Cart_Details_Table.UserId = @UserId
	COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		Rollback TRANSACTION;
		Select
			ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END
--===================================================================================
--4.Creating Stored Procedure for Delelting a CartBooDetail from Cart_Details_Table
--===================================================================================
CREATE PROCEDURE spDeleteCartDetails
	@CartID INT
AS
BEGIN
	Begin Try
	Begin Transaction
	IF EXISTS(SELECT * FROM Cart_Details_Table WHERE CartID = @CartID)
	BEGIN
		DELETE FROM Cart_Details_Table WHERE CartID = @CartID
	END
	COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		Rollback TRANSACTION;
		Select
			ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END