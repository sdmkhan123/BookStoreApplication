using BookStoreManager.Interface;
using BookStoreModels;
using BookStoreRepository.Interface;
using System;

namespace BookStoreManager.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository repository;
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }
        public int UserSignUp(SignUpModel signUpModel)
        {
            try
            {
                return this.repository.UserSignUp(signUpModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
