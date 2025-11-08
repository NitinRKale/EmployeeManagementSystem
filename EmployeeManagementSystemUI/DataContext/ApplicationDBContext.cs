using EmployeeManagementSystemUI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystemUI.DataContext
{
    public class ApplicationDBContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> option) : base(option) 
        {
        }

        public DbSet<EmployeeMaster> EmployeeMasters { get; set; }
        public DbSet<DepartmentMaster>  DepartmentMasters { get; set; }
        public DbSet<DesignationMaster> DesignationMasters { get; set; } 
    }
}
