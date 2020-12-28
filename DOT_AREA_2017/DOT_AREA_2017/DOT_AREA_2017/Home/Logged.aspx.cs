using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DOT_AREA_2017
{
    public partial class Logged : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var login = Request["login"];
            var email = Request["email"];
            var username = Request["username"];

            LabelUsername.Visible = true;
            LabelUsername.Text = String.Format("Welcome {0}", login);
        }
    }
}