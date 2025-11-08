using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystemUI.Models
{
    public class EmployeeMasterADO
    {
        [Key]
        public int EmpId { get; set; }

        [Required(ErrorMessage = "employee first name is required.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "first name must be between 3 and 20 characters.")]
        [Display(Name = "First Name")]
        public string EmpFirstName { get; set; }

        [Required(ErrorMessage = "employee middle name is required.")]
        [Display(Name = "Middle Name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Middle name must be between 3 and 20 characters.")]
        public string EmpMiddleName { get; set; }

        [Required(ErrorMessage = "employee last name is required.")]
        [Display(Name = "Last Name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Last name must be between 3 and 20 characters.")]
        public string EmpLastName { get; set; }

        [Required(ErrorMessage = "Please enter employee emailid")]
        [Display(Name = "Email Address")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Please select birth date.")]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }

        [Required, StringLength(15)]
        [Display(Name = "Gender")]
        public string EmpGender { get; set; }

        [Required(ErrorMessage = "phone number is required.")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "Please enter valid 10 digit phone number.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Employee address is required.")]
        [Display(Name = "Resident Address")]
        public string EmployeeAddress { get; set; }

        [Required(ErrorMessage = "Please enter employee salary"), Range(0, int.MaxValue)]
        [Display(Name = "Salary")]

        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Please select employee status")]
        [Display(Name = "Status")]
        public bool EmpStatus { get; set; }

        [ForeignKey("DepartmentMaster")]
        [Required(ErrorMessage = "Please Select Department")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        [ForeignKey("DesignationMaster")]
        [Required(ErrorMessage = "Please Select Designation")]
        [Display(Name = "Designation")]
        public int DesignationId { get; set; }
        public string? DesignationName { get; set; }

    }
}
