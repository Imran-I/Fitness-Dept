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

public partial class Master_FAQMstr : System.Web.UI.Page
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
            BindFAQ();
            GetFAQ();
        }
            
    }
    #endregion
    #region Bind FAQ
    public void BindFAQ()
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
                string Endpoint = "faqMaster?gymOwnerId=" + Session["gymOwnerId"].ToString() + "&questionType=X";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        gvFAQ.DataSource = dt;
                        gvFAQ.DataBind();
                        divGv.Visible = true;
                        DivForm.Visible = false;
                    }
                    else
                    {
                        divGv.Visible = false;
                        DivForm.Visible = true;
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
        divGv.Visible = false;
        DivForm.Visible = true;
    }
    #endregion
    #region Get FAQ Type
    public void GetFAQ()
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
                string Endpoint = "configMaster/getDropDownDetails?typeId=28";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;

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
                            ddlQuestionType.DataSource = dt;
                            ddlQuestionType.DataTextField = "configName";
                            ddlQuestionType.DataValueField = "configId";
                            ddlQuestionType.DataBind();
                        }
                        else
                        {
                            ddlQuestionType.DataBind();

                        }
                        ddlQuestionType.Items.Insert(0, new ListItem("Qustion Type *", "0"));
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
    #region Get Offer Type

    protected void ddlQuestionType_SelectedIndexChanged(object sender, EventArgs e)
    {
       if( ddlQuestionType.SelectedItem.Text == "Offer")
        {
            divOffer.Visible = true;
            GetOffer();
        }
        else
        {
            divOffer.Visible = false;
        }
    }

    public void GetOffer()
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

                        }
                        ddlOfferList.Items.Insert(0, new ListItem("Offer *", "0"));
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
    #region Submit 
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        if (btnSubmit.Text == "Submit")
        {
            InsertFAQ();
        }
        else
        {
            UpdateFAQ();
            
        }

    }
    #endregion
    #region Insert FAQ 
    public void InsertFAQ()
    {
        try
        {
            string offerId;
            if (ddlQuestionType.SelectedValue == "O")
            {
                divOffer.Visible = true;
                offerId = ddlOfferList.SelectedValue;
            }
            else
            {
                divOffer.Visible = false;
                offerId = "0";
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new FAQ()
                {
                    offerId = offerId,
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    questionType = ddlQuestionType.SelectedValue,
                    question = txtQuestion.Text,
                    answer = txtAnswer.Text,
                    createdBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("faqMaster/insert", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        Clear();
                        BindFAQ();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        Clear();
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
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert(`" + ResponseMsg.ToString().Trim() + "`);", true);
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
    #region Update FAQ 
    public void UpdateFAQ()
    {
        try
        {
            string offerId;
            if (ddlQuestionType.SelectedValue == "O")
            {
                divOffer.Visible = true;
                offerId = ddlOfferList.SelectedValue;
            }
            else
            {
                divOffer.Visible = false;
                offerId = "0";
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new FAQ()
                {    
                    faqId = ViewState["faqId"].ToString(),
                    offerId = offerId,
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    questionType = ddlQuestionType.SelectedValue,
                    question = txtQuestion.Text,
                    answer = txtAnswer.Text,
                    updatedBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("faqMaster/update", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        Clear();
                        BindFAQ();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        Clear();
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
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert(`" + ResponseMsg.ToString().Trim() + "`);", true);
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
        DivForm.Visible = false;
        Clear();
    }
    #endregion  
    #region Btn Edit Click Event
    protected void LnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            
            ImageButton lnkbtn = sender as ImageButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            Label lblfaqId = (Label)gvrow.FindControl("lblfaqId");
            Label lblofferId = (Label)gvrow.FindControl("lblofferId");
            Label lblquestion = (Label)gvrow.FindControl("lblquestion");
            Label lblanswer = (Label)gvrow.FindControl("lblanswer");
            Label lblquestionType = (Label)gvrow.FindControl("lblquestionType");
            ViewState["faqId"] = lblfaqId.Text.Trim();
            ddlQuestionType.Text = lblquestionType.Text.Trim();
            txtAnswer.Text = lblanswer.Text;
            txtQuestion.Text = lblquestion.Text.Trim();
            ddlQuestionType.Enabled = false;
            if (lblofferId.Text != "0")
            {
                if (ddlQuestionType.SelectedValue == "O")
                {
                    divOffer.Visible = true;
                    GetOffer();
                    ddlOfferList.SelectedValue = lblofferId.Text;
                }
                else
                {
                    divOffer.Visible = false;
                }
            }
            {

            }
         
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
            Label lblfaqId = (Label)gvrow.FindControl("lblfaqId");
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
                var activeOrInActive = new FAQ()
                {
                    queryType = QueryType.Trim(),
                    faqId = lblfaqId.Text.Trim(),
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("faqMaster/activeOrInActive", activeOrInActive).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindFAQ();
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
    #region Clear Function
    public void Clear()
    {
        ddlOfferList.ClearSelection();
        ddlQuestionType.ClearSelection();
        txtAnswer.Text = "";
        txtQuestion.Text = "";
        ddlQuestionType.Enabled = true;
        divOffer.Visible = false;
        btnSubmit.Text = "Submit";

    }
    #endregion
    #region FAQ Class
    public class FAQ
    {
        public string faqId { get ; set;}
        public string gymOwnerId { get; set; }
        public string queryType { get ; set;}
        public string offerId { get ; set;}
        public string question { get ; set;}
        public string answer { get ; set;}
        public string questionType { get ; set;}
        public string createdBy { get ; set;}
        public string updatedBy { get ; set;}
    }
    #endregion


}