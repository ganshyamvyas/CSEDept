using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_FillMarks : System.Web.UI.Page
{
    dbcode csedept = new dbcode();
    Auto id = new Auto();
    string DateNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            btnSave.Visible = false;
            msgFails.Visible = false;
            msgsuccess.Visible = false;

            if (!IsPostBack)
            {
                getAllSubjects();
                getAllDepts();
                getAllYears();
                getAllSems();

            }


        }
        catch (Exception)
        {
            Response.Redirect("Dashboard.aspx");
        }
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
        catch(Exception)
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
                msgFails.InnerText = ex.Message;
                msgFails.Visible = true;
            }
        }
        catch (Exception)
        { }
    }

   

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //string pid = Request.QueryString["pid"];
            foreach (GridViewRow row in grdProfileData.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    //csedept.ReplaceChr(Page.Controls);
                    
                    TextBox txtfirstmarks = row.FindControl("txtfirstmarks") as TextBox;
                    TextBox txtsecondmarks = row.FindControl("txtsecondmarks") as TextBox;
                    TextBox txtavg = row.FindControl("txtavg") as TextBox;
                    //DropDownList ddlPosition = row.FindControl("ddlPosition") as DropDownList;
                    TextBox txtassigmarks = row.FindControl("txtassigmarks") as TextBox;
                    TextBox txttotalmarks = row.FindControl("txttotalmarks") as TextBox;
                    HiddenField hdStudentID = row.FindControl("hdStudentID") as HiddenField;
                    if (btnSave.Text == "Save")
                    {
                        long UploadMarkID = id.auto1("FillMarksTbl", "ID");
                        csedept.ExecuteQuery("Insert into FillMarksTbl values (" + UploadMarkID + ",'" + ddlSubjects.SelectedValue + "','" + ddlStudentDepartment.SelectedValue + "','" + ddlStudentYear.SelectedValue + "','" + ddlStudentSemester.SelectedValue + "','" + hdStudentID.Value + "','" + txtfirstmarks.Text + "','" + txtsecondmarks.Text + "','" + txtavg.Text + "','" + txtassigmarks.Text + "','" + txttotalmarks.Text + "','" + DateNow + "','1')");
                    }
                    else
                    {
                        csedept.ExecuteQuery("update FillMarksTbl set FirstMidMarks = '" + txtfirstmarks.Text + "', SecondMidMarks = '" + txtsecondmarks.Text + "', AvgMidMarks = '" + txtavg.Text + "', AssiMarks = '" + txtassigmarks.Text + "', TotalMarks = '" + txttotalmarks.Text + "' where StudentID = '"+hdStudentID.Value+ "' and DeptID = '"+ddlStudentDepartment.SelectedValue+ "' and Year = '"+ddlStudentYear.SelectedValue+ "' and Semester = '"+ddlStudentSemester.SelectedValue+ "' and SubID = '"+ddlSubjects.SelectedValue+ "' and IsActive = '1'");
                    }
                }
            }
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Well done! You have successfully Done Operation.');window.location='ViewUploadedMarks.aspx';", true);
            ddlStudentDepartment.ClearSelection();
            ddlStudentSemester.ClearSelection();
            ddlStudentYear.ClearSelection();
            ddlSubjects.ClearSelection();
            grdProfileData.DataSource = null;
            grdProfileData.DataBind();
            btnSave.Text = "Save";

        }
        catch (Exception)
        { }
    }
 
    protected void txtfirstmarks_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txt = (TextBox)sender;
            GridViewRow gvr = (GridViewRow)txt.NamingContainer;
            TextBox txtfirstmarks = (TextBox)gvr.FindControl("txtfirstmarks");
            TextBox txtsecondmarks = (TextBox)gvr.FindControl("txtsecondmarks");
            TextBox txtavg = (TextBox)gvr.FindControl("txtavg");
            TextBox txtassigmarks = (TextBox)gvr.FindControl("txtassigmarks");
            TextBox txttotalmarks = (TextBox)gvr.FindControl("txttotalmarks");
            if(txtfirstmarks.Text=="")
            {
                txtfirstmarks.Text = "0.0";
            }
            if (txtsecondmarks.Text == "")
            {
                txtsecondmarks.Text = "0.0";
            }
            if (txtavg.Text == "")
            {
                txtavg.Text = "0.0";
            }
            if (txtassigmarks.Text == "")
            {
                txtassigmarks.Text = "0.0";
            }
            if (txttotalmarks.Text == "")
            {
                txttotalmarks.Text = "0.0";
            }

            double marksgetted = double.Parse(txtfirstmarks.Text) + double.Parse(txtsecondmarks.Text);
            double maxmarks = 20;


            double resultavg = (marksgetted / maxmarks)*10;

            txtavg.Text = resultavg.ToString();

            double assmarks = double.Parse(txtassigmarks.Text);

            double totalmarks = assmarks + resultavg;
            txttotalmarks.Text = totalmarks.ToString();
        }
        catch (Exception) { }
    }

    protected void txtsecondmarks_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        GridViewRow gvr = (GridViewRow)txt.NamingContainer;
        TextBox txtfirstmarks = (TextBox)gvr.FindControl("txtfirstmarks");
        TextBox txtsecondmarks = (TextBox)gvr.FindControl("txtsecondmarks");
        TextBox txtavg = (TextBox)gvr.FindControl("txtavg");
        TextBox txtassigmarks = (TextBox)gvr.FindControl("txtassigmarks");
        TextBox txttotalmarks = (TextBox)gvr.FindControl("txttotalmarks");

        if (txtfirstmarks.Text == "")
        {
            txtfirstmarks.Text = "0.0";
        }
        if (txtsecondmarks.Text == "")
        {
            txtsecondmarks.Text = "0.0";
        }
        if (txtavg.Text == "")
        {
            txtavg.Text = "0.0";
        }
        if (txtassigmarks.Text == "")
        {
            txtassigmarks.Text = "0.0";
        }
        if (txttotalmarks.Text == "")
        {
            txttotalmarks.Text = "0.0";
        }

        double marksgetted = double.Parse(txtfirstmarks.Text) + double.Parse(txtsecondmarks.Text);
        double maxmarks = 20;


        double resultavg = (marksgetted / maxmarks) * 10;

        txtavg.Text = resultavg.ToString();

        double assmarks = double.Parse(txtassigmarks.Text);

        double totalmarks = assmarks + resultavg;
        txttotalmarks.Text = totalmarks.ToString();
    }

    protected void txtassigmarks_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        GridViewRow gvr = (GridViewRow)txt.NamingContainer;
        TextBox txtfirstmarks = (TextBox)gvr.FindControl("txtfirstmarks");
        TextBox txtsecondmarks = (TextBox)gvr.FindControl("txtsecondmarks");
        TextBox txtavg = (TextBox)gvr.FindControl("txtavg");
        TextBox txtassigmarks = (TextBox)gvr.FindControl("txtassigmarks");
        TextBox txttotalmarks = (TextBox)gvr.FindControl("txttotalmarks");

        if (txtfirstmarks.Text == "")
        {
            txtfirstmarks.Text = "0.0";
        }
        if (txtsecondmarks.Text == "")
        {
            txtsecondmarks.Text = "0.0";
        }
        if (txtavg.Text == "")
        {
            txtavg.Text = "0.0";
        }
        if (txtassigmarks.Text == "")
        {
            txtassigmarks.Text = "0.0";
        }
        if (txttotalmarks.Text == "")
        {
            txttotalmarks.Text = "0.0";
        }

        double marksgetted = double.Parse(txtfirstmarks.Text) + double.Parse(txtsecondmarks.Text);
        double maxmarks = 20;


        double resultavg = (marksgetted / maxmarks) * 10;

        txtavg.Text = resultavg.ToString();

        double assmarks = double.Parse(txtassigmarks.Text);

        double totalmarks = assmarks + resultavg;
        txttotalmarks.Text = totalmarks.ToString();
    }

    

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string SubId = csedept.ExecuteScalar("select count(*) from FillMarksTbl where IsActive='1' and SubID = '" + ddlSubjects.SelectedValue+"' and IsActive='1'");
            if (int.Parse(SubId) > 0)
            {
                grdProfileData.DataSource = csedept.SelectQuery("select FillMarksTbl.ID,FillMarksTbl.StudentID, StudentTbl.RollNo, StudentTbl.Name, StudentTbl.FathersName,  FillMarksTbl.FirstMidMarks, FillMarksTbl.SecondMidMarks, FillMarksTbl.AvgMidMarks, FillMarksTbl.AssiMarks, FillMarksTbl.TotalMarks from FillMarksTbl inner join StudentTbl on FillMarksTbl.StudentID = StudentTbl.ID where FillMarksTbl.SubID = '" + ddlSubjects.SelectedValue + "' and StudentTbl.DeptID = '" + ddlStudentDepartment.SelectedValue + "' and StudentTbl.Year = '" + ddlStudentYear.SelectedValue + "' and StudentTbl.Semester = '" + ddlStudentSemester.SelectedValue + "' and StudentTbl.IsActive='1' and FillMarksTbl.IsActive='1'");
                grdProfileData.DataBind();
                btnSave.Text = "Update";
                
            }
            else
            {

                DataTable dtData = new DataTable();
                dtData = csedept.SelectQuery("select ID as [StudentID], RollNo, Name, FathersName from StudentTbl where DeptID = '" + ddlStudentDepartment.SelectedValue + "' and Year = '" + ddlStudentYear.SelectedValue + "' and Semester = '" + ddlStudentSemester.SelectedValue + "' and IsActive = '1'");
                dtData.Columns.Add("FirstMidMarks");
                dtData.Columns.Add("SecondMidMarks");
                dtData.Columns.Add("AvgMidMarks");
                dtData.Columns.Add("AssiMarks");
                dtData.Columns.Add("TotalMarks");
                grdProfileData.DataSource = dtData;
                grdProfileData.DataBind();
                btnSave.Text = "Save";
            }
            if (grdProfileData.Rows.Count > 0)
            {
                btnSave.Visible = true;
            }

        }
        catch(Exception)
        { }
    }

 
}