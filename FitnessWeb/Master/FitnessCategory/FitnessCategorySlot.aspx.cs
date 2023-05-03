using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_FitnessCategorySlot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null && Session["userRole"] == null)
        {
            Session.Clear();
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["LogoutUrl"].Trim(), true);
        }
        if (!IsPostBack)
        {

        }
    }
    #region Add Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ViewSlotList.Visible = false;
        divGv.Visible = false;
        DivForm.Visible = true;
    }
    #endregion
    #region Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
      
        divDates.Visible = false;
    }
    #endregion

    protected void chkWorkingDays_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("FitnessCategoryPrice.aspx");
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        ViewSlotList.Visible = true;
        divView.Visible = false;
        divEdit.Visible = true;
      
    }

    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        divView.Visible = false;
        divEdit.Visible = true;
       
        divDates.Visible = true;
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        ViewSlotList.Visible = true;
        divView.Visible = true;
        divEdit.Visible = false;
    }
}