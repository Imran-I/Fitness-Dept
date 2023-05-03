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
public partial class Master_ManageLeads : System.Web.UI.Page
{
    Helper helper = new Helper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null && Session["userRole"] == null)
        {
            Session.Clear();
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["LogoutUrl"].Trim(), true);
        }
        if (!IsPostBack)
        {
            BindFollowUpStatus();
            BindFollowUpMode();
            BindMngLeads();
        }

    }

    #region Bind Grid Manage leads  
    public void BindMngLeads()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "user/GetaAdminUser?gymOwnerId=" + Session["gymOwnerId"].ToString() + "&branchId=" + Session["branchId"].ToString() + "";
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
                            gvMngLeads.DataSource = dt;
                            gvMngLeads.DataBind();
                            divGridView.Visible = true;
                        }
                        else
                        {
                            gvMngLeads.DataSource = null;
                            gvMngLeads.DataBind();
                            divGv.Visible = false;
                            DivForm.Visible = true;
                        }

                    }
                    else
                    {
                        gvMngLeads.DataSource = null;
                        gvMngLeads.DataBind();
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

    #region Add Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        BindFollowUpStatus();
        BindFollowUpMode();



        divGv.Visible = false;
        DivForm.Visible = true;
    }
    #endregion

    #region Bind FollowUp Type
    public void BindFollowUpStatus()
    {
        try

        {
            ddlFollowUpStatus.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sUrl = Session["BaseUrl"].ToString().Trim() + "configMaster/getDropDownDetails?typeId=24";
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
                            ddlFollowUpStatus.DataSource = dt;
                            ddlFollowUpStatus.DataTextField = "configName";
                            ddlFollowUpStatus.DataValueField = "configId";
                            ddlFollowUpStatus.DataBind();
                        }
                        else
                        {
                            ddlFollowUpStatus.DataBind();
                        }                      
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlFollowUpStatus.Items.Insert(0, new ListItem("FollowUp Status *", "0"));
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

    #region Bind FollowUp Mode
    public void BindFollowUpMode()
    {
        try

        {
            ddlFollowUpMode.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sUrl = Session["BaseUrl"].ToString().Trim() + "configMaster/getDropDownDetails?typeId=23";
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
                            ddlFollowUpMode.DataSource = dt;
                            ddlFollowUpMode.DataTextField = "configName";
                            ddlFollowUpMode.DataValueField = "configId";
                            ddlFollowUpMode.DataBind();
                        }
                        else
                        {
                            ddlFollowUpMode.DataBind();
                        }   
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlFollowUpMode.Items.Insert(0, new ListItem(" FollowUp Mode *", "0"));
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
 
    #region Btnsubmit Click Event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubmit.Text == "Submit")
        {
            InsertEmp();
        }
        else
        {
            UpdateEmp();
        }
    }
    #endregion

    #region Insert Employee
    public void InsertEmp()
    {
        int StatusCodes;
        string ImageUrl;
        if (Fuimage.HasFile)
        {
            helper.UploadImage(Fuimage, Session["BaseUrl"].ToString().Trim() + "UploadImage", out StatusCodes, out ImageUrl);
        }
        else
        {
            ImageUrl = "";
        }

        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new MstrManageLeads()
                {
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    firstName = txtFirstName.Text,
                    lastName = txtLastName.Text,
                    dob = txtDOB.Text,
                    gender = ddlGender.SelectedValue,
                    maritalStatus = ddlMaritalStatus.SelectedValue,
                    mobileNo = txtMobileNo.Text,
                    mailId = txtEmailId.Text,

                    addressLine1 = txtAddress1.Text,
                    addressLine2 = txtAddress2.Text,
                    zipcode = txtPincode.Text,
                    city = hfCity.Value,
                    state = hfState.Value,
                    district = hfDistrict.Value,
                    rewardPoints = 0,
                    rewardUtilized = 0,
                    promoNotification = "Y",

                    followUpStatus = ddlFollowUpStatus.SelectedValue,
                    followUpMode = ddlFollowUpMode.SelectedValue,
                    enquirydate = txtEnquiryDate.Text,
                    enquiryReason = txtEnquiryReason.Text,
                    photoLink = ImageUrl.Trim(),

                    createdBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("user/InsertAdminUser", Insert).Result;

                var Fitness = response.Content.ReadAsStringAsync().Result;
                int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                if (response.IsSuccessStatusCode)
                {
                    if (StatusCode == 1)
                    {
                        clear();
                        BindMngLeads();
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

    #region Update Employee
    public void UpdateEmp()
    {
        int StatusCodes;
        string ImageUrl;
        if (Fuimage.HasFile)
        {
            helper.UploadImage(Fuimage, Session["BaseUrl"].ToString().Trim() + "UploadImage", out StatusCodes, out ImageUrl);
        }
        else
        {
            ImageUrl = imgEmpPhotoPrev.ImageUrl;
        }
        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var update = new MstrManageLeads()
                {
                    userId = ViewState["userId"].ToString(),
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    firstName = txtFirstName.Text,
                    lastName = txtLastName.Text,
                    dob = txtDOB.Text,
                    gender = ddlGender.SelectedValue,
                    maritalStatus = ddlMaritalStatus.SelectedValue,
                    mobileNo = txtMobileNo.Text,
                    mailId = txtEmailId.Text,

                    addressLine1 = txtAddress1.Text,
                    addressLine2 = txtAddress2.Text,
                    zipcode = txtPincode.Text,
                    city = hfCity.Value,
                    district = hfDistrict.Value,
                    state = hfState.Value,
                    rewardPoints = 0,
                    rewardUtilized = 0,
                    promoNotification = "Y",

                    followUpStatus = ddlFollowUpStatus.SelectedValue,
                    followUpMode = ddlFollowUpMode.SelectedValue,
                    enquirydate = txtEnquiryDate.Text,
                    enquiryReason = txtEnquiryReason.Text,
                    photoLink = ImageUrl.Trim(),

                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("user/updateAdminUser", update).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        clear();
                        BindMngLeads();
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

    #region Btn Edit Click Event
    protected void LnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton lnkbtn = sender as ImageButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;

            Label lbluserId = (Label)gvrow.FindControl("lbluserId");
            ViewState["userId"] = lbluserId.Text;
            Label lblfirstName = (Label)gvrow.FindControl("lblfirstName");
            txtFirstName.Text = lblfirstName.Text;
            Label lbllastName = (Label)gvrow.FindControl("lbllastName");
            txtLastName.Text = lbllastName.Text;

            Label lbldob = (Label)gvrow.FindControl("lbldob");
            //DateTime DOB = Convert.ToDateTime(lbldob.Text);
            //txtDOB.Text = DOB.ToString("yyyy-MM-dd");
            txtDOB.Text = lbldob.Text;
            Label lblgender = (Label)gvrow.FindControl("lblgender");
            ddlGender.SelectedValue = lblgender.Text;
            Label lblmaritalStatus = (Label)gvrow.FindControl("lblmaritalStatus");
            if(lblmaritalStatus.Text.Trim() != "")
            {
                ddlMaritalStatus.SelectedValue = lblmaritalStatus.Text;
            }
            

            Label lblmobileNo = (Label)gvrow.FindControl("lblmobileNo");
            txtMobileNo.Text = lblmobileNo.Text;
            Label lblmailId = (Label)gvrow.FindControl("lblmailId");
            txtEmailId.Text = lblmailId.Text;

            Label lbladdressLine1 = (Label)gvrow.FindControl("lbladdressLine1");
            txtAddress1.Text = lbladdressLine1.Text;
            Label lbladdressLine2 = (Label)gvrow.FindControl("lbladdressLine2");
            txtAddress2.Text = lbladdressLine2.Text;
            Label lblzipcode = (Label)gvrow.FindControl("lblzipcode");
            txtPincode.Text = lblzipcode.Text;

            Label lblcity = (Label)gvrow.FindControl("lblcity");
            txtCity.Text = lblcity.Text;
            Label lbldistrict = (Label)gvrow.FindControl("lbldistrict");
            txtDistrict.Text = lbldistrict.Text;
            Label lblstate = (Label)gvrow.FindControl("lblstate");
            txtState.Text = lblstate.Text;

            BindFollowUpStatus();
            Label lblfollowUpStatus = (Label)gvrow.FindControl("lblfollowUpStatus");
            ddlFollowUpStatus.SelectedValue = lblfollowUpStatus.Text;

            BindFollowUpMode();
            Label lblfollowUpMode = (Label)gvrow.FindControl("lblfollowUpMode");
            ddlFollowUpMode.SelectedValue = lblfollowUpMode.Text;

            Label lblenquirydate = (Label)gvrow.FindControl("lblenquirydate");
            //DateTime EnquiryDate = Convert.ToDateTime(lblenquirydate.Text);
            //txtEnquiryDate.Text = EnquiryDate.ToString("yyyy-MM-dd");
            txtEnquiryDate.Text = lblenquirydate.Text;
            Label lblenquiryReason = (Label)gvrow.FindControl("lblenquiryReason");
            txtEnquiryReason.Text = lblenquiryReason.Text;

            Label lblphotoLink = (Label)gvrow.FindControl("lblphotoLink");
            imgEmpPhotoPrev.ImageUrl = lblphotoLink.Text.Trim();

            if (imgEmpPhotoPrev.ImageUrl == "")
            {
                imgEmpPhotoPrev.ImageUrl = "~/img/User.png";
            }
            hfCity.Value = lblcity.Text;
            hfState.Value = lblstate.Text;
            hfDistrict.Value = lbldistrict.Text;
            divGv.Visible = false;
            DivForm.Visible = true;
            btnSubmit.Text = "Update";
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
            Label lbluserId = (Label)gvrow.FindControl("lbluserId");

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
                var activeOrInActive = new MstrManageLeads()
                {
                    queryType = QueryType.Trim(),
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("employee/activeOrInActive", activeOrInActive).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {

                        clear();
                        BindMngLeads();
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
        btnSubmit.Text = "Submit";
        ViewState["userId"] = "";

        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtDOB.Text = "";
        ddlGender.ClearSelection();
        ddlMaritalStatus.ClearSelection();
        txtMobileNo.Text = "";
        txtEmailId.Text = "";

        txtAddress1.Text = "";
        txtAddress2.Text = "";
        txtPincode.Text = "";
        txtCity.Text = "";
        txtDistrict.Text = "";
        txtState.Text = "";
        hfCity.Value = "";
        hfState.Value = "";
        hfDistrict.Value = "";

        ddlFollowUpStatus.ClearSelection();
        ddlFollowUpMode.ClearSelection();
        txtEnquiryDate.Text = "";
        txtEnquiryReason.Text = "";
    }
    #endregion
    public class MstrManageLeads
    {
        public string queryType { get; set; }
        public string userId { get; set; }
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string zipcode { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string maritalStatus { get; set; }
        public string dob { get; set; }
        public string mobileNo { get; set; }
        public string mailId { get; set; }
        public string photoLink { get; set; }
        public string activeStatus { get; set; }
        public int rewardPoints { get; set; }
        public int rewardUtilized { get; set; }
        public string promoNotification { get; set; }
        public string enquiryReason { get; set; }
        public string enquirydate { get; set; }
        public string followUpMode { get; set; }
        public string followUpModeName { get; set; }
        public string followUpStatus { get; set; }
        public string followUpStatusName { get; set; }
        public string updatedBy { get; set; }
        public string createdBy { get; set; }
    }

}