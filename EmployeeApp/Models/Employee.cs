namespace EmployeeApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }  // Навігаційне властивість для відношення до Department

        public int CompanyId { get; set; }
        public Company Company { get; set; }        // Навігаційне властивість для відношення до Company

        public int PositionId { get; set; }
        public Position Position { get; set; }      // Навігаційне властивість для відношення до Position
    }
}
