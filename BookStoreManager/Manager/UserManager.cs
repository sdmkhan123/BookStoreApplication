using BookStoreManager.Interface;
using BookStoreModels;
using BookStoreRepository.Interface;
using System;
using System.Threading.Tasks;

namespace BookStoreManager.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository repository;
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }
        public async Task<int> UserSignUp(SignUpModel signUpModel)
        {
            try
            {
                return await this.repository.UserSignUp(signUpModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<int> Login(LoginModel loginModel)
        {
            try
            {
                return await this.repository.Login(loginModel);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<int> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                return await this.repository.ResetPassword(resetPasswordModel);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
