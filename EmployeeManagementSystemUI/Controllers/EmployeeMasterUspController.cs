using EmployeeManagementSystemUI.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystemUI.Controllers
{
    public class EmployeeMasterUspController : Controller
    {
        IEmployeeMasterUSP _employeeMasterUSP;
        public EmployeeMasterUspController(IEmployeeMasterUSP employeeMasterUSP)
        {
            _employeeMasterUSP = employeeMasterUSP;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
