using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Employee_App.Pages.Employee
{
    public class EditModel : PageModel
    {
        public Employee employee = new Employee();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=employees;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id",id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                employee.id = "" + reader.GetInt32(0);
                                employee.name = "" + reader.GetString(1);
                                employee.email = "" + reader.GetString(2);
                                employee.phone = "" + reader.GetString(3);
                                employee.address = "" + reader.GetString(4);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
        public void OnPost()
        {
            employee.name = Request.Form["name"];
            employee.email = Request.Form["email"];
            employee.address = Request.Form["address"];
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
                    String sql = "UPDATE clients  SET name=@name,email=@email,phone=@phone,address=@address,id=@id WHERE id=@id;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@name", employee.name);
                        command.Parameters.AddWithValue("@email", employee.email);
                        command.Parameters.AddWithValue("@phone", employee.phone);
                        command.Parameters.AddWithValue("@address", employee.address);
                        command.Parameters.AddWithValue("@id", employee.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Employee/Index");
        }
    }
}
