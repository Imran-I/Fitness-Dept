using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_FitnessCategoryPrice : System.Web.UI.Page
{
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
            GetTrainingType();
            GetCategory();
            GetPlanDuration();
            GetTax();
            GetOfferDetails();
            txtFromdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtTodate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtFromdateGV.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtTodateGV.Text = DateTime.Now.ToString("dd-MM-yyyy");
            BindCategoryPrice();

        }
    }
    #endregion
    #region Add Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divGv.Visible = false;
        DivForm.Visible = true;
    }
    #endregion
    #region Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        ClearCategoryPrice();
    }
    #endregion
    #region Submit 
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        if (btnSubmit.Text == "Submit")
        {

            InsertCategoryPrice();
        }
        else
        {
            UpdateCategoryPrice();
        }

    }
    #endregion  
    #region Clear
    public void ClearCategoryPrice()
    {
        txtFromdate.Text = string.Empty;
        txtTodate.Text = string.Empty;
        ddlCategoryList.Enabled = true;
        ddlCategoryList.ClearSelection();
        ddltrainingType.ClearSelection();
        ddlTax.ClearSelection();
        ddlPlanDuration.ClearSelection();
        ddltrainingType.Enabled = true;
        // ddlOfferGv.ClearSelection();
        ddlOffer.ClearSelection();
        txttax.Text = "";
        txtPrice.Text = "";
        txtNoOfCycles.Text = "";
        txtNetAmount.Text = "";
        txtactutalAmount.Text = "";
        txtdisplayAmount.Text = "";
        RbtnlcyclePaymentsAllowed.SelectedValue = "N";
        RbtnlPriceMode.SelectedValue = "D";
        //RbtnlcyclePaymentsAllowed.ClearSelection();
        //RbtnlPriceMode.ClearSelection();
        DivForm.Visible = false;
        divGv.Visible = true;
        ViewState["TaxPercentage"] = null;
        btnSubmit.Text = "Submit";
    }
    #endregion
    #region Get Category
    public void GetCategory()
    {
        try
        {
            ddlCategoryList.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "categoryMaster/GetDropDownDetails?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
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
                            ddlCategoryList.DataSource = dt;
                            ddlCategoryList.DataTextField = "categoryName";
                            ddlCategoryList.DataValueField = "categoryId";
                            ddlCategoryList.DataBind();
                        }
                        else
                        {
                            ddlCategoryList.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlCategoryList.Items.Insert(0, new ListItem("Category List *", "0"));
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
    #region Get TrainingType
    public void GetTrainingType()
    {
        try
        {
            ddltrainingType.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "trainingTypeMaster/getDropDownDeatils?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
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
                            ddltrainingType.DataSource = dt;
                            ddltrainingType.DataTextField = "trainingTypeName";
                            ddltrainingType.DataValueField = "trainingTypeId";
                            ddltrainingType.DataBind();
                        }
                        else
                        {
                            ddltrainingType.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddltrainingType.Items.Insert(0, new ListItem("Training Type *", "0"));
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
    #region Get Plan Duration
    public void GetPlanDuration()
    {
        try
        {
            ddlPlanDuration.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "configMaster/getDropDownDetails?typeId=13";
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
                            ddlPlanDuration.DataSource = dt;
                            ddlPlanDuration.DataTextField = "configName";
                            ddlPlanDuration.DataValueField = "configId";
                            ddlPlanDuration.DataBind();
                        }
                        else
                        {
                            ddlPlanDuration.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlPlanDuration.Items.Insert(0, new ListItem("Plan Duration  *", "0"));
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
    #region Get Tax Percentage 
    protected void ddlTax_SelectedIndexChanged(object sender, EventArgs e)
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


            if (txtNetAmount.Text != "" && txtNetAmount.Text != "0.00" && txtNetAmount.Text != "0")
            {
                GetNetAmount();
            }
            else
            {
                txtNetAmount.Text = "";
                txtPrice.Text = "";
                txttax.Text = "";
            }
        }
    }
    #endregion
    #region Get OfferDetails
    public void GetOfferDetails()
    {
        try
        {
            ddlOffer.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string sUrl = Session["BaseUrl"].ToString().Trim() + "offerMapping/GetMstrOfferMappingUser?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                     "&branchId=" + Session["branchId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;
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
                            ddlOffer.DataSource = dt;
                            ddlOffer.DataTextField = "OfferValueType";
                            ddlOffer.DataValueField = "offerId";
                            ddlOffer.DataBind();
                            ddlOfferGv.DataSource = dt;
                            ddlOfferGv.DataTextField = "OfferValueType";
                            ddlOfferGv.DataValueField = "offerId";
                            ddlOfferGv.DataBind();

                            Session["offer"] = dt;
                        }
                        else
                        {
                            ddlOffer.DataBind();
                            ddlOfferGv.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlOffer.Items.Insert(0, new ListItem("Offer *", "0"));
                    ddlOfferGv.Items.Insert(0, new ListItem("Offer *", "0"));
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
    protected void txtNetAmount_TextChanged(object sender, EventArgs e)
    {
        if (ViewState["TaxPercentage"] != null && ViewState["TaxPercentage"].ToString() != "" && txtNetAmount.Text != "")
        {
            if (txtNetAmount.Text != "0.00" && txtNetAmount.Text != "0")
            {
                GetNetAmount();
            }
            else
            {
                txtNetAmount.Text = "";
                txtPrice.Text = "";
                txttax.Text = "";
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
                string Endpoint = "fitnessCategoryPrice/GetTax?netAmount=" + txtNetAmount.Text + "" +
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
                            txtPrice.Text = dt.Rows[0]["netAmount"].ToString();
                            txttax.Text = dt.Rows[0]["tax"].ToString();
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
    #region No of Cycle Visible
    protected void RbtnlcyclePaymentsAllowed_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RbtnlcyclePaymentsAllowed.SelectedValue == "Y")
        {
            divNoofCycle.Visible = true;
        }
        else
        {
            divNoofCycle.Visible = false;

        }
    }
    #endregion
    #region Bind Category Price
    public void BindCategoryPrice()
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
                string Endpoint = "fitnessCategoryPrice?queryType=GetMstrPrice&gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                  "&branchId=" + Session["branchId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                btnCancel.Visible = false;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        gvCategoryPrice.DataSource = dt;
                        gvCategoryPrice.DataBind();
                        divGv.Visible = true;
                        DivForm.Visible = false;
                        btnCancel.Visible = true;
                        btnCancel.Visible = true;
                    }
                    else
                    {
                        divGv.Visible = false;
                        DivForm.Visible = true;
                        btnCancel.Visible = false;
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
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
    public void InsertCategoryPrice()
    {
        try
        {
            //if(txtNoOfCycles.Text == "")
            //{
            //    txtNoOfCycles.Text = "0";
            //}
            if (RbtnlcyclePaymentsAllowed.SelectedValue == "N")
            {
                txtNoOfCycles.Text = "0";
            }
           
            string offerName = string.Empty;
            string[] offerNames;
     
         
                offerNames = ddlOffer.SelectedItem.Text.Split('~');
                offerName = offerNames[0];
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new CategoryPrice()
                {
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    categoryId = ddlCategoryList.SelectedValue,
                    trainingTypeId = ddltrainingType.SelectedValue,
                    planDuration = ddlPlanDuration.SelectedValue,
                    trainingMode = RbtnlPriceMode.SelectedValue,
                    price = txtPrice.Text,
                    taxId = ddlTax.SelectedValue,
                    tax = txttax.Text,
                    netAmount = txtNetAmount.Text,
                    actualAmount = txtactutalAmount.Text,
                    displayAmount = txtdisplayAmount.Text,
                    cyclePaymentsAllowed = RbtnlcyclePaymentsAllowed.SelectedValue,
                    maxNoOfCycles = txtNoOfCycles.Text,
                    offerId = ddlOffer.SelectedValue,
                    offerName = offerName,
                    fromDate=txtFromdate.Text,
                    toDate=txtTodate.Text,
                    createdBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("fitnessCategoryPrice/insert", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindCategoryPrice();
                        ClearCategoryPrice();
                        ddlOfferGv.ClearSelection();
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
    #region Update Function 
    public void UpdateCategoryPrice()
    {
        try
        {

            string NoOfCycles;
            if (RbtnlcyclePaymentsAllowed.SelectedValue == "N")
            {
                NoOfCycles = "0";
            }
            else
            {
                NoOfCycles = txtNoOfCycles.Text;
            }
            string offerid = string.Empty;
            string offerName = string.Empty;
            string[] offerNames;
            string fromdate = string.Empty;
            string todate = string.Empty;
            if (ddlOffer.SelectedIndex != 0)
            {
                fromdate = txtFromdate.Text;
                todate = txtTodate.Text;
                offerid = ddlOffer.SelectedValue;
                offerNames = ddlOffer.SelectedItem.Text.Split('~');
                offerName = offerNames[0];
            }
            else
            {
                fromdate = txtFromdateGV.Text;
                todate = txtTodateGV.Text;
                offerid = ddlOfferGv.SelectedValue;
                offerNames = ddlOfferGv.SelectedItem.Text.Split('~');
                offerName = offerNames[0];
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new CategoryPrice()
                {
                    priceId = ViewState["priceId"].ToString(),
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    categoryId = ddlCategoryList.SelectedValue,
                    trainingTypeId = ddltrainingType.SelectedValue,
                    planDuration = ddlPlanDuration.SelectedValue,
                    trainingMode = RbtnlPriceMode.SelectedValue,
                    price = txtPrice.Text,
                    taxId = ddlTax.SelectedValue,
                    tax = txttax.Text,
                    netAmount = txtNetAmount.Text,
                    actualAmount = txtactutalAmount.Text,
                    displayAmount = txtdisplayAmount.Text,
                    cyclePaymentsAllowed = RbtnlcyclePaymentsAllowed.SelectedValue,
                    maxNoOfCycles = NoOfCycles,
                    offerId = offerid,
                    offerName = offerName,
                    fromDate=fromdate,
                    toDate=todate,
                    updatedBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("fitnessCategoryPrice/update", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindCategoryPrice();
                        ClearCategoryPrice();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    else
                    {
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
    #region Btn Edit Click Event
    protected void LnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton lnkbtn = sender as ImageButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            Label lblpriceId = (Label)gvrow.FindControl("lblpriceId");
            Label lblcategoryId = (Label)gvrow.FindControl("lblcategoryId");
            Label lbltrainingTypeId = (Label)gvrow.FindControl("lbltrainingTypeId");
            Label lbltrainingMode = (Label)gvrow.FindControl("lbltrainingMode");
            Label lblplanDuration = (Label)gvrow.FindControl("lblplanDuration");
            Label lblprice = (Label)gvrow.FindControl("lblprice");
            Label lbltax = (Label)gvrow.FindControl("lbltax");
            Label lbltaxId = (Label)gvrow.FindControl("lbltaxId");
            Label lblnetAmount = (Label)gvrow.FindControl("lblnetAmount");
            Label lblactualAmount = (Label)gvrow.FindControl("lblactualAmount");
            Label lbldisplayAmount = (Label)gvrow.FindControl("lbldisplayAmount");
            Label lblcyclePaymentsAllowed = (Label)gvrow.FindControl("lblcyclePaymentsAllowed");
            Label lblmaxNoOfCycles = (Label)gvrow.FindControl("lblmaxNoOfCycles");
            Label lblofferId = (Label)gvrow.FindControl("lblofferId");
            Label lblofferName = (Label)gvrow.FindControl("lblofferName");
            Label lblfromDate = (Label)gvrow.FindControl("lblfromDate");
            Label lbltoDate = (Label)gvrow.FindControl("lbltoDate");
            ViewState["priceId"] = lblpriceId.Text.Trim();
            GetCategory();
            ddlCategoryList.SelectedValue = lblcategoryId.Text;
            ddlCategoryList.Enabled = false;
            GetTrainingType();
            ddltrainingType.SelectedValue = lbltrainingTypeId.Text;
            GetPlanDuration();
            ddlPlanDuration.SelectedValue = lblplanDuration.Text;
            RbtnlPriceMode.SelectedValue = lbltrainingMode.Text;
            txtPrice.Text = lblprice.Text;
            GetTax();
            ddlTax.SelectedValue = lbltaxId.Text;
            txttax.Text = lbltax.Text;
            txtNetAmount.Text = lblnetAmount.Text;
            txtactutalAmount.Text = lblactualAmount.Text;
            txtdisplayAmount.Text = lbldisplayAmount.Text;
            RbtnlcyclePaymentsAllowed.SelectedValue = lblcyclePaymentsAllowed.Text;
            if (lblcyclePaymentsAllowed.Text == "Y")
            {
                divNoofCycle.Visible = true;
                txtNoOfCycles.Text = lblmaxNoOfCycles.Text;
            }
            else
            {
                divNoofCycle.Visible = false;
                txtNoOfCycles.Text = lblmaxNoOfCycles.Text;
            }
            if (lblofferId.Text != "")
            {
                GetOfferDetails();
                ddlOffer.SelectedValue = lblofferId.Text;

            }
            txtFromdate.Text = lblfromDate.Text;
            txtTodate.Text = lbltoDate.Text;

            // txtNetAmount.ReadOnly = false;
            string[] Tp;
            string[] Percentage;
            Tp = ddlTax.SelectedItem.Text.Split('-');
            Percentage = Tp[1].Split(',');
            ViewState["TaxPercentage"] = Percentage[0];
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
            Label lblpriceId = (Label)gvrow.FindControl("lblpriceId");
            Label lblgymOwnerId = (Label)gvrow.FindControl("lblgymOwnerId");
            Label lblbranchId = (Label)gvrow.FindControl("lblbranchId");
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
                var activeOrInActive = new CategoryPrice()
                {
                    queryType = QueryType.Trim(),
                    priceId = lblpriceId.Text.Trim(),
                    gymOwnerId = lblgymOwnerId.Text.Trim(),
                    branchId = lblbranchId.Text.Trim(),
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("fitnessCategoryPrice/activeOrInActive", activeOrInActive).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindCategoryPrice();
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

    protected void RbtnlPriceMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetTrainingType();
        if (RbtnlPriceMode.SelectedValue == "O")
        {
            ddltrainingType.SelectedIndex = ddltrainingType.Items.IndexOf(ddltrainingType.Items.FindByText("Online"));
            ddltrainingType.Enabled = false;
        }
        else
        {

            ddltrainingType.Items.Remove(ddltrainingType.Items.FindByText("Online"));
            ddltrainingType.Enabled = true;
        }
    }
    #region Offer selected indexx changed 
    protected void ddlOffer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOffer.SelectedIndex != 0)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["offer"];
            var offerdetails = dt.AsEnumerable().Where(x => x.Field<Int64>("offerId") == Convert.ToInt64(ddlOffer.SelectedValue)).CopyToDataTable();
            decimal actual = Convert.ToDecimal(txtactutalAmount.Text);
            decimal display;
            decimal offervalue;
            offervalue = Convert.ToDecimal(offerdetails.Rows[0]["offerValue"].ToString());
            if (offerdetails.Rows[0]["offerType"].ToString() == "F")
            {
                display = actual - offervalue - 1;
            }
            else
            {
                display = (actual * offervalue) / 100 - 1;
            }

            txtdisplayAmount.Text = display.ToString();
            txtNetAmount.Text = display.ToString();
            if (ViewState["TaxPercentage"] != null && ViewState["TaxPercentage"].ToString() != "" && txtNetAmount.Text != "")
            {
                if (txtNetAmount.Text != "0.00" && txtNetAmount.Text != "0")
                {
                    GetNetAmount();
                }
                else
                {
                    txtNetAmount.Text = "";
                    txtPrice.Text = "";
                    txttax.Text = "";
                }
            }
        }
        else
        {
            txtdisplayAmount.Text = "";
            txtNetAmount.Text = "";
        }

    }
    #endregion
    #region btn Submit Event Gridview
    protected void btnSubGV_Click(object sender, EventArgs e)
    {
        if(txtFromdate.Text != "" && txtTodate.Text!= "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Enter FromDate And ToDate');", true);
        }

        if (ddlOfferGv.SelectedIndex != 0)
        {
            int select = 0;

            foreach (GridViewRow item in gvCategoryPrice.Rows)
            {
                CheckBox chkItem = item.FindControl("chkItem") as CheckBox;
                Label lblactiveStatusgv = (Label)item.FindControl("lblactiveStatusgv");
                if (lblactiveStatusgv.Text == "A")
                {
                    if (chkItem.Checked == true)
                    {
                        select = 1;
                        Label lblpriceId = (Label)item.FindControl("lblpriceId");
                        Label lblcategoryId = (Label)item.FindControl("lblcategoryId");
                        Label lbltrainingTypeId = (Label)item.FindControl("lbltrainingTypeId");
                        Label lbltrainingMode = (Label)item.FindControl("lbltrainingMode");
                        Label lblplanDuration = (Label)item.FindControl("lblplanDuration");
                        Label lblprice = (Label)item.FindControl("lblprice");
                        Label lbltax = (Label)item.FindControl("lbltax");
                        Label lbltaxId = (Label)item.FindControl("lbltaxId");
                        Label lblnetAmount = (Label)item.FindControl("lblnetAmount");
                        Label lblactualAmount = (Label)item.FindControl("lblactualAmount");
                        Label lbldisplayAmount = (Label)item.FindControl("lbldisplayAmount");
                        Label lblcyclePaymentsAllowed = (Label)item.FindControl("lblcyclePaymentsAllowed");
                        Label lblmaxNoOfCycles = (Label)item.FindControl("lblmaxNoOfCycles");

                        ViewState["priceId"] = lblpriceId.Text.Trim();
                        GetCategory();
                        ddlCategoryList.SelectedValue = lblcategoryId.Text;
                        GetTrainingType();
                        ddltrainingType.SelectedValue = lbltrainingTypeId.Text;
                        GetPlanDuration();
                        ddlPlanDuration.SelectedValue = lblplanDuration.Text;
                        RbtnlPriceMode.SelectedValue = lbltrainingMode.Text;
                        txtPrice.Text = lblprice.Text;
                        GetTax();
                        ddlTax.SelectedValue = lbltaxId.Text;
                        txttax.Text = lbltax.Text;
                        txtactutalAmount.Text = lblactualAmount.Text;
                        DataTable dt = new DataTable();
                        dt = (DataTable)Session["offer"];
                        var offerdetails = dt.AsEnumerable().Where(x => x.Field<Int64>("offerId") == Convert.ToInt64(ddlOfferGv.SelectedValue)).CopyToDataTable();
                        decimal actual = Convert.ToDecimal(txtactutalAmount.Text);
                        decimal display;
                        decimal offervalue;
                        offervalue = Convert.ToDecimal(offerdetails.Rows[0]["offerValue"].ToString());
                        if (offerdetails.Rows[0]["offerType"].ToString() == "F")
                        {
                            display = actual - offervalue - 1;
                        }
                        else
                        {
                            display = (actual * offervalue) / 100 - 1;
                        }

                        txtdisplayAmount.Text = display.ToString();
                        txtNetAmount.Text = display.ToString();

                        RbtnlcyclePaymentsAllowed.SelectedValue = lblcyclePaymentsAllowed.Text;
                        txtNoOfCycles.Text = lblmaxNoOfCycles.Text;
                        txtNoOfCycles.Text = lblmaxNoOfCycles.Text;
                        string[] Tp;
                        string[] Percentage;
                        Tp = ddlTax.SelectedItem.Text.Split('-');
                        Percentage = Tp[1].Split(',');
                        ViewState["TaxPercentage"] = Percentage[0];
                      
                        if (ViewState["TaxPercentage"] != null && ViewState["TaxPercentage"].ToString() != "" && txtNetAmount.Text != "")
                        {
                            if (txtNetAmount.Text != "0.00" && txtNetAmount.Text != "0")
                            {
                                GetNetAmount();
                            }
                            else
                            {
                                txtNetAmount.Text = "";
                                txtPrice.Text = "";
                                txttax.Text = "";
                            }
                        }
                        UpdateCategoryPrice();
                    }
                    else
                    {

                    }

                }

                else
                {

                }

            }
            ddlOfferGv.ClearSelection();
            if (select == 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Any One Price');", true);
            }

        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Offer');", true);
        }
    }
    #endregion
    #region Btn cancel EVent
    protected void btnCancelGV_Click(object sender, EventArgs e)
    {
        ddlOfferGv.ClearSelection();
        BindCategoryPrice();

        txtFromdateGV.Text = string.Empty;
        txtTodateGV.Text = string.Empty;
    }
    #endregion
    #region CategoryPrice Class
    public class CategoryPrice
    {
        public string queryType { get; set; }
        public string priceId { get; set; }
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string categoryId { get; set; }
        public string trainingTypeId { get; set; }
        public string planDuration { get; set; }
        public string trainingMode { get; set; }
        public string price { get; set; }
        public string taxId { get; set; }
        public string tax { get; set; }
        public string netAmount { get; set; }
        public string actualAmount { get; set; }
        public string displayAmount { get; set; }
        public string cyclePaymentsAllowed { get; set; }
        public string maxNoOfCycles { get; set; }
        public string createdBy { get; set; }
        public string offerId { get; set; }
        public string offerName { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }

        public string updatedBy { get; set; }
    }
    #endregion


    protected void ddlCategoryList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCategoryList.SelectedItem.Text == "General")
        {
            RbtnlPriceMode.Enabled = false;
            ddltrainingType.Enabled= false;
            ddltrainingType.SelectedItem.Text = "General";
        }
        else
        {
            RbtnlPriceMode.Enabled = true;
			ddltrainingType.Enabled = true;
			ddltrainingType.ClearSelection();
            GetTrainingType();
        }
    }
}
