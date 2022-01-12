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
                    sqlCommand.Parameters.AddWithValue("@OrderQuantity", cartModel.OrderQuantity);
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

