using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Master_WorkOutPlan_TranWorkoutPlan : System.Web.UI.Page
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
            Session["WorkOutTypes"] = "";
            Session["WorkOutTypeText"] = "";
            btnWorkOutplan.Style.Remove("background-color");
            btnWorkOutplan.Style.Remove("color");
            btnWorkOutplan.Style.Remove("border-top");
            btnWorkOutplan.Style.Remove("border-right");
            btnWorkOutplan.Style.Remove("border-left");
            btnWorkOutplan.Style.Add("border-bottom", "2px solid #c3c4c6");
            btnDietPlan.Style.Remove("border-bottom");
            btnDietPlan.Style.Add("border-top-left-radius", "6px");
            btnDietPlan.Style.Add("border-top-right-radius", "6px");
            btnDietPlan.Style.Add("border-top", "2px solid #a2a4a796");
            btnDietPlan.Style.Add("border-right", "2px solid #a2a4a796");
            btnDietPlan.Style.Add("border-right", "2px solid #a2a4a796");
            btnDietPlan.Style.Add("background-color", "#37a737");
            btnDietPlan.Style.Add("color", "#fff");

            btnDietPlan.Style.Add("border-left", "2px solid #a2a4a796");
            //DateTime FromDate = Convert.ToDateTime(Session["fromDateWorkOutplan"].ToString());
            //DateTime ToDate = Convert.ToDateTime(Session["toDateWorkOutplan"].ToString());
            txtFromDate.Text = Session["fromDateWorkOutplan"].ToString();
            txtToDate.Text = Session["toDateWorkOutplan"].ToString();
            txtTotalCalories.Text = Session["TDEEWorkOutplan"].ToString();
            BindDietType();
            divWorkOutPlan.Visible = false;
            divDietPlan.Visible = true;
            divDietListView.Visible = false;
            btnShowSubmitButton.Visible = false;
            ViewState["Flag"] = "0";
            ViewState["Flags"] = "0";
            ViewState["FlagApprove"] = "0";
            List<ListWorkOutTypeSub> Options = new List<ListWorkOutTypeSub>();
            Session["OptionsList"] = Options;
            if (Session["ApprovedStatus"].ToString() == "A")
            {
                dietPlanForm.Visible = false;
                GetUserItemListFOrApproved();

            }
            else
            {
                dietPlanForm.Visible = true;
                GetUserItemList();

            }

        }

        if (ViewState["FlagApprove"].ToString() == "2")
        {
            btnApprove.Visible = true;
        }
        else
        {
            btnApprove.Visible = false;
        }

        if (ViewState["Flags"].ToString() == "3")
        {
            CreateChecklistWithOption(Session["WorkOutTypes"].ToString(), Session["WorkOutTypeText"].ToString());

        }
        if (ViewState["Flag"].ToString() == "2")
        {
            CreateDynamicButtons();

        }




    }
    #region Diet plan  Button
    protected void btnDietPlan_Click(object sender, EventArgs e)
    {
        AddWorkOutPlan.Visible = false;
        btnWorkOutplan.Style.Remove("background-color");
        btnWorkOutplan.Style.Remove("color");
        btnWorkOutplan.Style.Remove("border-top");
        btnWorkOutplan.Style.Remove("border-right");
        btnWorkOutplan.Style.Remove("border-left");
        btnWorkOutplan.Style.Add("border-bottom", "2px solid #c3c4c6");
        btnDietPlan.Style.Remove("border-bottom");
        btnDietPlan.Style.Add("border-top-left-radius", "6px");
        btnDietPlan.Style.Add("border-top-right-radius", "6px");
        btnDietPlan.Style.Add("border-top", "2px solid #a2a4a796");
        btnDietPlan.Style.Add("border-right", "2px solid #a2a4a796");
        btnDietPlan.Style.Add("border-left", "2px solid #a2a4a796");
        btnDietPlan.Style.Add("background-color", "#37a737");
        btnDietPlan.Style.Add("color", "#fff");
        txtTotalCalories.Text = Session["TDEEWorkOutplan"].ToString();
        if (Session["ApprovedStatus"].ToString() == "A")
        {
            dietPlanForm.Visible = false;
            GetUserItemListFOrApproved();

        }
        else
        {
            dietPlanForm.Visible = true;
            GetUserItemList();

        }
        divWorkOutPlan.Visible = false;
        divDietPlan.Visible = true;
    }
    #endregion
    #region Work Out Plan Button
    protected void btnWorkOutplan_Click(object sender, EventArgs e)
    {
        AddWorkOutPlan.Visible = false;
        btnDietPlan.Style.Remove("background-color");
        btnDietPlan.Style.Remove("color");
        btnDietPlan.Style.Remove("border-top");
        btnDietPlan.Style.Remove("border-right");
        btnDietPlan.Style.Remove("border-left");
        btnDietPlan.Style.Add("border-bottom", "2px solid #c3c4c6");
        btnWorkOutplan.Style.Remove("border-bottom");
        btnWorkOutplan.Style.Add("border-top-left-radius", "6px");
        btnWorkOutplan.Style.Add("border-top-right-radius", "6px");
        btnWorkOutplan.Style.Add("border-top", "2px solid #a2a4a796");
        btnWorkOutplan.Style.Add("border-right", "2px solid #a2a4a796");
        btnWorkOutplan.Style.Add("border-left", "2px solid #a2a4a796");
        btnWorkOutplan.Style.Add("background-color", "#37a737");
        btnWorkOutplan.Style.Add("color", "#fff");
        BindWorkingDay();
        GetUserWorkOutItemList();
        //BindWorkOutType();
        divWorkOutPlan.Visible = true;
        divDietPlan.Visible = false;
        //DateTime FromDate = Convert.ToDateTime(Session["fromDateWorkOutplan"].ToString());
        //DateTime ToDate = Convert.ToDateTime(Session["toDateWorkOutplan"].ToString());
        btnDietPlan.Style.Add("border-left", "2px solid #a2a4a796");
        //txtWorKFromdate.Text = FromDate.ToString("yyyy-MM-dd");
        //txtWorkTodate.Text = ToDate.ToString("yyyy-MM-dd");
        txtWorKFromdate.Text = Session["fromDateWorkOutplan"].ToString();
        txtWorkTodate.Text = Session["toDateWorkOutplan"].ToString();
        if (Session["ApprovedStatus"].ToString() == "A")
        {
            WorkOutPlanForm.Visible = false;
            divWorkOutplanList.Visible = false;
        }
        else
        {
            divWorkOutplanList.Visible = true;
            WorkOutPlanForm.Visible = true;
        }
    }
    #endregion

    #region Diet Plan 

    #region Bind Diet Type
    public void BindDietType()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "dietTypeMaster/getDropDropDetails";
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
                            ddlDietType.DataSource = dt;
                            ddlDietType.DataTextField = "dietTypeName";
                            ddlDietType.DataValueField = "dietTypeId";
                            ddlDietType.DataBind();
                        }
                        else
                        {
                            ddlDietType.DataBind();
                        }
                        ddlDietType.Items.Insert(0, new ListItem("Diet Type *", "0"));
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
    #region Bind Food Item
    public void BindFoodItem(string mealType, string FoodItemId, string dietTypeId, string dietTimeId, string uniqueId)
    {
        try
        {
            ddlFoodItemList.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sUrl = Session["BaseUrl"].ToString().Trim() + "GetFoodItemBasedOnMealType?mealType=" + mealType.Trim() + "&dietTypeId=" + dietTypeId.Trim() + ""
                    + "&uniqueId=" + uniqueId + "&userId=" + Session["UserIdWorkOutplan"].ToString() + ""
                    + "&bookingId=" + Session["BookingIDWorkOutplan"].ToString() + "&gymOwnerId=" + Session["gymOwnerId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        dt.Columns.Add(new DataColumn("foodItems", System.Type.GetType("System.String"), "foodItemId + '~' + dietTimeId"));
                        if (dt.Rows.Count > 0)
                        {
                            ddlFoodItemList.DataSource = dt;
                            ddlFoodItemList.DataTextField = "foodItemName";
                            ddlFoodItemList.DataValueField = "foodItems";
                            ddlFoodItemList.DataBind();
                            ddlFoodItemList.SelectedValue = FoodItemId + '~' + dietTimeId;
                        }
                        else
                        {
                            ddlFoodItemList.DataBind();
                        }
                        ddlFoodItemList.Items.Insert(0, new ListItem("Select", "0"));
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
    public void BindFoodItem(string mealType, string dietTypeId)
    {
        try
        {
            ddlFoodItemList.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sUrl = Session["BaseUrl"].ToString().Trim() + "GetFoodItemBasedOnMealType?mealType=" + mealType.Trim() + "&dietTypeId=" + dietTypeId.Trim() + ""
                  + "&uniqueId=0&userId=" + Session["UserIdWorkOutplan"].ToString() + ""
                  + "&bookingId=" + Session["BookingIDWorkOutplan"].ToString() + "&gymOwnerId=" + Session["gymOwnerId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        dt.Columns.Add(new DataColumn("foodItems", System.Type.GetType("System.String"), "foodItemId + '~' + dietTimeId"));
                        if (dt.Rows.Count > 0)
                        {
                            ddlFoodItemList.DataSource = dt;
                            ddlFoodItemList.DataTextField = "foodItemName";
                            ddlFoodItemList.DataValueField = "foodItems";
                            ddlFoodItemList.DataBind();

                        }
                        else
                        {
                            ddlFoodItemList.DataBind();
                        }
                        ddlFoodItemList.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    else
                    {
                        AddBenefits.Visible = false;
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    AddBenefits.Visible = false;
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

    #region Btn Submit Click
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Insert();
        GetUserItemList();
    }
    #endregion
    #region Insert To get user Diet Plan
    public void Insert()
    {

        try
        {
            //DateTime FromDate = Convert.ToDateTime(Session["fromDateWorkOutplan"].ToString());

            //DateTime ToDate = Convert.ToDateTime(Session["toDateWorkOutplan"].ToString());
            //if (Convert.ToDateTime(txtFromDate.Text) < FromDate)
            //{
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Enter From Date Between " + FromDate.ToString("dd-MM-yyyy") + " And " + ToDate.ToString("dd-MM-yyyy") + "');", true);
            //    return;
            //}
            //if (ToDate < Convert.ToDateTime(txtToDate.Text))
            //{
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Enter From Date Between " + FromDate.ToString("dd-MM-yyyy") + " And " + ToDate.ToString("dd-MM-yyyy") + "');", true);
            //    return;
            //}
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string sUrl = Session["BaseUrl"].ToString().Trim() + "userFoodMenu/insertUserDietFood?userId=" + Session["UserIdWorkOutplan"].ToString() + ""
                    + "&bookingId=" + Session["BookingIDWorkOutplan"].ToString() + "&dietType=" + ddlDietType.SelectedValue + "&"
                    + "wakeUpTime=" + txtWakeUpTime.Text.Trim() + "&fromDate=" + txtFromDate.Text + "&toDate=" + txtToDate.Text + ""
                    + "&totalCalories=" + txtTotalCalories.Text.Trim() + "&createdBy=" + Session["userId"].ToString() + "&gymOwnerId=" + Session["gymOwnerId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["UserFoodMenu"].ToString();

                    if (StatusCode == 1)
                    {
                        List<foodItem> foodItem = JsonConvert.DeserializeObject<List<foodItem>>(ResponseMsg);
                        var other = JsonConvert.DeserializeObject<dynamic>(ResponseMsg);
                        var others = JsonConvert.SerializeObject(other);
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(others);
                        var firstItem = foodItem.ElementAt(0);
                        ViewState["dietTypeId"] = dt.Rows[0]["dietTypeId"].ToString();
                        txtFromDate.Enabled = false;
                        txtToDate.Enabled = false;
                        ddlDietType.Enabled = false;
                        txtWakeUpTime.Enabled = false;
                        txtTotalCalories.Enabled = false;
                        DietSubmit.Visible = false;
                        btnShowSubmitButton.Visible = true;
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
    #region Bind Meal Type
    public void BindMealType()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "userFoodMenu/listUserDietFood?userId=" + Session["UserIdWorkOutplan"].ToString() + ""
                    + "&bookingId=" + Session["BookingIDWorkOutplan"].ToString() + "&gymOwnerId=" + Session["gymOwnerId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["UserFoodMenu"].ToString();

                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            gvBindBreakFast.DataSource = dt;
                            gvBindBreakFast.DataBind();
                            divDietListView.Visible = true;
                        }
                        else
                        {
                            gvBindBreakFast.DataBind();
                            divDietListView.Visible = false;
                        }

                    }
                    else
                    {
                        divDietListView.Visible = false;
                        // ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    divDietListView.Visible = false;
                    //  ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + response.ReasonPhrase.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region Get User Diet Plan List
    public void GetUserItemList()
    {
        try
        {
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sUrl = Session["BaseUrl"].ToString().Trim() + "userFoodMenu?userId=" + Session["UserIdWorkOutplan"].ToString() + ""
                    + "&bookingId=" + Session["BookingIDWorkOutplan"].ToString() + "&gymOwnerId=" + Session["gymOwnerId"].ToString() + "";

                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["UserFoodMenu"].ToString();

                    if (StatusCode == 1)
                    {
                        divDietListView.Visible = true;
                        List<foodItem> foodItem = JsonConvert.DeserializeObject<List<foodItem>>(ResponseMsg);
                        var other = JsonConvert.DeserializeObject<dynamic>(ResponseMsg);
                        var others = JsonConvert.SerializeObject(other);
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(others);
                        var firstItem = foodItem.ElementAt(0);
                        ViewState["dietTypeId"] = dt.Rows[0]["dietTypeId"].ToString();
                        lblDietTYpe.Text = dt.Rows[0]["dietTypeName"].ToString();
                        lblTotalCalories.Text= Session["TDEEWorkOutplan"].ToString();
                        Session["foodItem"] = foodItem;
                        BindMealType();
                        dietPlanForm.Visible = false;
                        ViewState["FlagApprove"] = 2;
                        btnApprove.Visible = true;
                    }
                    else
                    {
                        dietPlanForm.Visible = true;
                        divDietListView.Visible = false;
                        // ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    dietPlanForm.Visible = true;
                    divDietListView.Visible = false;
                    // ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + response.ReasonPhrase.ToString().Trim() + "');", true);
                }

            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }

    public void GetUserItemListFOrApproved()
    {
        try
        {
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sUrl = Session["BaseUrl"].ToString().Trim() + "userFoodMenu?userId=" + Session["UserIdWorkOutplan"].ToString() + ""
                    + "&bookingId=" + Session["BookingIDWorkOutplan"].ToString() + "&gymOwnerId=" + Session["gymOwnerId"].ToString() + "";

                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["UserFoodMenu"].ToString();

                    if (StatusCode == 1)
                    {
                        divDietListView.Visible = true;
                        List<foodItem> foodItem = JsonConvert.DeserializeObject<List<foodItem>>(ResponseMsg);
                        var other = JsonConvert.DeserializeObject<dynamic>(ResponseMsg);
                        var others = JsonConvert.SerializeObject(other);
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(others);
                        var firstItem = foodItem.ElementAt(0);
                        ViewState["dietTypeId"] = dt.Rows[0]["dietTypeId"].ToString();
                        lblDietTYpe.Text = dt.Rows[0]["dietTypeName"].ToString();
                        lblTotalCalories.Text = Session["TDEEWorkOutplan"].ToString();
                        Session["foodItem"] = foodItem;
                        BindMealType();
                        foreach (DataListItem item in gvBindBreakFast.Items)
                        {
                            LinkButton btnAdd = item.FindControl("btnAdd") as LinkButton;
                            btnAdd.Visible = false;
                            DataList dtlBreakfast = item.FindControl("dtlBreakfast") as DataList;
                            foreach (DataListItem gvitem in dtlBreakfast.Items)
                            {
                                LinkButton btnEdit = gvitem.FindControl("btnEdit") as LinkButton;
                                btnEdit.Visible = false;
                                LinkButton btnDelete = gvitem.FindControl("btnDelete") as LinkButton;
                                btnDelete.Visible = false;
                            }

                        }
                        ViewState["FlagApprove"] = 2;
                        btnApprove.Visible = true;

                    }
                    else
                    {

                        divDietListView.Visible = false;
                        // ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    divDietListView.Visible = false;
                    // ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + response.ReasonPhrase.ToString().Trim() + "');", true);
                }

            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region Bind Diet Plan gvBindBreakFast_ItemDataBound
    protected void gvBindBreakFast_ItemDataBound(object sender, DataListItemEventArgs e)
    {

        if (e.Item.ItemIndex != -1)
        {
            List<foodItem> foodItems = new List<foodItem>();
            foodItems = (List<foodItem>)Session["foodItem"];
            var foodItem = foodItems.ElementAt(0);
            var dataList = e.Item.FindControl("dtlBreakfast") as DataList;
            LinkButton btnMealType = e.Item.FindControl("btnMealType") as LinkButton;
            if (btnMealType.Text == "Breakfast")
            {
                dataList.DataSource = foodItem.breakFast;
                dataList.DataBind();
            }
            if (btnMealType.Text == "Snacks1")
            {
                dataList.DataSource = foodItem.Snacks1;
                dataList.DataBind();
            }
            if (btnMealType.Text == "Lunch")
            {
                dataList.DataSource = foodItem.Lunch;
                dataList.DataBind();
            }
            if (btnMealType.Text == "Snacks2")
            {
                dataList.DataSource = foodItem.Snacks2;
                dataList.DataBind();

            }
            if (btnMealType.Text == "Dinner")
            {
                dataList.DataSource = foodItem.Dinner;
                dataList.DataBind();
            }
            if (btnMealType.Text == "Snacks3")
            {
                dataList.DataSource = foodItem.Snacks3;
                dataList.DataBind();
            }
            //if (btnMealType.Text == "BreakFast_Alter")
            //{
            //    dataList.DataSource = foodItem.breakFast_Alter;
            //    dataList.DataBind();
            //}
            //if (btnMealType.Text == "Lunch_Alter")
            //{
            //    dataList.DataSource = foodItem.Lunch_Alter;
            //    dataList.DataBind();
            //}
            //if (btnMealType.Text == "Snacks_Alter1")
            //{
            //    dataList.DataSource = foodItem.Snacks_Alter1;
            //    dataList.DataBind();
            //}
            //if (btnMealType.Text == "Dinner_Alter")
            //{
            //    dataList.DataSource = foodItem.Dinner_Alter;
            //    dataList.DataBind();
            //}
            //if (btnMealType.Text == "Snacks_Alter2")
            //{
            //    dataList.DataSource = foodItem.Snacks_Alter2;
            //    dataList.DataBind();
            //}
            //if (btnMealType.Text == "Snacks_Alter3")
            //{
            //    dataList.DataSource = foodItem.Snacks_Alter3;
            //    dataList.DataBind();
            //}

        }

    }
    #endregion

    #region btnMeal Type Click To get True And False Method

    protected void btnShowSubmitButton_Click(object sender, EventArgs e)
    {
        AddBenefits.Visible = false;
        if (DietSubmit.Visible == true)
        {
            btnShowSubmitButton.Text = "Show Button";
            DietSubmit.Visible = false;

        }
        else
        {
            btnShowSubmitButton.Text = "Hide Button";
            DietSubmit.Visible = true;
        }

    }
    #endregion
    #region Btn Add Food Item In List
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        DataListItem gvrow = lnkbtn.NamingContainer as DataListItem;
        DataList dtlBreakfast = (DataList)gvrow.FindControl("dtlBreakfast");
        
        Label lblfromTimes = gvrow.FindControl("lblfromTimes") as Label;
        Label lbltoTimes = gvrow.FindControl("lbltoTimes") as Label;
        Label lblConfigId = (Label)gvrow.FindControl("lblConfigId");
       
        ViewState["fromTime"] = lblfromTimes.Text.Trim();
        ViewState["toTime"] = lbltoTimes.Text.Trim();
        AddBenefits.Visible = true;
        BindFoodItem(lblConfigId.Text.Trim(), ViewState["dietTypeId"].ToString());
        btnSubSubmit.Text = "Submit";
    }
    #endregion
    #region Btn Edit Food Item In List
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        DataListItem gvrow = lnkbtn.NamingContainer as DataListItem;
        Label lblmealType = (Label)gvrow.FindControl("lblmealType");
        Label lbldietTimeId = (Label)gvrow.FindControl("lbldietTimeId");
        Label lblfromTime = (Label)gvrow.FindControl("lblfromTime");
        Label lbltoTime = (Label)gvrow.FindControl("lbltoTime");
        Label lblfoodItemId = (Label)gvrow.FindControl("lblfoodItemId");
        Label lblfoodItemName = (Label)gvrow.FindControl("lblfoodItemName");
        Label lbluniqueId = (Label)gvrow.FindControl("lbluniqueId");
        Label lblUserfoodDietTimeId = (Label)gvrow.FindControl("lblUserfoodDietTimeId");
        ViewState["dietTimeId"] = lbldietTimeId.Text.Trim();
        ViewState["fromTime"] = lblfromTime.Text.Trim();
        ViewState["toTime"] = lbltoTime.Text.Trim();
        ViewState["foodItemId"] = lblfoodItemId.Text.Trim();
        ViewState["foodItemName"] = lblfoodItemName.Text.Trim();
        ViewState["uniqueId"] = lbluniqueId.Text.Trim();
        ViewState["UserfoodDietTimeId"] = lblUserfoodDietTimeId.Text.Trim();

        AddBenefits.Visible = true;
        BindFoodItem(lblmealType.Text.Trim(), lblfoodItemId.Text, ViewState["dietTypeId"].ToString(), lbldietTimeId.Text, lbluniqueId.Text);
        btnSubSubmit.Text = "Update";
    }
    #endregion
    #region Btn Delete Food Item In List
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        DataListItem gvrow = lnkbtn.NamingContainer as DataListItem;
        Label lbluniqueId = (Label)gvrow.FindControl("lbluniqueId");
        Label lblUserfoodDietTimeId = (Label)gvrow.FindControl("lblUserfoodDietTimeId");
        ViewState["UserfoodDietTimeId"] = lblUserfoodDietTimeId.Text.Trim();
        ViewState["uniqueId"] = lbluniqueId.Text.Trim();
        DeleteFoodItem();

    }
    #endregion

    #region Add,Update Delete Food Item Methods
    public void DeleteFoodItem()
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
                var Insert = new AddfoodItem()
                {
                    uniqueId = ViewState["uniqueId"].ToString(),
                    UserfoodDietTimeId = ViewState["UserfoodDietTimeId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("userFoodMenu/delete", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        GetUserItemList();
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
    public void InsertFoodItem()
    {
        try
        {
            string Foods = string.Empty;
            Foods = ddlFoodItemList.SelectedValue;
            string[] food = Foods.Split('~');
            DateTime FromTime = Convert.ToDateTime(ViewState["fromTime"].ToString());
            DateTime ToTime = Convert.ToDateTime(ViewState["toTime"].ToString());
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new AddfoodItem()
                {
                    userId = Session["UserIdWorkOutplan"].ToString(),
                    bookingId = Session["BookingIDWorkOutplan"].ToString(),
                    dietTimeId = food[1],
                    dietTypeId = ViewState["dietTypeId"].ToString(),
                    foodItemId = food[0],
                    foodItemName = ddlFoodItemList.SelectedItem.Text,
                    fromTime = FromTime.ToString("HH:mm"),
                    ToTime = ToTime.ToString("HH:mm"),
                    createdBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("userFoodMenu/insert", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        AddBenefits.Visible = false;
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
    public void UpdateFoodItem()
    {
        try
        {
            string Foods = string.Empty;
            Foods = ddlFoodItemList.SelectedValue;
            string[] food = Foods.Split('~');
            DateTime FromTime = Convert.ToDateTime(ViewState["fromTime"].ToString());
            DateTime ToTime = Convert.ToDateTime(ViewState["toTime"].ToString());
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new AddfoodItem()
                {
                    uniqueId = ViewState["uniqueId"].ToString(),
                    dietTimeId = food[1],
                    foodItemId = food[0],
                    fromTime = FromTime.ToString("HH:mm"),
                    ToTime = ToTime.ToString("HH:mm"),
                    foodItemName = ddlFoodItemList.SelectedItem.Text,
                    UserfoodDietTimeId = ViewState["UserfoodDietTimeId"].ToString(),
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("userFoodMenu/update", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        AddBenefits.Visible = false;
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
    #region Diet Plan Update And Delete BtnSubmit and Cancel Click Class
    protected void btnSubSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubSubmit.Text == "Submit")
        {
            InsertFoodItem();
        }
        else
        {
            UpdateFoodItem();
        }
        GetUserItemList();
    }
    protected void btnSubCancel_Click(object sender, EventArgs e)
    {
        ddlFoodItemList.ClearSelection();
        ddlFoodItemList.Items.Clear();
        AddBenefits.Visible = false;
        ViewState["dietTimeId"] = "";
        ViewState["fromTime"] = "";
        ViewState["toTime"] = "";
        ViewState["foodItemId"] = "";
        ViewState["foodItemName"] = "";
        ViewState["uniqueId"] = "";
    }
    #endregion

    #region Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnShowSubmitButton.Visible = false;
        DietPlanClear();
    }
    #endregion

    #region  Diet Plan Clear
    public void DietPlanClear()
    {
        txtFromDate.Enabled = true;
        txtToDate.Enabled = true;
        ddlDietType.Enabled = true;
        txtWakeUpTime.Enabled = true;
        txtTotalCalories.Enabled = true;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        txtWakeUpTime.Text = string.Empty;
        ddlDietType.ClearSelection();
        txtTotalCalories.Text = string.Empty;
        divDietListView.Visible = false;
        //DateTime FromDate = Convert.ToDateTime(Session["fromDateWorkOutplan"].ToString());
        //DateTime ToDate = Convert.ToDateTime(Session["toDateWorkOutplan"].ToString());
        btnDietPlan.Style.Add("border-left", "2px solid #a2a4a796");
        txtFromDate.Text = Session["fromDateWorkOutplan"].ToString();
        txtToDate.Text = Session["toDateWorkOutplan"].ToString();
    }
    #endregion
    #region Diet Plan Class
    DataTable ConvertToDataTable<TSource>(IEnumerable<TSource> source)
    {
        var props = typeof(TSource).GetProperties();

        var dt = new DataTable();
        dt.Columns.AddRange(
          props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray()
        );

        source.ToList().ForEach(
          i => dt.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray())
        );

        return dt;
    }
    public class foodItem
    {
        public string userId { get; set; }
        public string dietTypeNameId { get; set; }
        public string dietTypeName { get; set; }
        public string dietTypeId { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string branchId { get; set; }
        public string branchName { get; set; }
        public string gymOwnerId { get; set; }
        public string bookingId { get; set; }
        public string breakfastTime { get; set; }
        public List<breakFastDetails> breakFast { get; set; }
        //public List<breakFastDetails> breakFast_Alter { get; set; }
        public string snacks1Time { get; set; }
        public List<breakFastDetails> Snacks1 { get; set; }
        //public List<breakFastDetails> Snacks_Alter1 { get; set; }
        public string lunchTime { get; set; }
        public List<breakFastDetails> Lunch { get; set; }
        //public List<breakFastDetails> Lunch_Alter { get; set; }
        public string snacks2Time { get; set; }
        public List<breakFastDetails> Snacks2 { get; set; }
        //public List<breakFastDetails> Snacks_Alter2 { get; set; }
        public string dinnerTime { get; set; }
        public List<breakFastDetails> Dinner { get; set; }
        //public List<breakFastDetails> Dinner_Alter { get; set; }
        public string snacks3Time { get; set; }
        public List<breakFastDetails> Snacks3 { get; set; }
        //public List<breakFastDetails> Snacks_Alter3 { get; set; }
    }
    public class breakFastDetails
    {
        public string uniqueId { get; set; }
        public string mealType { get; set; }
        public string foodItemId { get; set; }
        public string foodItemName { get; set; }
        public string protein { get; set; }
        public string carbs { get; set; }
        public string fat { get; set; }
        public string servingIn { get; set; }
        public string servingInId { get; set; }
        public string calories { get; set; }
        public string foodDietTimeId { get; set; }
        public string fromTime { get; set; }
        public string toTime { get; set; }
        public string UserfoodDietTimeId { get; set; }

    }
    public class AddfoodItem
    {
        public string uniqueId { get; set; }
        public string bookingId { get; set; }
        public string userId { get; set; }
        public string dietTimeId { get; set; }
        public string foodItemId { get; set; }
        public string foodItemName { get; set; }
        public string dietTypeId { get; set; }
        public string fromTime { get; set; }
        public string ToTime { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
        public string UserfoodDietTimeId { get; set; }


    }

    #endregion

    #region Btnc Close For Popup
    protected void btnFoodClose_Click(object sender, EventArgs e)
    {
        AddBenefits.Visible = false;
    }
    #endregion
    #endregion

    #region Work Out plan
    #region Get User WorkOut Plan List
    public void GetUserWorkOutItemList()
    {
        try
        {
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sUrl = Session["BaseUrl"].ToString().Trim() + "UserWorkOutPlan/GetWorkPlanBasedBookingIdandDay?userId=" + Session["UserIdWorkOutplan"].ToString() + "&bookingId=" + Session["BookingIDWorkOutplan"].ToString() + "";

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
                            gvUserWorkOutPlan.DataSource = dt;
                            gvUserWorkOutPlan.DataBind();
                            DivGridView.Visible = true;

                            ViewState["FlagApprove"] = 2;
                            btnApprove.Visible = true;
                        }
                        else
                        {
                            gvUserWorkOutPlan.DataBind();
                            DivGridView.Visible = false;
                        }
                    }
                    else
                    {

                        DivGridView.Visible = false;
                        WorkOutPlanForm.Visible = true;
                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    DivGridView.Visible = false;
                    WorkOutPlanForm.Visible = true;
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + response.ReasonPhrase.ToString().Trim() + "');", true);
                }

            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
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
                            dtlWorkOutPlanList.DataSource = dt;
                            dtlWorkOutPlanList.DataBind();
                        }
                        else
                        {
                            dtlWorkOutPlanList.DataBind();
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
    #region Bind Work Out plan
    public void BindWorkOutType()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "configMaster/getDropDownDetails?typeId=19";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        List<WorkOutType> WorkOutType = JsonConvert.DeserializeObject<List<WorkOutType>>(ResponseMsg);
                        Session["WorkOutType"] = WorkOutType;
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            //ddlWorkOutType.DataSource = dt;
                            //ddlWorkOutType.DataTextField = "configName";
                            //ddlWorkOutType.DataValueField = "configId";
                            //ddlWorkOutType.DataBind();
                            CreateDynamicButtons();
                        }
                        else
                        {
                            //ddlWorkOutType.DataBind();
                        }
                        // ddlWorkOutType.Items.Insert(0, new ListItem("Work Out Type *", "0"));
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
    public class WorkOutType
    {
        public string configName { get; set; }
        public string configId { get; set; }

    }

    #endregion
    #region Create Dynamic Button For CAtegory
    public void CreateDynamicButtons()
    {
        List<WorkOutType> WorkOutType = new List<WorkOutType>();
        WorkOutType = (List<WorkOutType>)Session["WorkOutType"];
        var MyList = WorkOutType;

        Table myTable = new Table();
        TableRow TR = new TableRow();
        foreach (var item in MyList)
        {
            TableCell TD = new TableCell();
            Button TB1 = new Button();
            TB1.ID = item.configId;
            TB1.CssClass = "btnCancel";
            TB1.Text = item.configName;
            TB1.Click += btnWorkOutType_Click;
            TableCell TD3 = new TableCell();
            TD3.Controls.Add(TB1);
            TR.Controls.Add(TD3);

            myTable.Controls.Add(TR);
        }

        divWorkOut.Controls.Add(myTable);
        Session["slotTable"] = myTable;
        ViewState["Flag"] = "2";
    }
    public void btnWorkOutType_Click(object sender, EventArgs e)
    {
        string WorkOutType = (sender as Button).ID;
        string WorkOutTypeText = (sender as Button).Text;
        List<ListWorkOutTypeSub> Options = (List<ListWorkOutTypeSub>)Session["OptionsList"];
        List<ListWorkOutTypeSub> OptionaParticular = Options.Where(x => x.workoutTypeId == WorkOutType).Select(x => x).ToList();
        if (Session["WorkOutTypes"].ToString() != "")
        {
            string workoutTypeId = string.Empty;
            string workoutType = string.Empty;
            string workoutId = string.Empty;
            string workout = string.Empty;
            string setId = string.Empty;
            string sets = string.Empty;
            string RepId = string.Empty;
            string Reps = string.Empty;
            string weightId = string.Empty;
            string weight = string.Empty;
            string Checked = string.Empty;
            string[] workoutTypes;
            DataTable SETS = (DataTable)Session["Sets"];
            List<WorkOutTypeSub> workOutTypeSub = new List<WorkOutTypeSub>();
            workOutTypeSub = (List<WorkOutTypeSub>)Session["WorkOutTypeSub"];
            var MyList = workOutTypeSub;
            Table myTable = (Table)Session["slotData"];
            for (int i = 0; i < chkSubCategory.Items.Count; i++)
            {
                if (chkSubCategory.Items[i].Selected == true)
                {
                    workoutId += chkSubCategory.Items[i].Value + ",";
                    workout += chkSubCategory.Items[i].Text + ",";
                    Checked += "true" + ",";
                }
                else
                {
                    workoutId += chkSubCategory.Items[i].Value + ",";
                    workout += chkSubCategory.Items[i].Text + ",";
                    Checked += "false" + ",";
                }
            }
            workoutTypes = workout.Split(',');
            if (workoutTypes.Length > 0)
            {
                foreach (TableRow trc in myTable.Rows)
                {

                    foreach (TableCell tc in trc.Cells)
                    {
                        foreach (Control htc in tc.Controls)
                        {
                            var dropdownlist = htc as DropDownList;
                            var Textbox = htc as TextBox;
                            if (dropdownlist != null)
                            {
                                for (int j = 0; j < workoutTypes.Count(); j++)
                                {
                                    if (dropdownlist.ID == workoutTypes[j] + "ddlSETS")
                                    {
                                        if (dropdownlist.SelectedValue != "0")
                                        {
                                            setId += dropdownlist.ID + ",";
                                            sets += dropdownlist.SelectedValue + ",";

                                        }
                                        else
                                        {
                                            setId += dropdownlist.ID + ",";
                                            sets += "0" + ",";
                                        }

                                    }

                                }
                            }
                            if (Textbox != null)
                            {
                                for (int j = 0; j < workoutTypes.Count(); j++)
                                {
                                    if (Textbox.ID == workoutTypes[j] + "Reps")
                                    {
                                        if (Textbox.Text.Trim() != "")
                                        {
                                            RepId += Textbox.ID + ",";
                                            Reps += Textbox.Text + ",";

                                        }
                                        else
                                        {
                                            RepId += Textbox.ID + ",";
                                            Reps += "0" + ",";
                                        }
                                    }
                                    if (Textbox.ID == workoutTypes[j] + "weight")
                                    {
                                        if (Textbox.Text.Trim() != "")
                                        {
                                            weightId += Textbox.ID + ",";
                                            weight += Textbox.Text + ",";

                                        }
                                        else
                                        {
                                            weightId += Textbox.ID + ",";
                                            weight += "0" + ",";

                                        }
                                    }

                                }
                            }

                        }
                    }
                }



                if (Options.Count > 0)
                {
                    Options = GetPlanDetails(Session["WorkOutTypes"].ToString(), Session["WorkOutTypeText"].ToString(), workoutId.ToString().TrimEnd(','), workout.ToString().TrimEnd(','),
               setId.ToString().TrimEnd(','), sets.ToString().TrimEnd(','), RepId.ToString().TrimEnd(','), Reps.ToString().TrimEnd(','),
               weightId.ToString().TrimEnd(','), weight.ToString().TrimEnd(','), Checked.ToString().TrimEnd(','), Options);
                }
                else
                {
                    Options = GetPlanDetails(Session["WorkOutTypes"].ToString(), Session["WorkOutTypeText"].ToString(), workoutId.ToString().TrimEnd(','), workout.ToString().TrimEnd(','),
                setId.ToString().TrimEnd(','), sets.ToString().TrimEnd(','), RepId.ToString().TrimEnd(','), Reps.ToString().TrimEnd(','),
                weightId.ToString().TrimEnd(','), weight.ToString().TrimEnd(','), Checked.ToString().TrimEnd(','));
                    Session["OptionsList"] = Options;
                }

            }
            BindWorkOutSubType(WorkOutType, WorkOutTypeText);

        }
        else
        {
            BindWorkOutSubType(WorkOutType, WorkOutTypeText);
        }


    }
    #endregion
    #region Create Dynamic DropDown And TextBoxes
    public void CreateChecklistWithOption(string WorkOutType, string WorkOutTypeText)
    {

        DataTable SETS = (DataTable)Session["Sets"];
        List<WorkOutTypeSub> workOutTypeSub = new List<WorkOutTypeSub>();
        workOutTypeSub = (List<WorkOutTypeSub>)Session["WorkOutTypeSub"];
        var MyList = workOutTypeSub;
        Table myTable = new Table();
        myTable.ID = WorkOutType + "Type";

        foreach (var item in MyList)
        {
            DropDownList CB = new DropDownList();
            CB.DataSource = SETS;
            CB.DataTextField = "configName";
            CB.DataValueField = "configId";
            CB.DataBind();
            CB.Items.Insert(0, new ListItem("Select", "0"));
            CB.Width = 100;
            CB.ID = item.workoutType + "ddlSETS";
            CB.CssClass = "form-select";
            TableRow TR = new TableRow();
            TableCell TD = new TableCell();
            TD.Controls.Add(CB);
            TR.Controls.Add(TD);

            TextBox TB = new TextBox();
            TB.ID = item.workoutType + "Reps";
            TB.CssClass = "txtbox";
            TableCell TD2 = new TableCell();
            TD2.Controls.Add(TB);
            TR.Controls.Add(TD2);

            TextBox TB1 = new TextBox();
            TB1.ID = item.workoutType + "weight";
            TB1.CssClass = "txtbox";
            TableCell TD3 = new TableCell();
            TD3.Controls.Add(TB1);
            TR.Controls.Add(TD3);

            myTable.Controls.Add(TR);

        }


        Session["WorkOutTypes"] = WorkOutType;
        Session["WorkOutTypeText"] = WorkOutTypeText;
        ddlBindSets.Controls.Add(myTable);
        Session["slotData"] = myTable;
        ViewState["Flags"] = "3";


    }
    public static List<ListWorkOutTypeSub> GetPlanDetails(string workoutTypeId, string workoutType, string workoutId
        , string workout, string setId, string sets, string RepId, string Reps, string weightId, string weight, string Checked)
    {
        string[] workoutTypeIds;
        string[] RepIds;
        string[] Repss;
        string[] weightIds;
        string[] workoutTypes;
        string[] workoutIds;
        string[] workouts;
        string[] setIds;
        string[] setss;
        string[] weights;
        string[] Checkeds;

        workoutTypeIds = workoutTypeId.Split(',');
        workoutTypes = workoutType.Split(',');
        RepIds = RepId.Split(',');
        Repss = Reps.Split(',');
        weightIds = weightId.Split(',');
        weights = weight.Split(',');
        workoutIds = workoutId.Split(',');
        workouts = workout.Split(',');
        setIds = setId.Split(',');
        setss = sets.Split(',');
        Checkeds = Checked.Split(',');
        List<ListWorkOutTypeSub> lst = new List<ListWorkOutTypeSub>();

        for (int i = 0; i < workoutIds.Count(); i++)
        {
            lst.AddRange(new List<ListWorkOutTypeSub>
            {
                new ListWorkOutTypeSub { workoutTypeId=workoutTypeId,workoutType=workoutType, workoutId = workoutIds[i],
                workout=workouts[i],setId=setIds[i],sets=setss[i],RepId=RepIds[i],Reps=Repss[i],weightId=weightIds[i],
                weight=weights[i],Checked=Checkeds[i]
                }

            }); ;
        }

        return lst;

    }

    public static List<ListWorkOutTypeSub> GetPlanDetails(string workoutTypeId, string workoutType, string workoutId
       , string workout, string setId, string sets, string RepId, string Reps, string weightId, string weight, string Checked, List<ListWorkOutTypeSub> Options)
    {
        string[] workoutTypeIds;
        string[] RepIds;
        string[] Repss;
        string[] weightIds;
        string[] workoutTypes;
        string[] workoutIds;
        string[] workouts;
        string[] setIds;
        string[] setss;
        string[] weights;
        string[] Checkeds;

        workoutTypeIds = workoutTypeId.Split(',');
        workoutTypes = workoutType.Split(',');
        RepIds = RepId.Split(',');
        Repss = Reps.Split(',');
        weightIds = weightId.Split(',');
        weights = weight.Split(',');
        workoutIds = workoutId.Split(',');
        workouts = workout.Split(',');
        setIds = setId.Split(',');
        setss = sets.Split(',');
        Checkeds = Checked.Split(',');
        for (int i = 0; i < workoutIds.Count(); i++)
        {
            List<ListWorkOutTypeSub> list = new List<ListWorkOutTypeSub>();
            list = Options.Where(n => n.workoutId == workoutIds[i]).ToList();

            if (list.Count() > 0)
            {
                foreach (var item in Options)
                {
                    if (item.workoutId == workoutIds[i])
                    {
                        item.Checked = Checkeds[i];
                        item.setId = setIds[i];
                        item.sets = setss[i];
                        item.RepId = RepIds[i];
                        item.Reps = Repss[i];
                        item.weightId = weightIds[i];
                        item.weight = weights[i];

                    }

                }

            }
            else
            {
                Options.AddRange(new List<ListWorkOutTypeSub>
                {
                    new ListWorkOutTypeSub {
                        workoutTypeId=workoutTypeId,workoutType=workoutType , workoutId = workoutIds[i],
                    workout=workouts[i],setId=setIds[i],sets=setss[i],RepId=RepIds[i],Reps=Repss[i],weightId=weightIds[i],
                    weight=weights[i],Checked=Checkeds[i]
                    }

                });
            }
        }

        return Options;

    }

    #endregion
    #region Bind Work Out Sub Category 
    public void BindWorkOutSubType(string WorkOutType, string WorkOutTypeText)
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "workoutType/GetWorkOutTypeSub?gymOwnerId=" + Session["gymOwnerId"].ToString() + ""
                     + "&branchId=" + Session["branchId"].ToString() + "&workoutCatTypeId=" + WorkOutType.Trim() + "";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();
                    List<ListWorkOutTypeSub> Options = (List<ListWorkOutTypeSub>)Session["OptionsList"];
                    List<ListWorkOutTypeSub> OptionaParticular = Options.Where(x => x.workoutTypeId == WorkOutType).Select(x => x).ToList();

                    if (StatusCode == 1)
                    {
                        List<WorkOutTypeSub> WorkOutTypeSub = JsonConvert.DeserializeObject<List<WorkOutTypeSub>>(ResponseMsg);
                        Session["WorkOutTypeSub"] = WorkOutTypeSub;
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            ddlBindSets.Controls.Clear();
                            divsubCategory.Visible = true;
                            chkSubCategory.DataSource = dt;
                            chkSubCategory.DataTextField = "workoutType";
                            chkSubCategory.DataValueField = "workoutTypeId";
                            chkSubCategory.DataBind();
                            BindSets();
                           
                            if (OptionaParticular.Count > 0)
                            {
                                DataTable SETS = (DataTable)Session["Sets"];
                                List<WorkOutTypeSub> workOutTypeSub = new List<WorkOutTypeSub>();
                                workOutTypeSub = (List<WorkOutTypeSub>)Session["WorkOutTypeSub"];
                                var MyList = workOutTypeSub;
                                Table myTable = new Table();
                                myTable.ID = WorkOutType + "Type";

                                foreach (var item in OptionaParticular)
                                {
                                    DropDownList CB = new DropDownList();
                                    CB.DataSource = SETS;
                                    CB.DataTextField = "configName";
                                    CB.DataValueField = "configId";
                                    CB.DataBind();
                                    CB.Items.Insert(0, new ListItem("Select", "0"));
                                    CB.Width = 100;
                                    CB.ID = item.setId;
                                    CB.CssClass = "form-select";
                                    CB.SelectedValue = item.sets;
                                    TableRow TR = new TableRow();
                                    TableCell TD = new TableCell();
                                    TD.Controls.Add(CB);
                                    TR.Controls.Add(TD);

                                    TextBox TB = new TextBox();
                                    TB.ID = item.RepId;
                                    TB.Text = item.Reps;
                                    TB.CssClass = "txtbox";
                                    TableCell TD2 = new TableCell();
                                    TD2.Controls.Add(TB);
                                    TR.Controls.Add(TD2);

                                    TextBox TB1 = new TextBox();
                                    TB1.ID = item.weightId;
                                    TB1.Text = item.weight;
                                    TB1.CssClass = "txtbox";
                                    TableCell TD3 = new TableCell();
                                    TD3.Controls.Add(TB1);
                                    TR.Controls.Add(TD3);

                                    myTable.Controls.Add(TR);
                                    for (int i = 0; i < chkSubCategory.Items.Count; i++)
                                    {
                                        if (item.workoutId == chkSubCategory.Items[i].Value)
                                        {
                                            if (item.Checked == "true")
                                            {
                                                chkSubCategory.Items[i].Selected = true;
                                            }

                                            else
                                            {
                                                chkSubCategory.Items[i].Selected = false;
                                            }
                                        }
                                    }

                                }



                                Session["WorkOutTypes"] = WorkOutType;
                                Session["WorkOutTypeText"] = WorkOutTypeText;
                                ddlBindSets.Controls.Add(myTable);
                                Session["slotData"] = myTable;
                                ViewState["Flags"] = "3";

                            }
                            else
                            {
                                CreateChecklistWithOption(WorkOutType, WorkOutTypeText);
                            }

                        }
                        else
                        {
                          
                            ddlBindSets.Controls.Clear();
                            divsubCategory.Visible = false;
                            chkSubCategory.DataBind();
                        }


                    }
                    else
                    {
                        ViewState["Flags"] = "0";
                       
                        ddlBindSets.Controls.Clear();
                        divsubCategory.Visible = false;
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
    #region Bind Sets
    public void BindSets()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "configMaster/getDropDownDetails?typeId=22";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        List<Sets> Sets = JsonConvert.DeserializeObject<List<Sets>>(ResponseMsg);

                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        Session["Sets"] = dt;
                        if (dt.Rows.Count > 0)
                        {

                        }
                        else
                        {

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
   
    #region btnSubmit
    protected void btnWorkOutSubmit_Click(object sender, EventArgs e)
    {
        InsertWorkOutPlan();
    }
    #endregion
    #region InsertWorkOutPlan
    public void InsertWorkOutPlan()
    {

        string workoutTypeId = string.Empty;
        string workoutId = string.Empty;
        string csetType = string.Empty;
        string cnoOfReps = string.Empty;    
        string cweight = string.Empty;
        List<ListWorkOutTypeSub> Options = (List<ListWorkOutTypeSub>)Session["OptionsList"];

        if (Session["WorkOutTypes"].ToString() != "")
        {
            string workoutTypeIds = string.Empty;
            string workoutType = string.Empty;
            string workoutIds = string.Empty;
            string workout = string.Empty;
            string setId = string.Empty;
            string sets = string.Empty;
            string RepId = string.Empty;
            string Reps = string.Empty;
            string weightId = string.Empty;
            string weight = string.Empty;
            string Checked = string.Empty;
            string[] workoutTypes;
            DataTable SETS = (DataTable)Session["Sets"];
            List<WorkOutTypeSub> workOutTypeSub = new List<WorkOutTypeSub>();
            workOutTypeSub = (List<WorkOutTypeSub>)Session["WorkOutTypeSub"];
            var MyList = workOutTypeSub;
            Table myTable = (Table)Session["slotData"];
            for (int i = 0; i < chkSubCategory.Items.Count; i++)
            {
                if (chkSubCategory.Items[i].Selected == true)
                {
                    workoutIds += chkSubCategory.Items[i].Value + ",";
                    workout += chkSubCategory.Items[i].Text + ",";
                    Checked += "true" + ",";
                }
                else
                {
                    workoutIds += chkSubCategory.Items[i].Value + ",";
                    workout += chkSubCategory.Items[i].Text + ",";
                    Checked += "false" + ",";
                }
            }
            workoutTypes = workout.Split(',');
            if (workoutTypes.Length > 0)
            {
                foreach (TableRow trc in myTable.Rows)
                {

                    foreach (TableCell tc in trc.Cells)
                    {
                        foreach (Control htc in tc.Controls)
                        {
                            var dropdownlist = htc as DropDownList;
                            var Textbox = htc as TextBox;
                            if (dropdownlist != null)
                            {
                                for (int j = 0; j < workoutTypes.Count(); j++)
                                {
                                    if (dropdownlist.ID == workoutTypes[j] + "ddlSETS")
                                    {
                                        if (dropdownlist.SelectedValue != "0")
                                        {
                                            setId += dropdownlist.ID + ",";
                                            sets += dropdownlist.SelectedValue + ",";

                                        }
                                        else
                                        {
                                            setId += dropdownlist.ID + ",";
                                            sets += "0" + ",";
                                        }

                                    }

                                }
                            }
                            if (Textbox != null)
                            {
                                for (int j = 0; j < workoutTypes.Count(); j++)
                                {
                                    if (Textbox.ID == workoutTypes[j] + "Reps")
                                    {
                                        if (Textbox.Text.Trim() != "")
                                        {
                                            RepId += Textbox.ID + ",";
                                            Reps += Textbox.Text + ",";

                                        }
                                        else
                                        {
                                            RepId += Textbox.ID + ",";
                                            Reps += "0" + ",";
                                        }
                                    }
                                    if (Textbox.ID == workoutTypes[j] + "weight")
                                    {
                                        if (Textbox.Text.Trim() != "")
                                        {
                                            weightId += Textbox.ID + ",";
                                            weight += Textbox.Text + ",";

                                        }
                                        else
                                        {
                                            weightId += Textbox.ID + ",";
                                            weight += "0" + ",";

                                        }
                                    }

                                }
                            }

                        }
                    }
                }
            }



            if (Options.Count > 0)
            {
                Options = GetPlanDetails(Session["WorkOutTypes"].ToString(), Session["WorkOutTypeText"].ToString(), workoutIds.ToString().TrimEnd(','), workout.ToString().TrimEnd(','),
           setId.ToString().TrimEnd(','), sets.ToString().TrimEnd(','), RepId.ToString().TrimEnd(','), Reps.ToString().TrimEnd(','),
           weightId.ToString().TrimEnd(','), weight.ToString().TrimEnd(','), Checked.ToString().TrimEnd(','), Options);
            }
            else
            {
                Options = GetPlanDetails(Session["WorkOutTypes"].ToString(), Session["WorkOutTypeText"].ToString(), workoutIds.ToString().TrimEnd(','), workout.ToString().TrimEnd(','),
            setId.ToString().TrimEnd(','), sets.ToString().TrimEnd(','), RepId.ToString().TrimEnd(','), Reps.ToString().TrimEnd(','),
            weightId.ToString().TrimEnd(','), weight.ToString().TrimEnd(','), Checked.ToString().TrimEnd(','));
                Session["OptionsList"] = Options;
            }

        }

        List<ListWorkOutTypeSub> Option = Options.Where(x => x.Checked == "true").Select(x => x).ToList();
        foreach (var item in Option)
        {
            workoutTypeId += item.workoutTypeId + ",";
            workoutId += item.workoutId + ",";
            csetType += item.sets + ",";
            cnoOfReps += item.Reps + ",";
            cweight += item.weight + ",";

        }
        if (Option.Count() <= 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Any One Work Out');", true);
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
                InsertTranrWorkoutPlan_In Insert = new InsertTranrWorkoutPlan_In()
                {
                    lstTranUserWorkoutPlan = GetUserWorkoutPlanDetails(Session["gymOwnerId"].ToString(),
                   Session["branchId"].ToString(), workoutTypeId.ToString().TrimEnd(','), workoutId.ToString().TrimEnd(','),
                   Session["BookingIDWorkOutplan"].ToString(), ViewState["workingDay"].ToString(), txtWorKFromdate.Text.Trim(),
                   txtWorkTodate.Text.Trim(), csetType.ToString().TrimEnd(','), cnoOfReps.ToString().TrimEnd(','),
                   cweight.ToString().TrimEnd(','), Session["UserIdWorkOutplan"].ToString(), Session["userId"].ToString())
                };
                HttpResponseMessage response = client.PostAsJsonAsync("UserWorkOutPlan/insert", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        GetUserWorkOutItemList();
                        workOutclear();
                        Option.Clear();
                        Session["OptionsList"] = Option;
                        Session["slotTable"] = "";
                        Session["WorkOutTypes"] = "";
                        Session["slotData"] = "";
                        ViewState["Flag"] = "0";
                        ViewState["Flags"] = "0";
                        txtWorKFromdate.Text = "";
                        txtWorkTodate.Text = "";
                        txtWorKFromdate.Text = Session["fromDateWorkOutplan"].ToString();
                        txtWorkTodate.Text = Session["toDateWorkOutplan"].ToString();
                        AddWorkOutPlan.Visible = false;
                        divsubCategory.Visible = false;
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
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Work Plan Already Exists');", true);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }

    public static List<UserWorkoutPlan> GetUserWorkoutPlanDetails(string gymOwnerId, string branchId, string workoutCatTypeId,
        string workoutTypeId, string bookingId, string day, string fromDate, string toDate, string csetType, string cnoOfReps,
        string cweight, string userId, string createdBy)
    {
        string[] workoutTypeIds;
        string[] workoutCatTypeIds;
        string[] csetTypes;
        string[] cnoOfRepss;
        string[] cweights;


        workoutTypeIds = workoutTypeId.Split(',');

        workoutCatTypeIds = workoutCatTypeId.Split(',');
        csetTypes = csetType.Split(',');
        cnoOfRepss = cnoOfReps.Split(',');
        cweights = cweight.Split(',');
        List<UserWorkoutPlan> lst = new List<UserWorkoutPlan>();
        for (int i = 0; i < workoutTypeIds.Count(); i++)
        {
            lst.AddRange(new List<UserWorkoutPlan>
            {
                new UserWorkoutPlan { gymOwnerId=gymOwnerId,branchId=branchId , workoutCatTypeId = workoutCatTypeIds[i],
                workoutTypeId=workoutTypeIds[i],bookingId=bookingId,day=day,fromDate=fromDate,toDate=toDate,
                csetType=csetTypes[i],cnoOfReps=cnoOfRepss[i],cweight=cweights[i],userId=userId,createdBy=createdBy
                }

            }); ;
        }
        return lst;

    }
    #endregion

    #region Add Work Out Popup
    protected void btnAddWorkOut_Click(object sender, EventArgs e)
    {
        Session["slotTable"] = "";
        Session["slotData"] = "";
        ViewState["Flag"] = "0";
        ViewState["Flags"] = "0";
        LinkButton lnkbtn = sender as LinkButton;
        DataListItem gvrow = lnkbtn.NamingContainer as DataListItem;
        LinkButton btnworkingDay = (LinkButton)gvrow.FindControl("btnworkingDay");
        ViewState["workingDay"] = btnworkingDay.Text;
        AddWorkOutPlan.Visible = true;
        BindWorkOutType();
        WorkOutPlanForm.Visible = true;
    }
    #endregion
    #region Approve Button Click
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserBookedList.aspx");
    }
    #endregion
    #region btn Closes For Work Out
    protected void btnCloses_Click(object sender, EventArgs e)
    {
        List<ListWorkOutTypeSub> Option = new List<ListWorkOutTypeSub>();
        Option.Clear();

        Session["OptionsList"] = Option;
        Session["slotTable"] = "";
        Session["WorkOutTypes"] = "";
        Session["slotData"] = "";
        ViewState["Flag"] = "0";
        ViewState["Flags"] = "0";
        txtWorKFromdate.Text = "";
        txtWorkTodate.Text = "";
        //DateTime FromDate = Convert.ToDateTime(Session["fromDateWorkOutplan"].ToString());
        //DateTime ToDate = Convert.ToDateTime(Session["toDateWorkOutplan"].ToString());
        txtWorKFromdate.Text = Session["fromDateWorkOutplan"].ToString();
        txtWorkTodate.Text = Session["toDateWorkOutplan"].ToString();
        AddWorkOutPlan.Visible = false;
        divsubCategory.Visible = false;
    }
    #endregion
    #region WorkOut Clear and Cancel
    protected void btnWorkOutCancel_Click(object sender, EventArgs e)
    {
        // BindWorkOutType();
        workOutclear();
        txtWorKFromdate.Text = "";
        txtWorkTodate.Text = "";
        //DateTime FromDate = Convert.ToDateTime(Session["fromDateWorkOutplan"].ToString());
        //DateTime ToDate = Convert.ToDateTime(Session["toDateWorkOutplan"].ToString());
        txtWorKFromdate.Text = Session["fromDateWorkOutplan"].ToString();
        txtWorkTodate.Text = Session["toDateWorkOutplan"].ToString();
    }
    public void workOutclear()
    {
        AddBenefits.Visible = false;
        //ddlWorkOutType.ClearSelection();
        ddlWorkingDay.ClearSelection();
        chkSubCategory.Items.Clear();
        ddlBindSets.Controls.Clear();
        divsubCategory.Visible = false;

        List<ListWorkOutTypeSub> Option = new List<ListWorkOutTypeSub>();
        Option.Clear();

        Session["OptionsList"] = Option;
    }
    #endregion
    #region Work Out Plan Class
    public class WorkOutTypeSub
    {
        public string workoutTypeId { get; set; }
        public string workoutType { get; set; }

    }

    public class ArrayWorkOutTypeSub
    {
        public string workoutTypeId { get; set; }
        public string workoutType { get; set; }
        public string sets { get; set; }
        public string Reps { get; set; }
        public string weight { get; set; }

    }

    public class Sets
    {
        public string configId { get; set; }
        public string configName { get; set; }

    }
    public class ListWorkOutTypeSub
    {
        public string workoutTypeId { get; set; }
        public string workoutType { get; set; }
        public string workoutId { get; set; }
        public string workout { get; set; }
        public string setId { get; set; }
        public string sets { get; set; }
        public string RepId { get; set; }
        public string Reps { get; set; }
        public string weightId { get; set; }
        public string weight { get; set; }
        public string Checked { get; set; }


    }


    public class InsertTranrWorkoutPlan_In
    {
        public List<UserWorkoutPlan> lstTranUserWorkoutPlan { get; set; }
    }
    public class UserWorkoutPlan
    {
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string workoutCatTypeId { get; set; }
        public string workoutTypeId { get; set; }
        public string bookingId { get; set; }
        public string day { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string csetType { get; set; }
        public string cnoOfReps { get; set; }
        public string cweight { get; set; }
        public string userId { get; set; }
        public string createdBy { get; set; }
    }
    #endregion
    #endregion


}
