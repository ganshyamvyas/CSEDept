using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ViewFeedBackAnswer : System.Web.UI.Page
{
    dbcode csedept = new dbcode();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
          

            if (!IsPostBack)
            {
               
                getAllDepts();
                getAllYears();
                getAllSems();
            }

        }
        catch (Exception)
        { }
    }
    public void getAllDepts()
    {
        try
        {
            ddlStudentDepartment.DataSource = csedept.SelectQuery("Select ID, DepartmentName from DepartmentTbl order by DepartmentName asc");
            ddlStudentDepartment.DataValueField = "ID";
            ddlStudentDepartment.DataTextField = "DepartmentName";
            ddlStudentDepartment.DataBind();
            ddlStudentDepartment.Items.Insert(0, new ListItem("------------Select Department-------------", "0"));
        }
        catch (Exception)
        { }
    }
    public void getAllYears()
    {
        try
        {
            ddlStudentYear.DataSource = csedept.SelectQuery("Select YrID, Year from YearTbl where IsActive='1' order by Year asc");
            ddlStudentYear.DataValueField = "YrID";
            ddlStudentYear.DataTextField = "Year";
            ddlStudentYear.DataBind();
            ddlStudentYear.Items.Insert(0, new ListItem("------------Select Year-------------", "0"));
        }
        catch (Exception)
        { }
    }


    public void getAllSems()
    {
        try
        {
            ddlStudentSemester.DataSource = csedept.SelectQuery("Select SemID, Semester from SemesterTbl where YearID = '" + ddlStudentYear.SelectedValue + "' and IsActive='1' order by Semester asc");
            ddlStudentSemester.DataValueField = "SemID";
            ddlStudentSemester.DataTextField = "Semester";
            ddlStudentSemester.DataBind();
            ddlStudentSemester.Items.Insert(0, new ListItem("------------Select Semester-------------", "0"));
        }
        catch (Exception)
        { }
    }
    protected void ddlStudentYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            try
            {
                if (ddlStudentYear.SelectedValue != "0")
                {
                    getAllSems();
                }

            }
            catch (Exception ex)
            {
                
            }
        }
        catch (Exception)
        { }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            grdDetails.DataSource = csedept.SelectQuery("select StudentFeedBackTbl.ID, DepartmentTbl.DepartmentName,StudentTbl.RollNo,StudentTbl.Name, StudentTbl.Year, StudentTbl.Semester, FeedBackQuesTbl.FeedBackQues, StudentFeedBackTbl.Answer from StudentFeedBackTbl inner join FeedBackQuesTbl on StudentFeedBackTbl.QuestionID = FeedBackQuesTbl.ID inner join StudentTbl on StudentFeedBackTbl.StudentID = StudentTbl.ID inner join DepartmentTbl on  StudentTbl.DeptID = DepartmentTbl.ID where FeedBackQuesTbl.IsActive = '1' and StudentFeedBackTbl.IsActive='1' and StudentTbl.DeptID = '"+ddlStudentDepartment.SelectedValue+ "' and Year = '"+ddlStudentYear.SelectedValue+ "' and Semester = '"+ddlStudentSemester.SelectedValue+ "' and StudentTbl.IsActive='1'");
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