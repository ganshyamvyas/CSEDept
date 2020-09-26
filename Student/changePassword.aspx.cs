using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_changePassword : System.Web.UI.Page
{
    dbcode csedept = new dbcode();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            msgsuccess.Visible = false;
            msgFails.Visible = false;
        }
        catch (Exception ex)
        {
            msgFails.InnerText = ex.Message;
            msgFails.Visible = true;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {

            string OldPassword = txtCurrentPassword.Text;
            string Aid = Session["Id"].ToString();
            string oldpass = csedept.ExecuteScalar("Select Password from StudentTbl where Id='" + Aid + "' and Password='" + OldPassword + "' and IsActive='1'");
            if (OldPassword == oldpass)
            {
                string NewPassword = txtNewPassword.Text;
                csedept.ExecuteQuery("Update StudentTbl set Password ='" + NewPassword + "' where Id='" + Aid + "' ");
                Response.Write("<script>alert('Your Password Is Succesfully Updated !!')</script>");
                //msgsuccess.Visible = true;
            }
            else
            {
                //msgFails.Visible = true;
                Response.Write("<script>alert('Wrong Password Entered')</script>");
            }
        }
        catch (Exception ex)
        {
            msgFails.InnerText = ex.Message;
            msgFails.Visible = true;
        }
    }
}