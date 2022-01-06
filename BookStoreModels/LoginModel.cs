﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreModels
{
    public class LoginModel
    {
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
