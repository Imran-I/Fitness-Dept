using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DashBoardold : System.Web.UI.Page
{
    readonly Helper Helper = new Helper();
    readonly string BaseUri;
    readonly string GetBookingAndEnquires;
    readonly string BindBookingList;
    readonly string BindEnquiriesList;
    string Session_BranchId;
    string Session_GymOwnerId;
    string Token;
    readonly string LogOutUri;




    public DashBoardold()
    {
        BaseUri = $"{ConfigurationManager.AppSettings["BaseUrl"].Trim()}";
        LogOutUri = $"{ConfigurationManager.AppSettings["LogoutUrl"].Trim()}";

        GetBookingAndEnquires = $"{BaseUri}dashBoardForBranch/getBookingEnquiresCount";
        BindBookingList = $"{BaseUri}dashBoardForBranch/getBookinglist";
        BindEnquiriesList = $"{BaseUri}dashBoardForBranch/getEnquirylist";
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
            Session_GymOwnerId = Session["gymOwnerId"].ToString();
            Session_BranchId = Session["branchId"].ToString();

            if (!IsPostBack)
            {
                SetTodayDateInFromAndToDate();
                BindBookingAndEnquiresCount();
                HideOrShowBookingListOrEnquiries("FromDateAndToDate");
            }
        }
        catch (Exception Ex)
        {
            ShowErrorPopup(Ex);
        }
    }

    #region Search And Reset
    #region Search
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindBookingAndEnquiresCount();
        }
        catch (Exception Ex)
        {
            ShowErrorPopup(Ex);
        }
    }
    #endregion
    #region Reset 
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        try
        {
            SetTodayDateInFromAndToDate();
            BindBookingAndEnquiresCount();
        }
        catch (Exception Ex)
        {
            ShowErrorPopup(Ex);
        }
    }
    #endregion
    #endregion

    #region Bind BookingAndEnquiryListCount,BookingList And EnquiryList
    #region Bookings Count Click
    protected void LnkBtnBookingCount_Click(object sender, EventArgs e)
    {
        try
        {
            BindBookingListGv();
        }
        catch (Exception Ex)
        {
            ShowErrorPopup(Ex);
        }
    }
    #endregion
    #region Enquiry Count Click
    protected void LnkBtnEnquiryCount_Click(object sender, EventArgs e)
    {
        try
        {
            BingEnquiriesListGv();
        }
        catch (Exception Ex)
        {
            ShowErrorPopup(Ex);
        }
    }
    #endregion
    #region Back Button Click
    protected void BtnBactoFromAndToDatePage_Click(object sender, EventArgs e)
    {
        try
        {
            BindBookingAndEnquiresCount();
            HideOrShowBookingListOrEnquiries("FromDateAndToDate");
        }
        catch (Exception Ex)
        {
            ShowErrorPopup(Ex);
        }
    }
    #endregion
    #region Bind Booking List
    void BindBookingListGv()
    {
        try
        {
            string RequestUri = $"{BindBookingList}?gymOwnerId={Session_GymOwnerId}&branchId={Session_BranchId}&fromDate={getFromOrToDate("FromDate")}&toDate={getFromOrToDate("ToDate")}";

            Helper.APIGet(RequestUri, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                GvBookingList.DataSource = Dt;
                GvBookingList.DataBind();
                HideOrShowBookingListOrEnquiries("BookingList");
            }
            else
            {
                ShowInfoPopup(Response);
            }
        }
        catch (Exception Ex)
        {
            ShowErrorPopup(Ex);
        }
    }
    #endregion
    #region Bind Enquries List
    void BingEnquiriesListGv()
    {
        try
        {
            string RequestUri = $"{BindEnquiriesList}?gymOwnerId={Session_GymOwnerId}&branchId={Session_BranchId}&fromDate={getFromOrToDate("FromDate")}&toDate={getFromOrToDate("ToDate")}";

            Helper.APIGet(RequestUri, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                GvEnquiriesList.DataSource = Dt;
                GvEnquiriesList.DataBind();
                HideOrShowBookingListOrEnquiries("EnquiryList");
            }
            else
            {
                ShowInfoPopup(Response);
            }
        }
        catch (Exception Ex)
        {
            ShowErrorPopup(Ex);
        }
    }
    #endregion
    #region BindBookingAndEnquires Count
    void BindBookingAndEnquiresCount()
    {
        try
        {
            string requestUri = $"{GetBookingAndEnquires}?gymOwnerId={Session_GymOwnerId}&branchId={Session_BranchId}&fromDate={getFromOrToDate("FromDate")}&toDate={getFromOrToDate("ToDate")}";
            Helper.APIGet(requestUri, Token, out DataTable Dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                if (Dt.Rows.Count > 0)
                {
                    LnkBtnBookingCount.Text = Dt.Rows[0]["BookingCount"].ToString();
                    LnkBtnEnquiryCount.Text = Dt.Rows[0]["EnquiryCount"].ToString();
                }
            }
            else
            {
                ShowInfoPopup(Response);
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            txtFromDate.Text = getFromOrToDate("FromDate");
            txtToDate.Text = getFromOrToDate("ToDate");
        }
    }
    #endregion
    #endregion

    #region UserDefine Function
    #region Hide Or Show Booking List, Enquiry List, FromDate and ToDate Div
    void HideOrShowBookingListOrEnquiries(string Show)
    {
        try
        {
            if (Show == "BookingList")
            {
                divBookingList.Visible = true;
                divFromDateToDateAndCount.Visible = false;
                divEnquiryList.Visible = false;

                BtnBactoFromAndToDatePage.Visible = true;
            }
            else if (Show == "EnquiryList")
            {
                divEnquiryList.Visible = true;
                divBookingList.Visible = false;
                divFromDateToDateAndCount.Visible = false;

                BtnBactoFromAndToDatePage.Visible = true;
            }
            else if (Show == "FromDateAndToDate")
            {
                divFromDateToDateAndCount.Visible = true;
                divBookingList.Visible = false;
                divEnquiryList.Visible = false;

                BtnBactoFromAndToDatePage.Visible = false;
            }
        }
        catch (Exception Ex)
        {
            ShowErrorPopup(Ex);
        }
    }
    #endregion
    #region GetFromDateAndToDate
    string getFromOrToDate(string FromOrToDate)
    {
        if (FromOrToDate == "FromDate")
            return txtFromDate.Text.Trim();
        else if (FromOrToDate == "ToDate")
            return txtToDate.Text.Trim();
        else
            return null;
    }
    #endregion
    #region SetTodayDateInFromAndToDate
    void SetTodayDateInFromAndToDate()
    {
        try
        {
            txtFromDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion
    #region Alerts
    void ShowSuccessPopup(string Message)
    {
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert(`" + Message.Trim() + "`);", true);
    }
    void ShowInfoPopup(string Message)
    {
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert(`" + Message.Trim() + "`);", true);
    }
    void ShowErrorPopup(Exception Ex)
    {
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert(`" + Ex.Message.Trim() + "`);", true);
    }
    #endregion
    #endregion
}
