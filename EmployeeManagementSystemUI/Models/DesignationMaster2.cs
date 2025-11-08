using System;
using System.Collections.Generic;

namespace EmployeeManagementSystemUI.Models;

public partial class DesignationMaster2
{
    public int DesignationId { get; set; }

    public string DesignationName { get; set; } = null!;

    public virtual ICollection<EmployeeMaster2> EmployeeMasters { get; set; } = new List<EmployeeMaster2>();
}
