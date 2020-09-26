using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_FeedbackForm : System.Web.UI.Page
{
    dbcode csedept = new dbcode();
    Auto id = new Auto();
    string DateNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            msgFails.Visible = false;
            msgsuccess.Visible = false;

            if (!IsPostBack)
            {
               
                getDetails();

            }


        }
        catch (Exception)
        {
            Response.Redirect("Dashboard.aspx");
        }
    }

  public void getDetails()
    {
        try
        {

            string count = csedept.ExecuteScalar("select count(*) StudentID from StudentFeedBackTbl where StudentID = '"+ Session["Id"].ToString()+ "' and IsActive='1'");
            if (count != "0")
            {
                feedbackheading.InnerText = "You had already Submitted Feedback";
                btnSave.Visible = false;
            }
            else
            {
                DataTable dtData = new DataTable();
                dtData = csedept.SelectQuery("select ID, FeedBackQues from FeedBackQuesTbl where IsActive = '1'");

                grdProfileData.DataSource = dtData;
                grdProfileData.DataBind();
            }
          
        }
        catch (Exception)
        { }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            
            foreach (GridViewRow row in grdProfileData.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                  

                    RadioButton rdexecellent = row.FindControl("rdexecellent") as RadioButton;
                    RadioButton rdgood = row.FindControl("rdgood") as RadioButton;
                    RadioButton rdfair = row.FindControl("rdfair") as RadioButton;
                    RadioButton rdbad = row.FindControl("rdbad") as RadioButton;

                    
                    HiddenField hdQuestionID = row.FindControl("hdQuestionID") as HiddenField;
                    long FeedBackID = id.auto1("StudentFeedBackTbl", "ID");
                    if (rdexecellent.Checked == true)
                    {
                        
                        csedept.ExecuteQuery("Insert into StudentFeedBackTbl values (" + FeedBackID + ",'" + Session["Id"].ToString() + "','" + hdQuestionID.Value + "','Excellent','" + DateNow + "','1')");
                    }
                    else if(rdgood.Checked==true)
                    {
                        csedept.ExecuteQuery("Insert into StudentFeedBackTbl values (" + FeedBackID + ",'" + Session["Id"].ToString() + "','" + hdQuestionID.Value + "','Good','" + DateNow + "','1')");
                    }
                    else if (rdfair.Checked == true)
                    {
                        csedept.ExecuteQuery("Insert into StudentFeedBackTbl values (" + FeedBackID + ",'" + Session["Id"].ToString() + "','" + hdQuestionID.Value + "','Fair','" + DateNow + "','1')");
                    }
                    else if (rdgood.Checked == true)
                    {
                        csedept.ExecuteQuery("Insert into StudentFeedBackTbl values (" + FeedBackID + ",'" + Session["Id"].ToString() + "','" + hdQuestionID.Value + "','Poor','" + DateNow + "','1')");
                    }
                }
            }
            msgsuccess.Visible = true;
            btnSave.Visible = false;
            Response.Redirect("ViewFeedBack.aspx");
            grdProfileData.DataSource = null;
            grdProfileData.DataBind();
            

        }
        catch (Exception)
        { }
    }


 

}