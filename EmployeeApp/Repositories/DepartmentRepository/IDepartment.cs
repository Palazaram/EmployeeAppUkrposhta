using EmployeeApp.Models;

namespace EmployeeApp.Repositories.DepartmentRepository
{
    public interface IDepartment
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
    }
}
