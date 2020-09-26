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

public partial class Admin_ManageStudents : System.Web.UI.Page
{
    dbcode csedept = new dbcode();
    Auto id = new Auto();
    string Role;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                getAllDepts();
                getAllYears();
                getAllSems();
                getAllStudents();
            }
            msgFails.Visible = false;
            msgsuccess.Visible = false;
            btnUpdate.Visible = false;
            EntryForm.Visible = false;
        }
        catch (Exception ex)
        {
            Response.Redirect("Default.aspx");
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

            ddldept.DataSource = csedept.SelectQuery("Select ID, DepartmentName from DepartmentTbl order by DepartmentName asc");
            ddldept.DataValueField = "ID";
            ddldept.DataTextField = "DepartmentName";
            ddldept.DataBind();
            ddldept.Items.Insert(0, new ListItem("------------Select Department-------------", "0"));
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


    public void getAllStudents()
    {
        try
        {
            grdDetails.DataSource = csedept.SelectQuery("Select StudentTbl.ID,StudentTbl.DeptID, DepartmentTbl.DepartmentName, StudentTbl.RollNo, StudentTbl.Name, StudentTbl.FathersName, StudentTbl.MothersName, StudentTbl.EmailID, StudentTbl.ContactNo, StudentTbl.FathersContactNo, StudentTbl.MothersContactNo, StudentTbl.ParentsLoginPassword, StudentTbl.Password, StudentTbl.Address, StudentTbl.Year, StudentTbl.Semester, StudentTbl.Photo, StudentTbl.CreateDate, StudentTbl.ModifiedDate from StudentTbl inner join DepartmentTbl on StudentTbl.DeptID = DepartmentTbl.ID where StudentTbl.IsActive='1'");
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
                long ID = id.auto1("StudentTbl", "ID");
                string dateNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");
                Flpimages.SaveAs(Server.MapPath("~/StudentIMG/" + ID + Flpimages.FileName));
                string path = "StudentIMG/" + ID + Flpimages.FileName;
                
                csedept.ExecuteQuery("Insert into StudentTbl values (" + ID + ",'" + ddldept.SelectedValue + "','"+txtrollno.Text+"','" + txtName.Text + "','"+txtfathername.Text+"','"+txtmothername.Text+"','"+txtEmailID.Text+"','" + txtContactNo.Text + "','"+txtfcontactno.Text+"','"+txtmcontactno.Text+"','"+txtparentspassword.Text+"','" + txtPassword.Text + "','" + txtaddr.Text + "','"+ddlyear.SelectedValue+"','" + ddlsemester.SelectedValue + "','"+path+"','"+dateNow+"',NULL,'1')");
                csedept.ClearInputs(Page.Controls);
                getAllStudents();
                msgsuccess.Visible = true;
                msgsuccess.InnerText = "Student Added successfully";
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
                string StudentID = grdDetails.Rows[e.RowIndex].Cells[3].Text;
                csedept.ExecuteQuery("Update StudentTbl set IsActive='0', ModifiedDate='" + dateNow + "' where ID='" + StudentID + "'");
                csedept.ClearInputs(Page.Controls);
                getAllStudents();
                msgsuccess.Visible = true;
                msgsuccess.InnerText = "Student Deleted successfully";
                btnSave.Visible = true;
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
            Session["StudentID"] = grdDetails.Rows[e.NewSelectedIndex].Cells[3].Text;
            ddldept.SelectedValue = grdDetails.Rows[e.NewSelectedIndex].Cells[4].Text;
            txtrollno.Text = grdDetails.Rows[e.NewSelectedIndex].Cells[6].Text;
            txtName.Text = grdDetails.Rows[e.NewSelectedIndex].Cells[7].Text;
            txtfathername.Text = grdDetails.Rows[e.NewSelectedIndex].Cells[8].Text;
            txtmothername.Text = grdDetails.Rows[e.NewSelectedIndex].Cells[9].Text;
            txtEmailID.Text = grdDetails.Rows[e.NewSelectedIndex].Cells[10].Text;
            txtContactNo.Text = grdDetails.Rows[e.NewSelectedIndex].Cells[11].Text;
            txtfcontactno.Text = grdDetails.Rows[e.NewSelectedIndex].Cells[12].Text;
            txtmcontactno.Text = grdDetails.Rows[e.NewSelectedIndex].Cells[13].Text;
            txtparentspassword.Text = grdDetails.Rows[e.NewSelectedIndex].Cells[14].Text;
            txtPassword.Text = grdDetails.Rows[e.NewSelectedIndex].Cells[15].Text;
            txtaddr.Text = grdDetails.Rows[e.NewSelectedIndex].Cells[16].Text;
            ddlyear.SelectedValue = grdDetails.Rows[e.NewSelectedIndex].Cells[17].Text;
            ddlsemester.SelectedValue = grdDetails.Rows[e.NewSelectedIndex].Cells[18].Text;
            imgPerson.ImageUrl = "../" + grdDetails.Rows[e.NewSelectedIndex].Cells[19].Text;
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
            string StudentID = Session["StudentID"].ToString();
            if (Flpimages.HasFile == true)
            {
                Flpimages.SaveAs(Server.MapPath("~/StudentIMG/" + StudentID + Flpimages.FileName));
                string path = "StudentIMG/" + StudentID + Flpimages.FileName;

                csedept.ExecuteQuery("Update StudentTbl set DeptID='"+ddldept.SelectedValue+ "' , RollNo = '"+txtrollno.Text+"', Name='" + txtName.Text + "', FathersName = '"+txtfathername.Text+ "', MothersName = '"+txtmothername.Text+"',EmailID='" + txtEmailID.Text + "', ContactNo='" + txtContactNo.Text + "', FathersContactNo = '"+txtfcontactno.Text+ "', MothersContactNo = '"+txtmcontactno.Text+ "', ParentsLoginPassword = '"+txtparentspassword.Text+"', Password='" + txtPassword.Text + "',Address = '"+txtaddr.Text+ "', Year='"+ddlyear.SelectedValue+ "',Semester = '"+ddlsemester.SelectedValue+"',Image='" + path + "', ModifiedDate='" + dateNow + "' where ID='" + StudentID + "'");
                csedept.ClearInputs(Page.Controls);
                getAllStudents();
                msgsuccess.Visible = true;
                msgsuccess.InnerText = "Student Updated successfully";
            }
            else
            {
                csedept.ExecuteQuery("Update StudentTbl set DeptID='" + ddldept.SelectedValue + "' , RollNo = '" + txtrollno.Text + "', Name='" + txtName.Text + "', FathersName = '" + txtfathername.Text + "', MothersName = '" + txtmothername.Text + "',EmailID='" + txtEmailID.Text + "', ContactNo='" + txtContactNo.Text + "', FathersContactNo = '" + txtfcontactno.Text + "', MothersContactNo = '" + txtmcontactno.Text + "', ParentsLoginPassword = '" + txtparentspassword.Text + "', Password='" + txtPassword.Text + "',Address = '" + txtaddr.Text + "', Year='" + ddlyear.SelectedValue + "',Semester = '" + ddlsemester.SelectedValue + "', ModifiedDate='" + dateNow + "' where ID='" + StudentID + "'");
                csedept.ClearInputs(Page.Controls);
                getAllStudents();
                msgsuccess.Visible = true;
                msgsuccess.InnerText = "Student Updated successfully";
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
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[19].Visible = false;
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
                grdDetails.DataSource = csedept.SelectQuery("Select StudentTbl.ID,StudentTbl.DeptID, DepartmentTbl.DepartmentName as [Dept Name]. StudentTbl.RollNo, StudentTbl.Name, StudentTbl.FathersName, StudentTbl.MothersName, StudentTbl.EmailID, StudentTbl.ContactNo, StudentTbl.FathersContactNo, StudentTbl.MothersContactNo, StudentTbl.ParentsLoginPassword, StudentTbl.Password, StudentTbl.Address, StudentTbl.Year, StudentTbl.Semester, StudentTbl.Photo, StudentTbl.CreateDate, StudentTbl.ModifiedDate from StudentTbl inner join DepartmentTbl on StudentTbl.DeptID = DepartmentTbl.ID where StudentTbl.Name like '%" + txtSearchName.Text+ "%' or StudentTbl.ContactNo like '%"+txtSearchName.Text+"%' and StudentTbl.IsActive='1'");
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
            getAllStudents();
        }
        catch (Exception)
        { }
    }
    protected void btnAllExport_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = csedept.SelectQuery("Select StudentTbl.RollNo, StudentTbl.Name, DepartmentTbl.DepartmentName, StudentTbl.FathersName, StudentTbl.MothersName, StudentTbl.EmailID, StudentTbl.ContactNo, StudentTbl.FathersContactNo, StudentTbl.MothersContactNo, StudentTbl.ParentsLoginPassword, StudentTbl.Password, StudentTbl.Address, StudentTbl.Year, StudentTbl.Semester, StudentTbl.CreateDate, StudentTbl.ModifiedDate from StudentTbl inner join DepartmentTbl on StudentTbl.DeptID = DepartmentTbl.ID where StudentTbl.IsActive='1'");
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "StudentTbl");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = "StudentTbl" + DateTime.Now.ToString("dd-MMM-yyyy") + ".xlsx";
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
            if (ddlStudentDepartment.SelectedValue != "0" && ddlStudentYear.SelectedValue != "0" && ddlStudentSemester.SelectedValue != "0")
            {
                if (flpFile.HasFile)
                {
                    if (flpFile.PostedFile.ContentType == "application/vnd.ms-excel" || flpFile.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        string fileName = Server.MapPath("~/StudentFiles/") + Path.GetFileName(flpFile.PostedFile.FileName);
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
                        string query = "Select * from [Student$]";
                        OleDbConnection con = new OleDbConnection(conString);
                        OleDbDataAdapter data = new OleDbDataAdapter(query, con);
                        data.Fill(dt);
                        int i = 0;
                        //File_Upload file = new File_Upload();  
                        for (i = 0; i < dt.Rows.Count; i++)
                        {
                            string rollno = dt.Rows[i]["RollNo"].ToString();
                            string Name = dt.Rows[i]["Name"].ToString();
                            string Fname = dt.Rows[i]["FathersName"].ToString();
                            string Mname = dt.Rows[i]["MothersName"].ToString();
                            string EmailID = dt.Rows[i]["EmailID"].ToString();
                            string ContactNo = dt.Rows[i]["ContactNo"].ToString();
                            string FContactNo = dt.Rows[i]["FathersContactNo"].ToString();
                            string MContactNo = dt.Rows[i]["MothersContactNo"].ToString();
                            string ParentsLoginPass = dt.Rows[i]["ParentsLoginPassword"].ToString();
                            string Password = dt.Rows[i]["Password"].ToString();
                            string Address = dt.Rows[i]["Address"].ToString();
                            string path = "assets/images/Avatar.png";
                            long SID = id.auto1("StudentTbl", "ID");
                             string dateNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");
                            csedept.ExecuteQuery("Insert into StudentTbl values (" + SID + ",'" + ddlStudentDepartment.SelectedValue + "','"+ rollno+"','" + Name + "','"+Fname+"','"+Mname+"','" + EmailID + "','" + ContactNo + "','"+FContactNo+"','"+MContactNo+"','"+ParentsLoginPass+"','" + Password + "','" + Address + "','" + ddlStudentYear.SelectedValue + "','" + ddlStudentSemester.SelectedValue + "','"+path+"','" + dateNow + "',NULL,'1')");
                        }
                        getAllStudents();
                        msgsuccess.Visible = true;
                        msgsuccess.InnerText = "Students Successfully Uploaded";
                    }
                }
                else
                {
                    msgFails.Visible = true;
                    msgFails.InnerText = "Please Select the Excel File";
                }
            }
            else
            {
                msgFails.Visible = true;
                msgFails.InnerText = "Please Select Department , Year & Semester";
            }
        }
        catch (Exception ex)
        {
            msgFails.Visible = true;
            msgFails.InnerText = ex.Message;
        }
    }
}