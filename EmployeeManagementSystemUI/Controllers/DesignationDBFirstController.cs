using EmployeeManagementSystemUI.DataAccessLayer;
using EmployeeManagementSystemUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystemUI.Controllers
{
    public class DesignationDBFirstController : Controller
    {
        private readonly DesignationRepository _repository;

        public DesignationDBFirstController(DesignationRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var designations = _repository.GetAll();
            return View(designations);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(DesignationMaster2 designation)
        {
            if (ModelState.IsValid)
            {
                _repository.Insert(designation);
                return RedirectToAction(nameof(Index));
            }
            return View(designation);
        }

        public IActionResult Edit(int id)
        {
            var designation = _repository.GetById(id);
            if (designation == null) return NotFound();
            return View(designation);
        }

        [HttpPost]
        public IActionResult Edit(DesignationMaster2 designation)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(designation);
                return RedirectToAction(nameof(Index));
            }
            return View(designation);
        }

        public IActionResult Details(int id)
        {
            var designation = _repository.GetById(id);
            if (designation == null) return NotFound();
            return View(designation);
        }

        public IActionResult Delete(int id)
        {
            var designation = _repository.GetById(id);
            if (designation == null) return NotFound();
            return View(designation);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}