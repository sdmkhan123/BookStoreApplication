using BookStoreModels;
using BookStoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        public IConfiguration Configuration { get; }
        public FeedbackRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        SqlConnection sqlConnection;

        //Adding book to cart api
        public string AddFeedback(FeedbackModel feedbackModel)
        {
            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDbConnectionString"));
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SpAddFeedback", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserId", feedbackModel.UserId);
                sqlCommand.Parameters.AddWithValue("@BookId", feedbackModel.BookId);
                sqlCommand.Parameters.AddWithValue("@Comments", feedbackModel.Comments);
                sqlCommand.Parameters.AddWithValue("@Ratings", feedbackModel.Ratings);
                sqlConnection.Open();
                int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                if (result == 2)
                {
                    return "BookId not exists";
                }
                else if (result == 1)
                {
                    return "Already given Feedback for this book";
                }
                else
                {
                    return "Feedback added successfully";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

    }
}
