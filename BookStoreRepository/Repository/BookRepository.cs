using BookStoreModels;
using BookStoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BookStoreRepository.Repository
{
    public class BookRepository : IBookRepository
    {
        public IConfiguration configuration { get; }
        public BookRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public int AddBook(BookModel bookModel)
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreDbConnectionString"));
            try
            {
                SqlCommand sqlCommand = new SqlCommand("spAddBook", sqlConnection);
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public int UpdateBookDetails(BookModel bookModel)
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreDbConnectionString"));
            try
            {
                SqlCommand sqlCommand = new SqlCommand("sp_UpdateBooks", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@BookId", bookModel.BookId);
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
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public BookModel RetrieveBookDetails(int bookId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreDbConnectionString"));
            try
            {
                SqlCommand sqlCommand = new SqlCommand("spRetieveBookDetails", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@BookId", bookId);

                sqlConnection.Open();
                BookModel bookModel = new BookModel();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        bookModel.BookId = Convert.ToInt32(sqlDataReader["BookId"]);
                        bookModel.BookName = sqlDataReader["BookName"].ToString();
                        bookModel.AuthorName = sqlDataReader["AuthorName"].ToString();
                        bookModel.DiscountPrice = Convert.ToInt32(sqlDataReader["DiscountPrice"]);
                        bookModel.OriginalPrice = Convert.ToInt32(sqlDataReader["OriginalPrice"]);
                        bookModel.BookDescription = sqlDataReader["BookDescription"].ToString();
                        bookModel.Rating = Convert.ToInt32(sqlDataReader["Rating"]);
                        bookModel.Reviewer = Convert.ToInt32(sqlDataReader["Reviewer"]);
                        bookModel.Image = sqlDataReader["Image"].ToString();
                        bookModel.BookCount = Convert.ToInt32(sqlDataReader["BookCount"]);
                    }
                    return bookModel;
                }
                else
                {
                    return null;
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public int DeleteBook(int bookId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreDbConnectionString"));
            try
            {
                SqlCommand sqlCommand = new SqlCommand("spDeleteBookDetails", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@BookId", bookId);

                sqlConnection.Open();
                int result = sqlCommand.ExecuteNonQuery();
                return result;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public List<BookModel> GetAllBooks()
        {
            SqlConnection sqlConnection = new SqlConnection(this.configuration.GetConnectionString("BookStoreDbConnectionString"));
            try
            {
                string storeprocedure = "spGetAllBook";
                SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    List<BookModel> allBookList = new List<BookModel>();
                    while (sqlDataReader.Read())
                    {
                        BookModel bookModel = new BookModel();
                        bookModel.BookId = Convert.ToInt32(sqlDataReader["BookId"]);
                        bookModel.BookName = sqlDataReader["BookName"].ToString();
                        bookModel.AuthorName = sqlDataReader["AuthorName"].ToString();
                        bookModel.DiscountPrice = Convert.ToInt32(sqlDataReader["DiscountPrice"]);
                        bookModel.OriginalPrice = Convert.ToInt32(sqlDataReader["OriginalPrice"]);
                        bookModel.BookDescription = sqlDataReader["BookDescription"].ToString();
                        bookModel.Rating = Convert.ToInt32(sqlDataReader["Rating"]);
                        bookModel.Reviewer = Convert.ToInt32(sqlDataReader["Reviewer"]);
                        bookModel.Image = sqlDataReader["Image"].ToString();
                        bookModel.BookCount = Convert.ToInt32(sqlDataReader["BookCount"]);
                        allBookList.Add(bookModel);
                    }
                    return allBookList;
                }
                else
                {
                    return null;
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
