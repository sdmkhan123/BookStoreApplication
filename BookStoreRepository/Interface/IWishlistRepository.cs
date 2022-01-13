using BookStoreModels;

namespace BookStoreRepository.Interface
{
    public interface IWishlistRepository
    {
        int AddWishlist(WishlistModel wishlistModel);
    }
}