using EmployeeManagementSystemUI.Models;

namespace EmployeeManagementSystemUI.IRepository
{
    public interface IEmployeeMasterUSP
    {
        IEnumerable<EmployeeMasterADO> getAllEmployees();
        EmployeeMasterADO getEmployeeDetailsById(int id);
        void addEmployeeDetails(EmployeeMasterADO department);
        void updateEmployeeDetails(EmployeeMasterADO department);
        void deleteEmployeeDetails(int id);
    }
}
