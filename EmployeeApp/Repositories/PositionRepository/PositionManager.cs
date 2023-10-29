using EmployeeApp.Models;
using EmployeeApp.Utilities;
using System.Data.SqlClient;
using System.Data;

namespace EmployeeApp.Repositories.PositionRepository
{
    public class PositionManager : IPosition
    {
        string connectionString = ConnectionString.CName;

        public async Task<IEnumerable<Position>> GetAllPositionsAsync()
        {
            List<Position> lstPosition = new List<Position>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllPosition", con);
                cmd.CommandType = CommandType.StoredProcedure;
                await con.OpenAsync();
                SqlDataReader rdr = await cmd.ExecuteReaderAsync();

                while (await rdr.ReadAsync())
                {
                    Position position = new Position
                    {
                        Id = (int)rdr["Id"],
                        Name = (string)rdr["Name"],
                    };

                    lstPosition.Add(position);
                }
            }
            return lstPosition;
        }
    }
}
