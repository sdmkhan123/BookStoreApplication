using BookStoreModels;
using System.Collections.Generic;

namespace BookStoreRepository.Interface
{
    public interface ICartRepository
    {
        int AddToCart(CartModel cartModel);

        int UpdateCartQuantity(int cartId, int quantity);

        List<CartModel> RetrieveCartDetails(int userId);
    }
}