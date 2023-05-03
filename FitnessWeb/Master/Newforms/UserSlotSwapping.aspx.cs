using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_Newforms_UserSlotSwapping : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null && Session["userRole"] == null)
        {
            Session.Clear();
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["LogoutUrl"].Trim(), true);
        }
        if (!IsPostBack)
        {
            GetSwapSlotType();
            BindUserSwapSlots();
            BindWorkingSlot();
            ddlUserName.Items.Insert(0, new ListItem("Users Name *", "0"));
        }

    }

    #region Bind User SwapSlots
    public void BindUserSwapSlots()
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
                string Endpoint = Session["BaseUrl"].ToString().Trim() + "userSlotSwapping/SlotSwapping?queryType=getUserSlotSwapping&gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        gvUser.DataSource = dt;
                        gvUser.DataBind();
                        divGv.Visible = true;
                        DivForm.Visible = false;
                        btnCancel.Visible = true;
                    }
                    else
                    {
                        divGv.Visible = false;
                        DivForm.Visible = true;
                        btnCancel.Visible = false;
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
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

    #region Get Users
    public void GetUsers()
    {
        try
        {
            ddlUserName.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = Session["BaseUrl"].ToString().Trim() + "userSlotSwapping/SlotSwapping?queryType=gettrainerSlotBasedUser&gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "&trainerId" + Session["userId"].ToString() + "&slotSwapId=" + ddlslotswaptype.SelectedValue + "";
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
                            ddlUserName.DataSource = dt;
                            ddlUserName.DataTextField = "UserName";
                            ddlUserName.DataValueField = "userId";
                            ddlUserName.DataBind();
                        }
                        else
                        {
                            ddlUserName.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlUserName.Items.Insert(0, new ListItem("Users Name *", "0"));
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

    public void GetUsersPlans()
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
                string Endpoint = Session["BaseUrl"].ToString().Trim() + "userSlotSwapping/SlotSwapping?queryType=getUserPlanDetails&gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "&trainerId=" + Session["userId"].ToString() + "&userId=" + ddlUserName.SelectedValue + "";
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

                            //userswapType.Text="Slot SwapType : " + dt.Rows[0]["configName"].ToString();
                            //userslot.Text="Slot Timing : " + dt.Rows[0]["SlotTime"].ToString();
                            userswapType.Text = dt.Rows[0]["configName"].ToString();
                            userslot.Text = dt.Rows[0]["SlotTime"].ToString();
                            ViewState["UserswaptypeId"] = dt.Rows[0]["slotSwapType"].ToString();
                            ViewState["UserslotId"] = dt.Rows[0]["slotId"].ToString();
                            ViewState["fromDate"] = dt.Rows[0]["fromDate"].ToString();
                            ViewState["bookingId"] = dt.Rows[0]["bookingId"].ToString();
                            Userplandetails.Visible = true;
                        }
                        else
                        {
                            userswapType.Text = "Slot SwapType : - ";
                            userslot.Text = "Slot Timing : - ";
                            ViewState["UserswaptypeId"] = "";
                            ViewState["UserslotId"] = "";
                            ViewState["fromDate"] = "";
                            ViewState["bookingId"] = "";
                            Userplandetails.Visible = false;
                        }

                    }
                    else
                    {
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
    #region Get SlotType
    public void GetSwapSlotType()
    {
        try
        {
            ddlslotswaptype.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = Session["BaseUrl"].ToString().Trim() + "userSlotSwapping/SlotSwapping?queryType=getSwapType";
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
                            ddlslotswaptype.DataSource = dt;
                            ddlslotswaptype.DataTextField = "configName";
                            ddlslotswaptype.DataValueField = "configId";
                            ddlslotswaptype.DataBind();
                        }
                        else
                        {
                            ddlslotswaptype.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlslotswaptype.Items.Insert(0, new ListItem("Swap SlotType *", "0"));
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divGv.Visible = false;
        DivForm.Visible = true;
    }

    protected void ddlslotswaptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetUsers();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        UserClear();
    }
    public void UserClear()
    {
        ddlUserName.ClearSelection();
        ddlslotswaptype.ClearSelection();
        DivForm.Visible = false;
        divGv.Visible = true;
        btnSubmit.Text = "Submit";
        Userplandetails.Visible = false;
        chkSlotList.ClearSelection();
        txtlogDate.Text = string.Empty;
    }

    #region Bind Working Slot Grid
    public void BindWorkingSlot()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "slotMaster/GetSlotstoActivate?"
                    + "gymOwnerId=" + Session["gymOwnerId"].ToString() + "&branchId=" + Session["branchId"].ToString() + "";
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
                            chkSlotList.DataSource = dt;
                            chkSlotList.DataTextField = "slotTime";
                            chkSlotList.DataValueField = "slotId";
                            chkSlotList.DataBind();

                        }
                        else
                        {
                            chkSlotList.DataBind();
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
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    protected void ddlUserName_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetUsersPlans();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        InsertUser();
    }
    #region Insert Function 
    public void InsertUser()
    {
        try
        {
            if(chkSlotList.SelectedValue== "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Any one slot to swap!!!');", true);
                BindUserSwapSlots();
                UserClear();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new UserInsert()
                {
                    newslotId = chkSlotList.SelectedValue,
                    oldslotId = ViewState["UserslotId"].ToString(),
                    userId = ddlUserName.SelectedValue,
                    slotfromDate = ViewState["fromDate"].ToString(),
                    toDate= txtlogDate.Text,
                    bookingId = ViewState["bookingId"].ToString(),
                    slotswapTypeId = ViewState["UserswaptypeId"].ToString(),
                    createdBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("userSlotSwapping/insert", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindUserSwapSlots();

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
                UserClear();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion


    #region User Insert
    public class UserInsert
    {
        public string newslotId { get; set; }
        public string oldslotId { get; set; }
        public string userId { get; set; }
        public string slotswapTypeId { get; set; }
        public string slotfromDate { get; set; }
        public string toDate { get; set; }
        public string bookingId { get; set; }
        public string createdBy { get; set; }
    }
    #endregion
}