using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Main : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
            if (Session["Id"] != null)
            {
                userhello.InnerText = Session["AdminName"].ToString();
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
    }
   
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Default.aspx");
    }    
}
