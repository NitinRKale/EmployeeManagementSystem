using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystemUI.Models
{
    public class DesignationMaster
    {

        [Key]
        public int DesignationId { get; set; }

        [Required(ErrorMessage ="Please enter designation.")]
        public string DesignationName { get; set; }
    }
}
