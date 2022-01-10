using BookStoreModels;
using System.Threading.Tasks;

namespace BookStoreManager.Interface
{
    public interface IUserManager
    {
        Task<int> UserSignUp(SignUpModel signUpModel);
        Task<int> Login(LoginModel loginModel);
        Task<int> ResetPassword(ResetPasswordModel resetPasswordModel);
        int ForgotPassword(string Email);
    }
}