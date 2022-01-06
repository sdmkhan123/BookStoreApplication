using BookStoreModels;

namespace BookStoreRepository.Interface
{
    public interface IUserRepository
    {
        string UserSignUp(SignUpModel signUpModel);
    }
}