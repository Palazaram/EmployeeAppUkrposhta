using EmployeeApp.Models;

namespace EmployeeApp.Repositories.EmployeeRepository
{
    public interface IEmployee
    {
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeByIdAsync(int? id);
        Task DeleteEmployeeAsync(int? id);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(string searchString, int? companyId, int? departmentId, int? positionId);
    }
}
