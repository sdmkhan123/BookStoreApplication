using BookStoreModels;
using System.Collections.Generic;

namespace BookStoreRepository.Interface
{
    public interface IWishlistRepository
    {
        int AddWishlist(WishlistModel wishlistModel);

        int DeleteBookFromWishlist(int wishlistId);

        List<WishlistModel> GetWishList(int userId);
    }
}