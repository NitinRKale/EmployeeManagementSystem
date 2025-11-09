using System;
using System.Collections.Generic;

namespace EmployeeManagementSystemUI.Models;

public partial class EmployeeMaster2
{
    /// this is just test attempt of creating pull request
    public int EmpId { get; set; }

    public string EmpFirstName { get; set; } = null!;

    public string EmpMiddleName { get; set; } = null!;

    public string EmpLastName { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string EmployeeAddress { get; set; } = null!;

    public decimal Salary { get; set; }

    public bool EmpStatus { get; set; }

    public int DepartmentId { get; set; }

    public int DesignationId { get; set; }

    public DateTime BirthDate { get; set; }

    public string EmpGender { get; set; } = null!;

    public virtual DepartmentMaster2 Department { get; set; } = null!;

    public virtual DesignationMaster2 Designation { get; set; } = null!;
}
