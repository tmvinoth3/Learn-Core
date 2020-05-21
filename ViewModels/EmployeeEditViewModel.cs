namespace web_hello.ViewModels
{
    public class EmployeeEditViewModel : CreateEmployeeViewModel
    {
        public int Id { get; set; }
        public string ExistingImagePath { get; set; }
    }
}