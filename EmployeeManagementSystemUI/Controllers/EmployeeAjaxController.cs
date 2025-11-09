using EmployeeManagementSystemUI.DataContext;
using EmployeeManagementSystemUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Threading.Tasks;

namespace EmployeeManagementSystemUI.Controllers
{
    public class EmployeeAjaxController : Controller
    {
        private readonly ApplicationDBContext _dbContext;

        public EmployeeAjaxController(ApplicationDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _dbContext.EmployeeMasters
                .Include(dep => dep.DepartmentMaster)
                .Include(des => des.DesignationMaster)
                .Select(e => new
                {
                    e.EmpId,
                    e.EmpFirstName,
                    e.EmpMiddleName,
                    e.EmpLastName,
                    e.EmailId,
                    //BirthDate = e.BirthDate.HasValue ? e.BirthDate.Value.ToString("yyyy-MM-dd") : "",
                    BirthDate = e.BirthDate.HasValue ? e.BirthDate.Value.ToString("dd-MM-yyyy") : "",
                    e.EmpGender,
                    e.PhoneNumber,
                    e.EmployeeAddress,
                    e.Salary,
                    EmpStatus = e.EmpStatus ? "Active" : "Inactive",
                    Department = e.DepartmentMaster != null ? e.DepartmentMaster.DepartmentName : "",
                    Designation = e.DesignationMaster != null ? e.DesignationMaster.DesignationName : ""
                })
                .ToListAsync();

            return Json(new { data = employees });
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _dbContext.DepartmentMasters.ToListAsync();
            ViewBag.Designations = await _dbContext.DesignationMasters.ToListAsync();
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([FromBody] EmployeeMaster employee)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return Json(new { success = "Fail", message = "Error creating employee." });
        //        }

        //        await _dbContext.EmployeeMasters.AddAsync(employee);
        //        await _dbContext.SaveChangesAsync();

        //        return Json(new { success = true, result = "Saved", message = "Employee created successfully.", ErrorMessage = "", redirectTo = Url.Action("Index", "Employee") });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = "Error", ErrorMessage = ex.Message });
        //    }
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeMaster employee)
        {
            if (employee == null)
            {
                return Json(new { success = false, message = "Invalid request payload. Make sure JSON is sent and property names match the model." });
            }

            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = new List<string>();
                    foreach (var entry in ModelState)
                    {
                        foreach (var err in entry.Value.Errors)
                        {
                            // prefer the explicit error message, fall back to exception message
                            errors.Add(string.IsNullOrWhiteSpace(err.ErrorMessage) ? (err.Exception?.Message ?? "") : err.ErrorMessage);
                        }
                    }

                    return Json(new { success = false, message = "Validation failed.", errors });
                }

                await _dbContext.EmployeeMasters.AddAsync(employee);
                await _dbContext.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    result = "Saved",
                    message = "Employee created successfully.",
                    ErrorMessage = "",
                    redirectTo = Url.Action("Index", "EmployeeAjax")
                });
            }
            catch (Exception ex)
            {
                // Optional: log the exception
                return Json(new { success = false, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id > 0)
            {
                var Employee = await _dbContext.EmployeeMasters.FindAsync(id);
                ViewBag.Departments = await _dbContext.DepartmentMasters.ToListAsync();
                ViewBag.Designations = await _dbContext.DesignationMasters.ToListAsync();
                return View(Employee);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeMaster employee)
        {
            if (employee == null)
            {
                return Json(new { success = false, message = "Invalid request payload. Ensure JSON is sent and property names match the model." });
            }

            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = new List<string>();
                    foreach (var entry in ModelState)
                    {
                        foreach (var err in entry.Value.Errors)
                        {
                            errors.Add(string.IsNullOrWhiteSpace(err.ErrorMessage) ? (err.Exception?.Message ?? "") : err.ErrorMessage);
                        }
                    }
                    return Json(new { success = false, message = "Validation failed.", errors });
                }

                // Attach and update the entity. If tracking issues arise consider fetching the entity and mapping properties.
                _dbContext.EmployeeMasters.Update(employee);
                await _dbContext.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    result = "Updated",
                    message = "Employee updated successfully.",
                    ErrorMessage = "",
                    redirectTo = Url.Action("Index", "EmployeeAjax")
                });
            }
            catch (Exception ex)
            {
                // Log exception as needed
                return Json(new { success = false, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id > 0)
            {
                var Employee = await _dbContext.EmployeeMasters
                        .Include(dep => dep.DepartmentMaster)
                            .Include(des => des.DesignationMaster).FirstOrDefaultAsync(e => e.EmpId == id);
                return View(Employee);
            }
            return NotFound();
        }

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
        public async Task<IActionResult> DeleteConfirmed(int empId)
        {
            try
            {
                if (empId <= 0)
                {
                    return Json(new { success = false, message = "Error encountered while delete employee." });
                }

                var employee = await _dbContext.EmployeeMasters.FirstOrDefaultAsync(d => d.EmpId == empId);

                if (employee != null)
                {
                    _dbContext.EmployeeMasters.Remove(employee);
                    await _dbContext.SaveChangesAsync();
                    return Json(new
                    {
                        success = true,
                        result = "Deleted",
                        message = "Employee deleted successfully.",
                        ErrorMessage = "",
                        redirectTo = Url.Action("Index", "EmployeeAjax")
                    });
                }
                return Json(new { success = false, message = "Employee details was not found" });
            }
            catch (Exception ex)
            {
                return Json(new { success = "Error", ErrorMessage = ex.Message });
            }
        }
    }
}
