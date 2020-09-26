using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AdminLogin : System.Web.UI.Page
{
    dbcode csedpt = new dbcode();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
   {
       try
       {
           MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
           Byte[] ePass;
           UTF8Encoding encoder = new UTF8Encoding();
           ePass = md5Hasher.ComputeHash(encoder.GetBytes(txtPassword.Text));
           string Password = Convert.ToBase64String(ePass);
           string pass = csedpt.ExecuteScalar("select Password from AdminLoginTbl where UserName='" + txtUserName.Text + "' and Password='" + Password + "' and IsActive='1'");
           if (Password == pass)
           {
               DataTable dt = csedpt.SelectQuery("select ID, UserName from AdminLoginTbl where UserName='" + txtUserName.Text + "' and Password='" + Password + "' and IsActive='1'");
               Session["Id"] = dt.Rows[0]["Id"].ToString();
               Session["AdminName"] = txtUserName.Text;
               Session["Role"] = "1";
               Response.Redirect("Dashboard.aspx");
           }
           else
           {
               Response.Write("<Script>alert('Invalid User Name or Password')</Script>");
           }
       }
       catch (Exception ex)
       {  }
   }
}