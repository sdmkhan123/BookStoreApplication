using System.ComponentModel.DataAnnotations;

namespace BookStoreModels
{
    public class ResetPasswordModel
    {
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
