using EmployeeApp.Models;
using EmployeeApp.Utilities;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Numerics;

namespace EmployeeApp.Repositories.EmployeeRepository
{
    public class EmployeeManager : IEmployee
    {
        string connectionString = ConnectionString.CName;

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(string searchString, int? companyId, int? departmentId, int? positionId)
        {
            List<Employee> lstEmployee = new List<Employee>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@SearchString", searchString));
                cmd.Parameters.Add(new SqlParameter("@CompanyId", companyId ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@DepartmentId", departmentId ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@PositionId", positionId ?? (object)DBNull.Value));

                await con.OpenAsync();
                SqlDataReader rdr = await cmd.ExecuteReaderAsync();

                while (await rdr.ReadAsync())
                {
                    Employee employee = new Employee
                    {
                        Id = (int)rdr["Id"],
                        FullName = (string)rdr["FullName"],
                        Address = (string)rdr["Address"],
                        Phone = (string)rdr["Phone"],
                        BirthDate = (DateTime)rdr["BirthDate"],
                        HireDate = (DateTime)rdr["HireDate"],
                        Salary = (decimal)rdr["Salary"],
                        DepartmentId = (int)rdr["DepartmentId"],
                        CompanyId = (int)rdr["CompanyId"],
                        PositionId = (int)rdr["PositionId"],
                        Department = new Department { Id = (int)rdr["DepartmentId"], Name = (string)rdr["DepartmentName"] },
                        Company = new Company { Id = (int)rdr["CompanyId"], Name = (string)rdr["CompanyName"] },
                        Position = new Position { Id = (int)rdr["PositionId"], Name = (string)rdr["PositionName"] }
                    };

                    lstEmployee.Add(employee);
                }
            }
            return lstEmployee;
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FullName", employee.FullName);
                cmd.Parameters.AddWithValue("@Address", employee.Address);
                cmd.Parameters.AddWithValue("@Phone", employee.Phone);
                cmd.Parameters.AddWithValue("@BirthDate", employee.BirthDate);
                cmd.Parameters.AddWithValue("@HireDate", employee.HireDate);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
                cmd.Parameters.AddWithValue("@CompanyId", employee.CompanyId);
                cmd.Parameters.AddWithValue("@PositionId", employee.PositionId);

                await con.OpenAsync(); 
                await cmd.ExecuteNonQueryAsync(); 
            }
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", employee.Id);
                cmd.Parameters.AddWithValue("@FullName", employee.FullName);
                cmd.Parameters.AddWithValue("@Address", employee.Address);
                cmd.Parameters.AddWithValue("@Phone", employee.Phone);
                cmd.Parameters.AddWithValue("@BirthDate", employee.BirthDate);
                cmd.Parameters.AddWithValue("@HireDate", employee.HireDate);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
                cmd.Parameters.AddWithValue("@CompanyId", employee.CompanyId);
                cmd.Parameters.AddWithValue("@PositionId", employee.PositionId);

                await con.OpenAsync(); 
                await cmd.ExecuteNonQueryAsync(); 
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int? id)
        {
            Employee employee = new Employee();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Employee WHERE Id= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                await con.OpenAsync();
                SqlDataReader rdr = await cmd.ExecuteReaderAsync();

                while (await rdr.ReadAsync())
                {
                    employee.Id = (int)rdr["Id"];
                    employee.FullName = (string)rdr["FullName"];
                    employee.Address = (string)rdr["Address"];
                    employee.Phone = (string)rdr["Phone"];
                    employee.BirthDate = (DateTime)rdr["BirthDate"];
                    employee.HireDate = (DateTime)rdr["HireDate"];
                    employee.Salary = (decimal)rdr["Salary"];
                    employee.DepartmentId = (int)rdr["DepartmentId"];
                    employee.CompanyId = (int)rdr["CompanyId"];
                    employee.PositionId = (int)rdr["PositionId"];
                }
            }
            return employee;
        }

        public async Task DeleteEmployeeAsync(int? id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
