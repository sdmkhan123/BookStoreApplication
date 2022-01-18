using System.ComponentModel.DataAnnotations;

namespace BookStoreModels
{
    public class OrdersModel
    {

        [Key]
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public int AddressId { get; set; }

        public int BookId { get; set; }

        public int TotalPrice { get; set; }

        public int BookQuantity { get; set; }

        public string OrderDate { get; set; }

        public BookModel bookModel { get; set; }
    }
}
