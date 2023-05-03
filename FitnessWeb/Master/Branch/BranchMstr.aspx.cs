using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_Branch_BranchMstr : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtLatitude.Attributes.Add("ReadOnly", "ReadOnly");
        txtLongitude.Attributes.Add("ReadOnly", "ReadOnly");
        spAddorEdit.InnerText = "Add ";
        if (Session["userId"] == null && Session["userRole"] == null)
        {
            Session.Clear();
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["LogoutUrl"].Trim(), true);
        }
        if (!IsPostBack)
        {
            BindBranchMaster();
            BindOwner();
            if (Session["userRole"].ToString().Trim() == "Sadmin" || Session["userRole"].ToString().Trim() == "GymOwner")
            {
                gvBranchMstr.Columns[4].Visible = true;
                gvBranchMstr.Columns[5].Visible = true;
                gvBranchMstr.Columns[6].Visible = true;
            }
            else if (Session["userRole"].ToString().Trim() == "Employee" || Session["userRole"].ToString().Trim() == "Admin")
            {
                gvBranchMstr.Columns[4].Visible = true;
                gvBranchMstr.Columns[5].Visible = false;
                gvBranchMstr.Columns[6].Visible = false;
            }
        }
    }

    #region Add Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        spAddorEdit.InnerText = "Add ";
        divGv.Visible = false;
        DivForm.Visible = true;
        divApprovalPopUp.Visible = false;
    }
    #endregion

    #region Bind Owner List
    public void BindOwner()
    {
        try
        {
            ddlOwnerList.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sUrl = "";


                if (Session["userRole"].ToString().Trim() == "GymOwner")
                {
                    sUrl = $"{Session["BaseUrl"]}ownerMaster/IndividualOwner?gymOwnerId={Session["gymOwnerId"]}";
                }
                else
                {
                    sUrl = Session["BaseUrl"].ToString().Trim() + "ownerMaster/Getddlowner";
                }
                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            ddlOwnerList.DataSource = dt;
                            ddlOwnerList.DataTextField = "gymOwnerName";
                            ddlOwnerList.DataValueField = "gymOwnerId";
                            ddlOwnerList.DataBind();
                            ddlOwnerList.Items.Insert(0, new ListItem("Gym Owner *", "0"));
                            if (Session["userRole"].ToString().Trim() == "Employee" || Session["userRole"].ToString().Trim() == "Admin")
                            {
                                var firstitem = ddlOwnerList.Items.FindByValue(Session["gymOwnerId"].ToString());
                                ddlOwnerList.Items.Clear();
                                ddlOwnerList.Items.Add(firstitem);
                                ddlOwnerList.SelectedValue = firstitem.Value;
                                ddlOwnerList.Enabled = false;
                            }
                        }
                        else
                        {
                            ddlOwnerList.DataBind();
                            ddlOwnerList.Items.Insert(0, new ListItem("Gym Owner *", "0"));
                        }

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        ddlOwnerList.DataBind();
                        ddlOwnerList.Items.Insert(0, new ListItem("Gym Owner *", "0"));
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + response.ReasonPhrase.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region Bind Branch Master  
    public void BindBranchMaster()
    {
        try

        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string sUrl = string.Empty;
                if (Session["userRole"].ToString().Trim() == "Sadmin" & Session["branchId"].ToString().Trim() == "0")
                {
                    sUrl = Session["BaseUrl"].ToString().Trim() + "branch?queryType=GetBranchMstrSA";

                }
                else if (Session["userRole"].ToString().Trim() == "Sadmin")
                {
                    sUrl = Session["BaseUrl"].ToString().Trim() + "branch?queryType=GetBranchMstr&gymOwnerId=" + Session["gymOwnerId"].ToString() + "&branchId=" + Session["branchId"].ToString() + "";

                }
                else if (Session["userRole"].ToString().Trim() == "GymOwner" & Session["branchId"].ToString().Trim() == "0")
                {
                    sUrl = Session["BaseUrl"].ToString().Trim() + "branch?queryType=GetBranchMstrOwner&gymOwnerId=" + Session["gymOwnerId"].ToString() + "";
                }
                else if (Session["userRole"].ToString().Trim() == "GymOwner")
                {
                    sUrl = Session["BaseUrl"].ToString().Trim() + "branch?queryType=GetBranchMstr&gymOwnerId=" + Session["gymOwnerId"].ToString() + "&branchId=" + Session["branchId"].ToString() + "";
                }
                else if (Session["userRole"].ToString().Trim() == "Employee" || Session["userRole"].ToString().Trim() == "Admin")
                {
                    sUrl = Session["BaseUrl"].ToString().Trim() + "branch?queryType=GetBranchMstr&gymOwnerId=" + Session["gymOwnerId"].ToString() + "&branchId=" + Session["branchId"].ToString() + "";
                }

                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            gvBranchMstr.DataSource = dt;
                            gvBranchMstr.DataBind();
                            divGridView.Visible = true;
                        }
                        else
                        {
                            gvBranchMstr.DataSource = null;
                            gvBranchMstr.DataBind();
                            divGv.Visible = false;
                            DivForm.Visible = true;
                        }

                    }
                    else
                    {
                        gvBranchMstr.DataSource = null;
                        gvBranchMstr.DataBind();
                        divGv.Visible = false;
                        DivForm.Visible = true;
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + response.ReasonPhrase.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region Insert Branch
    public void InsertBranch()
    {
        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                DateTime StartTime = Convert.ToDateTime(txtfromTime.Text.Trim());
                DateTime EndTime = Convert.ToDateTime(txttoTime.Text.Trim());
                var Insert = new MstrBranch()
                {
                    gymOwnerId = ddlOwnerList.SelectedValue,
                    branchName = txtBranchName.Text,
                    shortName = txtShortName.Text,
                    latitude = txtLatitude.Text,
                    longitude = txtLongitude.Text,
                    address1 = txtAddress1.Text,
                    address2 = txtAddress2.Text,
                    pincode = txtPincode.Text,
                    city = hfCity.Value,
                    state = hfState.Value,
                    district = hfDistrict.Value,
                    primaryMobileNumber = txtMobileNumber.Text,
                    secondayMobilenumber = txtsecondayMobilenumber.Text,
                    emailId = txtemail.Text,
                    gstNumber = txtGstNumber.Text,
                    fromtime= StartTime.ToString("HH:mm"),
                    totime = EndTime.ToString("HH:mm"),
                    approvalStatus = Session["approvalStatus"].ToString(),
                    createdBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("branch/insert", Insert).Result;
                var Fitness = response.Content.ReadAsStringAsync().Result;
                int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();
                if (response.IsSuccessStatusCode)
                {

                    if (StatusCode == 1)
                    {
                        clear();                       
                        BindBranchMaster();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region Update Branch
    public void UpdateBranch()
    {
        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                DateTime StartTime = Convert.ToDateTime(txtfromTime.Text.Trim());
                DateTime EndTime = Convert.ToDateTime(txttoTime.Text.Trim());
                var update = new MstrBranch()
                {
                    branchId = ViewState["branchId"].ToString(),
                    gymOwnerId = ddlOwnerList.SelectedValue,
                    branchName = txtBranchName.Text,
                    shortName = txtShortName.Text,
                    latitude = txtLatitude.Text,
                    longitude = txtLongitude.Text,
                    address1 = txtAddress1.Text,
                    address2 = txtAddress2.Text,
                    pincode = txtPincode.Text,
                    city = hfCity.Value,
                    state = hfState.Value,
                    district = hfDistrict.Value,
                    primaryMobileNumber = txtMobileNumber.Text,
                    secondayMobilenumber = txtsecondayMobilenumber.Text,
                    emailId = txtemail.Text,
                    gstNumber = txtGstNumber.Text,
                    fromtime = StartTime.ToString("HH:mm"),
                    totime = EndTime.ToString("HH:mm"),
                    approvalStatus = ViewState["approvalStatus"].ToString(),
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("branch/update", update).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        clear();
                        BindBranchMaster();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region Btnsubmit Click Event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubmit.Text == "Submit")
        {
            InsertBranch();
        }
        else
        {
            UpdateBranch();
        }
    }
    #endregion

    #region Btn Edit Click Event
    protected void LnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton lnkbtn = sender as ImageButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;

            Label lblbranchId = (Label)gvrow.FindControl("lblbranchId");
            ViewState["branchId"] = lblbranchId.Text;
            Label lblgymOwnerId = (Label)gvrow.FindControl("lblgymOwnerId");
            BindOwner();
            ddlOwnerList.SelectedValue = lblgymOwnerId.Text;
            ddlOwnerList.Enabled = false;

            Label lblbranchName = (Label)gvrow.FindControl("lblbranchName");
            txtBranchName.Text = lblbranchName.Text;

            Label lblcity = (Label)gvrow.FindControl("lblcity");
            txtCity.Text = lblcity.Text;
            Label lblstate = (Label)gvrow.FindControl("lblstate");
            txtState.Text = lblstate.Text;

            Label lblshortName = (Label)gvrow.FindControl("lblshortName");
            txtShortName.Text = lblshortName.Text;

            Label lbladdress1 = (Label)gvrow.FindControl("lbladdress1");
            txtAddress1.Text = lbladdress1.Text;
            Label lbladdress2 = (Label)gvrow.FindControl("lbladdress2");
            txtAddress2.Text = lbladdress2.Text;

            Label lblpincode = (Label)gvrow.FindControl("lblpincode");
            txtPincode.Text = lblpincode.Text;
            Label lbldistrict = (Label)gvrow.FindControl("lbldistrict");
            txtDistrict.Text = lbldistrict.Text;
            Label lblfromtime = (Label)gvrow.FindControl("lblfromtime");
            txtfromTime.Text = lblfromtime.Text;
            Label lbltotime = (Label)gvrow.FindControl("lbltotime");
            txttoTime.Text = lbltotime.Text;

            Label lbllatitude = (Label)gvrow.FindControl("lbllatitude");
            txtLatitude.Text = lbllatitude.Text;
            hflatitude.Value = lbllatitude.Text;
            Label lbllongitude = (Label)gvrow.FindControl("lbllongitude");
            txtLongitude.Text = lbllongitude.Text;
            hflongitude.Value = lbllatitude.Text;
            Label lblprimaryMobileNumber = (Label)gvrow.FindControl("lblprimaryMobileNumber");
            Label lblsecondayMobilenumber = (Label)gvrow.FindControl("lblsecondayMobilenumber");
            Label lblmailId = (Label)gvrow.FindControl("lblmailId");
            Label lblgstNumber = (Label)gvrow.FindControl("lblgstNumber");
            Label lblapprovalStatus = (Label)gvrow.FindControl("lblapprovalStatus");
            txtMobileNumber.Text = lblprimaryMobileNumber.Text;
            txtsecondayMobilenumber.Text = lblsecondayMobilenumber.Text;
            txtemail.Text = lblmailId.Text;
            txtGstNumber.Text = lblgstNumber.Text;
            hfCity.Value = lblcity.Text;
            hfState.Value = lblstate.Text;
            hfDistrict.Value = lbldistrict.Text;
            divGv.Visible = false;
            DivForm.Visible = true;
            btnSubmit.Text = "Update";
            spAddorEdit.InnerText = "Edit ";
            ViewState["approvalStatus"] = lblapprovalStatus.Text;
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }

    }
    #endregion

    #region Active or Inactive  Click Event
    protected void lnkActiveOrInactive_Click(object sender, EventArgs e)
    {

        try
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            Label lblbranchId = (Label)gvrow.FindControl("lblbranchId");
            Label lblgymOwnerId = (Label)gvrow.FindControl("lblgymOwnerId");
            LinkButton lblActiveStatus = (LinkButton)lnkbtn.FindControl("lnkActiveOrInactive");
            string sActiveStatus = lblActiveStatus.Text.Trim() == "Active" ? "A" : "D";
            string QueryType = string.Empty;
            if (sActiveStatus.Trim() == "D")
            {
                QueryType = "active";
            }
            else
            {
                QueryType = "inActive";
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var activeOrInActive = new MstrBranch()
                {
                    queryType = QueryType.Trim(),
                    gymOwnerId = lblgymOwnerId.Text,
                    branchId = lblbranchId.Text,
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("branch/activeOrInActive", activeOrInActive).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        clear();
                        BindBranchMaster();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region Update Approval Status

    protected void btnSubmitPopup_Click(object sender, EventArgs e)
    {
        ChangeApprovalStatus(ViewState["ApproveBranchId"].ToString(), "C", ViewState["ApproveGymOwnerId"].ToString());
    }

    protected void btnCancelPopup_Click(object sender, EventArgs e)
    {
        divApprovalPopUp.Visible = false;
        txtReason.Text = "";
        divCancellationReason.Visible = false;
        divsubmit.Visible = false;
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        divCancellationReason.Visible = false;
        divsubmit.Visible = false;
        ChangeApprovalStatus(ViewState["ApproveBranchId"].ToString(), "A", ViewState["ApproveGymOwnerId"].ToString());
    }

    protected void btnCancelApprove_Click(object sender, EventArgs e)
    {
        divCancellationReason.Visible = true;
        divsubmit.Visible = true;
        divApprvCancelBtns.Visible = false;
    }
    protected void LnkApprovalStatus_Click(object sender, EventArgs e)
    {
        divApprovalPopUp.Visible = true;
        divApprvCancelBtns.Visible = true;
        divCancellationReason.Visible = false;
        divsubmit.Visible = false;
        LinkButton lnkbtn = sender as LinkButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        LinkButton lblGvapprovalStatus = (LinkButton)gvrow.FindControl("LnkApprovalStatus");
        Label lblbranchId = (Label)gvrow.FindControl("lblbranchId");
        Label lblgymOwnerId = (Label)gvrow.FindControl("lblgymOwnerId");
        string sActiveStatus = lblGvapprovalStatus.Text.Trim() == "Waiting List" ? "N" : "A";
        ViewState["ApproveBranchId"] = lblbranchId.Text;
        ViewState["ApproveStatus"] = sActiveStatus.Trim();
        ViewState["ApproveGymOwnerId"] = lblgymOwnerId.Text;

    }

    public void ChangeApprovalStatus(string BranchId, string ApprovalStatus, string GymOwnerId)
    {
        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new MstrBranch()
                {
                    gymOwnerId = GymOwnerId,
                    branchId = BranchId,
                    approvalStatus = ApprovalStatus,
                    updatedBy = Session["userId"].ToString(),
                    cancellationReason = txtReason.Text == "" ? null : txtReason.Text
                };

                HttpResponseMessage response = client.PostAsJsonAsync("branch/updateApprovalStatus", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        clear();
                        BindBranchMaster();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }

    #endregion


    #region Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
   
    public void clear()
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        ddlOwnerList.Enabled = true;
        ddlOwnerList.ClearSelection();
        txtBranchName.Text = "";
        txtShortName.Text = "";
        txtAddress1.Text = "";
        txtAddress2.Text = "";
        txtPincode.Text = "";
        hfCity.Value = "";
        hfState.Value = "";
        hfDistrict.Value = "";
        txtLatitude.Text = "";
        txtLongitude.Text = "";
        txtMobileNumber.Text = "";
        txtsecondayMobilenumber.Text = "";
        txtemail.Text = "";
        txtGstNumber.Text = "";
        txtCity.Text = "";
        txtDistrict.Text = "";
        txtState.Text = "";
        divApprovalPopUp.Visible = false;
        divApprvCancelBtns.Visible = false;
        btnSubmit.Text = "Submit";
    }

    #endregion

    public class MstrBranch
    {
        public string queryType { get; set; }
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string branchName { get; set; }
        public string shortName { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string pincode { get; set; }
        public string primaryMobileNumber { get; set; }
        public string secondayMobilenumber { get; set; }
        public string emailId { get; set; }
        public string gstNumber { get; set; }
        public string activeStatus { get; set; }
        public string approvalStatus { get; set; }
        public string updatedBy { get; set; }
        public string createdBy { get; set; }
        public string cancellationReason { get; set; }
        public string fromtime { get; set; }
        public string totime { get; set; }

    }
  
}