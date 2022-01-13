using BookStoreModels;

namespace BookStoreManager.Interface
{
    public interface IWishlistManager
    {
        int AddWishlist(WishlistModel wishlistModel);

        int DeleteBookFromWishlist(int wishlistId);
    }
}