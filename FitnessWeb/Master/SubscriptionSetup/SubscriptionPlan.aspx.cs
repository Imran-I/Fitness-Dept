using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_SubscriptionPlan : System.Web.UI.Page
{
    Helper helper = new Helper();
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null && Session["userRole"] == null)
        {
            Session.Clear();
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["LogoutUrl"].Trim(), true);
        }
        if (!IsPostBack)
        {
            GetTax();
            GetSubscriptionPlan();
        }
    }
    #endregion
    #region Get SubscriptionPlan
    public void GetSubscriptionPlan()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "subscriptionPlanMaster?gymOwnerId="
                             + Session["gymOwnerId"].ToString().Trim() + "&branchId="
                             + Session["branchId"].ToString().Trim() + "";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        gvSubscriptionPlan.DataSource = dt;
                        gvSubscriptionPlan.DataBind();
                        DivForm.Visible = false;
                        divGv.Visible = true;
                        btnCancel.Visible = true;
                    }
                    else
                    {
                        DivForm.Visible = true;
                        divGv.Visible = false;
                        btnCancel.Visible = false;
                    }
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion
    #region Ddl Tax
    protected void ddlTax_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtAmount.ReadOnly = false;
        if (ddlTax.SelectedValue != "0")
        {
            // txtNetAmount.ReadOnly = false;
            string[] Tp;
            string[] Percentage;
            Tp = ddlTax.SelectedItem.Text.Split(',');
            for (int i = 0; i < Tp.Count(); i++)
            {
                Percentage = Tp[i].Split('-');
                ViewState["TaxPercentage"] = Percentage[1];
            }
            ViewState["TaxCount"] = Tp.Count();


            if (txtAmount.Text != "0.00" && txtAmount.Text != "0")
            {
                GetNetAmount();
            }
            else
            {
                txtAmount.Text = "";
                txtprice.Text = "";
                txttaxAmount.Text = "";
            }
        }
    }
    #endregion
    #region Get Tax
    public void GetTax()
    {
        try
        {
            ddlTax.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "taxMaster/GetddlTax?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            ddlTax.DataSource = dt;
                            ddlTax.DataTextField = "taxDetails";
                            ddlTax.DataValueField = "taxId";
                            ddlTax.DataBind();
                        }
                        else
                        {
                            ddlTax.DataBind();
                        }                  
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlTax.Items.Insert(0, new ListItem("Tax  *", "0"));
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;

                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Errorresponse.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion
    #region Get NetAmount
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        if (ViewState["TaxPercentage"] != null && ViewState["TaxPercentage"].ToString() != "" && txtAmount.Text != "")
        {
            if (txtAmount.Text != "0.00" && txtAmount.Text != "0")
            {
                if (ddlTax.SelectedValue != "0")
                {
                    // txtNetAmount.ReadOnly = false;
                    string[] Tp;
                    string[] Percentage;
                    Tp = ddlTax.SelectedItem.Text.Split(',');
                    for (int i = 0; i < Tp.Count(); i++)
                    {
                        Percentage = Tp[i].Split('-');
                        ViewState["TaxPercentage"] = Percentage[1];
                    }
                    ViewState["TaxCount"] = Tp.Count();
                     GetNetAmount();
                    
                }
            }
            else
            {
                txtAmount.Text = "";
                txtprice.Text = "";
                txttaxAmount.Text = "";
            }
        }
    }
    public void GetNetAmount()
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
                string Endpoint = "fitnessCategoryPrice/GetTax?netAmount=" + txtAmount.Text + "" +
                   "&taxPercentage=" + ViewState["TaxPercentage"].ToString() + "";

                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            txtprice.Text = dt.Rows[0]["netAmount"].ToString();
                            txttaxAmount.Text = dt.Rows[0]["tax"].ToString();
                        }
                    }
                    else
                    {
                        ddlTax.Items.Clear();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;

                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Errorresponse.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion    
    #region Add Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Planclear();
        divGv.Visible = false;
        DivForm.Visible = true;
        AddBenefits.Visible = false;
    }
    #endregion
    #region Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        AddBenefits.Visible = false;
    }
    #endregion
    #region Btn Edit Click Event
    protected void LnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            AddBenefits.Visible = false;
            ImageButton lnkbtn = sender as ImageButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;

            Label lblsubscriptionPlanId = (Label)gvrow.FindControl("lblsubscriptionPlanId");
            Label lblpackageName = (Label)gvrow.FindControl("lblpackageName");
            Label lbldescription = (Label)gvrow.FindControl("lbldescription");
            Label lblimageUrl = (Label)gvrow.FindControl("lblimageUrl");
            Label lbltaxId = (Label)gvrow.FindControl("lbltaxId");
            Label lblnetAmount = (Label)gvrow.FindControl("lblnetAmount");
            Label lblamount = (Label)gvrow.FindControl("lblamount");
            Label lblnoOfDays = (Label)gvrow.FindControl("lblnoOfDays");
            Label lbltax = (Label)gvrow.FindControl("lbltax");
            Label lbltaxName = (Label)gvrow.FindControl("lbltaxName");
            Label lblcredits = (Label)gvrow.FindControl("lblcredits");
            Label lblcgstTax = (Label)gvrow.FindControl("lblcgstTax");
            Label lblsgstTax = (Label)gvrow.FindControl("lblsgstTax");
            Label lblisTrialAvailable = (Label)gvrow.FindControl("lblisTrialAvailable");
            Label lblnoOfTrialDays = (Label)gvrow.FindControl("lblnoOfTrialDays");

            txtDescription.Text = lbldescription.Text.Trim();
            txtPackageName.Text = lblpackageName.Text.Trim();
            txtNoOfDays.Text = lblnoOfDays.Text.Trim();
            txtAmount.Text = lblnetAmount.Text.Trim();
            txtCredits.Text = lblcredits.Text.Trim();
            txtNoOfDays.Text = lblnoOfDays.Text.Trim();
            txtNoofTrailDays.Text = lblnoOfTrialDays.Text.Trim();
            imgpreview.Src = lblimageUrl.Text.Trim();
            ddlTax.SelectedValue = lbltaxId.Text.Trim();
            txtprice.Text = lblamount.Text.Trim();
            txttaxAmount.Text = lbltax.Text.Trim();

            if (lblisTrialAvailable.Text.Trim() == "N")
            {
                RbtnlTrailAvail.SelectedValue = "No";
            }
            else
            {
                RbtnlTrailAvail.SelectedValue = "Yes";
            }
            if (RbtnlTrailAvail.SelectedValue == "Yes")
            {
                txtNoofTrailDays.Visible = true;
                lblNoofTrailDays.Visible = true;
            }
            else
            {
                txtNoofTrailDays.Visible = false;
                lblNoofTrailDays.Visible = false;
            }
            ViewState["subscriptionPlanId"] = lblsubscriptionPlanId.Text.Trim();
            btnPlanSubmit.Text = "Update";
            divGv.Visible = false;
            DivForm.Visible = true;

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
            Label lblsubscriptionPlanId = (Label)gvrow.FindControl("lblsubscriptionPlanId");

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
                var activeOrInActive = new subscriptionPlanActive()
                {
                    queryType = QueryType.Trim(),
                    subscriptionPlanId = lblsubscriptionPlanId.Text.Trim(),
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("subscriptionPlanMaster/activeOrInActive", activeOrInActive).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        GetSubscriptionPlan();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                    if (statusCode == 0)
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
    #region Plan PlanSubmit 
    protected void btnPlanSubmit_Click(object sender, EventArgs e)
    {
        if (btnPlanSubmit.Text == "Submit")
        {
            InsertPlan();
        }
        else
        {
            UpdatePlan();
        }
    }
    #endregion
    #region Insert Plan
    public void InsertPlan()
    {
        try
        {
            string TrailAvail;
            string NoofTrailDays;
            if (RbtnlTrailAvail.SelectedValue == "No")
            {
                TrailAvail = "N";
                NoofTrailDays = "0";
            }
            else
            {
                TrailAvail = "Y";
                NoofTrailDays = txtNoofTrailDays.Text;
            }


            int SCode;
            string Response;
            helper.UploadImage(fuimage, Session["BaseUrl"].ToString().Trim() + "UploadImage", out SCode, out Response);
            if (SCode == 0)
            {
                Response = null;
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new subscriptionPlan()
                {
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    packageName = txtPackageName.Text,
                    noOfDays = txtNoOfDays.Text,
                    description = txtDescription.Text,
                    imageUrl = Response,
                    tax = txttaxAmount.Text,
                    taxId = ddlTax.SelectedValue,
                    amount = txtprice.Text,
                    netAmount = txtAmount.Text,
                    credits = txtCredits.Text,
                    isTrialAvailable = TrailAvail,
                    noOfTrialDays = txtNoofTrailDays.Text,
                    createdBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("subscriptionPlanMaster/insert", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        Planclear();
                        GetSubscriptionPlan();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        divGv.Visible = true;
                        DivForm.Visible = false;
                    }
                    else
                    {
                        Planclear();
                        GetSubscriptionPlan();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        divGv.Visible = true;
                        DivForm.Visible = false;
                    }
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
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
    #region Update Plan
    public void UpdatePlan()
    {
        try
        {
            string TrailAvail;
            string NoofTrailDays;
            if (RbtnlTrailAvail.SelectedValue == "No")
            {
                TrailAvail = "N";
                NoofTrailDays = "0";
            }
            else
            {
                TrailAvail = "Y";
                NoofTrailDays = txtNoofTrailDays.Text;
            }

            int SCode;
            string Response;
            if (fuimage.HasFile)
            {

                helper.UploadImage(fuimage, Session["BaseUrl"].ToString().Trim() + "UploadImage", out SCode, out Response);
                if (SCode == 0)
                {
                    Response = null;
                }
            }
            else
            {
                Response = imgpreview.Src;
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new subscriptionPlan()
                {
                    subscriptionPlanId = ViewState["subscriptionPlanId"].ToString(),
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    packageName = txtPackageName.Text,
                    noOfDays = txtNoOfDays.Text,
                    description = txtDescription.Text,
                    imageUrl = Response,
                    tax = txttaxAmount.Text,
                    taxId = ddlTax.SelectedValue,
                    amount = txtprice.Text,
                    netAmount = txtAmount.Text,
                    credits = txtCredits.Text,
                    isTrialAvailable = TrailAvail,
                    noOfTrialDays = NoofTrailDays,
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("subscriptionPlanMaster/update", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        Planclear();
                        GetSubscriptionPlan();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        divGv.Visible = true;
                        DivForm.Visible = false;
                    }
                    else
                    {
                        Planclear();
                        GetSubscriptionPlan();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        divGv.Visible = true;
                        DivForm.Visible = false;
                    }
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
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
    #region Clear
    public void Planclear()
    {
        txtDescription.Text = "";
        txtNoofTrailDays.Text = "";
        txtPackageName.Text = "";
        txtAmount.Text = "";
        txtCredits.Text = "";
        txtNoOfDays.Text = "";
        imgpreview.Src = "~/img/Defaultupload.png";
        btnPlanSubmit.Text = "Submit";
        ddlTax.SelectedValue = "0";
        txtprice.Text = "";
        txttaxAmount.Text = "";
        RbtnlTrailAvail.SelectedValue = "Yes";
        txtNoofTrailDays.Visible = true;
        lblNoofTrailDays.Visible = true;
    }
    public void Benefiteclear()
    {
        txtsubDescription.Text = "";
        imgpreviewSub.Src = "~/img/Defaultupload.png";
        btnSubSubmit.Text = "Submit";
    }
    #endregion
    #region subscriptionPlan Insert & Update Classes
    public class subscriptionPlan
    {
        public string subscriptionPlanId { get; set; }
        public string packageName { get; set; }
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string noOfDays { get; set; }
        public string tax { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }
        public string taxId { get; set; }
        public string amount { get; set; }
        public string netAmount { get; set; }
        public string credits { get; set; }
        public string isTrialAvailable { get; set; }
        public string noOfTrialDays { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
    }
    public class subscriptionPlanActive
    {
        public string queryType { get; set; }
        public string subscriptionPlanId { get; set; }
        public string updatedBy { get; set; }
    }
    #endregion
    #region Get SubBenefit
    public void GetSubBenefit()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "subscriptionBenefits?subscriptionPlanId="
                             + ViewState["subscriptionPlanId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        gvSubBenefit.DataSource = dt;
                        gvSubBenefit.DataBind();
                        divBenefits.Visible = true;
                    }
                    else
                    {
                        divBenefits.Visible = false;
                    }
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion
    #region Add Benefits  Click Event
    protected void linkAddDetails_Click(object sender, EventArgs e)
    {

        txtsubDescription.Text = string.Empty;
        LinkButton lnkbtn = sender as LinkButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        Label lblsubscriptionPlanId = (Label)gvrow.FindControl("lblsubscriptionPlanId");
        Label lblpackageName = (Label)gvrow.FindControl("lblpackageName");
        BenefitplanName.InnerHtml = "Plan Name : " + lblpackageName.Text.Trim();
        ViewState["subscriptionPlanId"] = lblsubscriptionPlanId.Text.Trim();
        imgpreviewSub.Src = "~/img/Defaultupload.png";
        GetSubBenefit();
        AddBenefits.Visible = true;
        btnSubSubmit.Text = "Submit";
    }
    #endregion
    #region Benefits Cancel Click Event
    protected void btnSubCancel_Click(object sender, EventArgs e)
    {
        txtsubDescription.Text = string.Empty;
        AddBenefits.Visible = false;

    }
    #endregion
    #region Benefits  Submit Click Event
    protected void btnSubSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubSubmit.Text == "Submit")
        {
            InsertBenefit();
        }
        else
        {
            UpdateBenefit();
        }
    }
    #endregion
    #region SubBenefit Insert & Update Classes
    public class SubBenefit
    {
        public string uniqueId { get; set; }
        public string subscriptionPlanId { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
    }
    public class SubBenefitActive
    {
        public string queryType { get; set; }
        public string uniqueId { get; set; }
        public string updatedBy { get; set; }
    }
    #endregion
    #region Benefeits Edit  Click Event
    protected void LnkEditBenefeits_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkbtn = sender as ImageButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;

        Label lblSubBenefitId = (Label)gvrow.FindControl("lblSubBenefitId");
        Label lblsubscriptionPlanId = (Label)gvrow.FindControl("lblsubscriptionPlanId");
        Label lbldescription = (Label)gvrow.FindControl("lbldescription");
        Label lblimageUrl = (Label)gvrow.FindControl("lblimageUrl");

        txtsubDescription.Text = lbldescription.Text.Trim();
        imgpreviewSub.Src = lblimageUrl.Text.Trim();


        ViewState["SubBenefitId"] = lblSubBenefitId.Text.Trim();
        btnSubSubmit.Text = "Update";
    }
    #endregion
    #region Benefeits Active or Inactive  Click Event
    protected void lnkActiveOrInactiveBenefeits_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        Label lblSubBenefitId = (Label)gvrow.FindControl("lblSubBenefitId");

        LinkButton lblActiveStatus = (LinkButton)lnkbtn.FindControl("lnkActiveOrInactiveBenefeits");
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
            var activeOrInActive = new SubBenefitActive()
            {
                queryType = QueryType.Trim(),
                uniqueId = lblSubBenefitId.Text.Trim(),
                updatedBy = Session["userId"].ToString()
            };
            HttpResponseMessage response = client.PostAsJsonAsync("subscriptionBenefits/activeOrInActive", activeOrInActive).Result;
            if (response.IsSuccessStatusCode)
            {
                var Fitness = response.Content.ReadAsStringAsync().Result;
                int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                if (StatusCode == 1)
                {
                    GetSubBenefit();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                }
            }
            else
            {
                var Errorresponse = response.Content.ReadAsStringAsync().Result;
                int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                if (statusCode == 0)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                }

            }
        }
    }
    #endregion
    #region Insert Benefit
    public void InsertBenefit()
    {
        try
        {
            int SCode;
            string Response;
            helper.UploadImage(FileUpload1, Session["BaseUrl"].ToString().Trim() + "UploadImage", out SCode, out Response);
            if (SCode == 0)
            {
                Response = null;
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new SubBenefit()
                {
                    subscriptionPlanId = ViewState["subscriptionPlanId"].ToString(),
                    description = txtsubDescription.Text,
                    imageUrl = Response,
                    createdBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("subscriptionBenefits/insert", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        Benefiteclear();
                        GetSubBenefit();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        divGv.Visible = true;
                        DivForm.Visible = false;
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
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
    #region Update Benefit
    public void UpdateBenefit()
    {
        try
        {
            int SCode;
            string Response;
            if (FileUpload1.HasFile)
            {

                helper.UploadImage(FileUpload1, Session["BaseUrl"].ToString().Trim() + "UploadImage", out SCode, out Response);
                if (SCode == 0)
                {
                    Response = null;
                }
            }
            else
            {
                Response = imgpreviewSub.Src;
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new SubBenefit()
                {
                    uniqueId = ViewState["SubBenefitId"].ToString(),
                    subscriptionPlanId = ViewState["subscriptionPlanId"].ToString(),
                    description = txtsubDescription.Text,
                    imageUrl = Response,
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("subscriptionBenefits/update", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        Benefiteclear();
                        GetSubBenefit();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        divGv.Visible = true;
                        DivForm.Visible = false;
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
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
    #region Selected Index Changed
    protected void RbtnlTrailAvail_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RbtnlTrailAvail.SelectedValue == "Yes")
        {
            txtNoofTrailDays.Visible = true;
            lblNoofTrailDays.Visible = true;
        }
        else
        {
            txtNoofTrailDays.Visible = false;
            lblNoofTrailDays.Visible = false;
        }
    }
    #endregion
}