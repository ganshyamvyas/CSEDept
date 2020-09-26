using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Data;


public partial class Admin_changePassword : System.Web.UI.Page
{
    dbcode csedept = new dbcode();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            msgsuccess.Visible = false;
            msgFails.Visible = false;
        }
        catch(Exception ex)
        {
            msgFails.InnerText = ex.Message;
            msgFails.Visible = true;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
       try
        {   
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            Byte[] ePass;
            UTF8Encoding encoder = new UTF8Encoding();
            ePass = md5Hasher.ComputeHash(encoder.GetBytes(txtCurrentPassword.Text));
            string OldPassword = Convert.ToBase64String(ePass);
            string Aid = Session["Id"].ToString();
            string oldpass = csedept.ExecuteScalar("Select Password from AdminLoginTbl where Id='" + Aid + "' and Password='" + OldPassword + "' and IsActive='1'");
            if (OldPassword == oldpass)
            {
                MD5CryptoServiceProvider md5Hasher1 = new MD5CryptoServiceProvider();
                Byte[] ePassword;
                UTF8Encoding encoder1 = new UTF8Encoding();
                ePassword = md5Hasher1.ComputeHash(encoder.GetBytes(txtNewPassword.Text));
                string NewPassword = Convert.ToBase64String(ePassword);
                csedept.ExecuteQuery("Update AdminLoginTbl set Password ='" + NewPassword + "' where Id='" + Aid + "' ");
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