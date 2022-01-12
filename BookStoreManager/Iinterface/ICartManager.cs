using BookStoreModels;

namespace BookStoreManager.Interface
{
    public interface ICartManager
    {
        int AddToCart(CartModel cartModel);
    }
}