using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace EmployeeDetails.Pages
{
    public class EmployeesModel : PageModel
    {
        public List<EmployeeInfo> employees = new List<EmployeeInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmployeeData;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string selectQuery = "SELECT * FROM EmpDetails";
                SqlCommand command = new SqlCommand(selectQuery, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {   
                    EmployeeInfo employeeInfo = new EmployeeInfo();
                    employeeInfo.empId = "" + reader.GetInt32(0); 
                    employeeInfo.name = reader.GetString(1);
                    employeeInfo.email = reader.GetString(2);
                    employeeInfo.department = reader.GetString(3);
                    employeeInfo.phoneNumber = reader.GetString(4);
                    employees.Add(employeeInfo);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());
            }
        }
        public class EmployeeInfo
        {
            public string empId; 
            public string name;
            public string email;
            public string department;
            public string phoneNumber;
        }
    }
}
