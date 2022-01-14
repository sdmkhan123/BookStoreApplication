using BookStoreModels;
using BookStoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BookStoreRepository.Repository
{
    public class WishlistRepository : IWishlistRepository
    {
        public IConfiguration Configuration { get; }
        public WishlistRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public int AddWishlist(WishlistModel wishlistModel)
        {
            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDbConnectionString"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spCreateWishlist", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@BookId", wishlistModel.BookId);
                    sqlCommand.Parameters.AddWithValue("@UserId", wishlistModel.UserId);
                    sqlConnection.Open();
                    int result = sqlCommand.ExecuteNonQuery();
                    return result;
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
        public int DeleteBookFromWishlist(int wishlistId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDbConnectionString"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spDeleteWishlist", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@WishlistId", wishlistId);
                    sqlConnection.Open();
                    int result = sqlCommand.ExecuteNonQuery();
                    return result;
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

        public List<WishlistModel> GetWishList(int userId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDbConnectionString"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("SpGetBooksFromWishList", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        List<WishlistModel> wishList = new List<WishlistModel>();
                        while (sqlDataReader.Read())
                        {
                            BookModel booksModel = new BookModel();
                            WishlistModel wishListModel = new WishlistModel();

                            wishListModel.WishlistId = Convert.ToInt32(sqlDataReader["WishlistId"]);
                            wishListModel.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                            wishListModel.BookId = Convert.ToInt32(sqlDataReader["BookId"]);
                            booksModel.AuthorName = sqlDataReader["AuthorName"].ToString();
                            booksModel.BookName = sqlDataReader["BookName"].ToString();
                            booksModel.BookDescription = sqlDataReader["BookDescription"].ToString();
                            booksModel.DiscountPrice = Convert.ToInt32(sqlDataReader["DiscountPrice"]);
                            booksModel.OriginalPrice = Convert.ToInt32(sqlDataReader["OriginalPrice"]);
                            booksModel.BookCount = Convert.ToInt32(sqlDataReader["BookCount"]);
                            booksModel.Rating = Convert.ToInt32(sqlDataReader["Rating"]);
                            booksModel.Image = sqlDataReader["Image"].ToString();

                            wishList.Add(wishListModel);
                        }
                        return wishList;
                    }
                    else
                    {
                        return null;
                    }
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
