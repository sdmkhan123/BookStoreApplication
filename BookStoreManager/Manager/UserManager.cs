﻿using BookStoreManager.Interface;
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
        public int Login(LoginModel loginModel)
        {
            try
            {
                return this.repository.Login(loginModel);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                return this.repository.ResetPassword(resetPasswordModel);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
