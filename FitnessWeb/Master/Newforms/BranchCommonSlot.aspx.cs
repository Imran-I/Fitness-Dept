using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
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
    readonly private string BaseUri;
    readonly private string LogOutUri;
    readonly private string ActiveOrInactiveShiftUri;
    readonly Helper Helper = new Helper();
    private int Session_UserId;
    private string Token;
    public Master_WorkingSlotMstr()
    {
        BaseUri = $"{ConfigurationManager.AppSettings["BaseUrl"].Trim()}";
        LogOutUri = $"{ConfigurationManager.AppSettings["LogoutUrl"].Trim()}";     
        ActiveOrInactiveShiftUri = $"{BaseUri}slotMaster/activeOrInActive";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null && Session["userRole"] == null)
        {
            Session.Clear();
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["LogoutUrl"].Trim(), true);
        }
        Token = Session["APIToken"].ToString();
        Session_UserId = int.Parse(Session["userId"].ToString());
        if (!IsPostBack)
        {
            divGv.Visible = true;
            DivForm.Visible = false;            
            BindSlot();
            BindWorkingHours();
        }

    }
    #region Bind Working Hours
    public void BindWorkingHours()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "branch?queryType=GetBranchMstr&"
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
                                                       
                            DateTime fromTime = DateTime.Parse(dt.Rows[0]["fromtime"].ToString());                             
                            string Slotfromtime = fromTime.ToString("h:mm tt");
                            DateTime totime = DateTime.Parse(dt.Rows[0]["totime"].ToString());
                            string Slottotime = totime.ToString("h:mm tt");


                            ViewState["SlotTiming"] = Slotfromtime + ' '+ '-' + ' ' + Slottotime;
                           Branchworkinghours.InnerHtml = "Branch Working Hours :" + ViewState["SlotTiming"].ToString();
                        }
                        else
                        {
                            Branchworkinghours.InnerHtml = "Branch Working Hours :" + ' ' + '-';
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
    public void BindSlot()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "slotMaster?"
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
                            gvSlots.DataSource = dt;
                            gvSlots.DataBind();
                            divGridView.Visible = true;

                        }
                        else
                        {
                            gvSlots.DataSource = dt;
                            gvSlots.DataBind();
                            divGv.Visible = false;
                            DivForm.Visible = true;
                        }

                    }
                    else
                    {
                        gvSlots.DataSource = null;
                        gvSlots.DataBind();
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
        SingleInsert();
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
                var Insert = new BranchSlots
                {
                    branchId = Session["branchId"].ToString(),
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    Duration= ddlduration.SelectedValue,
                    //strengthPerSlot= txtgymstrength.Text.Trim(),
                    createdBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("slotMaster/insert", Insert).Result;

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
                BindSlot();
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
        WorkingSlotClear();
        BindSlot();
        divGv.Visible = true;
        DivForm.Visible = false;

    }
    #endregion

    #region Clear
    public void WorkingSlotClear()
    {
        ddlduration.SelectedIndex = 0;
        DivForm.Visible = false;
        divGv.Visible = true;
        btnSubmit.Text = "Submit";
    }
    #endregion 
    public class BranchSlots
    {
        public string branchId { get; set; }
        public string slotId { get; set; }
        public string gymOwnerId { get; set; }
        public string slotFromTime { get; set; }
        public string slotToTime { get; set; }
        public string Duration { get; set; }
        //public string strengthPerSlot { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
    }

    protected void lnkActiveOrInactive_Click1(object sender, EventArgs e)
    {
        try
        {
            LinkButton LnkBtn = sender as LinkButton;
            GridViewRow GvRow = (GridViewRow)LnkBtn.NamingContainer;

            string ActiveStatus, ShiftId;

            ActiveStatus = gvSlots.DataKeys[GvRow.RowIndex]["activeStatus"].ToString();
            ShiftId = gvSlots.DataKeys[GvRow.RowIndex]["slotId"].ToString();

            var ActiveOrInactive = new ActiveOrInactive_in()
            {
                QueryType = ActiveStatus == "A" ? "InActive" : "Active",
                slotId = int.Parse(ShiftId),
                UpdatedBy = Session_UserId
            };

            Helper.APIpost<ActiveOrInactive_in>(ActiveOrInactiveShiftUri, Token, ActiveOrInactive, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + Response.ToString().Trim() + "');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + Response.ToString().Trim() + "');", true);
            }
            BindSlot();
        }
        catch (Exception Ex)
        {           
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + Ex.ToString().Trim() + "');", true);
        }
    }

    private class ActiveOrInactive_in
    {
        public string QueryType { get; set; }
        public int slotId { get; set; }
        public int UpdatedBy { get; set; }
    }

    //protected void gvSlots_RowDataBound(object sender, GridViewRowEventArgs e)
    //{      
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        string Fromtime24 = DataBinder.Eval(e.Row.DataItem, "slotFromTime").ToString();
    //        DateTime fromtime = DateTime.ParseExact(Fromtime24, "HH:mm:ss", CultureInfo.InvariantCulture);
    //        string fromtime12 = fromtime.ToString("h:mm tt");
    //        e.Row.Cells[4].Text = fromtime12;
    //        string totime24 = DataBinder.Eval(e.Row.DataItem, "slotToTime").ToString();
    //        DateTime totime = DateTime.ParseExact(totime24, "HH:mm:ss", CultureInfo.InvariantCulture);
    //        string totime12 = totime.ToString("h:mm tt");
    //        e.Row.Cells[5].Text = totime12;
    //    }
    //}
}