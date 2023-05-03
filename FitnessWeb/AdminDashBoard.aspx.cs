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

public partial class AdminDashBoard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFromDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            GetTrainerReassignDetails();
            ddlTrainerList.ClearSelection();            
            GetTrainers();
            BindTrainnerAttendance();
            divtrainerbased.Visible=false;
        }
    }

    #region Bind Trainner Attendance
    public void BindTrainnerAttendance()
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
                string Endpoint = "trainerAttendance?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                    "&branchId=" + Session["branchId"].ToString() + "&date=" + txtFromDate.Text + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        divtrainerattendance.DataSource = dt;
                        divtrainerattendance.DataBind();
                        divtrainerattendancedatagrid.Visible = true;
                    }
                    else
                    {
                        divtrainerattendance.DataBind();
                        divtrainerattendancedatagrid.Visible = false;
                    }
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
             
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    divtrainerattendancedatagrid.Visible = false;
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Errorresponse.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion
    #region Get Trainers
    public void GetTrainers()
    {
        try
        {
            ddlTrainerList.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "trainer/ddlTrainerList?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "";
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
                            ddlTrainerList.DataSource = dt;
                            ddlTrainerList.DataTextField = "trainerName";
                            ddlTrainerList.DataValueField = "trainerId";
                            ddlTrainerList.DataBind();
                        }
                        else
                        {
                            ddlTrainerList.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlTrainerList.Items.Insert(0, new ListItem("Trainers Name *", "0"));
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
    #region Slot Click 
    protected void lbladminFromTime_Click(object sender, EventArgs e)
    {
        LinkButton lblFromTime_Click = sender as LinkButton;
        DataListItem gvrow = lblFromTime_Click.NamingContainer as DataListItem;
        Label lblslotId = gvrow.FindControl("lbladminslotId") as Label;
        ViewState["SlotId"] = lblslotId.Text;

        //ddlTrainerList.Items.Insert(0, new ListItem("Trainers Name *", "0"));
        //ddlTrainerList.Visible = true;

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
                   "&branchId=" + Session["branchId"].ToString() + "&getdate=" + txtFromDate.Text + "";
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
                            divgvreassign.Visible= true;
                            DataList1.Visible = true;
                            trainerreassignnodata.Visible = false; 
                        }
                        else
                        {
                            DataList1.DataBind();
                            DataList1.Visible = true;
                            divgvreassign.Visible = false;
                            trainerreassignnodata.Visible = true;
                        }

                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        divgvreassign.Visible = false;
                        DataList1.Visible = false;
                        trainerreassignnodata.Visible = true;
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
                string Endpoint = "trainerUserTracking?trainerId=" + ddlTrainerList.SelectedValue + "";
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
        ViewState["SlotId"] = lblslotId.Text;
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
        divUserattendance.Visible = true;
        GetUserList();
        GetUserWorkoutList();
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
                string Endpoint = "trainerUserTracking/GetUserList?trainerId=" + ddlTrainerList.SelectedValue + "&slotId=" + ViewState["SlotId"].ToString() + "";
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
                string Endpoint = "UserWorkOutPlan/GetCategoryTypeBasedonDateDayCategory?userId="+ ddlTrainerList.SelectedValue + " & categoryId="
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
        divUserWorkOutList.Visible = true;        
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
                string Endpoint = "trainerUserTracking/GetUserWorkOutList?trainerId=101503&slotId=" + ViewState["SlotId"].ToString() + "" +
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

    protected void ddlTrainerList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSlot();
        divtrainerbased.Visible = true;
        DivtrainerattentanceForm.Visible = false;
        DivtrainerreassignForm.Visible = false;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        trainerReassignform.Visible = false;
        divtrainerreassigneGv.Visible = true;
        ddlCategoryReassign.Items.Insert(0, new ListItem("Category *", "0"));
        ddlTrainingTypeReassign.Items.Insert(0, new ListItem("TrainingType *", "0"));
        ddlNewTrainerReassign.Items.Insert(0, new ListItem("NewTrainers *", "0"));
        GetTrainer();
        DivtrainerreassignForm.Visible = true;
        DivtrainerattentanceForm.Visible = false;
        GetTrainerReassignDetailsadmin();
    }

    protected void lnkaddattendance_Click(object sender, EventArgs e)
    {
        DivtrainerattentanceForm.Visible = true;
        DivtrainerreassignForm.Visible = false;
        GetTrainersadmin();
        BindTrainnerAttendanceadmin();
    }



    //Trainer Attendance
    #region Bind Trainner Attendance
    public void BindTrainnerAttendanceadmin()
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
                string Endpoint = "trainerAttendance?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                    "&branchId=" + Session["branchId"].ToString() + "&date=" + txtFromDate.Text + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        gvTrainer.DataSource = dt;
                        gvTrainer.DataBind();
                        divtrainerattentanceGv.Visible = true;
                        trainerattendanceform.Visible = false;
                        btnCancel.Visible = true;
                    }
                    else
                    {
                        divtrainerattentanceGv.Visible = false;
                        trainerattendanceform.Visible = true;
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

    #region Get Trainers
    public void GetTrainersadmin()
    {
        try
        {
            ddlTrainerattendanceList.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "trainer/ddlTrainerList?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "";
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
                            ddlTrainerattendanceList.DataSource = dt;
                            ddlTrainerattendanceList.DataTextField = "trainerName";
                            ddlTrainerattendanceList.DataValueField = "trainerId";
                            ddlTrainerattendanceList.DataBind();
                        }
                        else
                        {
                            ddlTrainerattendanceList.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlTrainerattendanceList.Items.Insert(0, new ListItem("Trainers Name *", "0"));
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
    protected void btntrainerattendanceAdd_Click(object sender, EventArgs e)
    {
        divtrainerattentanceGv.Visible = false;
        trainerattendanceform.Visible = true;
        divlogdate.Visible = true;
        divintime.Visible = true;
        divouttime.Visible = false;
    }

    protected void LnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkbtn = sender as ImageButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        Label lbluniqueId = (Label)gvrow.FindControl("lbluniqueId");
        Label lblempId = (Label)gvrow.FindControl("lblempId");
        Label lblOutTime = (Label)gvrow.FindControl("lblOutTime");
        Label lblinTime = (Label)gvrow.FindControl("lblinTime");
        divouttime.Visible = false;
        if (lblOutTime.Text != "-")
        {
            txtShiftEndTime.Text = lblOutTime.Text.Trim();
        }
        if (lblinTime.Text != "-")
        {
            txtShiftStartTime.Text = lblinTime.Text.Trim();
            divouttime.Visible = true;
        }

        ViewState["TrainerId"] = lbluniqueId.Text.Trim();
        ddlTrainerattendanceList.SelectedValue = lblempId.Text.Trim();
        divtrainerattentanceGv.Visible = false;
        trainerattendanceform.Visible = true;
        divlogdate.Visible = false;
        divintime.Visible = true;

        btnSubmit.Text = "Update";
        ddlTrainerattendanceList.Enabled = false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        divtrainerattentanceGv.Visible = true;
        trainerattendanceform.Visible = false;
        if (btnSubmit.Text == "Submit")
        {

            InsertTrainer();
        }
        else
        {
            UpdateTrainer();
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        TrainerClear();
    }

    #region Insert Function 
    public void InsertTrainer()
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
                var Insert = new TrainerInsert()
                {
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    empId = ddlTrainerattendanceList.SelectedValue,
                    logDate = txtlogDate.Text,
                    inTime = txtShiftStartTime.Text,
                    createdBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("trainerAttendance/insert", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindTrainnerAttendanceadmin();
                       
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
                TrainerClear();
                BindTrainnerAttendance();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region Update Function 
    public void UpdateTrainer()
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
                var Insert = new TrainerInsert()
                {
                    UniqueId = ViewState["TrainerId"].ToString(),
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    empId = ddlTrainerattendanceList.SelectedValue,
                    inTime = txtShiftStartTime.Text,
                    OutTime = txtShiftEndTime.Text,
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("trainerAttendance/update", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindTrainnerAttendanceadmin();

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
                TrainerClear();
                BindTrainnerAttendance();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region Trainer Insert
    public class TrainerInsert
    {
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string UniqueId { get; set; }
        public string empId { get; set; }
        public string logDate { get; set; }
        public string inTime { get; set; }
        public string OutTime { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
    }
    #endregion

    public void TrainerClear()
    {
        ddlTrainerattendanceList.ClearSelection();
        txtShiftStartTime.Text = string.Empty;
        txtShiftEndTime.Text = string.Empty;
        txtlogDate.Text = string.Empty;
        trainerattendanceform.Visible = false;
        divtrainerattentanceGv.Visible = true;
        btnSubmit.Text = "Submit";
        divlogdate.Visible = true;
        divintime.Visible = true;
        divouttime.Visible = false;
        ddlTrainerattendanceList.Enabled = true;

    }
    //Trainer Attendance    //Trainer Reassign

    #region Add Click
    protected void btnAddReassign_Click(object sender, EventArgs e)
    {
        // ViewSlotList.Visible = false;
        divtrainerreassigneGv.Visible = false;
        trainerReassignform.Visible = true;
        GetTrainer();
    }
    #endregion
    #region Get Trainer
    public void GetTrainerReassignDetailsadmin()
    {
        try
        {
            //ddlOldTrainerReassign.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "trainerReassign/getddlDetails?queryType=GetTranierReassignDetails&gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "&date=" + txtFromDate.Text + "";
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
                            gvTrainerReassign.DataSource = dt;
                            gvTrainerReassign.DataBind();
                            divtrainerreassigneGv.Visible = true;
                        }
                        else
                        {
                            gvTrainerReassign.DataBind();
                            divtrainerreassigneGv.Visible = false;
                        }

                    }
                    else
                    {
                        divtrainerreassigneGv.Visible = false;
                        trainerReassignform.Visible = true;
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    divtrainerreassigneGv.Visible = false;
                    trainerReassignform.Visible = true;
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
    public void GetTrainer()
    {
        try
        {
            ddlOldTrainerReassign.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "trainerReassign/getddlDetails?queryType=GetAbsentTrainers&gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
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
                            ddlOldTrainerReassign.DataSource = dt;
                            ddlOldTrainerReassign.DataTextField = "trainerName";
                            ddlOldTrainerReassign.DataValueField = "trainerId";
                            ddlOldTrainerReassign.DataBind();
                        }
                        else
                        {
                            ddlOldTrainerReassign.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlOldTrainerReassign.Items.Insert(0, new ListItem("Trainer *", "0"));
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
    protected void ddlOldTrainer_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCategory();
    }

    #region Get Category
    public void GetCategory()
    {
        try
        {
            ddlCategoryReassign.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "trainerReassign/getddlDetails?queryType=GetAbsentSpecialist&gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "&trainerId=" + ddlOldTrainerReassign.SelectedValue + "";
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
                            ddlCategoryReassign.DataSource = dt;
                            ddlCategoryReassign.DataTextField = "categoryName";
                            ddlCategoryReassign.DataValueField = "categoryId";
                            ddlCategoryReassign.DataBind();
                        }
                        else
                        {
                            ddlCategoryReassign.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlCategoryReassign.Items.Insert(0, new ListItem("Category *", "0"));
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
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlCategoryReassign.SelectedValue!= "0")
        {
            GetTrainingType();
        }        

    }
    #region Get TrainingType
    public void GetTrainingType()
    {
        try
        {
            ddlTrainingTypeReassign.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "trainerReassign/getddlDetails?queryType=GetAbsentTrainingType&gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "&trainerId=" + ddlOldTrainerReassign.SelectedValue + " " +
                   "&categoryId=" + ddlCategoryReassign.SelectedValue + "";
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
                            ddlTrainingTypeReassign.DataSource = dt;
                            ddlTrainingTypeReassign.DataTextField = "trainingTypeName";
                            ddlTrainingTypeReassign.DataValueField = "trainingTypeId";
                            ddlTrainingTypeReassign.DataBind();
                        }
                        else
                        {
                            ddlTrainingTypeReassign.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlTrainingTypeReassign.Items.Insert(0, new ListItem("TrainingType *", "0"));
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

    protected void ddlTrainingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTrainingTypeReassign.SelectedValue != "0")
        {
            GetSlotReassign();
            divSlotList.Visible = true;
        }
    }
    #region Get Slot
    public void GetSlotReassign()
    {
        try
        {
            chkSlotList.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "trainerReassign/getddlDetails?queryType=GetAbsentTrainerSlot&gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "&trainerId=" + ddlOldTrainerReassign.SelectedValue + " " +
                   "&categoryId=" + ddlCategoryReassign.SelectedValue + "&trainingTypeId=" + ddlTrainingTypeReassign.SelectedValue + "";
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
                            chkSlotList.DataSource = dt;
                            chkSlotList.DataTextField = "SlotTime";
                            chkSlotList.DataValueField = "slotId";
                            chkSlotList.DataBind();
                            trainernoslots.Visible = false;
                        }
                        else
                        {
                            chkSlotList.DataBind();
                            trainernoslots.Visible = false;
                        }

                    }
                    else
                    {
                        trainernoslots.Visible = true;
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

    protected void chkSlotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetNewTrainer();
    }
    #region Get NewTrainer
    public void GetNewTrainer()
    {
        try
        {
            string SlotId = string.Empty;
            for (int i = 0; i < chkSlotList.Items.Count; i++)
            {
                if (chkSlotList.Items[i].Selected == true)
                {
                    SlotId += chkSlotList.Items[i].Value + ',';
                }
            }
            ddlNewTrainerReassign.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "trainerReassign/getddlDetails?queryType=GetNewTrainerForAssign&gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "&trainerId=" + ddlOldTrainerReassign.SelectedValue + " " +
                   "&categoryId=" + ddlCategoryReassign.SelectedValue + "&trainingTypeId=" + ddlTrainingTypeReassign.SelectedValue + "" +
                       "&slotId=" + SlotId.ToString().TrimEnd(',') + "";
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
                            ddlNewTrainerReassign.DataSource = dt;
                            ddlNewTrainerReassign.DataTextField = "trainerName";
                            ddlNewTrainerReassign.DataValueField = "trainerId";
                            ddlNewTrainerReassign.DataBind();
                        }
                        else
                        {
                            ddlNewTrainerReassign.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlNewTrainerReassign.Items.Insert(0, new ListItem("New Trainer *", "0"));
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
    protected void btnSubmitReassign_Click(object sender, EventArgs e)
    {
        InsertCategorySlot();
    }


    #region Insert Category Slot
    public void InsertCategorySlot()
    {
        string slotId = string.Empty;

        int Count = 0;

        for (int i = 0; i < chkSlotList.Items.Count; i++)
        {

            if (chkSlotList.Items[i].Selected == true)
            {
                slotId += chkSlotList.Items[i].Value + ",";
                Count = 1;
            }

        }
        if (Count == 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Any One Slot');", true);
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
                InsertCategorySlots Insert = new InsertCategorySlots()
                {
                    lstTrainerReAssign = InsertCategorySlot(Session["gymOwnerId"].ToString(),
                   Session["branchId"].ToString(), slotId.ToString().TrimEnd(','),
                    ddlNewTrainerReassign.SelectedValue, ddlOldTrainerReassign.SelectedValue, ddlCategoryReassign.SelectedValue, ddlTrainingTypeReassign.SelectedValue,
                    Session["userId"].ToString(), txtFromDate.Text, txtToDateReassign.Text)
                };
                HttpResponseMessage response = client.PostAsJsonAsync("trainerReassign/insert", Insert).Result;
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
                        string[] msgs;
                        msgs = ResponseMsg.Split(',');

                        if (msgs.Contains("Trainer Reassign Details Is Already Exists !!!"))
                        {

                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Trainer Reassign Details Is Already Exists !!!');", true);
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        }

                    }
                    GetTrainerReassignDetails();
                    Clear();
                    GetTrainerReassignDetailsadmin();
                }
                else
                {

                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Category Slot Already Exists');", true);
                    }

                }
               
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }

    public static List<CategorySlots> InsertCategorySlot(string gymOwnerId, string branchId,
        string slotId, string newtrainerId, string oldtrainerId, string categoryId, string trainingTypeId, string createdBy, string fromDate, string toDate)
    {
        string[] slotIds;
        slotIds = slotId.Split(',');

        List<CategorySlots> lst = new List<CategorySlots>();
        for (int i = 0; i < slotIds.Count(); i++)
        {
            lst.AddRange(new List<CategorySlots>
            {
                new CategorySlots { gymOwnerId=gymOwnerId,branchId=branchId ,newTrainerId=newtrainerId,oldTrainerId=oldtrainerId,
                slotId=slotIds[i],categoryId=categoryId,trainingTypeId=trainingTypeId,createdBy=createdBy,fromDate=fromDate,toDate=toDate
                }

            }); ;
        }
        return lst;

    }
    #endregion
    protected void btnCancelReassign_Click(object sender, EventArgs e)
    {
        trainerReassignform.Visible = false;
        divtrainerreassigneGv.Visible = true;
        Clear();
    }

    #region Category Slot Class
    public class InsertCategorySlots
    {

        public List<CategorySlots> lstTrainerReAssign { get; set; }
    }
    public class CategorySlots
    {

        public string slotId { get; set; }

        public string gymOwnerId { get; set; }

        public string branchId { get; set; }

        public string categoryId { get; set; }

        public string trainingTypeId { get; set; }

        public string newTrainerId { get; set; }
        public string oldTrainerId { get; set; }

        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string createdBy { get; set; }
    }

    #endregion

    #region Clear
    public void Clear()
    {
        divtrainerreassigneGv.Visible = true;
        trainerReassignform.Visible = false;
        ddlOldTrainerReassign.ClearSelection();
        ddlNewTrainerReassign.ClearSelection();
        ddlTrainingTypeReassign.ClearSelection();
        ddlCategoryReassign.ClearSelection();
        chkSlotList.ClearSelection();
        divSlotList.Visible = false;
        txtFromDateReassign.Text = string.Empty;
        txtToDateReassign.Text = string.Empty;
        ddlOldTrainerReassign.Enabled = true;
        ddlNewTrainerReassign.Enabled = true;
        ddlTrainingTypeReassign.Enabled = true;
        ddlCategoryReassign.Enabled = true;
        btnSubmit.Text = "Submit";
    }
    #endregion

    //Trainer Reassign

    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        GetTrainerReassignDetails();
        BindTrainnerAttendance();
    }

}