using BookStoreModels;
using BookStoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class AddressRepository : IAddressRepository
    {
        public IConfiguration Configuration { get; }
        public AddressRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public int AddAddress(AddressModel addressModel)
        {
            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDbConnectionString"));
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SpAddUserAddress", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@Address", addressModel.Address);
                sqlCommand.Parameters.AddWithValue("@City", addressModel.City);
                sqlCommand.Parameters.AddWithValue("@State", addressModel.State);
                sqlCommand.Parameters.AddWithValue("@Type", addressModel.Type);
                sqlCommand.Parameters.AddWithValue("@UserId", addressModel.UserId);
                return sqlCommand.ExecuteNonQuery();
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
