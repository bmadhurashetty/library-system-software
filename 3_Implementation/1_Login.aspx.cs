using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Request.Cookies["user"].Expires = DateTime.Now.AddMinutes(-30);
            Request.Cookies.Remove("user");
        }
        catch (Exception)
        {
                
          
        }
        
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (rbtnType.Items[0].Selected == true)
        {
            if (inputEmail.Value == "admin" && inputPassword.Value == "123")
            {
                Response.Cookies["user"]["login"] = "true";
                Response.Redirect("Home.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "key", "<script>alert('Invalid Credentials!!!')</script>");                
            }
        }
        else if (rbtnType.Items[1].Selected == true)
        {
            try
            {
                SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LIBRARYConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("Select count(*) from Student Where studentid=@id and password=@pwd", conn);
                cmd.Parameters.AddWithValue("@id", inputEmail.Value);
                cmd.Parameters.AddWithValue("@pwd", inputPassword.Value);
               
                try
                {
                    conn.Open();
                    int cnt = int.Parse(cmd.ExecuteScalar().ToString());

                    if (cnt == 1)
                    {
                        Session["StudentId"] = inputEmail.Value;
                        Response.Redirect("StudentHome.aspx");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "key", "<script>alert('Invalid Credentials!!!')</script>");   
                    }
                   
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    conn.Close();
                }


            }
            catch (Exception ex)
            {

            }
        
        }
    }
}
