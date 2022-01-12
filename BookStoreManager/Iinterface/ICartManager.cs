using BookStoreModels;
using System.Collections.Generic;

namespace BookStoreManager.Interface
{
    public interface ICartManager
    {
        int AddToCart(CartModel cartModel);

        int UpdateCartQuantity(int cartId, int quantity);

        List<CartModel> RetrieveCartDetails(int userId);
    }
}