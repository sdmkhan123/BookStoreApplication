using BookStoreModels;

namespace BookStoreRepository.Interface
{
    public interface ICartRepository
    {
        int AddToCart(CartModel cartModel);
    }
}