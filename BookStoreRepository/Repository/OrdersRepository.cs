
using BookStoreModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using BookStoreRepository.Interface;

namespace BookStoreRepository.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        public IConfiguration Configuration { get; }
        public OrdersRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AddOrder(OrdersModel ordersModel)
        {
            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDbConnectionString"));
            try
            {
                string storeprocedure = "spAddingOrders";
                SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@UserId", ordersModel.UserId);
                sqlCommand.Parameters.AddWithValue("@AddressId", ordersModel.AddressId);
                sqlCommand.Parameters.AddWithValue("@BookId", ordersModel.BookId);
                sqlCommand.Parameters.AddWithValue("@BookQuantity", ordersModel.BookQuantity);

                sqlConnection.Open();
                int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                if (result == 2)
                {
                    return "BookId not exists";
                }
                else if (result == 1)
                {
                    return "Userid not exists";
                }
                else
                {
                    return "Ordered successfully";
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
