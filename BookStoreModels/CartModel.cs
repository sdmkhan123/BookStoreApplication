﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreModels
{
    public class CartModel
    {
        public int CartID { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int OrderQuantity { get; set; }
        public BookModel bookModel { get; set; }
    }
}
