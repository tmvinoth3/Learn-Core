using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using web_hello.Models;
using web_hello.ViewModels;

namespace web_hello.Controllers
{
    //[Route("{controller}/{action}/{id?}")]
    public class HomeController : Controller
    {
        private IEmployeeRepository repository;
        private IWebHostEnvironment hostEnv;
        private ILogger logger;

        public HomeController(IEmployeeRepository repo,IWebHostEnvironment hostingEnvironment,
                                ILogger<HomeController> logger)
        {
            repository = repo;
            hostEnv = hostingEnvironment;
            this.logger = logger;
        }

        [Route("~/")]
        public ActionResult Index()
        {
            logger.LogTrace("Index begin");
            List<Employee> EmpList = repository.GetAllEmployees();
            logger.LogTrace("Index End");
            return View(EmpList);            
        }

        public ActionResult Details(int id)
        {
            //throw new Exception("error");
            logger.LogTrace("Details begin");
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel();
            //Employee empModel = repository.GetEmployee(id??1);
            Employee empModel = repository.GetEmployee(id);
            if(empModel == null){
                Response.StatusCode = 404;
                return View("NotFound", id);

            }
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
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Image.CopyTo(stream);
                    }                         
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

        public ActionResult Edit(int Id)
        {
            Employee emp = repository.GetEmployee(Id);
            EmployeeEditViewModel empEditModel = new EmployeeEditViewModel{
                Id = emp.Id,
                Name = emp.Name,
                Email = emp.Email,
                Department = emp.Department,
                ExistingImagePath = emp.ImagePath
            };
            return View(empEditModel);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeEditViewModel model)
        {
            if(ModelState.IsValid)
            {
                string uniqueFileName = string.Empty; //To avoid same file name
                if(model.Image != null)
                {
                    if(model.ExistingImagePath != null)
                    {
                        string existingImagePath = Path.Combine(hostEnv.WebRootPath, "images", model.ExistingImagePath);
                        System.IO.File.Delete(existingImagePath);
                    }
                    string uploadFolder = Path.Combine(hostEnv.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Image.CopyTo(stream);
                    }                    
                }

                Employee newEmployee = new Employee{
                    Id = model.Id,
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    ImagePath = uniqueFileName
                };

                newEmployee = repository.Update(newEmployee);
                return RedirectToAction("details", new{id = newEmployee.Id});                
            }
            return View();
        }         
    }
}