using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class ContactUs
    {

        [Required]
        [Display(Name = "First Name")]
        [RegularExpression(pattern: @"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [StringLength(maximumLength: 35, ErrorMessage = "Fist Name must be atleast 3 characters long", MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(pattern: @"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [StringLength(maximumLength: 35, ErrorMessage = "Fist Name must be atleast 3 characters long", MinimumLength = 3)]
        public string LastName { get; set; }
        [Key]
        [Required]
        [Display(Name = "Email")]
        [DataType(dataType: DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(dataType: DataType.PhoneNumber)]
        [RegularExpression(pattern: @"^\(?([0]{1})\)?[-. ]?([1-9]{1})[-. ]?([0-9]{8})$", ErrorMessage = "Entered phone format is not valid.")]
        [StringLength(maximumLength: 10, ErrorMessage = "SA Contact Number must be exactly 10 digits long", MinimumLength = 10)]
        public string phone { get; set; }

        [Required]
        [Display(Name = "Reason for Contacting Us")]
        public Enums.Reason Reason { get; set; }

        [Required]
        [Display(Name = "Your Message")]
        [DataType(DataType.MultilineText)]
        [MinLength(3)]
        [MaxLength(255)]
        public string Message { get; set; }


        
    }
}

