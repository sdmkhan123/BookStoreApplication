using BookStoreModels;

namespace BookStoreRepository.Interface
{
    public interface IUserRepository
    {
        int UserSignUp(SignUpModel signUpModel);
        int Login(LoginModel loginModel);
    }
}