using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreModels
{
    public class FeedbackModel
    {
		[Key]
		public int FeedbackId { get; set; }
		public int UserId { get; set; }
		public int BookId { get; set; }
		public string Comments { get; set; }
		public int Ratings { get; set; }
		public SignUpModel User { get; set; }
	}
}
