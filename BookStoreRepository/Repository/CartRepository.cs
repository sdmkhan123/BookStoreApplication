using BookStoreModels;
using BookStoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class CartRepository : ICartRepository
    {
        public IConfiguration Configuration { get; }

        public CartRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public int AddToCart(CartModel cartModel)
        {
            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDbConnectionString"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spAddingCart", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@BookId", cartModel.BookId);
                    sqlCommand.Parameters.AddWithValue("@UserId", cartModel.UserId);
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
        public int UpdateCartQuantity(int cartId, int quantity)
        {
            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDbConnectionString"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spUpdateQuantity", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@CartID", cartId);
                    sqlCommand.Parameters.AddWithValue("@OrderQuantity", quantity);
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

        public List<CartModel> RetrieveCartDetails(int userId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDbConnectionString"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spGetCartDetails", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    List<CartModel> cartList = new List<CartModel>();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            CartModel cartModel = new CartModel();
                            BookModel bookModel = new BookModel();
                            bookModel.BookName = sqlDataReader["BookName"].ToString();
                            bookModel.AuthorName = sqlDataReader["AuthorName"].ToString();
                            bookModel.DiscountPrice = Convert.ToInt32(sqlDataReader["DiscountPrice"]);
                            bookModel.OriginalPrice = Convert.ToInt32(sqlDataReader["OriginalPrice"]);
                            cartModel.CartID = Convert.ToInt32(sqlDataReader["CartID"]);
                            cartModel.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                            cartModel.BookId = Convert.ToInt32(sqlDataReader["BookId"]);
                            cartModel.OrderQuantity = Convert.ToInt32(sqlDataReader["OrderQuantity"]);
                            cartModel.bookModel = bookModel;
                            cartList.Add(cartModel);
                        }
                        return cartList;
                    }
                    else
                    {
                        return null;
                    }
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
    }
}

