Use BookStoreDB

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
--Creating Stored Procedure for adding new Book in Cart_Details_Table
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
--Creating Stored Procedure for Update Quantity of a book in Cart_Details_Table
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