using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["BaseUrl"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"].Trim();
            Session["BaseUrlToken"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrlToken"].Trim();
            Session["userId"] = Request.QueryString["qUserId"].ToString().Trim();
            Session["userRole"] = Request.QueryString["qUserRole"].ToString().Trim();
            Session["userName"] = Request.QueryString["qUserName"].ToString().Trim();
            Session["roleId"] = Request.QueryString["qroleId"].ToString().Trim();
            Session["mailId"] = Request.QueryString["qmailId"].ToString().Trim();
            Session["branchName"] = Request.QueryString["qbranchName"].ToString().Trim();
            Session["approvalStatus"] = Request.QueryString["qapprovalStatus"].ToString().Trim(); ;
            Session["branchId"] = Request.QueryString["qbranchId"].ToString().Trim();
            Session["gymOwnerId"] = Request.QueryString["qgymOwnerId"].ToString().Trim();
            Session["APIToken"] = Request.QueryString["qAPIToken"].ToString().Trim();
            Session["mobileNo"] = Request.QueryString["qmobileNo"].ToString().Trim();




            if (Session["userRole"].ToString().Trim() == "Sadmin")
            {
                Response.Redirect("~/AdminLogin.aspx", false);
            }
            else if (Session["userRole"].ToString().Trim() == "GymOwner")
            {
                Response.Redirect("~/OwnerLogin.aspx", false);
            }
            //else if (Session["userRole"].ToString().Trim() == "Employee")
            //{
            //    Response.Redirect("~/AdminDashBoard.aspx", false);
            //}
            else if (Session["userRole"].ToString().Trim() == "Trainer")
            {
                Response.Redirect("~/DashBoard.aspx", false);
            }
            else if (Session["userRole"].ToString().Trim() == "Admin")
            {
                Response.Redirect("~/AdminDashBoard.aspx", false);
            }
            //else if (Session["userRole"].ToString().Trim() == "Employee" || Session["userRole"].ToString().Trim() == "Admin" || Session["userRole"].ToString().Trim() == "Trainer")
            //{
            //    Response.Redirect("~/DashBoard.aspx", false);
            //}
        }
    }
}