﻿using BookStoreManager.Interface;
using BookStoreModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApp.Controller
{
    public class CartController : ControllerBase
    {
        private readonly ICartManager cartManager;

        public CartController(ICartManager cartManager)
        {
            this.cartManager = cartManager;
        }

        [HttpPost]
        [Route("addToCarts")]
        public IActionResult AddToCart([FromBody] CartModel cartModel)
        {
            try
            {
                int result = this.cartManager.AddToCart(cartModel);
                if (result==1)
                {
                    return this.Ok(new { Status = true, Message = "Book Added succssfully to Cart", Data = result});
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book is not able to add into cart", Data = result});
                }
            }
            catch(Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("api/updateBookQuantity")]
        public IActionResult UpdateCartQuantity(int cartId, int quantity)
        {
            try
            {
                int result = this.cartManager.UpdateCartQuantity(cartId, quantity);
                if (result==1)
                {
                    return this.Ok(new { Status = true, Message = "Quantity Updated Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Quantity Updated Successfully", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}