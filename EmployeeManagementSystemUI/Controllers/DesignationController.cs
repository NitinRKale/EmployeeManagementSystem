using EmployeeManagementSystemUI.DataContext;
using EmployeeManagementSystemUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystemUI.Controllers
{
    public class DesignationController : Controller
    {
        private readonly ApplicationDBContext _dbContext;

        public DesignationController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var designations =  await _dbContext.DesignationMasters.ToListAsync();
            return View(designations);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DesignationMaster designationMaster)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _dbContext.DesignationMasters.AddAsync(designationMaster);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var designation =  await _dbContext.DesignationMasters.Where(d=>d.DesignationId == id).FirstOrDefaultAsync();
            if (designation != null)
            {
                return View(designation);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DesignationMaster designationMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _dbContext.DesignationMasters.Update(designationMaster);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }
            var designation = await _dbContext.DesignationMasters.Where(d=> d.DesignationId==id).FirstOrDefaultAsync();

            if (designation != null)
            {
                return View(designation);
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
            var designation =  await _dbContext.DesignationMasters.Where(d=>d.DesignationId==id).FirstOrDefaultAsync();
            if (designation != null)
            {
                return View(designation);
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
            var designation = await _dbContext.DesignationMasters.FindAsync(id);
            if (designation != null)
            {
                _dbContext.DesignationMasters.Remove(designation);
                await _dbContext.SaveChangesAsync();                
            }
            return RedirectToAction("Index");
        }

    }
}
