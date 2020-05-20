using System.Collections.Generic;

namespace web_hello.Models
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int Id);
        List<Employee> GetAllEmployees();
        Employee Create(Employee employee);
        Employee Update(Employee employee);
        Employee Delete(int Id);
    }
}