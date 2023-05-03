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

public partial class Master_BranchWorkOutDays : System.Web.UI.Page
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
            ViewState["workingDayId"] = "";
            BindWorkingDay();
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "branchWorkingDays?branchId=" + Session["branchId"].ToString() + "";
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
                            gvWorkingMstr.DataSource = dt;
                            gvWorkingMstr.DataBind();
                            divGridView.Visible = true;

                        }
                        else
                        {
                            gvWorkingMstr.DataSource = null;
                            gvWorkingMstr.DataBind();
                            divGv.Visible = false;
                            DivForm.Visible = true;
                        }

                    }
                    else
                    {
                        gvWorkingMstr.DataSource = null;
                        gvWorkingMstr.DataBind();
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

    #region Add Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        sameWorking.Visible = true;
        divGv.Visible = false;
        DivForm.Visible = true;
    }
    #endregion
    #region Btnsubmit Click Event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubmit.Text == "Submit")
        {
            InsertWorkingDay();
        }
        else
        {
            UpdateWorkingDay();
        }
    }
    #endregion

    #region InsertWorking Day
    public void InsertWorkingDay()
    {
        string workingDay = string.Empty;
        string isHolidays = string.Empty;
        string isHoliday = string.Empty;
        if (ChkHoliday.Checked == true)
        {
            isHolidays = "Y";
        }
        else
        {
            isHolidays = "N";

        }
        int Count = 0;
        for (int i = 0; i < chkWorkingDays.Items.Count; i++)
        {
            if (chkWorkingDays.Items[i].Selected == true)
            {
                Count = 1;
                workingDay += chkWorkingDays.Items[i].Text + ",";
                isHoliday += isHolidays + ",";
            }

        }




        if (Count == 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Any One Work Day');", true);
            return;
        }
        DateTime FromTime = Convert.ToDateTime(txtFromTime.Text.Trim());
        DateTime ToTime = Convert.ToDateTime(txtToTime.Text.Trim());

        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                InsertWorkingDay_In Insert = new InsertWorkingDay_In()
                {
                    LstOfbranchWorkingDays = InsertWorkingDay(Session["gymOwnerId"].ToString(),
                   Session["branchId"].ToString(), workingDay.ToString().TrimEnd(','),
                    FromTime.ToString("HH:mm"), ToTime.ToString("HH:mm"),
                   isHoliday.ToString().TrimEnd(','), Session["userId"].ToString())
                };
                HttpResponseMessage response = client.PostAsJsonAsync("branchWorkingDays/insert", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindWorkingDay();

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
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Working Day Already Exists');", true);
                    }

                }
                WorkingDayClear();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }

    public static List<WorkingDay> InsertWorkingDay(string gymOwnerId, string branchId,
        string workingDay, string fromTime, string toTime, string isHoliday, string createdBy)
    {
        string[] workingDays;
        string[] isHolidays;
        workingDays = workingDay.Split(',');
        isHolidays = isHoliday.Split(',');
        List<WorkingDay> lst = new List<WorkingDay>();
        for (int i = 0; i < workingDays.Count(); i++)
        {
            lst.AddRange(new List<WorkingDay>
            {
                new WorkingDay { gymOwnerId=gymOwnerId,branchId=branchId ,
                workingDay=workingDays[i],fromTime=fromTime,toTime=toTime,
                isHoliday=isHolidays[i],createdBy=createdBy
                }

            }); ;
        }
        return lst;

    }


    #endregion

    #region Update  Working Day
    public void UpdateWorkingDay()
    {
        string workingDay = string.Empty;
        string isHolidays = string.Empty;
        string isHoliday = string.Empty;
        if (ChkHoliday.Checked == true)
        {
            isHolidays = "Y";
        }
        else
        {
            isHolidays = "N";

        }

        for (int i = 0; i < chkWorkingDays.Items.Count; i++)
        {
            if (chkWorkingDays.Items[i].Text == ViewState["workingDay"].ToString())
            {
                if (chkWorkingDays.Items[i].Selected == true)
                {
                    workingDay += chkWorkingDays.Items[i].Text + ",";
                    isHoliday += isHolidays + ",";
                }

            }
        }
        DateTime FromTime = Convert.ToDateTime(txtFromTime.Text.Trim());
        DateTime ToTime = Convert.ToDateTime(txtToTime.Text.Trim());

        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                InsertWorkingDay_In Update = new InsertWorkingDay_In()
                {
                    LstOfbranchWorkingDays = UpdateWorkingDay(Session["gymOwnerId"].ToString(),
                    Session["branchId"].ToString(), ViewState["workingDayId"].ToString(), workingDay.ToString().TrimEnd(','),
                      FromTime.ToString("HH:mm"), ToTime.ToString("HH:mm"),
                    isHoliday.ToString().TrimEnd(','), Session["userId"].ToString())
                };
                HttpResponseMessage response = client.PostAsJsonAsync("branchWorkingDays/update", Update).Result;
                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindWorkingDay();

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
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Working Day Already Exists');", true);
                    }

                }
                WorkingDayClear();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    public static List<WorkingDay> UpdateWorkingDay(string gymOwnerId, string branchId, string workingDayId,
      string workingDay, string fromTime, string toTime, string isHoliday, string updatedBy)
    {

        string[] workingDays;
        string[] isHolidays;
        workingDays = workingDay.Split(',');

        isHolidays = isHoliday.Split(',');
        List<WorkingDay> lst = new List<WorkingDay>();
        for (int i = 0; i < workingDays.Count(); i++)
        {
            lst.AddRange(new List<WorkingDay>
            {
                new WorkingDay { gymOwnerId=gymOwnerId,branchId=branchId ,workingDayId=workingDayId,
                workingDay=workingDays[i],fromTime=fromTime,toTime=toTime,
                isHoliday=isHolidays[i],updatedBy=updatedBy
                }

            }); ;
        }
        return lst;

    }
    #endregion

    #region Btn Edit Click Event
    protected void LnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton lnkbtn = sender as ImageButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            Label lblisHoliday = (Label)gvrow.FindControl("lblisHoliday");
            Label lblworkingDayId = (Label)gvrow.FindControl("lblworkingDayId");
            Label lblworkingDay = (Label)gvrow.FindControl("lblworkingDay");

            for (int i = 0; i < chkWorkingDays.Items.Count; i++)
            {
                if (chkWorkingDays.Items[i].Text == lblworkingDay.Text)
                {
                    chkWorkingDays.Items[i].Attributes.Add("Class", "DaybtnClick");
                    chkWorkingDays.Items[i].Selected = true;
                    if (lblisHoliday.Text == "N")
                    {
                        ChkHoliday.Checked = false;
                    }
                    else
                    {
                        ChkHoliday.Checked = true;

                    }

                    chkWorkingDays.Items[i].Enabled = true;
                }
                else
                {
                    chkWorkingDays.Items[i].Attributes.Add("Class", "DaybtnNone");


                }
            }
            sameWorking.Visible = false;

            Label lblfromTime = (Label)gvrow.FindControl("lblfromTime");
            txtFromTime.Text = lblfromTime.Text.Trim();
            Label lbltoTime = (Label)gvrow.FindControl("lbltoTime");
            txtToTime.Text = lbltoTime.Text.Trim();

            ViewState["workingDayId"] = lblworkingDayId.Text.Trim();
            ViewState["isHoliday"] = lblisHoliday.Text.Trim();
            ViewState["workingDay"] = lblworkingDay.Text.Trim();
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

    #region Working Days CheckBox Changed Event
    protected void chkWorkingDays_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["workingDayId"].ToString() != "")
            {

                for (int i = 0; i < chkWorkingDays.Items.Count; i++)
                {
                    if (chkWorkingDays.Items[i].Text == ViewState["workingDay"].ToString())
                    {
                        if (chkWorkingDays.Items[i].Selected == true)
                        {
                            chkWorkingDays.Items[i].Attributes.Add("Class", "DaybtnClick");
                        }
                        chkWorkingDays.Items[i].Enabled = true;
                    }
                    else
                    {
                        chkWorkingDays.Items[i].Attributes.Add("Class", "DaybtnNone");

                    }
                }

            }
            else
            {
                for (int i = 0; i < chkWorkingDays.Items.Count; i++)
                {
                    if (chkWorkingDays.Items[i].Selected)
                    {
                        chkWorkingDays.Items[i].Attributes.Add("Class", "DaybtnClick");
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
    #region Same time Click Event
    protected void LnkSameTime_Click(object sender, EventArgs e)
    {
        try
        {
            chkWorkingDays.Items[1].Selected = true;
            chkWorkingDays.Items[2].Selected = true;
            chkWorkingDays.Items[3].Selected = true;
            chkWorkingDays.Items[4].Selected = true;
            chkWorkingDays.Items[5].Selected = true;
            chkWorkingDays.Items[6].Selected = true;
            chkWorkingDays.Items[0].Selected = true;
            for (int i = 0; i < chkWorkingDays.Items.Count; i++)
            {
                if (chkWorkingDays.Items[i].Selected)
                {
                    chkWorkingDays.Items[i].Attributes.Add("Class", "DaybtnClick");
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
        WorkingDayClear();

    }
    #endregion
    #region Clear
    public void WorkingDayClear()
    {
        sameWorking.Visible = true;
        ViewState["workingDayId"] = "";
        chkWorkingDays.ClearSelection();
        txtFromTime.Text = string.Empty;
        txtToTime.Text = string.Empty;
        DivForm.Visible = false;
        divGv.Visible = true;
        btnSubmit.Text = "Submit";
        ChkHoliday.Checked = false;
    }
    #endregion


    #region Working Day Class

    public class InsertWorkingDay_In
    {
        public List<WorkingDay> LstOfbranchWorkingDays { get; set; }
    }
    public class WorkingDay
    {
        public string branchId { get; set; }
        public string gymOwnerId { get; set; }
        public string workingDay { get; set; }
        public string fromTime { get; set; }
        public string toTime { get; set; }
        public string isHoliday { get; set; }
        public string workingDayId { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }

    }
    #endregion
}