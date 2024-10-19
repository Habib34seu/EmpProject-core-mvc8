using Emp.Domain.Models;
using Emp.Infrastructure.Data;
using Emp.Infrastructure.Repositories.IRepo;
using Microsoft.AspNetCore.Mvc;

namespace EMP.Web.Controllers
{
    public class EmployeeController(IUnitOfWork _unitOfWork) : Controller
    {
        public async Task<IActionResult> Index(int employeeId, string firstName)
        {
            IEnumerable<EmployeeEntity> empObj;

            if (employeeId == 0 && string.IsNullOrEmpty(firstName))
            {
               
                empObj = await _unitOfWork.Employee.GetAll(x => true);
            }
            else
            {
                // Apply the filter for specific employeeId and firstName
                empObj = await _unitOfWork.Employee.GetAll(x =>
                    (employeeId == 0 || x.EmployeeId == employeeId) &&
                    (string.IsNullOrEmpty(firstName) || x.FirstName == firstName)
                );
            }

            return View(empObj.ToList()); 
        }



        public async Task<IActionResult> CreateEmp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmp( EmployeeEntity employeeEntity)
        {
            
            if (ModelState.IsValid)
            {
               await _unitOfWork.Employee.Add(employeeEntity);
               await _unitOfWork.SaveAsync();
                TempData["success"] = "Employee created successfully";
                return RedirectToAction("Index", "Employee");
            }
           
            return View();
        }
        public async Task<IActionResult> EditEmp(string FirstName)
        {
            if (FirstName == null) return NotFound();

            EmployeeEntity emp = await _unitOfWork.Employee.Get(x => x.FirstName == FirstName);

            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmp(EmployeeEntity emp)
        {
           
            if (ModelState.IsValid)
            {
               await _unitOfWork.Employee.UpdateAsync(emp);
               await _unitOfWork.SaveAsync();
                TempData["success"] = "Employee updated successfully";
                return RedirectToAction("Index");
            }

            return View();
        }
        public async Task<IActionResult> DeleteEmp(int? EmployeeId)
        {
            if (EmployeeId == null || EmployeeId == 0) return NotFound();
            
            EmployeeEntity emp = await _unitOfWork.Employee.Get(x => x.EmployeeId == EmployeeId);

            return View(emp);
        }

        [HttpPost,ActionName("DeleteEmp")]
        public async Task<IActionResult> Delete(int? EmployeeId)
        {
            EmployeeEntity? emp = await _unitOfWork.Employee.Get(x => x.EmployeeId == EmployeeId);
            if (emp == null) return NotFound();

           await _unitOfWork.Employee.Remove(emp);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Employee deleted successfully";
            return RedirectToAction("Index");

           
        }
    }
}
