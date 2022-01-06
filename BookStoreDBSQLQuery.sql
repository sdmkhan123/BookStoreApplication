Create DataBase BookStoreDB
Use BookStoreDB
--===================================================================================
--Creating Table of User Details for User SignUp
--===================================================================================
Create Table User_SignUp_Table(
UserId int IDENTITY(1,1) Primary Key NOT NULL,
FullName varchar(20) NOT NULL,
EmailId varchar(50) NOT NULL Unique,
Password varchar(20) NOT NULL,
MobileNum bigint NOT NULL
)
--===================================================================================
-- Creating a stored procedure for SignUp the users / Inserting data into Table
--===================================================================================
Create procedure SignUpUsers
(
@FullName VARCHAR(20),
@EmailId VARCHAR(50),
@Password VARCHAR(20),
@MobileNum bigint
)   
as
Begin
BEGIN TRY
	Insert into User_SignUp_Table Values (@FullName,@EmailId,@Password, @MobileNum)
END TRY
BEGIN CATCH
	Select ERROR_MESSAGE() AS ErrorMessage;
END CATCH
End
--===================================================================================
-- Creating a stored procedure for login
--===================================================================================
Create procedure spForLogin
(
@EmailId VARCHAR(50),
@Password VARCHAR(20)
)   
as
Begin
	Begin Try
	SELECT EmailId, Password
	FROM User_SignUp_Table
	WHERE EmailId= @EmailId AND Password=@Password
	End Try
	BEGIN CATCH
	Select ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
End