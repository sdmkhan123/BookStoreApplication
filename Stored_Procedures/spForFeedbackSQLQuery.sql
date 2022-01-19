Use BookStoreDB
---------------Feedback table------------
create table FeedbackTable
(
FeedbackId int not null identity (1,1) primary key,
UserId int NOT NULL FOREIGN KEY REFERENCES User_SignUp_Table(UserId),
BookId int NOT NULL FOREIGN KEY REFERENCES Book_Details_Table(BookId),
Comments Varchar(max),
Ratings int
);
-------------CreateFeedbacktable-------------------
Create procedure SpAddFeedback(
@UserId INT,
@BookId INT,
@Comments Varchar(max),
@Ratings int
)		
As 
Declare @AverageRating int;
Begin
	Begin try
	Begin transaction
	IF EXISTS(SELECT * FROM FeedbackTable WHERE BookId = @BookId and UserId=@UserId)
		select 1; --already given feedback--
	Else
	Begin
		IF (EXISTS(SELECT * FROM Book_Details_Table WHERE BookId = @BookId))
		Begin
			Insert into FeedbackTable (UserId,BookId,Comments,Ratings ) values
			(@UserId,@BookId,@Comments,@Ratings);		
			select @AverageRating=AVG(Ratings)
			from FeedbackTable
			where BookId = @BookId;
			Update Book_Details_Table
			set Rating=@AverageRating, Reviewer=Reviewer+1
			where BookId = @BookId;
		End
		Else
		Begin
			Select 2; 
		End
	End
	COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
			Rollback TRANSACTION;
			Select
				ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
End

-----------Get feedback-----------
Create procedure spGetFeedbacks
	@BookId INT
AS
BEGIN
	Begin try
	Begin transaction
		select 
		FeedbackTable.UserId,FeedbackTable.Comments,FeedbackTable.Ratings,User_SignUp_Table.FullName
		FROM User_SignUp_Table
		inner join FeedbackTable
		on FeedbackTable.UserId=User_SignUp_Table.UserId
		where BookId=@BookId
	COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
			Rollback TRANSACTION;
			Select
				ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
End