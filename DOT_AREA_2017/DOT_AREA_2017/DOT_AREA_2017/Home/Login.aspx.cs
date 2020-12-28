using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace DOT_AREA_2017
{
    public partial class Login : Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            con.Open();
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if ((TextLogin.Text != "" && TextLogin != null)
                    && (TextPass.Text != "" && TextPass.Text != null))
                {
                    string sql = "select * from [User] where Login=@login AND Password=@password";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@login", TextLogin.Text);
                    cmd.Parameters.AddWithValue("@password", TextPass.Text);

                    
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        var url = String.Format("~/Home/Logged.aspx?login={0}&email={1}&username={2}", reader["Login"], reader["Email"], reader["UserName"]);
                        Response.Redirect(url);
                    }
                    else
                    {
                        Label1.ForeColor = System.Drawing.Color.Red;
                        Label1.Visible = true;
                        Label1.Text = "Invalid login or password ...";
                    }
                    reader.Close();
                    con.Close();
                }
                else
                {
                    Label1.ForeColor = System.Drawing.Color.Red;
                    Label1.Visible = true;
                    Label1.Text = "Invalid login or password ...";
                }
            }
            catch (SqlException ex)
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Visible = true;
                Label1.Text = ex.Message;
            }
        }
    }
}