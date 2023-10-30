using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Cooperativa
{
    public partial class login : System.Web.UI.Page
    {
        private Dictionary<string, string> users = new Dictionary<string, string>
        {
            { "admin", "password123" }  // example user
        };

        public DataTable GetUserData(string username)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conCoopac"].ToString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM tLogin WHERE Username = @Username", connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable resultTable = new DataTable();
                    adapter.Fill(resultTable);

                    return resultTable;
                }
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conCoopac"].ToString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT PasswordHash FROM tLogin WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        string storedPasswordHash = result.ToString();
                        // Aquí deberías comparar el hash almacenado con el hash de la contraseña ingresada.
                        // En este ejemplo simplificado, solo comparamos directamente:
                        return storedPasswordHash == password;
                    }
                    return false;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable userData = GetUserData("admin");
                if (userData.Rows.Count > 0)
                {
                    // Hacer algo con los datos, por ejemplo, mostrarlos en un GridView
                    GridView1.DataSource = userData;
                    GridView1.DataBind();
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text; // En la vida real, esto debería ser hash.

            if (AuthenticateUser(username, password))
            {
                lblMessage.Text = "Login Successful!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                // Puedes redirigir al usuario a otra página si lo deseas.
                // Response.Redirect("HomePage.aspx");
            }
            else
            {
                lblMessage.Text = "Invalid username or password!";
            }
        }
    }
}