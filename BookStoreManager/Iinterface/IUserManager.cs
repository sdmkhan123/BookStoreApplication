using BookStoreModels;

namespace BookStoreManager.Interface
{
    public interface IUserManager
    {
        string UserSignUp(SignUpModel signUpModel);
    }
}