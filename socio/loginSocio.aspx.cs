using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Cooperativa.socio
{
    public partial class loginSocio : System.Web.UI.Page
    {
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