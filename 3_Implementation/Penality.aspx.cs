using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class Penality : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LIBRARYConnectionString"].ConnectionString);
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da;
    DataSet ds = new DataSet();
    String query;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void txt_AssignId_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtPenality.Text = "";
            query = "select a.bookid, s.studentid, s.studentname, a.assigneddate, a.returndate, a.penality, Status=(Select statusname from statusdetails where statusid=a.statusid) from Assign a inner join Student s ON a.studentid=s.studentid where assignid='" + txt_AssignId.Text.Trim() + "'";
            da = new SqlDataAdapter(query, con);
            con.Open();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Grd_Pending_Status.DataSource = ds;
                Grd_Pending_Status.DataBind();
            }
        }
        catch
        {
            con.Close();
        }
    }
    
    protected void Grd_Pending_Status_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                da = new SqlDataAdapter(query, con);
                //Find the DropDownList in the Row
                DropDownList ddl_status = (e.Row.FindControl("ddl_status") as DropDownList);
                //Select the Country of Customer in DropDownList                
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    SqlDataAdapter da1 = new SqlDataAdapter("select statusid, statusname from statusdetails", con);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        ddl_status.DataSource = ds1;
                        ddl_status.DataTextField = "statusname";
                        ddl_status.DataValueField = "statusid";
                        ddl_status.DataBind();
                        string status = (e.Row.FindControl("lblstatus") as Label).Text;
                        ddl_status.Items.FindByText(status).Selected = true;
                        //Add Default Item in the DropDownList
                        ddl_status.Items.Insert(0, new ListItem("Please select"));
                    }
                }
            }
        }
        catch
        {
            con.Close();
        }
    }
        
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        query = "Update Assign set penality='" + txtPenality.Text.Trim() + "' where assignid='" + txt_AssignId.Text.Trim() + "'";
        cmd = new SqlCommand(query, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
       
        ClientScript.RegisterStartupScript(GetType(), "key", "<script>alert('Updated Successfully!!!')</script>"); 
    }
}