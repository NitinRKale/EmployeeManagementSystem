using EmployeeManagementSystemUI.DataAccessLayer;
using EmployeeManagementSystemUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace EmployeeManagementSystemUI.Controllers
{
    public class DepartmentDBFirstController : Controller
    {
        private readonly DepartmentRepository _repository;

        public DepartmentDBFirstController(DepartmentRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var departments = _repository.GetAll();
            return View(departments);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(DepartmentMaster2 department)
        {
            if (ModelState.IsValid)
            {
                _repository.Insert(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public IActionResult Details(int id)
        {
            var department = _repository.GetById(id);
            if (department == null) return NotFound();
            return View(department);
        }

        public IActionResult Edit(int id)
        {
            var department = _repository.GetById(id);
            if (department == null) return NotFound();
            return View(department);
        }

        [HttpPost]
        public IActionResult Edit(DepartmentMaster2 department)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public IActionResult Delete(int id)
        {
            var department = _repository.GetById(id);
            if (department == null) return NotFound();
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
