using ASP;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_GymOwner_ShiftMaster : System.Web.UI.Page
{
    readonly Helper Helper = new Helper();
    readonly private string BaseUri;
    readonly private string LogOutUri;
    readonly private string BindGridViewUri;
    readonly private string InsertShiftUri;
    readonly private string UpdateShiftUri;
    readonly private string ActiveOrInactiveShiftUri;
    private string Token;
    private int Session_BranchId;
    private int Session_UserId;

    public Master_GymOwner_ShiftMaster()
    {
        BaseUri = $"{ConfigurationManager.AppSettings["BaseUrl"].Trim()}";
        LogOutUri = $"{ConfigurationManager.AppSettings["LogoutUrl"].Trim()}";
        BindGridViewUri = $"{BaseUri}shiftMaster/branchBasedShift";
        InsertShiftUri = $"{BaseUri}shiftMaster/insert";
        UpdateShiftUri = $"{BaseUri}shiftMaster/update";
        ActiveOrInactiveShiftUri = $"{BaseUri}shiftMaster/activeOrInActive";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["userId"] == null && Session["userRole"] == null)
            {
                Session.Clear();
                Response.Redirect(LogOutUri, true);
            }

            Token = Session["APIToken"].ToString();
            Session_BranchId = int.Parse(Session["branchId"].ToString());
            Session_UserId = int.Parse(Session["userId"].ToString());

            if (!IsPostBack)
            {
                BindGridView();
            }
        }
        catch (Exception ex)
        {
            ShowErrorPopup(ex);
        }
    }

    #region BindGridView
    private void BindGridView()
    {
        try
        {
            string BingGridViewFullUri = $"{BindGridViewUri}?branchId={Session_BranchId}";
            Helper.APIGet(BingGridViewFullUri, Token, out DataTable dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                ShowOrHideGrid(ShowOrHide: "Show");

                GvShiftMaster.DataSource = dt;
                GvShiftMaster.DataBind();
            }
            else
            {
                ShowOrHideGrid(ShowOrHide: "Hide");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region Add
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            ShowOrHideGrid("Hide");
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region Cancel
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            BindGridView();
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region Show Or Hide Grid And Form
    private void ShowOrHideGrid(string ShowOrHide)
    {
        try
        {
            if (ShowOrHide == "Show")
            {
                Clear();
                DivGv.Visible = true;
                DivForm.Visible = false;
            }
            else
            {
                DivGv.Visible = false;
                DivForm.Visible = true;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region Clear
    private void Clear()
    {
        txtShiftName.Text = string.Empty;
        txtGracePeriod.Text = string.Empty;
        txtShiftStartTime.Text = string.Empty;
        txtShiftEndTime.Text = string.Empty;
        txtBreakStartTime.Text = string.Empty;
        txtBreakEndTime.Text = string.Empty;
        BtnSubmit.Text = "Submit";
    }
    #endregion

    #region Insert Or Update Shift
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int StatusCode = 0;
            string Response = "";
            DateTime StartTime = Convert.ToDateTime(txtShiftStartTime.Text.Trim());
            DateTime EndTime = Convert.ToDateTime(txtShiftEndTime.Text.Trim());
            DateTime BreakStartTime = Convert.ToDateTime(txtBreakStartTime.Text.Trim());
            DateTime BreakEndTime = Convert.ToDateTime(txtBreakEndTime.Text.Trim());
            if (BtnSubmit.Text == "Submit")
            {

                var Insert = new InsertShift_In()
                {
                    BranchId = Session_BranchId,
                    ShiftName = txtShiftName.Text.Trim(),
                    StartTime = StartTime.ToString("HH:mm"),
                    EndTime = EndTime.ToString("HH:mm"),
                    BreakStartTime = BreakStartTime.ToString("HH:mm"),
                    BreakEndTime = BreakEndTime.ToString("HH:mm"),
                    GracePeriod = int.Parse(txtGracePeriod.Text.Trim()),
                    CreatedBy = Session_UserId
                };
                Helper.APIpost<InsertShift_In>(InsertShiftUri, Token, Insert, out StatusCode, out Response);
            }
            else
            {
                var Update = new UpdateShift_In()
                {
                    ShiftId = int.Parse(ViewState["ShiftId"].ToString()),
                    BranchId = Session_BranchId,
                    ShiftName = txtShiftName.Text.Trim(),
                    StartTime = StartTime.ToString("HH:mm"),
                    EndTime = EndTime.ToString("HH:mm"),
                    BreakStartTime = BreakStartTime.ToString("HH:mm"),
                    BreakEndTime = BreakEndTime.ToString("HH:mm"),
                    GracePeriod = int.Parse(txtGracePeriod.Text.Trim()),
                    UpdatedBy = Session_UserId
                };
                Helper.APIpost<UpdateShift_In>(UpdateShiftUri, Token, Update, out StatusCode, out Response);
            }
            if (StatusCode == 1)
            {
                BindGridView();
                ShowSuccessPopup(Response);
            }
            else
            {
                ShowInfoPopup(Response);
            }
        }
        catch (Exception ex)
        {
            ShowErrorPopup(ex);
        }
    }
    /// <summary>
    /// this class is used as input for insert Shift
    /// </summary>
    private class InsertShift_In
    {
        public int BranchId { get; set; }
        public string ShiftName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string BreakStartTime { get; set; }
        public string BreakEndTime { get; set; }
        public int GracePeriod { get; set; }
        public int CreatedBy { get; set; }
    }

    /// <summary>
    /// this class is used as input for Update Shift
    /// </summary>
    private class UpdateShift_In
    {
        public int ShiftId { get; set; }
        public int BranchId { get; set; }
        public string ShiftName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string BreakStartTime { get; set; }
        public string BreakEndTime { get; set; }
        public int GracePeriod { get; set; }
        public int UpdatedBy { get; set; }
    }
    #endregion

    #region Edit
    protected void ImgEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton ImgBtn = sender as ImageButton;
            GridViewRow GvRow = (GridViewRow)ImgBtn.NamingContainer;

            Label GvShiftName = GvRow.FindControl("GvLblShiftName") as Label;
            Label GvGracePeriod = GvRow.FindControl("GvLblGracePeriod") as Label;
            Label GvShiftStartTime = GvRow.FindControl("GvLblStartTime") as Label;
            Label GvShiftEndTime = GvRow.FindControl("GvLblEndTime") as Label;
            Label GvBreakStartTime = GvRow.FindControl("GvLblBreakStartTime") as Label;
            Label GvBreakEndTime = GvRow.FindControl("GvLblBreakEndTime") as Label;

            ViewState["ShiftId"] = GvShiftMaster.DataKeys[GvRow.RowIndex]["ShiftId"].ToString();

            txtShiftName.Text = GvShiftName.Text.Trim();
            txtGracePeriod.Text = GvGracePeriod.Text.Trim();
            txtShiftStartTime.Text = GvShiftStartTime.Text.Trim();
            txtShiftEndTime.Text = GvShiftEndTime.Text.Trim();
            txtBreakStartTime.Text = GvBreakStartTime.Text.Trim();
            txtBreakEndTime.Text = GvBreakEndTime.Text.Trim();

            ShowOrHideGrid(ShowOrHide: "Hide");

            BtnSubmit.Text = "Update";
        }
        catch (Exception Ex)
        {
            ShowErrorPopup(Ex);
        }
    }
    #endregion

    #region Active Or Inactive
    protected void lnkActiveOrInactive_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton LnkBtn = sender as LinkButton;
            GridViewRow GvRow = (GridViewRow)LnkBtn.NamingContainer;

            string ActiveStatus, ShiftId;

            ActiveStatus = GvShiftMaster.DataKeys[GvRow.RowIndex]["ActiveStatus"].ToString();
            ShiftId = GvShiftMaster.DataKeys[GvRow.RowIndex]["ShiftId"].ToString();

            var ActiveOrInactive = new ActiveOrInactive_in()
            {
                QueryType = ActiveStatus == "A" ? "InActive" : "Active",
                ShiftId = int.Parse(ShiftId),
                UpdatedBy = Session_UserId
            };

            Helper.APIpost<ActiveOrInactive_in>(ActiveOrInactiveShiftUri, Token, ActiveOrInactive, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                ShowSuccessPopup(Response);
            }
            else
            {
                ShowInfoPopup(Response);
            }
            BindGridView();
        }
        catch (Exception Ex)
        {
            ShowErrorPopup(Ex);
        }
    }
    /// <summary>
    /// this class is used as input for active and inactive
    /// </summary>
    private class ActiveOrInactive_in
    {
        public string QueryType { get; set; }
        public int ShiftId { get; set; }
        public int UpdatedBy { get; set; }
    }
    #endregion

    #region Alerts
    public void ShowSuccessPopup(string Message)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert(`" + Message.Trim() + "`);", true);
    }
    public void ShowInfoPopup(string Message)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert(`" + Message.Trim() + "`);", true);
    }
    public void ShowErrorPopup(Exception Ex)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert(`" + Ex.Message.Trim() + "`);", true);
    }
    #endregion
}
