using BookStoreModels;
using BookStoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
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

        public int UpdateAddress(AddressModel addressModel)
        {
            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDbConnectionString"));
            try
            {
                SqlCommand sqlCommand = new SqlCommand("spUpdateUserAddress", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Address", addressModel.Address);
                sqlCommand.Parameters.AddWithValue("@City", addressModel.City);
                sqlCommand.Parameters.AddWithValue("@State", addressModel.State);
                sqlCommand.Parameters.AddWithValue("@Type", addressModel.Type);
                sqlCommand.Parameters.AddWithValue("@AddressID", addressModel.AddressId);
                sqlConnection.Open();
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

        public List<AddressModel> GetUserAddress(int userId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDbConnectionString"));
            try
            {
                SqlCommand sqlCommand = new SqlCommand("GetAddressDetails", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@UserId", userId);
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<AddressModel> ListOfaddresses = new List<AddressModel>();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        AddressModel addressModel = new AddressModel();
                        addressModel.AddressId = sqlDataReader.GetInt32("AddressId");
                        addressModel.Address = sqlDataReader.GetString("Address");
                        addressModel.City = sqlDataReader.GetString("City");
                        addressModel.State = sqlDataReader.GetString("State");
                        addressModel.Type = sqlDataReader.GetString("Type");
                        ListOfaddresses.Add(addressModel);
                    }
                }
                return ListOfaddresses;
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
