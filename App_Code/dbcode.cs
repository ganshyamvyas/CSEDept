using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Mail;
 

/// <summary>
/// Summary description for dbcode
/// </summary>
public class dbcode
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    public SqlDataAdapter da = new SqlDataAdapter();
    public SqlCommand cmd = new SqlCommand();
	public dbcode()
	{
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
            da.SelectCommand = cmd;
            cmd.Connection = con;
            con.Close();
        }
        else
        {
            da.SelectCommand = cmd;
            cmd.Connection = con;
            con.Close();
        }

	}
    public void ExecuteQuery(string sql)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        else
        {
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
    public DataTable SelectQuery(string sql)
    {
        try
        {
            con.Open();
            cmd.CommandText = sql;
            DataTable dt = new DataTable();
            da.Fill(dt);            
            return (dt);
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            con.Close();
        }
    }

    public string ExecuteScalar(string sql)
    {
        try
        {
            con.Open();
            cmd.CommandText = sql;
            return cmd.ExecuteScalar().ToString();
        }
        catch (Exception)
        {
            return null;
        }
        finally 
        {
            con.Close();
        }
    }

    public void AddParameter(string paramname, object paramvalue)
    {
        SqlParameter param = new SqlParameter(paramname, paramvalue);
        cmd.Parameters.Add(param);
    }

    public void AddParameter(IDataParameter param)
    {
        cmd.Parameters.Add(param);
    }
    public void ClearInputs(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Text = string.Empty;
            ClearInputs(ctrl.Controls);
        }
    }
    public static Control FindControlRecursive(Control root, string id)
    {
        if (root.ID == id)
            return root;

        return root.Controls.Cast<Control>()
           .Select(c => FindControlRecursive(c, id))
           .FirstOrDefault(c => c != null);
    }
    public void FindReplace(Repeater rpt, string DislblID, string DivDisID)
    {
        foreach (RepeaterItem repeated in rpt.Items)
        {
            HtmlGenericControl lblDiscount = (HtmlGenericControl)FindControlRecursive(repeated, DislblID);
            if (lblDiscount.InnerText == "" || lblDiscount.InnerText == "0" || lblDiscount.InnerText == null)
            {
                HtmlGenericControl pnelDiscount = (HtmlGenericControl)FindControlRecursive(repeated, DivDisID);
                pnelDiscount.Visible = false;
            }
            else
            {
                HtmlGenericControl pnelPrice = (HtmlGenericControl)FindControlRecursive(repeated, DivDisID);
                pnelPrice.Visible = true;
                lblDiscount.InnerHtml = lblDiscount.InnerText + "% </br>off";
            }
        }
    }
    public void sendsms(string body, string mobileNumber)
    {
        string result = "";
        WebRequest request = null;
        HttpWebResponse response = null;
        try
        {
            String sendToPhoneNumber = mobileNumber;
            String userid = "smartinfo";
            String passwd = "Smart@1";
            String url = "http://msgindia.in/sendsms.jsp?user=" + userid + "&password=" + passwd + "&mobiles=" + sendToPhoneNumber + "&sms=" + body + "&senderid=SMINFO";
            request = WebRequest.Create(url);

            response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader reader = new
            System.IO.StreamReader(stream, ec);
            result = reader.ReadToEnd();
            Console.WriteLine(result);
            reader.Close();
            stream.Close();
        }
        catch (Exception exp)
        {
            Console.WriteLine(exp.ToString());
        }
        finally
        {
            if (response != null)
                response.Close();
        }
    }
    public void SendMail(string body, string sendto, string from, string subject)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(sendto);
            mail.From = new MailAddress(from);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential("websitetesting94@gmail.com", "Admin@123$%");
            smtp.Port = 25;
            smtp.Host = "smtp.gmail.com";
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
        catch (Exception ex)
        {
            //msgFails.InnerText = ex.Message;
            //msgFails.Visible = true;
        }
    }

    public DataTable se { get; set; }
}