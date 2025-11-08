
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystemUI.Models
{
    public class DepartmentMaster
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage ="Please enter department name")]
        public string DepartmentName { get; set; }
    }
}
