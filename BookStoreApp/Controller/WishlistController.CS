using BookStoreManager.Interface;
using BookStoreModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreApp.Controller
{

    public class WishlistController : ControllerBase
    {

        private readonly IWishlistManager wishlistManager;


        public WishlistController(IWishlistManager wishlistManager)
        {
            this.wishlistManager = wishlistManager;
        }

        [HttpPost]
        [Route("api/addToWishlist")]
        public IActionResult AddWishlist([FromBody] WishlistModel wishlist)
        {
            try
            {
                int result = this.wishlistManager.AddWishlist(wishlist);
                if (result == 1)
                {

                    return this.Ok(new { Status = true, Message = "Book Added successfully to Wishlist", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book already added to wishlist", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("deleteWishlist")]
        public IActionResult DeleteBookFromWishlist(int wishlistId)
        {
            try
            {
                int result = this.wishlistManager.DeleteBookFromWishlist(wishlistId);
                if (result == 1)
                {

                    return this.Ok(new { Status = true, Message = "Wishlist deleted successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new  { Status = false, Message = "WishlistId does not exists", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
