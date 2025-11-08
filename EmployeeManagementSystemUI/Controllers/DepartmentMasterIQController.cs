using EmployeeManagementSystemUI.Models;
using EmployeeManagementSystemUI.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMgmtSystemADONET.Controllers
{
    public class DepartmentMasterIQController : Controller
    {
        private readonly IDepartmentMasterRepository_IQ _repository;

        public DepartmentMasterIQController(IDepartmentMasterRepository_IQ repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
             var departments = _repository.GetAllDepartments();
             return View(departments);
        }

        public IActionResult Details(int id)
        {
            var department = _repository.GetDepartmentById(id);
            if (department == null)
                return NotFound();
            return View(department);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentMaster department)
        {
            if (ModelState.IsValid)
            {
                _repository.AddDepartment(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public IActionResult Edit(int id)
        {
            var department = _repository.GetDepartmentById(id);
            if (department == null)
                return NotFound();
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DepartmentMaster department)
        {
            if (id != department.DepartmentId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _repository.UpdateDepartment(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public IActionResult Delete(int id)
        {
            var department = _repository.GetDepartmentById(id);
            if (department == null)
                return NotFound();
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteDepartment(id);
            return RedirectToAction(nameof(Index));
        }
    }
}