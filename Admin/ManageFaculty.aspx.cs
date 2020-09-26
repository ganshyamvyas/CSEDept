using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ManageFaculty : System.Web.UI.Page
{
    
    dbcode csedept = new dbcode();
    Auto id = new Auto();
    string Role;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Role = Session["Role"].ToString();
            if (Role == "3")
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    getAllFaculty();
                }
                msgFails.Visible = false;
                msgsuccess.Visible = false;
                btnUpdate.Visible = false;
                EntryForm.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Default.aspx");
        }
    }
    public void getAllFaculty()
    {
        try
        {
            grdDetails.DataSource = csedept.SelectQuery("Select ID, Name as [Faculty Name], EmailID as [Email ID], ContactNo as [Contact No], Password,Image, CreateDate as [Create Date], ModifiedDate as [Modified Date] from FacultyTbl where IsActive='1'");
            grdDetails.DataBind();
        }
        catch (Exception ex)
        {
            msgFails.Visible = true;
            msgFails.InnerText = ex.Message;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            if (Flpimages.HasFile == true)
            {
                long FacultyID = id.auto1("FacultyTbl", "ID");
                string dateNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");
                Flpimages.SaveAs(Server.MapPath("~/FacultyIMG/" + FacultyID + Flpimages.FileName));
                string path = "FacultyIMG/" + FacultyID + Flpimages.FileName;
                csedept.ExecuteQuery("Insert into FacultyTbl values (" + FacultyID + ",'" + txtFacultyName.Text + "','" + txtEmailID.Text + "','" + txtContactNo.Text + "','" + txtPassword.Text + "','"+path+"','" + dateNow + "',NULL,'1')");
                csedept.ClearInputs(Page.Controls);
                msgsuccess.Visible = true;
                msgsuccess.InnerText = "Faculty Added successfully";
            }
            else
            {
                msgFails.Visible = true;
                msgFails.InnerText = "Please Select Image";
            }
        }
        catch (Exception ex)
        {
            msgFails.Visible = true;
            msgFails.InnerText = ex.Message;
            EntryForm.Visible = true;
        }
    }
    protected void grdDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string dateNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                string FID = grdDetails.Rows[e.RowIndex].Cells[3].Text;
                csedept.ExecuteQuery("Update FacultyTbl set IsActive=0, ModifiedDate='" + dateNow + "' where ID='" + FID + "'");
                csedept.ClearInputs(Page.Controls);
                msgsuccess.Visible = true;
                msgsuccess.InnerText = "Faculty Deleted successfully";
                btnSave.Visible = true;
                getAllFaculty();
            }
        }
        catch (Exception ex)
        {
            msgFails.Visible = true;
            msgFails.InnerText = ex.Message;
        }
    }
    protected void grdDetails_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            Session["FID"] = grdDetails.Rows[e.NewSelectedIndex].Cells[3].Text;
            txtFacultyName.Text = grdDetails.Rows[e.NewSelectedIndex].Cells[4].Text;
            txtEmailID.Text = grdDetails.Rows[e.NewSelectedIndex].Cells[5].Text;
            txtContactNo.Text = grdDetails.Rows[e.NewSelectedIndex].Cells[6].Text;
            txtPassword.Text = grdDetails.Rows[e.NewSelectedIndex].Cells[7].Text;
            imgPerson.ImageUrl = "../" + grdDetails.Rows[e.NewSelectedIndex].Cells[8].Text;
            btnSave.Visible = false;
            btnUpdate.Visible = true;
            EntryForm.Visible = true;
        }
        catch (Exception ex)
        {
            msgFails.Visible = true;
            msgFails.InnerText = ex.Message;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string dateNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");
            string FID = Session["FID"].ToString();
            if (Flpimages.HasFile == true)
            {
                Flpimages.SaveAs(Server.MapPath("~/FacultyIMG/" + FID + Flpimages.FileName));
                string path = "FacultyIMG/" + FID + Flpimages.FileName;

                csedept.ExecuteQuery("Update FacultyTbl set Name='" + txtFacultyName.Text + "',EmailID='" + txtEmailID.Text + "', ContactNo='" + txtContactNo.Text + "', Password='" + txtPassword.Text + "',Image='" + path + "', ModifiedDate='" + dateNow + "' where ID='" + FID + "' ");
                csedept.ClearInputs(Page.Controls);
                getAllFaculty();
                msgsuccess.Visible = true;
                msgsuccess.InnerText = "Faculty Updated successfully";
            }
            else
            {
                csedept.ExecuteQuery("Update FacultyTbl set Name='" + txtFacultyName.Text + "',EmailID='" + txtEmailID.Text + "', ContactNo='" + txtContactNo.Text + "', Password='" + txtPassword.Text + "', ModifiedDate='" + dateNow + "' where ID='" + FID + "' ");
                csedept.ClearInputs(Page.Controls);
                getAllFaculty();
                msgsuccess.Visible = true;
                msgsuccess.InnerText = "Faculty Updated successfully";
            }
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }
        catch (Exception ex)
        {
            msgFails.Visible = true;
            msgFails.InnerText = ex.Message;
            btnUpdate.Visible = true;
            EntryForm.Visible = true;
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
                    if (cell.Text.Equals("&nbsp;"))
                    {
                        cell.Text = string.Empty;
                    }
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
            
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[8].Visible = false;
        }
        catch { }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        EntryForm.Visible = true;
    }
    protected void btnSearchName_Click(object sender, EventArgs e)
    {
        try
        {

            grdDetails.DataSource = csedept.SelectQuery("Select ID, Name as [Faculty Name], EmailID as [Email ID], ContactNo as [Contact No], Password,Image, CreateDate as [Create Date], ModifiedDate as [Modified Date] from FacultyTbl where Name like '%"+txtSearchName.Text+"%' and IsActive='1'");
            grdDetails.DataBind();
        }
        catch (Exception ex)
        {
            msgFails.Visible = true;
            msgFails.InnerText = ex.Message;
        }
    }
    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdDetails.PageIndex = e.NewPageIndex;
            getAllFaculty();
        }
        catch (Exception)
        { }
    }
    protected void btnAllExport_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = csedept.SelectQuery("Select Name, EmailID, ContactNo, Password, CreateDate, ModifiedDate from FacultyTbl where IsActive ='1'");
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "FacultyTbl");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = "FacultyTbl" + DateTime.Now.ToString("dd-MMM-yyyy") + ".xlsx";
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
    DataTable dt = new DataTable();
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (flpFile.HasFile)
            {
                if (flpFile.PostedFile.ContentType == "application/vnd.ms-excel" || flpFile.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string fileName = Server.MapPath("~/FacultyFiles/") + Path.GetFileName(flpFile.PostedFile.FileName);
                    flpFile.PostedFile.SaveAs(fileName);

                    string conString = "";
                    string ext = Path.GetExtension(flpFile.PostedFile.FileName);
                    if (ext.ToLower() == ".xls")
                    {
                        conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (ext.ToLower() == ".xlsx")
                    {
                        conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    string query = "Select * from [FacultyTbl$]";
                    OleDbConnection con = new OleDbConnection(conString);
                    OleDbDataAdapter data = new OleDbDataAdapter(query, con);
                    data.Fill(dt);
                    int i = 0;
                    //File_Upload file = new File_Upload();  
                    for (i = 0; i < dt.Rows.Count; i++)
                    {

                        string FacultyName = dt.Rows[i]["Name"].ToString();
                        string EmailID = dt.Rows[i]["EmailID"].ToString();
                        string ContactNo = dt.Rows[i]["ContactNo"].ToString();
                        string Password = dt.Rows[i]["Password"].ToString();

                        long FID = id.auto1("FacultyTbl", "ID");
                        string dateNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");
                        csedept.ExecuteQuery("Insert into FacultyTbl values (" + FID + ",'" + FacultyName + "','" + EmailID + "','" + ContactNo + "','" + Password + "','','" + dateNow + "',NULL,'1')");
                    }
                    getAllFaculty();
                    msgsuccess.Visible = true;
                    msgsuccess.InnerText = "Faculty Successfully Uploaded";
                }
            }
            else
            {
                msgFails.Visible = true;
                msgFails.InnerText = "Please Select the Excel File";
            }
        }
        catch (Exception)
        {
            msgFails.Visible = true;
        }
    }
    
}