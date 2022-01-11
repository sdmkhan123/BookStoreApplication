using BookStoreModels;
using BookStoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace BookStoreRepository.Repository
{
    public class BookRepository : IBookRepository
    {
        public IConfiguration Configuration { get; }
        public BookRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        SqlConnection sqlConnection;

        public int AddBook(BookModel bookModel)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
            try
            {
                using (sqlConnection)
                {
                    string storeprocedure = "Sp_AddBooks";
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
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
