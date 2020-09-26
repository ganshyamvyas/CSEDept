using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_ViewFeedBack : System.Web.UI.Page
{
    dbcode csedept = new dbcode();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           

            if (!IsPostBack)
            {
                getAllFeedbacks();
            }

        }
        catch (Exception)
        { }
    }

  
    public void getAllFeedbacks()
    {
        try
        {
            grdDetails.DataSource = csedept.SelectQuery("select StudentFeedBackTbl.ID, FeedBackQuesTbl.FeedBackQues, StudentFeedBackTbl.Answer from StudentFeedBackTbl inner join FeedBackQuesTbl on StudentFeedBackTbl.QuestionID = FeedBackQuesTbl.ID where FeedBackQuesTbl.IsActive = '1' and StudentFeedBackTbl.IsActive='1' and StudentFeedBackTbl.StudentID = '" + Session["Id"].ToString() + "'");
            grdDetails.DataBind();
        }
        catch (Exception)
        { }
    }

    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            e.Row.Cells[0].Visible = false;

        }
        catch (Exception)
        { }
    }
    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdDetails.PageIndex = e.NewPageIndex;
            //getAllStudents();
        }
        catch (Exception)
        { }
    }
}