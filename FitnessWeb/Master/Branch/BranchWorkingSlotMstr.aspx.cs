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
            BindWorkingDay();
            BindWorkingSlot();
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "branchWorkingDays/getWorkingDaysForSlot?branchId="+Session["branchId"].ToString() +"";
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "branchWorkingSlots/getBranchworkingDaysForSlots?"
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
                            gvWorkingDay.DataSource = dt;
                            gvWorkingDay.DataBind();
                            divGridView.Visible = true;

                        }
                        else
                        {
                            gvWorkingDay.DataSource = null;
                            gvWorkingDay.DataBind();
                            divGv.Visible = false;
                            DivForm.Visible = true;
                        }

                    }
                    else
                    {
                        gvWorkingDay.DataSource = null;
                        gvWorkingDay.DataBind();
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

    #region To view Slot Button Click
    protected void LnkView_Click(object sender, EventArgs e)
    {
        try
        {
            WorkingSlotClear();
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            DataList dtlWorkingSlots = (DataList)gvrow.FindControl("dtlWorkingSlots");
            Label lblworkingDayId = (Label)gvrow.FindControl("lblworkingDayId");
            if (dtlWorkingSlots.Visible == false)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                    string sUrl = Session["BaseUrl"].ToString().Trim() + "branchWorkingSlots/getBranchWorkingSlots?queryType=GetBranchWorkingSlots&"
                        + "gymOwnerId=" + Session["gymOwnerId"].ToString() + "&branchId=" + Session["branchId"].ToString() + ""
                        + "&workingDayId=" + lblworkingDayId.Text.Trim() + "";
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
                                
                                dtlWorkingSlots.Visible = true;
                                dtlWorkingSlots.DataSource = dt;
                                dtlWorkingSlots.DataBind();

                            }
                            else
                            {
                                dtlWorkingSlots.DataSource = null;
                                dtlWorkingSlots.DataBind();

                            }

                        }
                        else
                        {
                            dtlWorkingSlots.DataSource = null;
                            dtlWorkingSlots.DataBind();
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + response.ReasonPhrase.ToString().Trim() + "');", true);
                    }
                }
            }
            else
            {
                dtlWorkingSlots.Visible = false;
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
        string workingDayId = string.Empty;
     
        int Count = 0;

        for (int i = 0; i < chkWorkingDays.Items.Count; i++)
        {
           
            if (chkWorkingDays.Items[i].Selected == true)
            {
                workingDayId += chkWorkingDays.Items[i].Value + ",";
                Count = 1;
            }
           
        }
        if (Count == 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Any One Work Day');", true);
            return;
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
                InsertBranchWorkingSlots_In Insert = new InsertBranchWorkingSlots_In()
                {
                    LstOfbranchWorkingSlots = InsertWorkingDaySlot(Session["gymOwnerId"].ToString(),
                   Session["branchId"].ToString(), workingDayId.ToString().TrimEnd(','),
                    txtFromTime.Text.Trim(), txtToTime.Text.Trim(),Session["userId"].ToString())
                };
                HttpResponseMessage response = client.PostAsJsonAsync("branchWorkingSlots/insert", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindWorkingSlot();
                        WorkingSlotClear();
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
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }

    public static List<BranchWorkingSlots> InsertWorkingDaySlot(string gymOwnerId, string branchId,
        string workingDayId, string fromTime, string toTime, string createdBy)
    {
        string[] workingDayIds;
        workingDayIds = workingDayId.Split(',');

        List<BranchWorkingSlots> lst = new List<BranchWorkingSlots>();
        for (int i = 0; i < workingDayIds.Count(); i++)
        {
            lst.AddRange(new List<BranchWorkingSlots>
            {
                new BranchWorkingSlots { gymOwnerId=gymOwnerId,branchId=branchId ,
                workingDayId=workingDayIds[i],fromTime=fromTime,toTime=toTime,createdBy=createdBy
                }

            }); ;
        }
        return lst;

    }


    #endregion

    #region Slot Deactive Event
    protected void LnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            txtFromTimePop.Text = string.Empty;
            txtToTimePop.Text = string.Empty;
            AddSlots.Visible = false;
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;

            Label lblworkingDayId = (Label)gvrow.FindControl("lblworkingDayId");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Delete = new BranchWorkingSlots
                {
                    branchId = Session["branchId"].ToString(),
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    workingDayId = lblworkingDayId.Text.Trim()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("branchWorkingSlots/inActive", Delete).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindWorkingSlot();
                        WorkingSlotClear();
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

    #region Grid Add Event
    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;

        Label lblworkingDayId = (Label)gvrow.FindControl("lblworkingDayId");
        ViewState["lblworkingDayId"] = lblworkingDayId.Text.Trim();
        AddSlots.Visible = true;
    }
    #endregion

    #region PopUp Submit Button
    protected void btnSubSubmit_Click(object sender, EventArgs e)
    {
        SingleInsert();
    }
    #endregion

    #region Popup Cancel 
    protected void btnSubCancel_Click(object sender, EventArgs e)
    {
        txtFromTimePop.Text = string.Empty;
        txtToTimePop.Text = string.Empty;
        AddSlots.Visible = false;
    }
    #endregion

    #region Single Insert Method
    public void SingleInsert()
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
                var Insert = new BranchWorkingSlots
                {
                    branchId = Session["branchId"].ToString(),
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    workingDayId = ViewState["lblworkingDayId"].ToString().Trim(),
                    fromTime= txtFromTimePop.Text.Trim(),
                    toTime=txtToTimePop.Text.Trim(),
                    createdBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("branchWorkingSlots/singleinsert", Insert).Result;

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
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + response.ReasonPhrase.ToString().Trim() + "');", true);
                }
                WorkingSlotClear();
                BindWorkingSlot();
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

    }
    #endregion

    #region Clear
    public void WorkingSlotClear()
    {
        chkWorkingDays.ClearSelection();
        txtFromTime.Text = string.Empty;
        txtToTime.Text = string.Empty;
        txtFromTimePop.Text = string.Empty;
        txtToTimePop.Text = string.Empty;
        AddSlots.Visible = false;
        DivForm.Visible = false;
        divGv.Visible = true;
        btnSubmit.Text = "Submit";
    }
    #endregion
    public class InsertBranchWorkingSlots_In
    {
        public List<BranchWorkingSlots> LstOfbranchWorkingSlots { get; set; }
    }

    public class BranchWorkingSlots
    {
        public string branchId { get; set; }
        public string workingDayId { get; set; }
        public string gymOwnerId { get; set; }
        public string fromTime { get; set; }
        public string toTime { get; set; }
        public string createdBy { get; set; }
    }




   
}