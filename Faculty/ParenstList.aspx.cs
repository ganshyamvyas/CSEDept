using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faculty_ParenstList : System.Web.UI.Page
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
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('" + ex.Message + "');window.location='Dashboard.aspx';", true);
        }
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

    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //e.Row.Cells[2].Visible = false;
            //e.Row.Cells[3].Visible = false;
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('" + ex.Message + "');window.location='" + Request.Url.AbsoluteUri + "';", true);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            grdDetails.DataSource = csedept.SelectQuery("SELECT DepartmentTbl.DepartmentName, StudentTbl.RollNo, StudentTbl.Name, StudentTbl.FathersName, StudentTbl.MothersName, StudentTbl.FathersContactNo, StudentTbl.MothersContactNo, StudentTbl.ContactNo, StudentTbl.Year, StudentTbl.Semester from StudentTbl inner join DepartmentTbl on StudentTbl.DeptID = DepartmentTbl.ID where StudentTbl.Year = '" + ddlStudentYear.SelectedValue + "' and StudentTbl.Semester = '" + ddlStudentSemester.SelectedValue + "' and StudentTbl.DeptID = '" + ddlStudentDepartment.SelectedValue + "' and StudentTbl.IsActive = '1'");
            grdDetails.DataBind();

            if (grdDetails.Rows.Count > 0)
            {
                listheading.InnerText = "All Parents List";
                listheading.Visible = true;
            }
            else
            {
                listheading.InnerText = "No Parenst List yet...";
                listheading.Visible = true;
            }

        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('" + ex.Message + "');window.location='" + Request.Url.AbsoluteUri + "';", true);
        }
    }
    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdDetails.PageIndex = e.NewPageIndex;

        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('" + ex.Message + "');window.location='" + Request.Url.AbsoluteUri + "';", true);
        }
    }
}