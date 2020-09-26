using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ManageSemester : System.Web.UI.Page
{
    dbcode csedept = new dbcode();
    Auto id = new Auto();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            msgsuccess.Visible = false;
            msgFails.Visible = false;
            btnUpdate.Visible = false;
            if (!IsPostBack)
            {
                getAllYears();
                getAllSems();
            }
        }
        catch (Exception ex)
        {
            msgFails.InnerText = ex.Message;
            msgFails.Visible = true;
        }
    }
    public void getAllYears()
    {
        try
        {
            ddlyear.DataSource = csedept.SelectQuery("select YrID, Year from YearTbl where IsActive='1' order by YrID ASC");
            ddlyear.DataTextField = "Year";
            ddlyear.DataValueField = "YrID";
            ddlyear.DataBind();
            ddlyear.Items.Insert(0, new ListItem("-----Select Year-----", "0"));
            ddlyear.ClearSelection();
        }
        catch (Exception ex)
        {
           
        }
    }
    protected void getAllSems()
    {
        try
        {
            grdDetails.DataSource = csedept.SelectQuery("select SemesterTbl.SemID, YearTbl.Year, SemesterTbl.Semester, SemesterTbl.CreateDate, SemesterTbl.ModifiedDate from SemesterTbl inner join YearTbl on YearTbl.YrID = SemesterTbl.YearID  where SemesterTbl.IsActive='1' and YearTbl.IsActive='1'");
            grdDetails.DataBind();
            if (grdDetails.Rows.Count > 0)
            {
                catheading.InnerText = "All Semesters";
                catheading.Visible = true;
                list.Visible = true;
            }
            else
            {
                catheading.InnerText = "Sorry No Semester Yet!!";
                catheading.Visible = true;
                list.Visible = false;
            }
        }
        catch (Exception ex)
        {
            msgFails.InnerText = ex.Message;
            msgFails.Visible = true;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            string createdate = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");
            long SemID = id.auto1("SemesterTbl", "SemID");

            csedept.ExecuteQuery("insert into SemesterTbl values(" + SemID + ",'"+ddlyear.SelectedValue+"','" + txtsem.Text + "','" + createdate + "',NULL,'1')");
            msgsuccess.Visible = true;
            getAllSems();
            ddlyear.ClearSelection();
            csedept.ClearInputs(Page.Controls);
            
            

        }
        catch (Exception ex)
        {
            msgFails.InnerText = ex.Message;
            msgFails.Visible = true;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string SemID = Session["Semid"].ToString();
            string modifieddate = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");

            csedept.ExecuteQuery("update SemesterTbl set YearID='" + ddlyear.SelectedValue+ "', Semester = '"+txtsem.Text+"', ModifiedDate='" + modifieddate + "' where SemID='" + SemID + "' and IsActive='1' ");
            getAllSems();
            btnSave.Visible = true;
            ddlyear.ClearSelection();
            csedept.ClearInputs(Page.Controls);
            msgsuccess.Visible = true;


        }
        catch (Exception ex)
        {
            msgFails.InnerText = ex.Message;
            msgFails.Visible = true;
        }
    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (DataControlFieldCell cell in e.Row.Cells)
                {
                    foreach (Control control in cell.Controls)
                    {
                        LinkButton button = control as LinkButton;
                        if (button != null && button.CommandName == "Delete")
                            button.OnClientClick = "Confirm()";
                    }

                }
            }
            e.Row.Cells[2].Visible = false;
        }
        catch (Exception ex)
        {
            msgFails.InnerText = ex.Message;
            msgFails.Visible = true;
        }
    }
    protected void grdDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                string SemID = grdDetails.Rows[e.RowIndex].Cells[2].Text;
                csedept.ExecuteQuery("Update SemesterTbl set IsActive='0' where SemID='" + SemID + "' ");
                csedept.ClearInputs(Page.Controls);
                getAllSems();
                //msgsuccess.Visible = true;
                btnSave.Visible = true;
                Response.Write("<script>alert('Semester Has Been Deleted !!')</script>");
            }
        }
        catch (Exception ex)
        {
            msgFails.InnerText = ex.Message;
            msgFails.Visible = true;
        }
    }
    protected void grdDetails_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            Session["Semid"] = grdDetails.Rows[e.NewSelectedIndex].Cells[2].Text;
            ddlyear.SelectedValue = grdDetails.Rows[e.NewSelectedIndex].Cells[3].Text;
            txtsem.Text = grdDetails.Rows[e.NewSelectedIndex].Cells[4].Text;

            btnUpdate.Visible = true;
            btnSave.Visible = false;
        }
        catch (Exception ex)
        {
            msgFails.InnerText = ex.Message;
            msgFails.Visible = true;
        }
    }
}