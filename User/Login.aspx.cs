using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml.Linq;

namespace OnlineJobPortal.User
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataSet ds;
        DataTable dt = new DataTable();
        public static int key;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void fnConnectDB()
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
                conn = new SqlConnection(strcon);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    //Response.Write("<script>alert('Connected Successfully')</script>");
                }
            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            fnConnectDB();
            if (ddlType.SelectedValue == "Admin")
            {
                if (txtUsername.Text == "Admin" && txtPassword.Text == "123")
                {
                    Session["s_admin"] = txtUsername.Text.Trim();
                    Response.Redirect("../Admin/Dashboard.aspx");
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "<b>" + txtUsername.Text + " Invalid Admin!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                conn.Close();
            }
            if (ddlType.SelectedValue == "User")
            {
                String qry = "SELECT u_id, u_name ,Password from tblUser WHERE u_name='" + txtUsername.Text.Trim() + "' and Password='" + txtPassword.Text.Trim() + "'";
                cmd = new SqlCommand(qry, conn);
                
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Session["User"] = txtUsername.Text.Trim();
                    key = Convert.ToInt32(dt.Rows[0][0].ToString());
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "<b>" + txtUsername.Text + " Invalid Credentials!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                conn.Close();
            }
        }
    }
}