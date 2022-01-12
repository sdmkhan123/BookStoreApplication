using BookStoreManager.Interface;
using BookStoreModels;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class CartManager : ICartManager
    {
        private readonly ICartRepository cartRepository;

        public CartManager(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }
        public int AddToCart(CartModel cartModel)
        {
            try
            {
                return this.cartRepository.AddToCart(cartModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int UpdateCartQuantity(int cartId, int quantity)
        {
            try
            {
                return this.cartRepository.UpdateCartQuantity(cartId, quantity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
