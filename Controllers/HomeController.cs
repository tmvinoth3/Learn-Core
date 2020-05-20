using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using web_hello.Models;
using web_hello.ViewModels;

namespace web_hello.Controllers
{
    //[Route("{controller}/{action}/{id?}")]
    public class HomeController : Controller
    {
        private IEmployeeRepository repository;

        public HomeController(IEmployeeRepository repo)
        {
            repository = repo;
        }

        [Route("~/")]
        public ActionResult Index()
        {
            List<Employee> EmpList = repository.GetAllEmployees();
            return View(EmpList);
        }

        public ActionResult Details(int? id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel();
            Employee empModel = repository.GetEmployee(id??1);
            homeDetailsViewModel.employee = empModel;
            homeDetailsViewModel.Title = "Employee Details";

            //ViewData["Title"] = "Employee Details";
            //ViewBag.Title = "Employee Details";
            //ViewBag.Employee = empModel;

            return View(homeDetailsViewModel);
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if(ModelState.IsValid){
                Employee newEmployee = repository.Create(employee);
                return RedirectToAction("details", new{id = newEmployee.Id});                
            }
            return View();
        }    
    }
}