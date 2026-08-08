using System.ComponentModel.DataAnnotations;

namespace Visitor_Pass_Monitoring_System.Models.Dtos
{
    public class VisitorCreateDto
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Company is required.")]
        [StringLength(100, ErrorMessage = "Company cannot exceed 100 characters.")]
        public string Company { get; set; }

        [Required(ErrorMessage = "Contact number is required.")]
        [StringLength(20, ErrorMessage = "Contact number cannot exceed 20 characters.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact number")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Person to visit is required.")]
        [StringLength(100, ErrorMessage = "Person to visit cannot exceed 100 characters.")]
        [Display(Name = "Person to visit")]
        public string PersonToVisit { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        [StringLength(100, ErrorMessage = "Department cannot exceed 100 characters.")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Purpose is required.")]
        [StringLength(250, ErrorMessage = "Purpose cannot exceed 250 characters.")]
        public string Purpose { get; set; }

        [Required(ErrorMessage = "Valid ID presented is required.")]
        [StringLength(100, ErrorMessage = "Valid ID presented cannot exceed 100 characters.")]
        [Display(Name = "Valid ID presented")]
        public string ValidIdPresented { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? Notes { get; set; }
    }
}
