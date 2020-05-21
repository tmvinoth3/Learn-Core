using Microsoft.EntityFrameworkCore;

namespace web_hello.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 3,
                    Name= "John",
                    Email = "john@suits.com",
                    Department = Dept.None
                }
            );            
        }
    }
}