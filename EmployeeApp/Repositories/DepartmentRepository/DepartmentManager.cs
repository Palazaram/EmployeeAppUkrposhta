using EmployeeApp.Models;
using EmployeeApp.Utilities;
using System.Data.SqlClient;
using System.Data;

namespace EmployeeApp.Repositories.DepartmentRepository
{
    public class DepartmentManager : IDepartment
    {
        string connectionString = ConnectionString.CName;

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            List<Department> lstDepartment = new List<Department>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllDepartment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                await con.OpenAsync();
                SqlDataReader rdr = await cmd.ExecuteReaderAsync();

                while (await rdr.ReadAsync())
                {
                    Department department = new Department
                    {
                        Id = (int)rdr["Id"],
                        Name = (string)rdr["Name"],
                    };

                    lstDepartment.Add(department);
                }
            }
            return lstDepartment;
        }
    }
}
