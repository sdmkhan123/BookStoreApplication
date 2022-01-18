
using BookStoreModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using BookStoreRepository.Interface;
using System.Collections.Generic;

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
        public List<OrdersModel> RetrieveOrderDetails(int userId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDbConnectionString"));
            try
            {
                string storeprocedure = "spGetAllOrders";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    List<OrdersModel> order = new List<OrdersModel>();
                    if (sqlData.HasRows)
                    {
                        while (sqlData.Read())
                        {
                            OrdersModel orderModel = new OrdersModel();
                            BookModel bookModel = new BookModel();
                            bookModel.BookName = sqlData["BookName"].ToString();
                            bookModel.AuthorName = sqlData["AuthorName"].ToString();
                            bookModel.DiscountPrice = Convert.ToInt32(sqlData["DiscountPrice"]);
                            bookModel.OriginalPrice = Convert.ToInt32(sqlData["OriginalPrice"]);
                            bookModel.Image = sqlData["Image"].ToString();
                            orderModel.OrderId = Convert.ToInt32(sqlData["OrdersId"]);
                            orderModel.OrderDate = sqlData["OrderDate"].ToString();
                            orderModel.bookModel = bookModel;
                            order.Add(orderModel);
                        }
                        return order;
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
    }
}
