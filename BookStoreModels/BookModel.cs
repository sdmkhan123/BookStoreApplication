using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreModels
{
    public class BookModel
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public int DiscountPrice { get; set; }
        [Required]
        public int OriginalPrice { get; set; }
        [Required]
        public string BookDescription { get; set; }
        [Required]
        public float Rating { get; set; }
        [Required]
        public int Reviewer { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int BookCount { get; set; }
    }
}
