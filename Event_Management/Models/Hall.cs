using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Event_Management.Models
{
    public class Hall
    {
	    public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required!")]
        [StringLength(100)]
        [Display(Description = "Hall Name")]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Invalid First Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is Required!")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        //[RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Please enter correct email address")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Address is Required!")]
        [StringLength(100)]
        public string Address { get; set; }
        [Required(ErrorMessage = "Details is Required!")]
        //[StringLength(1000)]
        public string Details { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public int Mobile { get; set; }
        [Required(ErrorMessage = "Seats is Required!")]
        public int Seats { get; set; }
        [Required(ErrorMessage = "Status is Required!")]
        [StringLength(10)]
        public string Status { get; set; }
        public IEnumerable<Book> Books { get; set; }
        [Required(ErrorMessage = "Price is Required!")]
        public double Price { get; set; }

    }
}