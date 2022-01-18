Use BookStoreDB

--************************Implementing CURD Operations************************

--===================================================================================
--Creating Table of Address Details
--===================================================================================
create table Address_Details_Table
(
    AddressId int not null identity (1,1) primary key,
	Address varchar(max) not null,
	City varchar(50) not null,
	State varchar(50) not null,
	Type varchar(10) not null,
	UserId int NOT NULL FOREIGN KEY REFERENCES User_SignUp_Table(UserId)
)
--===================================================================================
--1.Creating Stored Procedure for adding new Address in Address_Details_Table
--===================================================================================
create procedure SpAddUserAddress
(
@Address varchar(600),
@City varchar(50),
@State varchar(50),
@Type varchar(10),
@UserId int
)
		
AS
BEGIN
	Begin Try
	Begin Transaction
		Insert into Address_Details_Table values
		(@Address, @City, @State, @Type, @UserId);
	COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		Rollback TRANSACTION;
		Select
			ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END
--===================================================================================
--2.Creating Stored Procedure for Update Address in Address_Details_Table
--===================================================================================
create procedure spUpdateUserAddress
(
@AddressID int,
@Address varchar(255),
@City varchar(50),
@State varchar(50),
@Type varchar(10)
)
AS
BEGIN
	Begin Try
	Begin Transaction
		If exists(Select * from Address_Details_Table where AddressId=@AddressID)
		begin
			UPDATE Address_Details_Table
			SET 
				Address= @Address,
				City = @City,
				State=@State,
				Type=@Type
			WHERE AddressId=@AddressID;
		end
	COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		Rollback TRANSACTION;
		Select
			ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END