Use BookStoreDB
--===================================================================================
--.Creating Table of Orders Details
--===================================================================================
create table OrderTable
(
	OrderId int not null identity (1,1) primary key,
	UserId int not null Foreign Key References User_SignUp_Table(UserId),
	BookId int not null Foreign Key References Book_Details_Table(BookId),
	AddressId int not null Foreign Key References Address_Details_Table(AddressId),
	TotalPrice int,
	BookQuantity int,
	OrderDate Date
)
--===================================================================================
--Creating Stored Procedure for Order a Book in Orders_Table
--===================================================================================
create procedure spAddingOrders
	@UserId INT,
	@AddressId int,
	@BookId INT ,
	@BookQuantity int
AS
	Declare @TotPrice int
BEGIN
	Begin try
	Begin transaction
	Select @TotPrice=DiscountPrice from Book_Details_Table where BookId = @BookId;
	IF EXISTS(SELECT * FROM Book_Details_Table WHERE BookId = @BookId)
	begin
		IF EXISTS(SELECT * FROM User_SignUp_Table WHERE UserId = @UserId)
		Begin			
			INSERT INTO OrderTable (UserId,AddressId,BookId,TotalPrice,BookQuantity,OrderDate) VALUES
			( @UserId,@AddressId,@BookId,@BookQuantity*@TotPrice,@BookQuantity,GETDATE())

			Update Book_Details_Table
			set BookCount=BookCount-@BookQuantity

			Delete from Cart_Details_Table
			where BookId = @BookId and UserId = @UserId
		end
		Else
		begin
			Select 1
		end
	end 
	Else
	begin
			Select 2
	end	
	COMMIT TRANSACTION; 
	End try
	Begin catch
	SELECT  ERROR_MESSAGE() AS ErrorMessage;  
	ROLLBACK TRANSACTION;  
	End catch 
END
------------get orders------------
create procedure spGetAllOrders
	@UserId INT
AS
BEGIN
	Begin Try
	Begin Transaction
	select 
		Book_Details_Table.BookId,Book_Details_Table.BookName,Book_Details_Table.AuthorName,
		Book_Details_Table.DiscountPrice,Book_Details_Table.OriginalPrice,Book_Details_Table.Image,
		OrderTable.OrderId,OrderTable.OrderDate
		FROM Book_Details_Table
		inner join OrderTable on OrderTable.BookId=Book_Details_Table.BookId where OrderTable.UserId=@UserId
	COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		Rollback TRANSACTION;
		Select
			ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END