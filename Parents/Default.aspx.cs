using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Parents_Default : System.Web.UI.Page
{
    dbcode csedpt = new dbcode();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            string Password = txtPassword.Text;
            string pass = csedpt.ExecuteScalar("select ParentsLoginPassword from StudentTbl where (FathersName='" + txtUserName.Text + "' or MothersName = '"+txtUserName.Text+"') and ParentsLoginPassword='" + txtPassword.Text + "' and IsActive='1'");
            if (Password == pass)
            {
                DataTable dt = csedpt.SelectQuery("select ID, Name from StudentTbl where (FathersName='" + txtUserName.Text + "' or MothersName = '" + txtUserName.Text + "') and ParentsLoginPassword='" + txtPassword.Text + "' and IsActive='1'");
                Session["Id"] = dt.Rows[0]["ID"].ToString();
                Session["AdminName"] = txtUserName.Text;
                Session["Role"] = "4";
                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                Response.Write("<Script>alert('Invalid User Name or Password')</Script>");
            }
        }
        catch (Exception ex)
        { }
    }
}