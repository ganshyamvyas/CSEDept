using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_myAttendance : System.Web.UI.Page
{
    dbcode csedept = new dbcode();
    int count = 0;
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
            grdDetails.DataSource = csedept.SelectQuery("select AttendanceTbl.StudentID, DepartmentTbl.DepartmentName,SubjectTbl.SubjectName, StudentTbl.RollNo, StudentTbl.Name, StudentTbl.Year, StudentTbl.Semester, AttendanceTbl.CreateDate, AttendanceTbl.Status from StudentTbl inner join DepartmentTbl on StudentTbl.DeptID = DepartmentTbl.ID inner join AttendanceTbl on StudentTbl.ID = AttendanceTbl.StudentID inner join SubjectTbl on AttendanceTbl.SubID = SubjectTbl.ID where AttendanceTbl.StudentID = '" + Session["Id"].ToString() + "' and AttendanceTbl.SubID = '" + ddlSubjects.SelectedValue + "' and StudentTbl.IsActive = '1' and AttendanceTbl.CreateDate between '" + txtfromsearch.Text + "' and  '"+txttosearch.Text+"' ");
            grdDetails.DataBind();
            
        }
        catch (Exception)
        { }
    }

    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowIndex != -1)
            {

                string status = e.Row.Cells[8].Text;


                if (status == "0")
                {
                    e.Row.Cells[8].Text = "Absent";
                }
                else if (status == "1")
                {
                    count++;
                    e.Row.Cells[8].Text = "Present";
                }
            }
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;

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