using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
/// <summary>
/// Summary description for Auto
/// </summary>
public class Auto
{
	public Auto()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    SqlDataReader dr;
    public long auto(string tablename, string fieldname)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("Select max(" + fieldname.ToString() + ")+1 from  " + tablename.ToString() + " ", con);
        dr = cmd.ExecuteReader();
        long i = 1;

        if (dr.Read())
        {
            string j = dr[0].ToString();
            if (dr[0].ToString() == null || dr[0].ToString() == "")
            {
                i = 10001;
            }
            else
            {
                i = (long.Parse(dr[0].ToString()));
            }
        }

        dr.Close();
        con.Close();
        return i;
    }
    public long auto1(string tablename, string fieldname)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("Select max(" + fieldname.ToString() + ")+1 from  " + tablename.ToString() + " ", con);
        dr = cmd.ExecuteReader();
        long i = 1;
        if (dr.Read())
        {
            string j = dr[0].ToString();
            if (dr[0].ToString() == null || dr[0].ToString() == "")
            {
                i = 1;
            }
            else
            {
                i = (long.Parse(dr[0].ToString()));
            }
        }

        dr.Close();
        con.Close();
        return i;
    }
}