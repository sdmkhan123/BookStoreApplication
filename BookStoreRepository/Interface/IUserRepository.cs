using BookStoreModels;
using System.Threading.Tasks;

namespace BookStoreRepository.Interface
{
    public interface IUserRepository
    {
        Task<int> UserSignUp(SignUpModel signUpModel);
        Task<int> Login(LoginModel loginModel);
        Task<int> ResetPassword(ResetPasswordModel resetPasswordModel);
    }
}