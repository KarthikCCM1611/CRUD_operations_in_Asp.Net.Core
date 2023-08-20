using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static EmployeeDetails.Pages.EmployeesModel;

namespace EmployeeDetails.Pages.Employees
{
    public class EditModel : PageModel
    {
        public EmployeeInfo employeeInfo = new EmployeeInfo();
        public String errorMsg = "";
        public String successMsg = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmployeeData;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string selectQuery = "SELECT * FROM EmpDetails WHERE empId=@id";
                SqlCommand command = new SqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    employeeInfo.empId = "" + reader.GetInt32(0);
                    employeeInfo.name = "" + reader.GetString(1);
                    employeeInfo.email = "" + reader.GetString(2);
                    employeeInfo.department = "" + reader.GetString(3);
                    employeeInfo.phoneNumber = "" + reader.GetString(4);
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return;
            }
        }

        public void OnPost()
        {
            employeeInfo.empId = Request.Form["empId"];
            employeeInfo.name = Request.Form["name"];
            employeeInfo.email = Request.Form["email"];
            employeeInfo.department = Request.Form["department"];
            employeeInfo.phoneNumber = Request.Form["phoneNumber"];
            if (employeeInfo.name.Length == 0 || employeeInfo.email.Length == 0 || employeeInfo.department.Length == 0 || employeeInfo.phoneNumber.Length == 0)
            {
                errorMsg = "All Fields are Required";
                return;
            }
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmployeeData;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string updateQuery = $"UPDATE EmpDetails SET name=@name, email=@email, department=@department, phoneNumber=@phoneNumber WHERE empId=@id";
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@name", employeeInfo.name);
                command.Parameters.AddWithValue("@email", employeeInfo.email);
                command.Parameters.AddWithValue("@department", employeeInfo.department);
                command.Parameters.AddWithValue("@phoneNumber", employeeInfo.phoneNumber);
                command.Parameters.AddWithValue("@id", employeeInfo.empId);

                int rows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());
                return;
            }

            employeeInfo.name = "";
            employeeInfo.email = "";
            employeeInfo.department = "";
            employeeInfo.phoneNumber = "";
            successMsg = "New Client Added Successfully";
            Response.Redirect("/Employees/Index");

        }
    }
}
