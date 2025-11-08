using System;
using System.Collections.Generic;

namespace EmployeeManagementSystemUI.Models;

public partial class DepartmentMaster2
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public virtual ICollection<EmployeeMaster2> EmployeeMasters { get; set; } = new List<EmployeeMaster2>();
}
