using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class StudentHome : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LIBRARYConnectionString"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da;
    DataSet ds;
    string sql_query;

    protected void Page_Load(object sender, EventArgs e)
    {
        GetAssignedBooks();
    }

    private void GetAssignedBooks()
    {
        try
        {
            sql_query = "Select * from Assign Where studentid='" + Session["StudentId"].ToString() + "'";
            da = new SqlDataAdapter(sql_query, con);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.ForeColor = System.Drawing.Color.Green;
                lblMsg.Font.Size = 16;
                lblMsg.Text = "Data Found";

                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();

                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Font.Size = 16;
                lblMsg.Text = "Data Not Found";
            }
        }
        catch
        {
            con.Close();
        }
    }

   
}