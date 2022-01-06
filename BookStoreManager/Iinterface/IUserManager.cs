using BookStoreModels;

namespace BookStoreManager.Interface
{
    public interface IUserManager
    {
        int UserSignUp(SignUpModel signUpModel);
    }
}