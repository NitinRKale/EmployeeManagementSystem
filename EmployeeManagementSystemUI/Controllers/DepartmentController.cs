using EmployeeManagementSystemUI.DataContext;
using EmployeeManagementSystemUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EmployeeManagementSystemUI.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDBContext _dbContext;

        public DepartmentController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var departments = _dbContext.DepartmentMasters.ToList();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentMaster departmentMaster)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentMaster);
            }
            await _dbContext.DepartmentMasters.AddAsync(departmentMaster);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Department");
        }

        #region Commented Code
        /*
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentMaster1 departmentMaster)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            await _dbContext.DepartmentMasters.AddAsync(departmentMaster);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Department");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var department = await _dbContext.DepartmentMasters.Where(d => d.DepartmentId == id).FirstOrDefaultAsync();

            if (department != null)
            {
                return View(department);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentMaster1 departmentMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _dbContext.DepartmentMasters.Update(departmentMaster);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var department = await _dbContext.DepartmentMasters.Where(d => d.DepartmentId == id).FirstOrDefaultAsync();
            if (department != null)
            {
                return View(department);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var department = await _dbContext.DepartmentMasters.FindAsync(id);
            if (department != null)
            {
                return View(department);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var department = await _dbContext.DepartmentMasters.FindAsync(id);
            if (department != null)
            {
                _dbContext.DepartmentMasters.Remove(department);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        */
        #endregion Commented Code
    }
}
