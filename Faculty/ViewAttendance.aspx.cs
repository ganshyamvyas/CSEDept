using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_ViewAttendance : System.Web.UI.Page
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
                msgFails.InnerText = ex.Message;
                msgFails.Visible = true;
            }
        }
        catch (Exception)
        { }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            grdDetails.DataSource = csedept.SelectQuery("select AttendanceTbl.StudentID, DepartmentTbl.DepartmentName,SubjectTbl.SubjectName, StudentTbl.RollNo, StudentTbl.Name, StudentTbl.Year, StudentTbl.Semester, AttendanceTbl.Status from StudentTbl inner join DepartmentTbl on StudentTbl.DeptID = DepartmentTbl.ID inner join AttendanceTbl on StudentTbl.ID = AttendanceTbl.StudentID inner join SubjectTbl on AttendanceTbl.SubID = SubjectTbl.ID where StudentTbl.DeptID = '" + ddlStudentDepartment.SelectedValue + "' and Year = '" + ddlStudentYear.SelectedValue + "' and Semester = '" + ddlStudentSemester.SelectedValue + "' and AttendanceTbl.SubID = '" + ddlSubjects.SelectedValue + "' and StudentTbl.IsActive = '1' and AttendanceTbl.CreateDate = '" + txtsearch.Text + "'");
            grdDetails.DataBind();
            if(grdDetails.Rows.Count>0)
            {
                btnAllExport.Visible = true;
            }
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

                string status = e.Row.Cells[7].Text;


                if (status == "0")
                {
                    e.Row.Cells[7].Text = "Absent";
                }
                else if (status == "1")
                {
                    e.Row.Cells[7].Text = "Present";
                }
            }
            e.Row.Cells[0].Visible = false;

        }
        catch (Exception)
        { }
    }

    protected void btnAllExport_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = csedept.SelectQuery("select DepartmentTbl.DepartmentName,SubjectTbl.SubjectName, StudentTbl.RollNo, StudentTbl.Name, StudentTbl.Year, StudentTbl.Semester, AttendanceTbl.Status from StudentTbl inner join DepartmentTbl on StudentTbl.DeptID = DepartmentTbl.ID inner join AttendanceTbl on StudentTbl.ID = AttendanceTbl.StudentID inner join SubjectTbl on AttendanceTbl.SubID = SubjectTbl.ID where StudentTbl.DeptID = '" + ddlStudentDepartment.SelectedValue + "' and Year = '" + ddlStudentYear.SelectedValue + "' and Semester = '" + ddlStudentSemester.SelectedValue + "' and AttendanceTbl.SubID = '" + ddlSubjects.SelectedValue + "' and StudentTbl.IsActive = '1' and AttendanceTbl.CreateDate = '" + txtsearch.Text + "'");
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "StudentAttendanceTbl");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = "StudentAttendanceTbl" + DateTime.Now.ToString("dd-MMM-yyyy") + ".xlsx";
                Response.AddHeader("content-disposition", "attachment;filename=" + fileName + "");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
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