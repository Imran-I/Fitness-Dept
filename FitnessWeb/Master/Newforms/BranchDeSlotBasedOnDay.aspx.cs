using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Master_WorkingSlotMstr : System.Web.UI.Page
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
            divGv.Visible = true;
            DivForm.Visible = false;
            BindWorkingDay();
            BindWorkingSlot();
            BindDeactivatedSlots();

        }

    }
    #region Bind Working Day
    public void BindWorkingDay()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "branchWorkingDays/getWorkingDaysForSlot?branchId=" + Session["branchId"].ToString() + "";
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
                            chkWorkingDays.DataSource = dt;
                            chkWorkingDays.DataTextField = "workingDay";
                            chkWorkingDays.DataValueField = "workingDayId";
                            chkWorkingDays.DataBind();
                        }
                        else
                        {
                            chkWorkingDays.DataBind();
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
    #region Add Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divGv.Visible = false;
        DivForm.Visible = true;
    }
    #endregion
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

    #region Bind Deactivated Working Slot Grid
    public void BindDeactivatedSlots()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "dayBasedDeactivatedslotMaster?"
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
                            gvDeactivatedSlots.DataSource = dt;
                            gvDeactivatedSlots.DataBind();
                            divGridView.Visible = true;

                        }
                        else
                        {
                            gvDeactivatedSlots.DataSource = dt;
                            gvDeactivatedSlots.DataBind();
                            divGv.Visible = false;
                            DivForm.Visible = true;
                        }

                    }
                    else
                    {
                        gvDeactivatedSlots.DataSource = null;
                        gvDeactivatedSlots.DataBind();
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



    #region Btn Submit Click Event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {      
            InsertWorkingSlot();       
    }
    #endregion

    #region Insert Working Slot
    public void InsertWorkingSlot()
    {
        try
        {
            string workingDayId = string.Empty;

            int Count = 0;
            int Count1 = 0;

            for (int i = 0; i < chkWorkingDays.Items.Count; i++)
            {

                if (chkWorkingDays.Items[i].Selected == true)
                {
                    Count = 1;
                    for (int j = 0; j < chkSlotList.Items.Count; j++)
                    {

                        if (chkSlotList.Items[j].Selected == true)
                        {
                            Count1 = 1;
                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                                client.DefaultRequestHeaders.Clear();
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                                BranchWorkingSlots Insert = new BranchWorkingSlots()
                                {

                                    gymOwnerId = Session["gymOwnerId"].ToString(),
                                    branchId = Session["branchId"].ToString(),
                                    slotId = chkSlotList.Items[j].Value,
                                    workingDayId = chkWorkingDays.Items[i].Value,
                                    createdBy = Session["userId"].ToString()


                                };
                                HttpResponseMessage response = client.PostAsJsonAsync("dayBasedDeactivatedslotMaster/insert", Insert).Result;
                                if (response.IsSuccessStatusCode)
                                {
                                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                                    if (StatusCode == 1)
                                    {

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
                                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Working Slot Already Exists');", true);
                                    }

                                }

                                BindDeactivatedSlots();
                                WorkingSlotClear();
                            }
                        }

                    }
                }

            }
            if (Count == 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Any One Work Day');", true);
                return;
            }

            if (Count1 == 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Any One Slot');", true);
                return;
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
        BindDeactivatedSlots();
        WorkingSlotClear();
        divGv.Visible = true;
        DivForm.Visible = false;

    }
    #endregion

    #region Clear
    public void WorkingSlotClear()
    {
        chkWorkingDays.ClearSelection();
        chkSlotList.ClearSelection();
        DivForm.Visible = false;
        divGv.Visible = true;
        btnSubmit.Text = "Submit";
    }
    #endregion
  

    public class BranchWorkingSlots
    {
        public string uniqueId { get; set; }
        public string queryType { get; set; }
        public string branchId { get; set; }
        public string slotId { get; set; }
        public string gymOwnerId { get; set; }
        public string workingDayId { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
    }
      

    #region Active or Inactive  Click Event
    protected void lnkActiveOrInactiveSub_Click(object sender, EventArgs e)
    {

        try
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            Label lbluniqueId = (Label)gvrow.FindControl("lbluniqueId");

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
                BranchWorkingSlots activeOrInActive = new BranchWorkingSlots()
                {
                    queryType=QueryType,
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    uniqueId = lbluniqueId.Text.Trim(),
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("dayBasedDeactivatedslotMaster/activeOrInActive", activeOrInActive).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {                       
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
                BindDeactivatedSlots();
                WorkingSlotClear();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
}