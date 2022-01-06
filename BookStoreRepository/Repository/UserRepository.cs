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
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration config;
        public string connectionString { get; set; }  = "BookStoreDbConnectionString";
        public UserRepository(IConfiguration configuration)
        {
            this.config = configuration;
        }
        public string EncryptPassword(string password)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }
        public string UserSignUp(SignUpModel signUpModel)
        {
            try
            {
                if(signUpModel != null)
                {
                    string ConnectionStrings = config.GetConnectionString(connectionString);
                    using (SqlConnection con = new SqlConnection(ConnectionStrings))
                    {
                        SqlCommand sqlCommand = new SqlCommand("SignUpUsers", con);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@FullName", signUpModel.FullName);
                        sqlCommand.Parameters.AddWithValue("@EmailId", signUpModel.EmailId);
                        sqlCommand.Parameters.AddWithValue("@Password", EncryptPassword(signUpModel.Password));
                        sqlCommand.Parameters.AddWithValue("@MobileNum", signUpModel.MobileNum);
                        con.Open();
                        int result = sqlCommand.ExecuteNonQuery();
                        con.Close();
                        if (result != 0)
                        {
                            return "Registration is successful"; ;
                        }
                        return "Registration is Unsuccessful";
                    }
                }
                else
                {
                    return "Registration is unsuccessful";
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
    }
}