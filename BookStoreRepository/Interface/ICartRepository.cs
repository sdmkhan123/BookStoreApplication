using BookStoreModels;

namespace BookStoreRepository.Interface
{
    public interface ICartRepository
    {
        int AddToCart(CartModel cartModel);

        int UpdateCartQuantity(int cartId, int quantity);
    }
}