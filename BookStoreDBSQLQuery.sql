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
BEGIN TRANSACTION;
	Insert into User_SignUp_Table Values (@FullName,@EmailId,@Password, @MobileNum)
COMMIT TRANSACTION;
END TRY
BEGIN CATCH
		Rollback TRANSACTION;
		Select
			ERROR_MESSAGE() AS ErrorMessage;
END CATCH
End
--===================================================================================
-- Creating a stored procedure for login
--===================================================================================
Create procedure spForLogin
(
@EmailId VARCHAR(50),
@Password VARCHAR(20),
@User int = Null OUTPUT
)
as
Begin
	Begin Try
	BEGIN TRANSACTION;
		IF EXISTS(SELECT * FROM User_SignUp_Table WHERE EmailId=@EmailId)
		BEGIN
			IF EXISTS(SELECT * FROM User_SignUp_Table WHERE EmailId=@EmailId AND Password=@Password)
			BEGIN
				SET @User = 2;
			END
			ELSE
			BEGIN
				SET @User = 1;
			END
		END
		ELSE
		BEGIN
			SET @User = 0;
		END
	COMMIT TRANSACTION;
	End Try
	BEGIN CATCH
		Rollback TRANSACTION;
		Select
			ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
End
--===================================================================================
-- Creating a stored procedure for password reset
--===================================================================================
Create procedure spForResetPassword
(       
    @EmailId VARCHAR(50),
    @NewPassword VARCHAR(20)
)   
as
Begin
	Begin Try
	BEGIN TRANSACTION;
	IF EXISTS(SELECT * FROM User_SignUp_Table WHERE EmailId=@EmailId)
	Begin
		Update User_SignUp_Table
		set Password=@NewPassword
		where EmailId=@EmailId
	End
	COMMIT TRANSACTION;
	End Try
	Begin CATCH
        IF (XACT_STATE()) = -1
        BEGIN  
            PRINT  N'The transaction is in an uncommittable state.' + 'Rolling back transaction.'  
            ROLLBACK TRANSACTION;  
        END;
        IF (XACT_STATE()) = 1
        BEGIN  
            PRINT N'The transaction is committable.' + 'Committing transaction.'  
            COMMIT TRANSACTION;
        END;
	End CATCH
End