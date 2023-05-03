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

public partial class Master_MessageTemplates : System.Web.UI.Page
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
            if (rbtntemplateType.SelectedValue == "M")
            {
                divPeid.Visible = false;
                divTpid.Visible = false;
            }
            else
            {
                divPeid.Visible = true;
                divTpid.Visible = true;
            }
            BindMessageTemplates();
        }

    }
    #endregion
    #region Bind Message Templates
    public void BindMessageTemplates()
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
                string Endpoint = "messageTemplateMaster";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        gvMessageTemplate.DataSource = dt;
                        gvMessageTemplate.DataBind();
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
    #region  Template Type Select Change 
    protected void rbtntemplateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtntemplateType.SelectedValue == "M")
        {
            divPeid.Visible = false;
            divTpid.Visible = false;
        }
        else
        {
            divPeid.Visible = true;
            divTpid.Visible = true;
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
            InsertMessageTemp();
        }
        else
        {
            UpdateMessageTemp();

        }

    }
    #endregion
    #region Insert Function 
    public void InsertMessageTemp()
    {
        try
        {
            string peid;
            string tpid;
            if (rbtntemplateType.SelectedValue == "M")
            {
                tpid = "0";
                peid = "0";
                txttpid.Text = tpid;
                txtpeid.Text = peid;
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new MessageTemplates()
                {
                    messageHeader = txtmessageHeader.Text,
                    subject = txtsubject.Text,
                    messageBody = txtmessageBody.Text,
                    templateType = rbtntemplateType.SelectedValue,
                    peid = txtpeid.Text,
                    tpid = txttpid.Text,
                    createdBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("messageTemplateMaster/insert", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        Clear();
                        BindMessageTemplates();
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
    #region Update Function 
    public void UpdateMessageTemp()
    {
        try
        {
            string peid;
            string tpid;
            if (rbtntemplateType.SelectedValue == "M")
            {
                tpid = "0";
                peid = "0";
                txttpid.Text = tpid;
                txtpeid.Text = peid;
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new MessageTemplates()
                {   uniqueId = ViewState["uniqueId"].ToString(),
                    messageHeader = txtmessageHeader.Text,
                    subject = txtsubject.Text,
                    messageBody = txtmessageBody.Text,
                    templateType = rbtntemplateType.SelectedValue,
                    peid = txtpeid.Text,
                    tpid = txttpid.Text,
                    updatedBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("messageTemplateMaster/update", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        Clear();
                        BindMessageTemplates();
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
    #region Btn Edit Click Event
    protected void LnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            ImageButton lnkbtn = sender as ImageButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            Label lbluniqueId = (Label)gvrow.FindControl("lbluniqueId");
            Label lblmessageHeader = (Label)gvrow.FindControl("lblmessageHeader");
            Label lblsubject = (Label)gvrow.FindControl("lblsubject");
            Label lblmessageBody = (Label)gvrow.FindControl("lblmessageBody");
            Label lbltemplateType = (Label)gvrow.FindControl("lbltemplateType");
            Label lblpeid = (Label)gvrow.FindControl("lblpeid");
            Label lbltpid = (Label)gvrow.FindControl("lbltpid");
            ViewState["uniqueId"] = lbluniqueId.Text.Trim();
            txtmessageHeader.Text = lblmessageHeader.Text.Trim();
            txtsubject.Text = lblsubject.Text;
            txtmessageBody.Text = lblmessageBody.Text.Trim();
            rbtntemplateType.SelectedValue = lbltemplateType.Text.Trim();
            rbtntemplateType.Enabled = false;
            if (rbtntemplateType.SelectedValue == "M")
            {
                divPeid.Visible = false;
                divTpid.Visible = false;
            }
            else
            {
                divPeid.Visible = true;
                divTpid.Visible = true;
            }
            txtpeid.Text = lblpeid.Text;
            txttpid.Text = lbltpid.Text;
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
    #region Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        Clear();
    }
    #endregion
    #region Clear 
    public void Clear()
    {
        txttpid.Text = "";
        txtpeid.Text = "";
        txtmessageBody.Text = "";
        txtmessageHeader.Text = "";
        txtsubject.Text = "";
        rbtntemplateType.SelectedValue = "M";
        divPeid.Visible = false;
        divTpid.Visible = false;
        rbtntemplateType.Enabled = true;
        btnSubmit.Text = "Submit";
    }
    #endregion
    #region Message Templates Class
    public class MessageTemplates
    {
        public string uniqueId { get; set; }
        public string messageHeader { get; set; }
        public string subject { get; set; }
        public string messageBody { get; set; }
        public string templateType { get; set; }
        public string peid { get; set; }
        public string tpid { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
    }
    #endregion
}