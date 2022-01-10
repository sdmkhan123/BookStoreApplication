Use BookStoreDB

CREATE PROC spUserForget
	@EmailId VARCHAR(50),
	@user INT = NULL OUTPUT
AS
BEGIN
	BEGIN TRY
	BEGIN TRANSACTION;
		IF EXISTS(SELECT * FROM User_SignUp_Table WHERE EmailId=@EmailId)
		BEGIN
			Set @user = 1;
		END
		ELSE
		BEGIN
			SET @user = 0;
		END
	COMMIT TRANSACTION;
	END TRY
	Begin CATCH
        IF (XACT_STATE()) = -1 
        BEGIN  
            PRINT  'The transaction is in an uncommittable state, Rolling back transaction.'  
            ROLLBACK TRANSACTION;  
        END;
        IF (XACT_STATE()) = 1
        BEGIN
            PRINT 'The transaction is committable, Committing transaction.'
            COMMIT TRANSACTION;
        END;
	End CATCH
END