using Emp.Domain.Models;
using Emp.Infrastructure.Data;
using Emp.Infrastructure.Repositories.IRepo;
using Microsoft.AspNetCore.Mvc;

namespace EMP.Web.Controllers
{
    public class EmployeeController(IUnitOfWork _unitOfWork) : Controller
    {
        public IActionResult Index(int employeeId, string firstName)
        {
            List<EmployeeEntity> empObj;

            if (employeeId == 0 && string.IsNullOrEmpty(firstName))
            {
                // Get all employees if no specific filter is provided
                empObj = _unitOfWork.Employee.GetAll(x => true).ToList();
            }
            else
            {
                // Apply the filter for specific employeeId and firstName
                empObj = _unitOfWork.Employee.GetAll(x =>
                    (employeeId == 0 || x.EmployeeId == employeeId) &&
                    (string.IsNullOrEmpty(firstName) || x.FirstName == firstName)
                ).ToList();
            }

            return View(empObj);
        }
        //public IActionResult Index(int employeeId, string firstName)
        //{
        //    List<EmployeeEntity> empObj = _unitOfWork.Employee.GetAll(x => x.EmployeeId == employeeId && x.FirstName == firstName).ToList();
        //    //_unitOfWork.Employee.GetAll().ToList();
        //    return View(empObj);
        //}

        public IActionResult CreateEmp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateEmp( EmployeeEntity employeeEntity)
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.Employee.Add(employeeEntity);
                _unitOfWork.Save();
                TempData["success"] = "Employee created successfully";
                return RedirectToAction("Index", "Employee");
            }
           
            return View();
        }
        public IActionResult EditEmp(string FirstName)
        {
            if (FirstName == null) return NotFound();

            EmployeeEntity emp = _unitOfWork.Employee.Get(x => x.FirstName == FirstName);

            return View(emp);
        }

        [HttpPost]
        public IActionResult EditEmp(EmployeeEntity emp)
        {
           
            if (ModelState.IsValid)
            {
                _unitOfWork.Employee.Update(emp);
                _unitOfWork.Save();
                TempData["success"] = "Employee updated successfully";
                return RedirectToAction("Index");
            }

            return View();
        }
        public IActionResult DeleteEmp(int? EmployeeId)
        {
            if (EmployeeId == null || EmployeeId == 0) return NotFound();
            
            EmployeeEntity emp = _unitOfWork.Employee.Get(x => x.EmployeeId == EmployeeId);

            return View(emp);
        }

        [HttpPost,ActionName("DeleteEmp")]
        public IActionResult Delete(int? EmployeeId)
        {
            EmployeeEntity? emp = _unitOfWork.Employee.Get(x => x.EmployeeId == EmployeeId);
            if (emp == null) return NotFound();

            _unitOfWork.Employee.Remove(emp);
            _unitOfWork.Save();
            TempData["success"] = "Employee deleted successfully";
            return RedirectToAction("Index");

           
        }
    }
}
