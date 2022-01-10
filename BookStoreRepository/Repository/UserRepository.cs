using BookStoreModels;
using BookStoreRepository.Interface;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
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
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionStrings))
                    {
                        SqlCommand sqlCommand = new SqlCommand("SignUpUsers", sqlConnection);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@FullName", signUpModel.FullName);
                        sqlCommand.Parameters.AddWithValue("@EmailId", signUpModel.EmailId);
                        sqlCommand.Parameters.AddWithValue("@Password", EncryptPassword(signUpModel.Password));
                        sqlCommand.Parameters.AddWithValue("@MobileNum", signUpModel.MobileNum);
                        sqlConnection.Open();
                        int result = await sqlCommand.ExecuteNonQueryAsync();
                        sqlConnection.Close();
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
                        int UserValue = (int)sqlCommand.Parameters["@User"].Value;
                        if (UserValue == 2)
                        {
                            return 2;
                        }
                        else if (UserValue == 1)
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

        public int ForgotPassword(string EmailId)
        {
            string ConnectionStrings = configuration.GetConnectionString(connectionString);
            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spUserForget", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@EmailId", EmailId);
                    sqlCommand.Parameters.Add("@user", SqlDbType.Int).Direction = ParameterDirection.Output;
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    int UserValue = Convert.ToInt32(sqlCommand.Parameters["@user"].Value);
                    if (UserValue != 0)
                    {
                        MailMessage mail = new MailMessage();
                        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                        mail.From = new MailAddress(this.configuration["Credentials:EmailId"]);
                        mail.To.Add(EmailId);
                        mail.Subject = "To Test Out Mail";
                        SendMSMQ();
                        mail.Body = ReceiveMSMQ();

                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new System.Net.NetworkCredential(this.configuration["Credentials:EmailId"], this.configuration["Credentials:Password"]);
                        SmtpServer.EnableSsl = true;
                        SmtpServer.Send(mail);
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }

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

        public void SendMSMQ()
        {
            MessageQueue messageQueue;
            if (MessageQueue.Exists(@".\Private$\BookStoreApp"))
            {
                messageQueue = new MessageQueue(@".\Private$\BookStoreApp");
            }
            else
            {
                messageQueue = MessageQueue.Create(@".\Private$\BookStoreApp");
            }

            string body = "This is for Testing SMTP mail from GMAIL";
            messageQueue.Label = "Mail Body";
            messageQueue.Send(body);
        }

        public string ReceiveMSMQ()
        {
            MessageQueue messageQueue = new MessageQueue(@".\Private$\BookStoreApp");
            var receivemsg = messageQueue.Receive();
            receivemsg.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            return receivemsg.Body.ToString();
        }
    }
}