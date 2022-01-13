using BookStoreModels;

namespace BookStoreRepository.Interface
{
    public interface IWishlistRepository
    {
        int AddWishlist(WishlistModel wishlistModel);

        int DeleteBookFromWishlist(int wishlistId);
    }
}