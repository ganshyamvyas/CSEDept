using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ViewUploadedMarks : System.Web.UI.Page
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

    protected void btnAllExport_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = csedept.SelectQuery("select DepartmentTbl.DepartmentName, SubjectTbl.SubjectName, StudentTbl.RollNo, StudentTbl.Name, StudentTbl.Year, StudentTbl.Semester, FillMarksTbl.FirstMidMarks as [I-MID (out of 10)], FillMarksTbl.SecondMidMarks as [II-MID (out of 10)], FillMarksTbl.AvgMidMarks, FillMarksTbl.AssiMarks as [ASSIGNMENT (out of 10)], FillMarksTbl.TotalMarks from StudentTbl inner join FillMarksTbl on FillMarksTbl.StudentID = StudentTbl.ID  inner join DepartmentTbl on FillMarksTbl.DeptID = DepartmentTbl.ID inner join SubjectTbl on FillMarksTbl.SubID = SubjectTbl.ID where FillMarksTbl.DeptID = '" + ddlStudentDepartment.SelectedValue + "' and FillMarksTbl.Year = '" + ddlStudentYear.SelectedValue + "' and FillMarksTbl.Semester = '" + ddlStudentSemester.SelectedValue + "' and FillMarksTbl.SubID = '" + ddlSubjects.SelectedValue + "' and StudentTbl.IsActive = '1' and FillMarksTbl.IsActive='1' ");
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "StudentInternalMarks");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = "StudentInternalMarks" + DateTime.Now.ToString("dd-MMM-yyyy") + ".xlsx";
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            grdDetails.DataSource = csedept.SelectQuery("select FillMarksTbl.ID, DepartmentTbl.DepartmentName, SubjectTbl.SubjectName, StudentTbl.RollNo, StudentTbl.Name, StudentTbl.Year, StudentTbl.Semester, FillMarksTbl.FirstMidMarks as [I-MID (out of 10)], FillMarksTbl.SecondMidMarks as [II-MID (out of 10)], FillMarksTbl.AvgMidMarks, FillMarksTbl.AssiMarks as [ASSIGNMENT (out of 10)], FillMarksTbl.TotalMarks from StudentTbl inner join FillMarksTbl on FillMarksTbl.StudentID = StudentTbl.ID  inner join DepartmentTbl on FillMarksTbl.DeptID = DepartmentTbl.ID inner join SubjectTbl on FillMarksTbl.SubID = SubjectTbl.ID where FillMarksTbl.DeptID = '" + ddlStudentDepartment.SelectedValue + "' and FillMarksTbl.Year = '" + ddlStudentYear.SelectedValue + "' and FillMarksTbl.Semester = '" + ddlStudentSemester.SelectedValue + "' and FillMarksTbl.SubID = '" + ddlSubjects.SelectedValue + "' and StudentTbl.IsActive = '1' and FillMarksTbl.IsActive='1' ");
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
        }
        catch (Exception)
        { }
    }
}