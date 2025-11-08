using EmployeeManagementSystemUI.DataContext;
using EmployeeManagementSystemUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EmployeeManagementSystemUI.Controllers
{
    public class EmployeeController : Controller
    {    
        private readonly ApplicationDBContext _dbContext;

        public EmployeeController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            var employees = await _dbContext.EmployeeMasters
                                 .Include(dep => dep.DepartmentMaster)
                                    .Include(des => des.DesignationMaster)
                                         .ToListAsync();

            var result = employees.Select(e => new
            {
                e.EmpId,
                e.EmpFirstName,
                e.EmpMiddleName,
                e.EmpLastName,
                e.EmailId,
                BirthDate = e.BirthDate.HasValue ? e.BirthDate.Value.ToString("dd-MM-yyyy") : "",
                e.EmpGender,
                e.PhoneNumber,
                e.EmployeeAddress,
                e.Salary,
                EmpStatus = e.EmpStatus ? "Active" : "Inactive",
                DepartmentMaster = e.DepartmentMaster?.DepartmentName ?? "",
                DesignationMaster = e.DesignationMaster?.DesignationName ?? ""
            }).ToList();

            return View(employees);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _dbContext.DepartmentMasters.ToListAsync();
            ViewBag.Designations = await _dbContext.DesignationMasters.ToListAsync();
            return View();
        }

        // CREATE: Save new employee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeMaster employee)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.EmployeeMasters.AddAsync(employee);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // UPDATE: Show edit form
        [HttpGet]        
        public async Task<IActionResult> Edit(int? id)
        {
            var employee = await _dbContext.EmployeeMasters.FindAsync(id);
            if (employee == null) return NotFound();

            //ViewBag.Departments = new SelectList(_dbContext.DepartmentMasters, "DepartmentId", "DepartmentName", employee.DepartmentId);
            //ViewBag.Designations = new SelectList(_dbContext.DesignationMasters, "DesignationId", "DesignationName", employee.DesignationId);

            ViewBag.Departments = await _dbContext.DepartmentMasters.ToListAsync();
            ViewBag.Designations = await _dbContext.DesignationMasters.ToListAsync();

            return View(employee);
        }
        
        // UPDATE: Save changes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeMaster employee)
        {
            if (id != employee.EmpId) return NotFound();

            if (ModelState.IsValid)
            {
                _dbContext.Update(employee);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // DELETE: Confirm delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _dbContext.EmployeeMasters
                .Include(e => e.DepartmentMaster)
                .Include(e => e.DesignationMaster)
                .FirstOrDefaultAsync(e => e.EmpId == id);

            if (employee == null) return NotFound();
            return View(employee);
        }

        // DELETE: Perform delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _dbContext.EmployeeMasters.FindAsync(id);
            if (employee != null)
            {
                _dbContext.EmployeeMasters.Remove(employee);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // DETAILS: View single employee
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _dbContext.EmployeeMasters
                .Include(e => e.DepartmentMaster)
                .Include(e => e.DesignationMaster)
                .FirstOrDefaultAsync(e => e.EmpId == id);

            if (employee == null) return NotFound();
            return View(employee);
        }
    }
}
