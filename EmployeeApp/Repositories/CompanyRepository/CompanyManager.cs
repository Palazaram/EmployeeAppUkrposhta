using EmployeeApp.Models;
using EmployeeApp.Utilities;
using System.Data.SqlClient;
using System.Data;

namespace EmployeeApp.Repositories.CompanyRepository
{
    public class CompanyManager : ICompany
    {
        string connectionString = ConnectionString.CName;

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            List<Company> lstCompany = new List<Company>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllCompany", con);
                cmd.CommandType = CommandType.StoredProcedure;
                await con.OpenAsync();
                SqlDataReader rdr = await cmd.ExecuteReaderAsync();

                while (await rdr.ReadAsync())
                {
                    Company company = new Company
                    {
                        Id = (int)rdr["Id"],
                        Name = (string)rdr["Name"],
                    };

                    lstCompany.Add(company);
                }
            }
            return lstCompany;
        }
    }
}
