using EmployeeManagementSystemUI.Models;

namespace EmployeeManagementSystemUI.IRepository
{
    public interface IDepartmentMasterRepository_usp
    {
        IEnumerable<DepartmentMaster> GetAll();
        DepartmentMaster GetById(int id);
        void Add(DepartmentMaster department);
        void Update(DepartmentMaster department);
        void Delete(int id);
    }
}
