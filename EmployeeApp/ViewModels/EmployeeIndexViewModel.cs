using EmployeeApp.Models;

namespace EmployeeApp.ViewModels
{
    public class EmployeeIndexViewModel
    {
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Company> Companies { get; set; }
        public IEnumerable<Position> Positions { get; set; }
        public IEnumerable<Department> Departments { get; set; }

        public string SearchString { get; set; } = "";

        public int? CompanyId { get; set; }
        public int? PositionId { get; set; }
        public int? DepartmentId { get; set; }
    }
}
