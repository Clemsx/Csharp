using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace DOT_AREA_2017
{
    public partial class Register : Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            con.Open();
        }

        protected void ButtonRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextLogin.Text != "" && TextLogin != null)
                {
                    string sql = "insert into [User] (Login,Password,Email,UserName) VALUES(@login,@password,@email,@username)";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@login", TextLogin.Text);
                    cmd.Parameters.AddWithValue("@password", TextPass.Text);
                    cmd.Parameters.AddWithValue("@email", TextEmail.Text);
                    cmd.Parameters.AddWithValue("@username", TextUser.Text);
                    cmd.ExecuteNonQuery();

                    con.Close();
                    Label1.Visible = true;
                    Label1.Text = "Registration successful !";

                    TextLogin.Text = "";
                    TextPass.Text = "";
                    TextEmail.Text = "";
                    TextUser.Text = "";
                }
                else
                {
                    Label1.ForeColor = System.Drawing.Color.Red;
                    Label1.Visible = true;
                    Label1.Text = "ERROR FORM";
                }
            }
            catch (SqlException ex)
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Visible = true;
                Label1.Text = ex.Message.ToString();

            }
        }

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }
    }
}