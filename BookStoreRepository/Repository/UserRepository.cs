using BookStoreModels;
using BookStoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration configuration;
        public string connectionString { get; set; }  = "BookStoreDbConnectionString";

        public UserRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string EncryptPassword(string password)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }

        public async Task<int> UserSignUp(SignUpModel signUpModel)
        {
            try
            {
                if(signUpModel != null)
                {
                    string ConnectionStrings = configuration.GetConnectionString(connectionString);
                    using (SqlConnection con = new SqlConnection(ConnectionStrings))
                    {
                        SqlCommand sqlCommand = new SqlCommand("SignUpUsers", con);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@FullName", signUpModel.FullName);
                        sqlCommand.Parameters.AddWithValue("@EmailId", signUpModel.EmailId);
                        sqlCommand.Parameters.AddWithValue("@Password", EncryptPassword(signUpModel.Password));
                        sqlCommand.Parameters.AddWithValue("@MobileNum", signUpModel.MobileNum);
                        con.Open();
                        int result = await sqlCommand.ExecuteNonQueryAsync();
                        con.Close();
                        return result;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
        public async Task<int> Login(LoginModel loginModel)
        {
            try
            {
                if (loginModel != null)
                {
                    string ConnectionStrings = configuration.GetConnectionString(connectionString);
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings))
                    {
                        SqlCommand sqlCommand = new SqlCommand("spForLogin", sqlConnection);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@EmailId", loginModel.EmailId);
                        sqlCommand.Parameters.AddWithValue("@Password", EncryptPassword(loginModel.Password));
                        sqlCommand.Parameters.Add("@User", SqlDbType.Int).Direction = ParameterDirection.Output;
                        sqlConnection.Open();
                        await sqlCommand.ExecuteNonQueryAsync();
                        int UserValue = (int)sqlCommand.Parameters["@user"].Value;
                        if(UserValue == 2)
                        {
                            return 2;
                        }
                        else if(UserValue == 1)
                        {
                            return 1;
                        }
                        return 0;
                    }
                }
                return -1;
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
        public async Task<int> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                if (resetPasswordModel.EmailId != null)
                {
                    string ConnectionStrings = configuration.GetConnectionString(connectionString);
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings))
                    {
                        SqlCommand sqlCommand = new SqlCommand("spForResetPassword", sqlConnection);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@EmailId", resetPasswordModel.EmailId);
                        sqlCommand.Parameters.AddWithValue("@NewPassword", EncryptPassword(resetPasswordModel.NewPassword));
                        sqlConnection.Open();
                        int result = await sqlCommand.ExecuteNonQueryAsync();
                        return result;
                    }
                }
                return 0;
            }
            catch (ArgumentException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }
    }
}