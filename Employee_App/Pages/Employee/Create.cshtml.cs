using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Employee_App.Pages.Employee
{
    public class CreateModel : PageModel
    {
        public Employee employee=new Employee();
        public string errorMessage ="";
        public string successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            employee.name = Request.Form["name"];
            employee.email = Request.Form["email"];
            employee.address    = Request.Form["address"];
            employee.phone = Request.Form["phone"];

            if (employee.name.Length == 0 || employee.email.Length == 0 || employee.address.Length == 0 || employee.phone.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //Saving to DB
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=employees;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO clients (name,email,phone,address) VALUES ( @name, @email, @phone, @address);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                            command.Parameters.AddWithValue("@name", employee.name);
                            command.Parameters.AddWithValue("@email", employee.email);
                            command.Parameters.AddWithValue("@phone", employee.phone);
                            command.Parameters.AddWithValue("@address", employee.address);

                            command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }



            employee.name = "";
            employee.email = "";
            employee.address = "";
            employee.phone = "";
            successMessage = "Employee Details Added Successfully";

            Response.Redirect("/Employee/Index");
        }
    }
}
