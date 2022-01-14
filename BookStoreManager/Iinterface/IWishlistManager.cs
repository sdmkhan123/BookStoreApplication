using BookStoreModels;
using System.Collections.Generic;

namespace BookStoreManager.Interface
{
    public interface IWishlistManager
    {
        int AddWishlist(WishlistModel wishlistModel);

        int DeleteBookFromWishlist(int wishlistId);

        List<WishlistModel> GetWishList(int userId);
    }
}