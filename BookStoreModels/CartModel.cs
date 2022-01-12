using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreModels
{
    public class CartModel
    {
        [Key]
        public int CartID { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int BookId { get; set; }

        public int OrderQuantity { get; set; }

        public BookModel bookModel { get; set; }
    }
}
