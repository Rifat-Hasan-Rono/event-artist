using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Event_Management.Models
{
    public class Book
    {
	    public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required!")]
        [StringLength(100)]
        [Display(Description = "User Name")]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Invalid First Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address is Required!")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Email is Required!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        //[RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",ErrorMessage = "Please enter correct email address")]

        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public int Mobile { get; set; }
        [Required(ErrorMessage = "Booking date is required")]
        [Display(Name = "Booking Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Remote("IsDateExist", "Books",AdditionalFields = "HallId",ErrorMessage = "Date already exist")]
        public DateTime DateTime { get; set; }
        
        public int HallId { get; set; }
        [Required(ErrorMessage = "Hall is Required!")]
        public Hall Hall { get; set; }
    }
}