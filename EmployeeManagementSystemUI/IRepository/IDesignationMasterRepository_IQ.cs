using EmployeeManagementSystemUI.Models;

namespace EmployeeManagementSystemUI.IRepository
{
    public interface IDesignationMasterRepository_IQ
    {
        IEnumerable<DesignationMaster> GetAll();
        DesignationMaster GetById(int id);
        void Add(DesignationMaster department);
        void Update(DesignationMaster department);
        void Delete(int id);
    }
}
