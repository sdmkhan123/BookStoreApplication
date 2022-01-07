using BookStoreModels;

namespace BookStoreManager.Interface
{
    public interface IUserManager
    {
        int UserSignUp(SignUpModel signUpModel);
        int Login(LoginModel loginModel);
        int ResetPassword(ResetPasswordModel resetPasswordModel);
    }
}