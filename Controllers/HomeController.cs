using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using web_hello.Models;
using web_hello.ViewModels;

namespace web_hello.Controllers
{
    //[Route("{controller}/{action}/{id?}")]
    public class HomeController : Controller
    {
        private IEmployeeRepository repository;
        private IWebHostEnvironment hostEnv;

        public HomeController(IEmployeeRepository repo, IWebHostEnvironment hostingEnvironment)
        {
            repository = repo;
            hostEnv = hostingEnvironment;
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
        //public ActionResult Create(Employee employee)
        public ActionResult Create(CreateEmployeeViewModel model)
        {
            if(ModelState.IsValid)
            {
                string uniqueFileName = string.Empty; //To avoid same file name
                if(model.Image != null)
                {
                    string uploadFolder = Path.Combine(hostEnv.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Employee newEmployee = new Employee{
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    ImagePath = uniqueFileName
                };

                newEmployee = repository.Create(newEmployee);
                return RedirectToAction("details", new{id = newEmployee.Id});                
            }
            return View();
        }    

        public ActionResult Delete(int Id)
        {
            Employee delEmp = repository.Delete(Id);
            return RedirectToAction("index");                
        }
    }
}