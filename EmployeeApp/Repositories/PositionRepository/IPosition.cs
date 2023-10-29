using EmployeeApp.Models;

namespace EmployeeApp.Repositories.PositionRepository
{
    public interface IPosition
    {
        Task<IEnumerable<Position>> GetAllPositionsAsync();
    }
}
