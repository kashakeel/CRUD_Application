using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Employee_App.Pages.Employee
{
    public class IndexModel : PageModel
    {
        public List<Employee> listEmployees=new List<Employee>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=employees;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                connection.Open();
                    String sql = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Employee employee = new Employee();
                                employee.id = "" + reader.GetInt32(0);
                                employee.name = "" + reader.GetString(1);
                                employee.email = "" + reader.GetString(2);
                                employee.phone = "" + reader.GetString(3);
                                employee.address = "" + reader.GetString(4);

                                listEmployees.Add(employee);
                            }
                        }    
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: "+ ex.ToString());
            }

        }
    }

    public class Employee
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
    }
}
