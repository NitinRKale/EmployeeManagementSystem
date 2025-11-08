using EmployeeManagementSystemUI.Models;

namespace EmployeeManagementSystemUI.IRepository
{
    public interface IDepartmentMasterRepository_IQ
    {
        IEnumerable<DepartmentMaster> GetAllDepartments();
        DepartmentMaster GetDepartmentById(int id);
        void AddDepartment(DepartmentMaster department);
        void UpdateDepartment(DepartmentMaster department);
        void DeleteDepartment(int id);

        List<DepartmentMaster> GetAllDepartmentsUsingDataAdapter();
    }
}
