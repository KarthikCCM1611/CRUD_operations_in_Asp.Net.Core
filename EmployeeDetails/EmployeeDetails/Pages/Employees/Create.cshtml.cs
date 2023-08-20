using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static EmployeeDetails.Pages.EmployeesModel;

namespace EmployeeDetails.Pages.Employees
{
    public class CreateModel : PageModel
    {
        public EmployeeInfo employeeInfo = new EmployeeInfo();
        public String errorMsg = "";
        public String successMsg = "";
        public void OnGet()
        {
        }

        public void OnPost() {
            employeeInfo.name = Request.Form["name"];
            employeeInfo.email = Request.Form["email"];
            employeeInfo.department = Request.Form["department"];
            employeeInfo.phoneNumber = Request.Form["phoneNumber"];
            if(employeeInfo.name.Length ==0 || employeeInfo.email.Length == 0 || employeeInfo.department.Length == 0 || employeeInfo.phoneNumber.Length == 0)
            {
                errorMsg = "All Fields are Required";
                return;
            }
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmployeeData;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                //string newEmployeeQuery = $"INSERT INTO EmpDetails(name, email, department, phoneNumber) VALUES ('{employeeInfo.name}','{employeeInfo.email}','{employeeInfo.department}','{employeeInfo.phoneNumber}')";
                //SqlCommand command = new SqlCommand(newEmployeeQuery, connection);

                string newEmployeeQueryWithParameters = $"INSERT INTO EmpDetails(name, email, department, phoneNumber) VALUES (@name,@email,@department,@phoneNumber)";
                SqlCommand command = new SqlCommand(newEmployeeQueryWithParameters, connection);
                command.Parameters.AddWithValue("@name",employeeInfo.name);
                command.Parameters.AddWithValue("@email", employeeInfo.email);
                command.Parameters.AddWithValue("@department", employeeInfo.department);
                command.Parameters.AddWithValue("@phoneNumber", employeeInfo.phoneNumber);

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
