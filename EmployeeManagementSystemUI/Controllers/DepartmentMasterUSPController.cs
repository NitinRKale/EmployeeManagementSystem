using EmployeeManagementSystemUI.Models;
using EmployeeManagementSystemUI.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystemUI.Controllers
{
    public class DepartmentMasterUSPController : Controller
    {
        private readonly IDepartmentMasterRepository_usp _repository;
        public DepartmentMasterUSPController(IDepartmentMasterRepository_usp repository)
        {
            _repository = repository;
        }

        // GET: DepartmentMaster
        public IActionResult Index()
        {
            var departments = _repository.GetAll();
            return View(departments);
        }

        // GET: DepartmentMaster/Details/5
        public IActionResult Details(int id)
        {
            var department = _repository.GetById(id);
            if (department == null)
                return NotFound();
            return View(department);
        }

        // GET: DepartmentMaster/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DepartmentMaster/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentMaster department)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: DepartmentMaster/Edit/5
        public IActionResult Edit(int id)
        {
            var department = _repository.GetById(id);
            if (department == null)
                return NotFound();
            return View(department);
        }

        // POST: DepartmentMaster/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DepartmentMaster department)
        {
            if (id != department.DepartmentId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _repository.Update(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: DepartmentMaster/Delete/5
        public IActionResult Delete(int id)
        {
            var department = _repository.GetById(id);
            if (department == null)
                return NotFound();
            return View(department);
        }

        // POST: DepartmentMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
