namespace EmployeeApp.Utilities
{
    public static  class ConnectionString
    {
        private static string cName = "Database=.;Initial Catalog=UkrposhtaDb;Trusted_Connection=True;TrustServerCertificate=True;";
        public static string CName { get => cName; }
    }
}
