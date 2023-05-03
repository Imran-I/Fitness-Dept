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

public partial class Master_OtherSetup_PaymentUPI : System.Web.UI.Page
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
            BindPaymentUPI();


        }
    }
    #endregion
    #region Bind PaymentUPI
    public void BindPaymentUPI()
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
                string Endpoint = "PaymentUPI?gymOwnerId="+Session["gymOwnerId"].ToString() + 
                    "&branchId="+ Session["branchId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["PaymentUPIDetails"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        gvPaymentUPI.DataSource = dt;
                        gvPaymentUPI.DataBind();
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
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        divGv.Visible = false;
                        DivForm.Visible = true;

                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
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
    #region Submit 
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        InsertPaymentUPI();
    

    }
    #endregion
    #region Insert Function 
    public void InsertPaymentUPI()
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
                var Insert = new PayMentUPI()
                {
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    UPIId = txtUPIId.Text,
                    name = txtname.Text,
                    phoneNumber = txtphoneNumber.Text,
                    merchantId = txtmerchantId.Text,
                    merchantCode = txtmerchantCode.Text,
                    mode = txtmode.Text,
                    orgId = txtorgId.Text,
                    sign = txtSign.Text,
                    url = txtSign.Text,
                    createdBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("PaymentUPI/insert", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        Clear();
                        BindPaymentUPI();
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
    #region Clear Fuction
    public void Clear()
    {
        txtUPIId.Text = "";
        txtmerchantCode.Text = "";
        txtmerchantId.Text = "";
        txtmode.Text = "";
        txtname.Text = "";
        txtorgId.Text = "";
        txtphoneNumber.Text = "";
        txtSign.Text = "";
        txturl.Text = "";
        btnSubmit.Text = "Submit";
    }
    #endregion
    #region PaymentUPI Class 
    public class PayMentUPI
    {
        public string paymentUPIDetailsId { get; set; }
        public string queryType { get; set; }
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string UPIId { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string merchantId { get; set; }
        public string merchantCode { get; set; }
        public string mode { get; set; }
        public string orgId { get; set; }
        public string sign { get; set; }
        public string url { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
    }
    #endregion
    //#region Update Function 
    //public void UpdatePaymentUPI()
    //{
    //    try
    //    {

    //        using (var client = new HttpClient())
    //        {
    //            client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
    //            client.DefaultRequestHeaders.Clear();
    //            client.DefaultRequestHeaders.Accept.Clear();
    //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
    //            var Insert = new PayMentUPI()
    //            {
    //                paymentUPIDetailsId = ViewState["paymentUPIDetailsId"].ToString(),
    //                gymOwnerId = Session["gymOwnerId"].ToString(),
    //                branchId = Session["branchId"].ToString(),
    //                UPIId = txtUPIId.Text,
    //                name = txtname.Text,
    //                phoneNumber = txtphoneNumber.Text,
    //                merchantId = txtmerchantId.Text,
    //                merchantCode = txtmerchantCode.Text,
    //                mode = txtmode.Text,
    //                orgId = txtorgId.Text,
    //                sign = txtSign.Text,
    //                url = txtSign.Text,
    //                updatedBy = Session["userId"].ToString()

    //            };
    //            HttpResponseMessage response = client.PostAsJsonAsync("PaymentUPI/update", Insert).Result;

    //            if (response.IsSuccessStatusCode)
    //            {
    //                var FinessList = response.Content.ReadAsStringAsync().Result;
    //                int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
    //                string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

    //                if (StatusCode == 1)
    //                {
    //                    Clear();
    //                    BindPaymentUPI();
    //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

    //                }
    //                else
    //                {
    //                    Clear();
    //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
    //                }
    //            }
    //            else
    //            {
    //                var Errorresponse = response.Content.ReadAsStringAsync().Result;
    //                int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
    //                string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
    //                if (statusCode == 0)
    //                {
    //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert(`" + ResponseMsg.ToString().Trim() + "`);", true);
    //                }

    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
    //    }
    //}
    //#endregion
    //#region Btn Edit Click Event
    //protected void LnkEdit_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {

    //        ImageButton lnkbtn = sender as ImageButton;
    //        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
    //        Label lblpaymentUPIDetailsId = (Label)gvrow.FindControl("lblpaymentUPIDetailsId");
    //        Label lblgymOwnerId = (Label)gvrow.FindControl("lblgymOwnerId");
    //        Label lblbranchId = (Label)gvrow.FindControl("lblbranchId");
    //        Label lblname = (Label)gvrow.FindControl("lblname");
    //        Label lblUPIId = (Label)gvrow.FindControl("lblUPIId");
    //        Label lblphoneNumber = (Label)gvrow.FindControl("lblphoneNumber");
    //        Label lblmerchantCode = (Label)gvrow.FindControl("lblmerchantCode");
    //        Label lblmerchantId = (Label)gvrow.FindControl("lblmerchantId");
    //        Label lblmode = (Label)gvrow.FindControl("lblmode");
    //        Label lblorgId = (Label)gvrow.FindControl("lblorgId");
    //        Label lblsign = (Label)gvrow.FindControl("lblsign");
    //        Label lblurl = (Label)gvrow.FindControl("lblurl");
    //        ViewState["paymentUPIDetailsId"] = lblpaymentUPIDetailsId.Text.Trim();
    //        txtUPIId.Text = lblUPIId.Text.Trim();
    //        txtname.Text = lblname.Text;
    //        txtphoneNumber.Text = lblphoneNumber.Text.Trim();
    //        txtmerchantId.Text = lblmerchantId.Text.Trim();
    //        txtmerchantCode.Text = lblmerchantCode.Text.Trim();
    //        txtmode.Text = lblmode.Text.Trim();
    //        txtorgId.Text = lblorgId.Text.Trim();
    //        txtSign.Text = lblsign.Text.Trim();
    //        txturl.Text = lblurl.Text.Trim();
    //        divGv.Visible = false;
    //        DivForm.Visible = true;
    //        btnSubmit.Text = "Update";

    //    }
    //    catch (Exception ex)
    //    {
    //        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
    //    }

    //}
    //#endregion 
    //#region Active or Inactive  Click Event
    //protected void lnkActiveOrInactive_Click(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        LinkButton lnkbtn = sender as LinkButton;
    //        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
    //        Label lblpaymentUPIDetailsId = (Label)gvrow.FindControl("lblpaymentUPIDetailsId");
    //        LinkButton lblActiveStatus = (LinkButton)lnkbtn.FindControl("lnkActiveOrInactive");
    //        string sActiveStatus = lblActiveStatus.Text.Trim() == "Active" ? "A" : "D";
    //        string QueryType = string.Empty;
    //        if (sActiveStatus.Trim() == "D")
    //        {
    //            QueryType = "active";
    //        }
    //        else
    //        {
    //            QueryType = "inActive";
    //        }

    //        using (var client = new HttpClient())
    //        {
    //            client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
    //            client.DefaultRequestHeaders.Clear();
    //            client.DefaultRequestHeaders.Accept.Clear();
    //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
    //            var activeOrInActive = new PayMentUPI()
    //            {
    //                queryType = QueryType.Trim(),
    //                paymentUPIDetailsId = lblpaymentUPIDetailsId.Text.Trim(),
    //                updatedBy = Session["userId"].ToString()
    //            };
    //            HttpResponseMessage response = client.PostAsJsonAsync("PaymentUPI/activeOrInActive", activeOrInActive).Result;
    //            if (response.IsSuccessStatusCode)
    //            {
    //                var Fitness = response.Content.ReadAsStringAsync().Result;
    //                int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
    //                string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

    //                if (StatusCode == 1)
    //                {
    //                    BindPaymentUPI();
    //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

    //                }
    //                else
    //                {
    //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
    //                }
    //            }
    //            else
    //            {
    //                var Errorresponse = response.Content.ReadAsStringAsync().Result;
    //                int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
    //                string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
    //                if (statusCode == 0)
    //                {
    //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
    //                }

    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
    //    }
    //}
    //#endregion
}