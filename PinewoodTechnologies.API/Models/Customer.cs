using System.ComponentModel.DataAnnotations;

namespace PinewoodTechnologies.API.Models
{
    // Represents a customer entity
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DoB { get; set; }

        [Required(ErrorMessage = "Address Line 1 is required")]
        [StringLength(100)]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [StringLength(100)]
        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [StringLength(100)]
        [Display(Name = "Address Line 3")]
        public string AddressLine3 { get; set; }

        [Required(ErrorMessage = "Postcode is required")]
        [StringLength(20)]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "Telephone Number is required")]
        [Phone(ErrorMessage = "Invalid Telephone Number")]
        [Display(Name = "Telephone Number")]
        public string TelephoneNumber { get; set; }
    }
}
