using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ManageFeedBackQues : System.Web.UI.Page
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
                getAllQuestion();
            }
        }
        catch (Exception ex)
        {
            msgFails.InnerText = ex.Message;
            msgFails.Visible = true;
        }
    }
    protected void getAllQuestion()
    {
        try
        {
            grdDetails.DataSource = csedept.SelectQuery("select ID, FeedBackQues, CreateDate, ModifiedDate from FeedBackQuesTbl where IsActive='1' ");
            grdDetails.DataBind();
            if (grdDetails.Rows.Count > 0)
            {
                catheading.InnerText = "All FeedBack Questions";
                catheading.Visible = true;
                list.Visible = true;
            }
            else
            {
                catheading.InnerText = "Sorry No FeedBack Questions Yet!!";
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
            long FID = id.auto1("FeedBackQuesTbl", "ID");

            csedept.ExecuteQuery("insert into FeedBackQuesTbl values(" + FID + ",'" + txtques.Text + "','" + createdate + "',NULL,'1')");
            csedept.ClearInputs(Page.Controls);
            getAllQuestion();
            msgsuccess.Visible = true;

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
            string FeeDID = Session["Cid"].ToString();
            string modifieddate = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");
           
                csedept.ExecuteQuery("update FeedBackQuesTbl set FeedBackQues='" + txtques.Text + "', ModifiedDate='" + modifieddate + "' where Id='" + FeeDID + "' and IsActive='1' ");
                getAllQuestion();
                btnSave.Visible = true;
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
                string FeeDID = grdDetails.Rows[e.RowIndex].Cells[2].Text;
                csedept.ExecuteQuery("Update FeedBackQuesTbl set IsActive='0' where Id='" + FeeDID + "' ");
                csedept.ClearInputs(Page.Controls);
                getAllQuestion();
                //msgsuccess.Visible = true;
                btnSave.Visible = true;
                Response.Write("<script>alert('Your FeedBack Question Has Been Deleted !!')</script>");
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
            Session["Cid"] = grdDetails.Rows[e.NewSelectedIndex].Cells[2].Text;
            txtques.Text = grdDetails.Rows[e.NewSelectedIndex].Cells[3].Text;
            
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