using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectDoc.Models

{
    public class Admin
    {
        public int ID { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "This field is required.")]
        public string UserName { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}