using System.ComponentModel.DataAnnotations;

namespace BookStoreModels
{
    public class WishlistModel
    {

        [Key]
        public int WishlistId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int BookId { get; set; }

        public BookModel Book { get; set; }
    }
}
