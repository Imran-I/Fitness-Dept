using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.UI.HtmlControls;

public partial class DashBoard : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFromDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            GetSlot();
            GetTrainerReassignDetails();
            GetUserOverallAttendance();
            GetTrainerOverall();
            GetTrainerWorkoutList();
            

        }
    }

    #endregion
    #region Get Trainer
    public void GetTrainerReassignDetails()
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
                string Endpoint = "trainerReassign/getddlDetails?queryType=GetTranierReassignDetails&gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
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
                            DataList1.DataSource = dt;
                            DataList1.DataBind();
                        }
                        else
                        {
                            DataList1.DataBind();
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
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion
    #region Get Trainer Over all 
    public void GetTrainerOverall()
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
                string Endpoint = "trainerTracking/GetTrainerWorkOutOverall?trainerId=" + Session["userId"].ToString() + "" +
                    "&date="+txtFromDate.Text+"";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["TrainerDetails"].ToString();
                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            decimal Completed = Convert.ToDecimal(dt.Rows[0]["Completed"].ToString());
                            decimal Total = Convert.ToDecimal(dt.Rows[0]["OverAll"].ToString());
                            decimal Progress = 0;
                            Progress = (Completed / Total) * 100;
                            int val = Convert.ToInt32(Progress);
                            divOverallWorkOut.Style.Add("--value", Convert.ToString(val));
                        }
                        else
                        {


                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    //ddlSlot.Items.Insert(0, new ListItem("Slot List *", "0"));
                }
                else
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
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
    #region Get Trainer Workout List 
    public void GetTrainerWorkoutList()
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
                string Endpoint = "trainerTracking/GetTrainerWorkOutList?trainerId=" + Session["userId"].ToString() + "&date=" + txtFromDate.Text + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["TrainerDetails"].ToString();
                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            dtlTrainerWorkOutList.DataSource = dt;
                            dtlTrainerWorkOutList.DataBind();
                        }
                        else
                        {

                            dtlTrainerWorkOutList.DataBind();

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    //ddlSlot.Items.Insert(0, new ListItem("Slot List *", "0"));
                }
                else
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
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
    //Trainer
    #region Get Slot 
    public void GetSlot()
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
                string Endpoint = "trainerUserTracking?trainerId=" + Session["userId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["TrainerSlot"].ToString();
                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            dtlSlot.DataSource = dt;
                            dtlSlot.DataBind();
                        }
                        else
                        {
                            dtlSlot.DataBind();

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    //ddlSlot.Items.Insert(0, new ListItem("Slot List *", "0"));
                }
                else
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
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
    #region Slot Click 
    protected void lblFromTime_Click(object sender, EventArgs e)
    {
        LinkButton lblFromTime_Click = sender as LinkButton;
        DataListItem gvrow = lblFromTime_Click.NamingContainer as DataListItem;
        Label lblslotId = gvrow.FindControl("lblslotId") as Label;
        Label lbltoTime = gvrow.FindControl("lbltoTime") as Label;
        Label lblcategoryId = gvrow.FindControl("lblcategoryId") as Label;
        Label lblcategoryName = gvrow.FindControl("lblcategoryName") as Label;
        Label lbltrainingTypeName = gvrow.FindControl("lbltrainingTypeName") as Label;
        ViewState["SlotId"] = lblslotId.Text;
        ViewState["fromTime"] = lblFromTime_Click.Text;
        ViewState["toTime"] = lbltoTime.Text;
        ViewState["TcategoryId"] = lblcategoryId.Text;
        lblTcategoryName.Text = lblcategoryName.Text;
        lblTtrainingTypeName.Text = lbltrainingTypeName.Text;
        for (int i = 0; i < dtlSlot.Items.Count; i++)
        {
            LinkButton lnk = dtlSlot.Items[i].FindControl("lblFromTime") as LinkButton;
            Label slot = dtlSlot.Items[i].FindControl("lblslotId") as Label;

            if (lblslotId.Text == slot.Text)
            {
                lnk.CssClass = "ddlSlotBtnSelect";

            }
            else
            {
                lnk.CssClass = "ddlSlotBtn";
            }
        }
        GetUserList();
        GetUserWorkoutList();
        divTrainerWorkout.Visible = true;
    }

    #endregion
    #region Get User 
    public void GetUserList()
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
                string Endpoint = "trainerUserTracking/GetUserList?trainerId=" + Session["userId"].ToString() + "&slotId=" + ViewState["SlotId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["UserList"].ToString();
                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            dtlUser.DataSource = dt;
                            dtlUser.DataBind();
                        }
                        else
                        {
                            dtlUser.DataBind();

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                }
                else
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
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
    #region Get User Over all attendance 
    public void GetUserOverallAttendance()
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
                string Endpoint = "trainerUserTracking/GetUserOverallattendance?trainerId=" + Session["userId"].ToString() + "&date=" + txtFromDate.Text + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["OverAll"].ToString();
                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            lblOverall.Text =   dt.Rows[0]["OVERALL"].ToString();
                            lblPresent.Text =   dt.Rows[0]["Present"].ToString();
                            lblAbsent.Text =   dt.Rows[0]["Absent"].ToString();
                            decimal Present = Convert.ToDecimal(dt.Rows[0]["Present"].ToString());
                            decimal Total = Convert.ToDecimal(dt.Rows[0]["OVERALL"].ToString());
                            decimal Progress = 0;
                            Progress = (Present / Total) * 100;
                            int val = Convert.ToInt32(Progress);
                            spoverll.InnerText = val.ToString();
                        }
                        else
                        {
                            

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    //ddlSlot.Items.Insert(0, new ListItem("Slot List *", "0"));
                }
                else
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
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
    #region Bind WorkOut
    public void BindWorkOutList()
    {
        try
        {
            DateTime TodayDate = DateTime.Parse(txtFromDate.Text);
            string s = TodayDate.DayOfWeek.ToString();
            string day = s.Substring(0, 2);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "UserWorkOutPlan/GetCategoryTypeBasedonDateDayCategory?userId=" + ViewState["userId"].ToString() + "&categoryId="
                    + ViewState["categoryId"].ToString() +
                    "&date=" + txtFromDate.Text + "&day=" + day + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;

                string Response;
                var Locresponse = response.Content.ReadAsStringAsync().Result;
                int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());

                if (response.IsSuccessStatusCode)
                {
                    if (statusCode == 1)
                    {
                        string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();

                        List<WorkItemGet> WorkItemGet = JsonConvert.DeserializeObject<List<WorkItemGet>>(ResponseMsg);
                        dtlWorkOut.DataSource = WorkItemGet;
                        dtlWorkOut.DataBind();
                        divWorkOutDtl.Visible = true;
                    }
                    else
                    {
                        Response = JObject.Parse(Locresponse)["Response"].ToString();
                        dtlWorkOut.DataBind();
                        divWorkOutDtl.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Response.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    divWorkOutDtl.Visible = false;
                    Response = JObject.Parse(Locresponse)["Response"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Response.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion
    #region User Click 
    protected void lblfirstName_Click(object sender, EventArgs e)
    {
        LinkButton lblFromTime_Click = sender as LinkButton;
        DataListItem gvrow = lblFromTime_Click.NamingContainer as DataListItem;
        Label lbluserId = gvrow.FindControl("lbluserId") as Label;
        Label lblcategoryId = gvrow.FindControl("lblcategoryId") as Label;
        ViewState["userId"] = lbluserId.Text;
        ViewState["categoryId"] = lblcategoryId.Text;
        BindWorkOutList();
    }
    #endregion
    #region Get  User Workout List
    public void GetUserWorkoutList()
    {
        try
        {
            DateTime TodayDate = DateTime.Parse(txtFromDate.Text);
            string s = TodayDate.DayOfWeek.ToString();
            string day = s.Substring(0, 2);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "trainerUserTracking/GetUserWorkOutList?trainerId=" + Session["userId"].ToString() + "&slotId=" + ViewState["SlotId"].ToString() + "" +
                    "&date=" + txtFromDate.Text + "&day=" + day + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["UserList"].ToString();
                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        List<UserList> lst = JsonConvert.DeserializeObject<List<UserList>>(ResponseMsg);

                        if (dt.Rows.Count > 0)
                        {
                            dtlUserList.DataSource = lst;
                            dtlUserList.DataBind();
                            divUserWorkOutList.Visible = true;

                        }
                        else
                        {
                            dtlUserList.DataBind();
                            divUserWorkOutList.Visible = false;
                        }
                    }
                    else
                    {
                        divUserWorkOutList.Visible = false;

                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                }
                else
                {
                    divUserWorkOutList.Visible = false;
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
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
    #region Workout Type DataBound
    protected void dtlWorkOutType_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            var Work = e.Item.DataItem as WorkItemGet;
            var dtlWorkOutList = e.Item.FindControl("dtlWorkOutList") as DataList;
            Label lblworkoutCatTypeId = e.Item.FindControl("lblworkoutCatTypeId") as Label;
            DateTime TodayDate = DateTime.Parse(txtFromDate.Text);
            string s = TodayDate.DayOfWeek.ToString();
            string day = s.Substring(0, 2);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                    string Endpoint = "UserWorkOutPlan/GetWorkoutTypeBasedonDateDay?userId=" + ViewState["userId"].ToString() + "&workoutCatTypeId="
                        + lblworkoutCatTypeId.Text + "&date=" + txtFromDate.Text + "&day=" + day + "";
                    HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                    string Response;
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());

                    if (response.IsSuccessStatusCode)
                    {
                        if (statusCode == 1)
                        {
                            string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                            dtlWorkOutList.DataSource = dt;
                            dtlWorkOutList.DataBind();
                        }
                        else
                        {
                            Response = JObject.Parse(Locresponse)["Response"].ToString();
                            //divGv.Visible = false;
                            dtlWorkOut.DataBind();
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Response.ToString().Trim() + "');", true);
                        }
                    }
                    else
                    {
                        Response = JObject.Parse(Locresponse)["Response"].ToString();
                        //divGv.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Response.ToString().Trim() + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
            }


        }
    }
    #endregion
    #region Bind WorkoutSets

    protected void dtlWorkOutList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            var Work = e.Item.DataItem as WorkItemGet;
            var dtlWorkOutSets = e.Item.FindControl("dtlWorkOutSets") as DataList;
            Label lblworkoutCatTypeId = e.Item.FindControl("lblworkoutCatTypeId") as Label;
            Label lblworkoutTypeId = e.Item.FindControl("lblworkoutTypeId") as Label;



            try
            {

                DateTime TodayDate = DateTime.Parse(txtFromDate.Text);
                string s = TodayDate.DayOfWeek.ToString();
                string day = s.Substring(0, 2);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                    string Endpoint = "UserWorkOutPlan/GetSetTypeBasedonDate?userId=" + ViewState["userId"].ToString() + "&workoutCatTypeId=" + lblworkoutCatTypeId.Text + "" +
                        "&workoutTypeId=" + lblworkoutTypeId.Text + "&date=" + txtFromDate.Text + "&day=" + day + "";
                    HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                    string Response;
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());

                    if (response.IsSuccessStatusCode)
                    {
                        if (statusCode == 1)
                        {
                            string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                            dtlWorkOutSets.DataSource = dt;
                            dtlWorkOutSets.DataBind();
                        }
                        else
                        {
                            Response = JObject.Parse(Locresponse)["Response"].ToString();
                            dtlWorkOut.DataBind();
                            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Response.ToString().Trim() + "');", true);
                        }
                    }
                    else
                    {
                        Response = JObject.Parse(Locresponse)["Response"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Response.ToString().Trim() + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
            }


        }
    }
    #endregion
    #region Insert WorkoutTracking

    protected void chkFinished_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox lnkbtn = sender as CheckBox;
        DataListItem gvrow = lnkbtn.NamingContainer as DataListItem;
        Label lblcsetType = (Label)gvrow.FindControl("lblcsetType");
        Label lblReps = (Label)gvrow.FindControl("lblReps");
        Label lblWeight = (Label)gvrow.FindControl("lblWeight");
        Label lblworkoutCatTypeId = (Label)gvrow.FindControl("lblworkoutCatTypeId");
        Label lblworkoutTypeId = (Label)gvrow.FindControl("lblworkoutTypeId");
        Label lblbookingId = (Label)gvrow.FindControl("lblbookingId");
        Label lbluserId = (Label)gvrow.FindControl("lbluserId");
        InsertBranch(lblworkoutCatTypeId.Text, lblworkoutTypeId.Text, lblbookingId.Text, lblcsetType.Text, lblReps.Text, lblWeight.Text, lbluserId.Text);
    }

    #region InsertWorkoutTracking
    public void InsertBranch(string workoutCatTypeId, string workoutTypeId, string bookingId, string setType, string noOfReps,
            string weight, string userId)
    {
        try
        {
            DateTime TodayDate = DateTime.Now;
            string s = TodayDate.DayOfWeek.ToString();
            string day = s.Substring(0, 2);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new InsertWorkoutTracking()
                {
                    workoutCatTypeId = workoutCatTypeId.Trim(),
                    workoutTypeId = workoutTypeId.Trim(),
                    bookingId = bookingId.Trim(),
                    date = TodayDate.ToString("yyyy-MM-dd").Trim(),
                    day = day.Trim(),
                    setType = setType.Trim(),
                    noOfReps = noOfReps.Trim(),
                    weight = weight.Trim(),
                    userId = userId.Trim(),
                    createdBy = Session["userId"].ToString().Trim()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("UserWorkOutTracking/insert", Insert).Result;
                var Fitness = response.Content.ReadAsStringAsync().Result;
                int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();
                if (response.IsSuccessStatusCode)
                {

                    if (StatusCode == 1)
                    {
                        GetUserWorkoutList();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #endregion
    #region Workout Done Click
    protected void chkWorkOutDone_CheckedChanged(object sender, EventArgs e)
    {
        UpdateTrainerWorkout();
    }
    #endregion
    #region Update Trainer WorkOut
    public void UpdateTrainerWorkout()
    {
        try
        {
            DateTime TodayDate = DateTime.Now;
            string s = TodayDate.DayOfWeek.ToString();
            string day = s.Substring(0, 2);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new UpdateTrainerWorkOut()
                {
                    trainerId = Session["userId"].ToString(),
                    slotId = ViewState["SlotId"].ToString(),
                    categoryId = ViewState["TcategoryId"].ToString(),
                    date = txtFromDate.Text,
                    createdBy = Session["userId"].ToString().Trim()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("trainerTracking/update", Insert).Result;
                var Fitness = response.Content.ReadAsStringAsync().Result;
                int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();
                if (response.IsSuccessStatusCode)
                {

                    if (StatusCode == 1)
                    {
                        GetUserWorkoutList();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region UserList DataBound
    protected void dtlUserList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {

            var list = e.Item.DataItem as UserList;
            DataList dataList = e.Item.FindControl("dtlUserWorkOutCat") as DataList;
            dataList.DataSource = list.UserWorkOutList;
            dataList.DataBind();
            HtmlControl div = e.Item.FindControl("progressbar") as HtmlControl;
            decimal yes = Convert.ToDecimal(list.YesStatus);
            decimal No = Convert.ToDecimal(list.NoStatus);
            decimal Total = yes + No;
            decimal Progress = 0;
            Progress = (yes / Total) * 100;
            int val = Convert.ToInt32(Progress);
            div.Style.Add("--value", Convert.ToString(val));
        }
    }
    #endregion
    #region WorkoutTracking Class

    public class WorkItemGet
    {
        public string workoutCatTypeId { get; set; }
        public string workoutCatTypeName { get; set; }
        public List<WorkOutList> WorkOutList { get; set; }

    }
    public class WorkOutList
    {
        public string workoutTypeId { get; set; }
        public string workoutType { get; set; }
        public string activeStatus { get; set; }


    }

    public class InsertWorkoutTracking
    {
        public string workoutCatTypeId { get; set; }
        public string workoutTypeId { get; set; }
        public string bookingId { get; set; }
        public string date { get; set; }
        public string day { get; set; }
        public string setType { get; set; }
        public string noOfReps { get; set; }
        public string weight { get; set; }
        public string userId { get; set; }
        public string createdBy { get; set; }
    }


    public class UserList
    {
        public string userId { get; set; }
        public string firstName { get; set; }
        public string NoStatus { get; set; }
        public string YesStatus { get; set; }
        public List<UserWorkOutList> UserWorkOutList { get; set; }

    }
    public class UserWorkOutList
    {
        public string workoutCatTypeId { get; set; }
        public string workOutName { get; set; }
        public string sets { get; set; }
        public string workoutType { get; set; }
        public string Date { get; set; }
        public string completedStatus { get; set; }


    }

    #endregion
    #region User Present Chk CheckChange
    protected void chkPresent_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkPresent = sender as CheckBox;
        DataListItem gvrow = chkPresent.NamingContainer as DataListItem;
        Label lbluserId = gvrow.FindControl("lbluserId") as Label;
        ViewState["userId"] = lbluserId.Text;
        UpdateUserAttendance();
        GetUserOverallAttendance();
    }
    #endregion
    #region Update User Attendance 
    public void UpdateUserAttendance()
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
                var Insert = new UserAttendance()
                {
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    userId = ViewState["userId"].ToString(),
                    logDate = txtFromDate.Text.ToString(),
                    OutTime = ViewState["toTime"].ToString(),
                    inTime = ViewState["fromTime"].ToString(),
                    createdBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("userAttendance/updateByTrainer", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        GetUserList();
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
    #region Update Attendance Class
    public class UserAttendance
    {
        public string gymOwnerId { get; set; }
        public string userId { get; set; }
        public string logDate { get; set; }
        public string branchId { get; set; }
        public string inTime { get; set; }
        public string OutTime { get; set; }
        public string createdBy { get; set; }

    }
    #endregion
    #region Update Trainer Workout Class
    public class UpdateTrainerWorkOut
    {
        public string trainerId { get; set; }
        public string slotId { get; set; }
        public string categoryId { get; set; }
        public string date { get; set; }
        public string createdBy { get; set; }

    }
    #endregion


    
}