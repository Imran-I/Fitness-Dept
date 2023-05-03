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

public partial class Master_OfferMapping : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null && Session["userRole"] == null)
        {
            Session.Clear();
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["LogoutUrl"].Trim(), true);
        }
        if (string.IsNullOrEmpty(Session["BranchId"] as string))
        {
            if (Session["UserRole"].ToString() == "Sadmin")
            {
                Response.Redirect("~/AdminLogin.aspx", false);
            }
            else if (Session["userRole"].ToString().Trim() == "GymOwner")
            {
                Response.Redirect("~/OwnerLogin.aspx", false);
            }
        }
        if (!IsPostBack)
        {
            BindGvOfferMapping();
        }
    }

    #region Bind GridView
    public void BindGvOfferMapping()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "offerMapping?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        List<OfferMapping> OwnerMstr = JsonConvert.DeserializeObject<List<OfferMapping>>(ResponseMsg);
                        DataTable dt = ConvertToDataTable(OwnerMstr);
                        if (dt.Rows.Count > 0)
                        {
                            divGv.Visible = true;
                            divForm.Visible = false;
                            gvOfferMapping.DataSource = dt;
                            gvOfferMapping.DataBind();
                        }
                        else
                        {
                            divGv.Visible = false;
                            divForm.Visible = true;
                            BindOfferDdl();
                            BindBranch();
                            gvOfferMapping.DataBind();
                        }
                    }
                    else
                    {
                        divGv.Visible = false;
                        divForm.Visible = true;
                        BindOfferDdl();
                        BindBranch();
                        gvOfferMapping.DataBind();
                    }

                }

            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    DataTable ConvertToDataTable<TSource>(IEnumerable<TSource> source)
    {
        var props = typeof(TSource).GetProperties();

        var dt = new DataTable();
        dt.Columns.AddRange(
          props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray()
        );

        source.ToList().ForEach(
          i => dt.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray())
        );

        return dt;
    }

    #endregion

    #region Add Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divGv.Visible = false;
        divForm.Visible = true;
        Cancel();
        BindBranch();
        BindOfferDdl();
    }
    #endregion

    #region Bind DropDown Branch
    public void BindBranch()
    {
        try

        {
            ddlBranchList.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sUrl = Session["BaseUrl"].ToString().Trim() + "branch/GetDropDownDetails?queryType=ddlBranchMstrOwner&gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "";
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
                            ddlBranchList.DataSource = dt;
                            ddlBranchList.DataTextField = "branchName";
                            ddlBranchList.DataValueField = "branchId";
                            ddlBranchList.DataBind();
                            ddlBranchList.SelectedValue = Session["branchId"].ToString();
                        }
                        else
                        {
                            ddlBranchList.DataBind();
                        }
                      
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
                ddlBranchList.Items.Insert(0, new ListItem("Branch *", "0"));
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region Bind DropDown Offer
    public void BindOfferDdl()
    {
        try
        {
            divOfferDetails.Visible = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sUrl = Session["BaseUrl"].ToString().Trim() + "offer/GetddlOffer?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "";
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
                            ddlOfferList.DataSource = dt;
                            ddlOfferList.DataTextField = "offerHeading";
                            ddlOfferList.DataValueField = "offerId";
                            ddlOfferList.DataBind();
                        }
                        else
                        {
                            ddlOfferList.DataBind();
                            divForm.Visible = false;
                            divGv.Visible = true;
                        }
                        ddlOfferList.Items.Insert(0, new ListItem("Offer *", "0"));
                    }
                    else
                    {
                        ddlOfferList.Items.Clear();
                        ddlOfferList.Items.Insert(0, new ListItem("Offer *", "0"));
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        divForm.Visible = false;
                        divGv.Visible = true;
                    }
                }
                else
                {
                    ddlOfferList.Items.Clear();
                    ddlOfferList.Items.Insert(0, new ListItem("Offer *", "0"));
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + response.ReasonPhrase.ToString().Trim() + "');", true);
                    divForm.Visible = false;
                    divGv.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region OfferMapping Details Show While Add
    #region Bind Offer  Master 
    public void BindOfferMaster()
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

                string sUrl = Session["BaseUrl"].ToString().Trim()
                        + "offer?offerId=" + ddlOfferList.SelectedValue + "&gymOwnerId=" + Session["gymOwnerId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    var Response = JObject.Parse(Locresponse)["Response"].ToArray();

                    if (StatusCode == 1)
                    {
                        List<OfferMasterClass> Offer = JsonConvert.DeserializeObject<List<OfferMasterClass>>(ResponseMsg);

                        divOfferDetails.Visible = true;
                        var OfferItems = Offer.ElementAt(0);

                        string Offertype = OfferItems.offerType.ToString();
                        if (Offertype == "P")
                        {
                            PerorFix.InnerText = "(in %)";
                        }
                        else
                        {
                            PerorFix.InnerText = "(in ₹)";
                        }

                        lblAmount.Text = OfferItems.offerValue.ToString();
                        lblMinAmt.Text = OfferItems.minAmt.ToString();
                        lblMaxAmt.Text = OfferItems.maxAmt.ToString();
                        lblOffCode.Text = OfferItems.offerCode.ToString();
                        lblOfferDes.Text = OfferItems.offerDescription.ToString();
                        lblOfferNameheading.Text = OfferItems.offerHeading.ToString();
                        lblfromdate.Text = OfferItems.fromDate.ToString();
                        lblTodate.Text = OfferItems.toDate.ToString();
                        if (ResponseMsg.Contains("offerRulesDetails"))
                        {
                            if (OfferItems.offerRulesDetails == null)
                            {
                                dtlRules.DataSource = null;
                                dtlRules.DataBind();
                                Rules.Visible = false;
                            }
                            else
                            {
                                Rules.Visible = true;
                                var OfferRules = OfferItems.offerRulesDetails.ToList();
                                DataTable OfferRulesType = ConvertToDataTable(OfferRules);
                                dtlRules.DataSource = OfferRulesType;
                                dtlRules.DataBind();
                            }

                        }
                        else
                        {
                            dtlRules.DataBind();
                            Rules.Visible = false;
                        }

                    }
                    else
                    {
                        dtlRules.DataSource = null;
                        dtlRules.DataBind();
                        Rules.Visible = false;
                        divOfferDetails.Visible = false;
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
    protected void ddlOfferList_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindOfferMaster();
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        Label lblGvBranchId = (Label)gvrow.FindControl("lblGvBranchId");
        Label lblGvBranchName = (Label)gvrow.FindControl("lblGvBranchName");
        Label lblGvOfferId = (Label)gvrow.FindControl("lblGvOfferId");
        Label lblGvOfferHeading = (Label)gvrow.FindControl("lblGvOfferHeading");

        ddlBranchList.Items.Insert(0, new ListItem(lblGvBranchName.Text, lblGvBranchId.Text));
        ddlBranchList.SelectedValue = lblGvBranchId.Text;

        ddlOfferList.Items.Insert(0, new ListItem(lblGvOfferHeading.Text, lblGvOfferId.Text));
        ddlOfferList.SelectedValue = lblGvOfferId.Text;
        ddlOfferList.Enabled = false;
        divGv.Visible = false;
        divForm.Visible = true;
        btnSubmit.Visible = false;
        BindOfferMaster();
    }
    #endregion

    #region Submit / Insert 
    protected void btnSubmit_Click(object sender, EventArgs e)
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

                var Insert = new OfferMapping()
                {
                    gymOwnerId = Session["gymOwnerId"].ToString().Trim(),
                    branchId = ddlBranchList.SelectedValue.Trim(),
                    offerId = ddlOfferList.SelectedValue.Trim(),
                    createdBy = Session["userId"].ToString().Trim()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("offerMapping/insert", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        BindGvOfferMapping();
                        Cancel();
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
        divGv.Visible = true;
        divForm.Visible = false;
        Cancel();
    }
    public void Cancel()
    {
        ddlOfferList.Items.Clear();
        ddlBranchList.Items.Clear();
        btnSubmit.Visible = true;
        ddlOfferList.Enabled = true;
    }
    #endregion

    #region Delete
    protected void lnkActiveOrInactive_Click(object sender, EventArgs e)
    {
        try
        {
            using (var client = new HttpClient())
            {
                LinkButton lnkbtn = sender as LinkButton;
                GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
                string sofferMappingId = gvOfferMapping.DataKeys[gvrow.RowIndex].Value.ToString();
                LinkButton lblActiveStatus = (LinkButton)lnkbtn.FindControl("lnkActiveOrInactive");

                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sActiveStatus = lblActiveStatus.Text.Trim() == "Active" ? "inActive" : "active";

                var Delete = new OfferMapping()
                {
                    queryType = sActiveStatus.Trim(),
                    offerMappingId = sofferMappingId.Trim(),
                    updatedBy = Session["userId"].ToString().Trim()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("offerMapping/activeOrInActive", Delete).Result;

                if (response.IsSuccessStatusCode)
                {
                    var LocalResponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(LocalResponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(LocalResponse)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        BindGvOfferMapping();
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

    #region Offer Class
    public class OfferMasterClass
    {
        public String offerId { get; set; }
        public String offerTypePeriod { get; set; }
        public String offerHeading { get; set; }
        public String offerDescription { get; set; }
        public String offerCode { get; set; }
        public String offerImageUrl { get; set; }
        public String fromDate { get; set; }
        public String toDate { get; set; }
        public String offerType { get; set; }
        public String offerValue { get; set; }
        public String minAmt { get; set; }
        public String maxAmt { get; set; }
        public String noOfTimesPerUser { get; set; }
        public String termsAndConditions { get; set; }
        public List<offerRulesDetails> offerRulesDetails { get; set; }

    }
    public class OfferMapping
    {
        public string queryType { get; set; }
        public string offerMappingId { get; set; }
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string branchName { get; set; }
        public string offerHeading { get; set; }
        public string offerId { get; set; }
        public string offerType { get; set; }
        public string offerValue { get; set; }
        public string offerTypePeriod { get; set; }
        public string offerDescription { get; set; }
        public string offerCode { get; set; }
        public string offerImageUrl { get; set; }
        public string minAmt { get; set; }
        public string maxAmt { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public int noOfTimesPerUser { get; set; }
        public string termsAndConditions { get; set; }
        public string expireStatus { get; set; }
        public string activeStatus { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }

    }
    public class offerRulesDetails
    {
        public String offerRuleId { get; set; }
        public String offerId { get; set; }
        public String offerRule { get; set; }
        public String ruleType { get; set; }
        public String activeStatus { get; set; }
    }
    #endregion

}
