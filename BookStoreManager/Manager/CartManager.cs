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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
