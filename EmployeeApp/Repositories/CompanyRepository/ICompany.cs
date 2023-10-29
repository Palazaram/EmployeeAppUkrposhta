using EmployeeApp.Models;

namespace EmployeeApp.Repositories.CompanyRepository
{
    public interface ICompany
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
    }
}
