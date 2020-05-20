using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace web_hello.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private AppDbContext context;

        public SQLEmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Employee Create(Employee employee)
        {
            context.Employees.Add(employee);            
            context.SaveChanges();
            return employee;
        }

        public Employee Delete(int Id)
        {
            Employee emp = context.Employees.Find(Id);
            if(emp != null)
            {
                context.Employees.Remove(emp);
                context.SaveChanges();
            }
            return emp;
        }

        public List<Employee> GetAllEmployees()
        {
            return context.Employees.ToList();
        }

        public Employee GetEmployee(int Id)
        {
            return context.Employees.Find(Id);
        }

        public Employee Update(Employee employeeChanges)
        {
            var employee = context.Employees.Attach(employeeChanges);
            employee.State = EntityState.Modified;
            context.SaveChanges();
            return employeeChanges;
        }
    }
}