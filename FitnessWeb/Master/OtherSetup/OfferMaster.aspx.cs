using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_OfferMaster : System.Web.UI.Page
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
            txtFromdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtTodate.Text = DateTime.Now.ToString("dd-MM-yyyy");

            Session["BaseUrl"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"].Trim();
            BindOffer();
        }
    }
    #endregion
    #region Add Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divGv.Visible = false;
        DivForm.Visible = true;
        AddOfferRules.Visible = false;
    }
    #endregion
    #region Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        OfferClear();
    }
    #endregion
    #region Submit 
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        if (btnSubmit.Text == "Submit")
        {

            InsertOffer();
        }
        else
        {
            UpdateOffer();
        }

    }
    #endregion
    #region Clear
    public void OfferClear()
    {
        
        rbtnOfferPeriod.ClearSelection();
        rbtnOfferValue.ClearSelection();
        txtTandc.Text = string.Empty;
        txtTodate.Text = string.Empty;
        txtofferValueFix.Text = string.Empty;
        txtOfferValue.Text = string.Empty;
        txtOfferRule.Text = string.Empty;
        txtOfferHead.Text = string.Empty;
        txtOfferCode.Text = string.Empty;
        txtOffDesc.Text = string.Empty;
        txtNooftimes.Text = string.Empty;
        txtMinAmt.Text = string.Empty;
        txtMaxAmt.Text = string.Empty;
        txtFromdate.Text = string.Empty;
        imgpreview.ImageUrl = "~/img/Defaultupload.png";
        DivForm.Visible = false;
        divGv.Visible = true;
        btnSubmit.Text = "Submit";
    }
    #endregion
    #region Bind Offer
    public void BindOffer()
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
                string Endpoint = "offer?gymOwnerId=" + Session["gymOwnerId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        gvOfferMstr.DataSource = dt;
                        gvOfferMstr.DataBind();
                        divGv.Visible = true;
                        DivForm.Visible = false;
                        AddOfferRules.Visible = false;
                    }
                    else
                    {
                        divGv.Visible = false;
                        DivForm.Visible = true;
                        AddOfferRules.Visible = false;
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
    #region Insert Function 
    public void InsertOffer()
    {
        try
        {
            string OfferValue = string.Empty;
            if(rbtnOfferValue.SelectedValue == "F")
            {
                OfferValue = txtofferValueFix.Text;
            }
            else
            {
                OfferValue = txtOfferValue.Text;
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
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Upload File');", true);
                return;
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new Offer()
                {
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    offerTypePeriod = rbtnOfferPeriod.SelectedValue,
                    offerHeading = txtOfferHead.Text,
                    offerDescription = txtOffDesc.Text,
                    offerCode = txtOfferCode.Text,
                    offerImageUrl = Response,
                    fromDate = txtFromdate.Text,
                    toDate = txtTodate.Text,
                    offerType = rbtnOfferValue.SelectedValue,
                    offerValue = OfferValue,
                    minAmt = txtMinAmt.Text,
                    maxAmt = txtMaxAmt.Text,
                    noOfTimesPerUser = txtNooftimes.Text,
                    termsAndConditions = txtTandc.Text,
                    createdBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("offer/insert", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindOffer();
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
                //OfferClear();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region Update Function 
    public void UpdateOffer()
    {
        try
        {
            string OfferValue = string.Empty;
            if (rbtnOfferValue.SelectedValue == "F")
            {
                OfferValue = txtofferValueFix.Text;
            }
            else
            {
                OfferValue = txtOfferValue.Text;
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
                Response = imgpreview.ImageUrl;
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Update = new Offer()
                {
                    offerId = ViewState["offerId"].ToString(),
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    offerTypePeriod = rbtnOfferPeriod.SelectedValue,
                    offerHeading = txtOfferHead.Text,
                    offerDescription = txtOffDesc.Text,
                    offerCode = txtOfferCode.Text,
                    offerImageUrl = Response,
                    fromDate = txtFromdate.Text,
                    toDate = txtTodate.Text,
                    offerType = rbtnOfferValue.SelectedValue,
                    offerValue = OfferValue,
                    minAmt = txtMinAmt.Text,
                    maxAmt = txtMaxAmt.Text,
                    noOfTimesPerUser = txtNooftimes.Text,
                    termsAndConditions = txtTandc.Text,
                    updatedBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("offer/update", Update).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindOffer();
                     
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
                OfferClear();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region Offer Value Set (Fix or Percentage)
    protected void rbtnOfferValue_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(rbtnOfferValue.SelectedValue == "F")
        {
            divOffPer.Visible = false;
            divofffix.Visible = true;
        }
        else
        {
            divOffPer.Visible = true;
            divofffix.Visible = false;
        }
    }

    protected void txtofferValueFix_TextChanged(object sender, EventArgs e)
    {
        txtMinAmt.Text = "";
        txtMaxAmt.Text = "";

    }

    #endregion
    #region Min Amount TextChange
    protected void txtMinAmt_TextChanged(object sender, EventArgs e)
    {
        txtMaxAmt.Text = "";
        if (rbtnOfferValue.SelectedValue == "F")
        {
            if (txtMinAmt.Text != "" & txtofferValueFix.Text != "")
            {
                if (Convert.ToInt32(txtMinAmt.Text) < Convert.ToInt32(txtofferValueFix.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('Must Enter Minimum Amount Greater than Offer Value');", true);

                    txtMinAmt.Text = "";
                    
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('Enter Offer Value And Minimum Amount');", true);
                
            }

        }

    }


    protected void txtMaxAmt_TextChanged(object sender, EventArgs e)
    {
        if (txtMinAmt.Text != "")
        {
            if ( Convert.ToInt32(txtMaxAmt.Text) < Convert.ToInt32(txtMinAmt.Text) )
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('Must Enter Max Amount Greater than Min Amount');", true);

                txtMaxAmt.Text = "";
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('Enter  Minimum Amount');", true);

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
            Label lblofferId = (Label)gvrow.FindControl("lblofferId");
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
                var activeOrInActive = new Offer()
                {
                    queryType = QueryType.Trim(),
                    offerId = lblofferId.Text.Trim(),
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("offer/activeOrInActive", activeOrInActive).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindOffer();
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
                OfferClear();
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
            Label lblofferId = (Label)gvrow.FindControl("lblofferId");
            Label lblofferTypePeriod = (Label)gvrow.FindControl("lblofferTypePeriod");
            Label lblofferHeading = (Label)gvrow.FindControl("lblofferHeading");
            Label lblofferDescription = (Label)gvrow.FindControl("lblofferDescription");
            Label lblofferCode = (Label)gvrow.FindControl("lblofferCode");
            Label lblofferImageUrl = (Label)gvrow.FindControl("lblofferImageUrl");
            Label lblfromDate = (Label)gvrow.FindControl("lblfromDate");
            Label lbltoDate = (Label)gvrow.FindControl("lbltoDate");
            Label lblofferType = (Label)gvrow.FindControl("lblofferType");
            Label lblofferValue = (Label)gvrow.FindControl("lblofferValue");
            Label lblminAmt = (Label)gvrow.FindControl("lblminAmt");
            Label lblmaxAmt = (Label)gvrow.FindControl("lblmaxAmt");
            Label lblnoOfTimesPerUser = (Label)gvrow.FindControl("lblnoOfTimesPerUser");
            Label lbltermsAndConditions = (Label)gvrow.FindControl("lbltermsAndConditions");
            ViewState["offerId"] = lblofferId.Text.Trim();
            rbtnOfferPeriod.SelectedValue = lblofferTypePeriod.Text.Trim();
            txtOfferHead.Text = lblofferHeading.Text.Trim();
            txtOffDesc.Text = lblofferDescription.Text.Trim();
            txtOfferCode.Text = lblofferCode.Text.Trim();
            txtFromdate.Text = lblfromDate.Text.Trim();
            //DateTime Fromdate = Convert.ToDateTime(lblfromDate.Text);
            //DateTime Todate = Convert.ToDateTime(lbltoDate.Text);
            //Convert.ToDateTime(lbltoDate.Text);
            //txtFromdate.Text = Fromdate.ToString("yyyy-MM-dd");
            //txtTodate.Text = Todate.ToString("yyyy-MM-dd");
            txtFromdate.Text = lblfromDate.Text;
            txtTodate.Text = lbltoDate.Text;
            rbtnOfferValue.SelectedValue = lblofferType.Text.Trim();
            if (rbtnOfferValue.SelectedValue == "F")
            {
                divOffPer.Visible = false;
                divofffix.Visible = true;
            }
            else
            {
                divOffPer.Visible = true;
                divofffix.Visible = false;
            }
            if (rbtnOfferValue.SelectedValue == "F" )
            {
                txtofferValueFix.Text = lblofferValue.Text.Trim();

            }
            else
            {
                txtOfferValue.Text = lblofferValue.Text.Trim();
            }
            imgpreview.ImageUrl = lblofferImageUrl.Text.Trim();
            txtMinAmt.Text = lblminAmt.Text;
            txtMaxAmt.Text = lblmaxAmt.Text;
            txtNooftimes.Text = lblnoOfTimesPerUser.Text;
            txtTandc.Text = lbltermsAndConditions.Text;
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
    #region Offer Class
    public class Offer
    {
        public string queryType { get; set; }
        public string offerId { get; set; }
        public string gymOwnerId { get; set; }
        public string offerTypePeriod { get; set; }
        public string offerHeading { get; set; }
        public string offerDescription { get; set; }
        public string offerCode { get; set; }
        public string offerImageUrl { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string offerType { get; set; }
        public string offerValue { get; set; }
        public string minAmt { get; set; }
        public string maxAmt { get; set; }
        public string noOfTimesPerUser { get; set; }
        public string termsAndConditions { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
    }
    #endregion

    //Offer Rules 

    #region Offer Rules 
    #region Add Rules Click
    protected void linkAddDetails_Click(object sender, EventArgs e)
    {
        AddOfferRules.Visible = true;
        LinkButton lnkbtn = sender as LinkButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        Label lblofferId = (Label)gvrow.FindControl("lblofferId");
        Label lblofferHeading = (Label)gvrow.FindControl("lblofferHeading");
        ViewState["offerId"] = lblofferId.Text.Trim();
        lblOffName.Text = lblofferHeading.Text.Trim();
        GetRuleType();
        BindOfferRule();
    }
    #endregion
    #region Cancel Click
    protected void btnSubCancel_Click(object sender, EventArgs e)
    {
        AddOfferRules.Visible = false;
        RulesClear();

    }
    #endregion
    #region Submit Click
    protected void btnSubSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubSubmit.Text == "Submit")
        {

            InsertOfferRules();
        }
        else
        {
            UpdateOfferRule();
        }
        
        
    }
    #endregion
    #region Bind Offer
    public void BindOfferRule()
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
                string Endpoint = "offerRule?offerId=" + ViewState["offerId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        gvOfferRule.DataSource = dt;
                        gvOfferRule.DataBind();
                       
                    }
                    else
                    {
                        gvOfferRule.DataSource = null;
                        gvOfferRule.DataBind();
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
    #region Clear
    public void RulesClear()
    {

        ddlRuleType.ClearSelection();
        txtOfferRule.Text = string.Empty;
        txtTodate.Text = string.Empty;
        btnSubSubmit.Text = "Submit";
    }
    #endregion
    #region Insert Function 
    public void InsertOfferRules()
    {
        try
        {
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
                var Insert = new OfferRule()
                {
                    offerId = ViewState["offerId"].ToString(),
                    offerRule = txtOfferRule.Text,
                    ruleType = ddlRuleType.SelectedValue,
                    createdBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("offerRule/insert", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindOfferRule();
                        RulesClear();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        //OfferClear();
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
    #region Update Function 
    public void UpdateOfferRule()
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
                var Update = new OfferRule()
                {
                    offerRuleId = ViewState["offerRuleId"].ToString(),
                    offerId = ViewState["offerId"].ToString(),
                    offerRule = txtOfferRule.Text,
                    ruleType = ddlRuleType.SelectedValue,
                    updatedBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("offerRule/update", Update).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindOfferRule();
                        RulesClear();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    else
                    {
                        RulesClear();
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
    #region Get Rule Type 
    public void GetRuleType()
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
                HttpResponseMessage response = client.GetAsync("configMaster/getDropDownDetails?typeId=25").Result;
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
                            ddlRuleType.DataSource = dt;
                            ddlRuleType.DataTextField = "configName";
                            ddlRuleType.DataValueField = "configId";
                            ddlRuleType.DataBind();
                        }
                        else
                        {
                            ddlRuleType.DataBind();

                        }
                        ddlRuleType.Items.Insert(0, new ListItem("Rule Type  *", "0"));
                    }
                    else
                    {
                        ddlRuleType.Items.Clear();
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
    #region Btn Edit Click Event
    protected void LnkEditRule_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton lnkbtn = sender as ImageButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            Label lblofferId = (Label)gvrow.FindControl("lblofferId");
            Label lblofferRuleId = (Label)gvrow.FindControl("lblofferRuleId");
            Label lblofferRule = (Label)gvrow.FindControl("lblofferRule");
            Label lblruleType = (Label)gvrow.FindControl("lblruleType");  
            Label lblofferHeading = (Label)gvrow.FindControl("lblofferHeading");  
            ViewState["offerRuleId"] = lblofferRuleId.Text.Trim();
            ViewState["offerId"] = lblofferId.Text.Trim();
            txtOfferRule.Text = lblofferRule.Text.Trim();
            lblOffName.Text = lblofferHeading.Text.Trim();
            ddlRuleType.SelectedValue = lblruleType.Text.Trim();
            btnSubSubmit.Text = "Update";
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region Active or Inactive  Click Event
    protected void lnkActiveOrInactiveRule_Click(object sender, EventArgs e)
    {

        try
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            Label lblofferRuleId = (Label)gvrow.FindControl("lblofferRuleId");
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
                var activeOrInActive = new OfferRule()
                {
                    queryType = QueryType.Trim(),
                    offerRuleId = lblofferRuleId.Text.Trim(),
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("offerRule/activeOrInActive", activeOrInActive).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindOfferRule();
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
    #region Offer Rule Class
    public class OfferRule
    {
        public string queryType { get; set; }
        public string offerId { get; set; }
        public string offerRuleId { get; set; }
        public string offerRule { get; set; }
        public string ruleType { get; set; }    
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
    }
    #endregion
    #endregion








}