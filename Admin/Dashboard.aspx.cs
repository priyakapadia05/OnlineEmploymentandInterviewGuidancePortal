using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace OnlineJobPortal.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                //Response.Redirect("../User/Login.aspx");
            }

            if (!IsPostBack)
            {
                fnConnectDB();
                fsnUsers();
                fnJobs();
                fnAppliedJobs();
                ContactCount();
                fnGuide();
            }
        }

        protected void ContactCount()
        {
            try
            {
                fnConnectDB();
                string qry = "SELECT Count(*) FROM tblContact";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    Session["Contact"] = dt.Rows[0][0];
                }
                else
                {
                    Session["Contact"] = 0;
                }
                conn.Close();
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        protected void fnAppliedJobs()
        {
            try
            {
                fnConnectDB();
                string qry = "SELECT Count(*) FROM tblAppliedJobs";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Session["AppliedJob"] = dt.Rows[0][0];
                }
                else
                {
                    Session["AppliedJob"] = 0;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        protected void fnJobs()
        {
            try
            {
                fnConnectDB();
                string qry = "SELECT Count(*) FROM tblJob";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Session["Job"] = dt.Rows[0][0];
                }
                else
                {
                    Session["Job"] = 0;
                }
               
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        protected void fsnUsers()
        {
            try
            {
                fnConnectDB();
                string qry = "SELECT Count(*) FROM tblUser";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Session["Users"] = dt.Rows[0][0];
                }
                else
                {
                    Session["Users"] = 0;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
        }

        protected void fnGuide()
        {
            try
            {
                fnConnectDB();
                string qry = "SELECT Count(*) FROM tblGuide";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Session["Guide"] = dt.Rows[0][0];
                }
                else
                {
                    Session["Guide"] = 0;
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
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
    }
}