using BookStoreModels;
using BookStoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace BookStoreRepository.Repository
{
    public class BookRepository : IBookRepository
    {
        public IConfiguration configuration { get; }
        public string connectionString { get; set; } = "BookStoreDbConnectionString";
        public BookRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public int AddBook(BookModel bookModel)
        {
            try
            {
                string ConnectionStrings = configuration.GetConnectionString(connectionString);
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings))
                {
                    string storeprocedure = "spAddBook";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@BookName", bookModel.BookName);
                    sqlCommand.Parameters.AddWithValue("@AuthorName", bookModel.AuthorName);
                    sqlCommand.Parameters.AddWithValue("@DiscountPrice", bookModel.DiscountPrice);
                    sqlCommand.Parameters.AddWithValue("@OriginalPrice", bookModel.OriginalPrice);
                    sqlCommand.Parameters.AddWithValue("@BookDescription", bookModel.BookDescription);
                    sqlCommand.Parameters.AddWithValue("@Rating", bookModel.Rating);
                    sqlCommand.Parameters.AddWithValue("@Reviewer", bookModel.Reviewer);
                    sqlCommand.Parameters.AddWithValue("@Image", bookModel.Image);
                    sqlCommand.Parameters.AddWithValue("@BookCount", bookModel.BookCount);
                    sqlConnection.Open();
                    int result = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
