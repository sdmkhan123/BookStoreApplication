using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreModels
{
    public class AddressModel
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }
    }
}
