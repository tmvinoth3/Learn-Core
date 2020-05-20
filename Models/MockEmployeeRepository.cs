using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace web_hello.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        public List<Employee> MockEmployees { get; }
        public MockEmployeeRepository()
        {
            MockEmployees = new List<Employee>{
                new Employee{Id = 1, Name = "Joey", Email="Joey@friends.com", Department=Dept.Acting},
                new Employee{Id = 2, Name = "Pheobes", Email="Pheobes@friends.com", Department=Dept.Parlour},
                new Employee{Id = 3, Name = "Chandler", Email="Chandler@friends.com", Department=Dept.IT}
            };
        }
        
        public Employee GetEmployee(int Id){
            return MockEmployees.FirstOrDefault(e => e.Id == Id);
        }

        public List<Employee> GetAllEmployees()
        {
            return MockEmployees;
        }

        public Employee Create(Employee employee)
        {
            employee.Id = MockEmployees.Max( emp => emp.Id) + 1;
            MockEmployees.Add(employee);
            return employee;
        }

        public Employee Update(Employee employee)
        {
            Employee UpdateEmp = MockEmployees.FirstOrDefault(emp => emp.Id == employee.Id);
            if(employee != null)
            {
                UpdateEmp.Name = employee.Name;
                UpdateEmp.Email = employee.Email;
                UpdateEmp.Department = employee.Department;
            }
            return employee;
        }

        public Employee Delete(int Id)
        {
            Employee employee = MockEmployees.FirstOrDefault(emp => emp.Id == Id);
            if(employee != null)
            {
                MockEmployees.Remove(employee);
            }
            return employee;
        }
    }
}