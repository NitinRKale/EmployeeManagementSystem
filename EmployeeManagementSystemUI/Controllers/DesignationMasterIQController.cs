using EmployeeManagementSystemUI.IRepository;
using EmployeeManagementSystemUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystemUI.Controllers
{
    public class DesignationMasterIQController : Controller
    {
        private readonly IDesignationMasterRepository_IQ _repository;

        public DesignationMasterIQController(IDesignationMasterRepository_IQ repository)
        {
            _repository = repository;
        }

        // GET: Designations
        public IActionResult Index()
        {
            var designations = _repository.GetAll();
            return View(designations);
        }

        // GET: DesignationMasterIQ/Details/5
        public IActionResult Details(int id)
        {
            var designationMaster = _repository.GetById(id);
            if (designationMaster == null)
                return NotFound();
            return View(designationMaster);
        }

        // GET: DesignationMasterIQ/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DesignationMasterIQ/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DesignationMaster designationMaster)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(designationMaster);
                return RedirectToAction(nameof(Index));
            }
            return View(designationMaster);
        }

        // GET: DesignationMasterIQ/Edit/5
        public IActionResult Edit(int id)
        {
            var designation = _repository.GetById(id);
            if (designation == null)
                return NotFound();
            return View(designation);
        }

        // POST: DesignationMasterIQ/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DesignationMaster designationMaster)
        {
            if (id != designationMaster.DesignationId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _repository.Update(designationMaster);
                return RedirectToAction(nameof(Index));
            }
            return View(designationMaster);
        }

        // GET: DesignationMasterIQ/Delete/5
        public IActionResult Delete(int id)
        {
            var designationMaster = _repository.GetById(id);
            if (designationMaster == null)
                return NotFound();
            return View(designationMaster);
        }

        // POST: DesignationMasterIQ/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
