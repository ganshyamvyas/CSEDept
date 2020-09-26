using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ManageSubjects : System.Web.UI.Page
{
    dbcode csedept = new dbcode();
    Auto id = new Auto();
    string DateNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            msgsuccess.Visible = false;
            msgFails.Visible = false;
            if (!IsPostBack)
            {
                getAllFaculities();
                getAllSubjects();
            }
            btnUpdate.Visible = false;
        }
        catch (Exception ex)
        {
            msgFails.InnerText = ex.Message;
            msgFails.Visible = true;
        }
    }
    public void getAllFaculities()
    {
        try
        {
            ddlfaculty.DataSource = csedept.SelectQuery("Select ID, Name from FacultyTbl where IsActive='1'");
            ddlfaculty.DataTextField = "Name";
            ddlfaculty.DataValueField = "ID";
            ddlfaculty.DataBind();
            ddlfaculty.Items.Insert(0, new ListItem("-----Select Faculty-----", "0"));
        }
        catch(Exception ex)
        {
            msgFails.InnerText = ex.Message;
            msgFails.Visible = true;
        }
    }
    public void getAllSubjects()
    {
        try
        {
            grdDetails.DataSource = csedept.SelectQuery("Select SubjectTbl.ID, FacultyTbl.ID as [Faculty ID], SubjectTbl.SubjectCode, SubjectTbl.SubjectName, FacultyTbl.Name as [Faculty Name], SubjectTbl.CreateDate, SubjectTbl.ModifiedDate from SubjectTbl inner join FacultyTbl on SubjectTbl.FacultytId = FacultyTbl.ID where SubjectTbl.Isactive='1' and FacultyTbl.IsActive='1' order by SubjectTbl.Id DESC");
            grdDetails.DataBind(); 
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

            long SubjectID = id.auto1("SubjectTbl", "ID");
            csedept.ExecuteQuery("insert into SubjectTbl values(" + SubjectID + ",'"+ddlfaculty.SelectedValue+"','"+txtSubjectCode.Text+"','" + txtSubjectName.Text + "','"+DateNow+"',NULL,'1')");
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Well done! You have successfully Done Operation.');window.location='ManageSubjects.aspx';", true);
        }
        catch (Exception)
        { }
    }

    protected void grdDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                string SID = grdDetails.Rows[e.RowIndex].Cells[2].Text;
                csedept.ExecuteQuery("Update SubjectTbl set ModifiedDate = '"+DateNow+"', IsActive='0' where Id='" + SID + "'");
                msgsuccess.Visible = true;
                getAllSubjects();
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
            Session["SubID"] = grdDetails.Rows[e.NewSelectedIndex].Cells[2].Text;
            ddlfaculty.SelectedValue = grdDetails.Rows[e.NewSelectedIndex].Cells[3].Text;
            txtSubjectCode.Text = grdDetails.Rows[e.NewSelectedIndex].Cells[4].Text;
            txtSubjectName.Text = grdDetails.Rows[e.NewSelectedIndex].Cells[5].Text;
            
            btnUpdate.Visible = true;
            btnSave.Visible = false;
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

            string SubProCode = Session["SubID"].ToString();
            csedept.ExecuteQuery("Update SubjectTbl set FacultytId = '"+ddlfaculty.SelectedValue+ "', SubjectCode='" + txtSubjectCode.Text + "', SubjectName = '"+txtSubjectName.Text+ "', ModifiedDate = '" + DateNow+"' where Id='" + SubProCode + "' ");
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Well done! You have successfully Done Operation.');window.location='ManageSubjects.aspx';", true);

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
                    // check all cells in one row
                    foreach (Control control in cell.Controls)
                    {
                        // Must use LinkButton here instead of ImageButton
                        // if you are having Links (not images) as the command button.
                        LinkButton button = control as LinkButton;
                        if (button != null && button.CommandName == "Delete")
                            // Add delete confirmation
                            button.OnClientClick = "Confirm()";
                    }
                }
            }
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
        }
        catch (Exception)
        { }
    }
}