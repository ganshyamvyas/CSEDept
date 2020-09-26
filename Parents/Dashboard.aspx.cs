using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Parents_Dashboard : System.Web.UI.Page
{
    dbcode csedeptm = new dbcode();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["AdminName"] != null)
            {
                getTotals();
            }
        }
        catch (Exception ex)
        {
        }
    }
    public void getTotals()
    {
        try
        {
            //faculty.InnerText = csedeptm.ExecuteScalar("select count(*) from FacultyTbl where IsActive='1'");
            //students.InnerText = csedeptm.ExecuteScalar("select count (*) from StudentTbl where IsActive ='1'");
            //subjects.InnerText = csedeptm.ExecuteScalar("select count (*) from SubjectTbl where IsActive ='1'");
        }
        catch (Exception)
        { }
    }
}