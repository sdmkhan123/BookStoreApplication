using BookStoreManager.Interface;
using BookStoreModels;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;

namespace BookStoreManager.Manager
{
    public class WishlistManager : IWishlistManager
    {
        private readonly IWishlistRepository wishlistRepository;

        public WishlistManager(IWishlistRepository wishlistRepository)
        {
            this.wishlistRepository = wishlistRepository;
        }

        public int AddWishlist(WishlistModel wishlistModel)
        {
            try
            {
                return this.wishlistRepository.AddWishlist(wishlistModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int DeleteBookFromWishlist(int wishlistId)
        {
            try
            {
                return this.wishlistRepository.DeleteBookFromWishlist(wishlistId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<WishlistModel> GetWishList(int userId)
        {
            try
            {
                return this.wishlistRepository.GetWishList(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
