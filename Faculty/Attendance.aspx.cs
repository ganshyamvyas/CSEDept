using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_Attendance : System.Web.UI.Page
{
    dbcode csedept = new dbcode();
    Auto id = new Auto();
    string createdDate = DateTime.Now.ToString("yyyy-MM-dd");
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            msgsuccess.Visible = false;
            msgFails.Visible = false;
            btnsave.Visible = false;
            btnSelect.Visible = false;
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

    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (DataControlFieldCell cell in e.Row.Cells)
                {
                    if (cell.Text.Equals("&nbsp;"))
                    {
                        cell.Text = string.Empty;
                    }
                    if (cell.Text.Contains("&amp;"))
                    {
                        cell.Text = cell.Text.Replace("&amp;", "&");
                    }

                }
            }
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



    protected void btnSelect_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSelect.Text == "Select All")
            {
                foreach (GridViewRow r in grdDetails.Rows)
                {
                    CheckBox chk = (CheckBox)r.FindControl("chk");
                    chk.Checked = true;
                }
                btnSelect.Text = "Unselect All";
            }
            else
            {
                foreach (GridViewRow r in grdDetails.Rows)
                {
                    CheckBox chk = (CheckBox)r.FindControl("chk");
                    chk.Checked = false;
                }
                btnSelect.Text = "Select All";
            }
            btnSelect.Visible = true;
            btnsave.Visible = true;
        }
        catch (Exception)
        { }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            grdDetails.DataSource = csedept.SelectQuery("select ID, Name from StudentTbl where DeptID = '" + ddlStudentDepartment.SelectedValue + "' and  Year = '" + ddlStudentYear.SelectedValue + "' and Semester = '" + ddlStudentSemester.SelectedValue + "' and IsActive = '1'");
            grdDetails.DataBind();
            if (grdDetails.Rows.Count > 0)
            {

                btnsave.Visible = true;
                btnSelect.Visible = true;
            }
        }
        catch (Exception)
        { }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow r in grdDetails.Rows)
            {

                CheckBox chk = (CheckBox)r.FindControl("chk");
                string AttID = id.auto("AttendanceTbl", "ID").ToString();
                HiddenField hdStudentID = (HiddenField)r.FindControl("hdStudentID");
                if (chk.Checked == true)
                {
                    csedept.ExecuteScalar("insert into AttendanceTbl values ('" + AttID + "','" + hdStudentID.Value + "','" + ddlSubjects.SelectedValue + "','1','" + createdDate + "',NULL,'1')");
                }
                else
                {
                    csedept.ExecuteScalar("insert into AttendanceTbl values ('" + AttID + "','" + hdStudentID.Value + "','" + ddlSubjects.SelectedValue + "','0','" + createdDate + "',NULL,'1')");
                }
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Well done! You have successfully Done Operation.');window.location='ViewAttendance.aspx';", true);
            }
        }
        catch (Exception)
        { }
    }

}