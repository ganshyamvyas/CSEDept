using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Parents_MyInternalMarks : System.Web.UI.Page
{
    dbcode csedept = new dbcode();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            msgsuccess.Visible = false;
            msgFails.Visible = false;

            if (!IsPostBack)
            {
                getAllSubjects();
            }

        }
        catch (Exception)
        { }
    }
   
    public void getAllSubjects()
    {
        try
        {
            ddlSubjects.DataSource = csedept.SelectQuery("Select ID, SubjectName from SubjectTbl where IsActive='1' order by SubjectName asc");
            ddlSubjects.DataValueField = "ID";
            ddlSubjects.DataTextField = "SubjectName";
            ddlSubjects.DataBind();
            ddlSubjects.Items.Insert(0, new ListItem("------------Select Subject-------------", "0"));
        }
        catch (Exception)
        { }
    }

    

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            grdDetails.DataSource = csedept.SelectQuery("select FillMarksTbl.ID, DepartmentTbl.DepartmentName, SubjectTbl.SubjectName, StudentTbl.RollNo, StudentTbl.Name, StudentTbl.Year, StudentTbl.Semester, FillMarksTbl.FirstMidMarks as [I-MID (out of 10)], FillMarksTbl.SecondMidMarks as [II-MID (out of 10)], FillMarksTbl.AvgMidMarks, FillMarksTbl.AssiMarks as [ASSIGNMENT (out of 10)], FillMarksTbl.TotalMarks from StudentTbl inner join FillMarksTbl on FillMarksTbl.StudentID = StudentTbl.ID  inner join DepartmentTbl on FillMarksTbl.DeptID = DepartmentTbl.ID inner join SubjectTbl on FillMarksTbl.SubID = SubjectTbl.ID where FillMarksTbl.StudentID = '"+ Session["Id"].ToString()+ "'  and FillMarksTbl.SubID = '" + ddlSubjects.SelectedValue + "' and StudentTbl.IsActive = '1' and FillMarksTbl.IsActive='1' ");
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