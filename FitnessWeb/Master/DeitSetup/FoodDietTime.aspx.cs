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

public partial class Master_DeitSetup_FoodDietTime : System.Web.UI.Page
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
            Session["BaseUrl"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"].Trim();
            GetMealType();
            GetFoodItem();
            BindFoodDietTime();
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
    #region Get Meal Types
    public void GetMealType()
    {
        try
        {
            ddlmealType.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                HttpResponseMessage response = client.GetAsync("configMaster/getDropDownDetails?typeId=20").Result;
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
                            ddlmealType.DataSource = dt;
                            ddlmealType.DataTextField = "configName";
                            ddlmealType.DataValueField = "configId";
                            ddlmealType.DataBind();
                        }
                        else
                        {
                            ddlmealType.DataBind();
                        }
                       
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlmealType.Items.Insert(0, new ListItem("Meal Type  *", "0"));
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
    #region Get FoodItem
    public void GetFoodItem()
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
                  string Endpoint = "foodItemMaster/GetDropDownDetails?gymOwnerId=" + Session["gymOwnerId"].ToString() + "";
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
                            chckfoodItem.DataSource = dt;
                            chckfoodItem.DataTextField = "foodItemName";
                            chckfoodItem.DataValueField = "foodItemId";
                            chckfoodItem.DataBind();
                        }
                        else
                        {
                            chckfoodItem.DataBind();

                        }
                    }
                    else
                    {
                        chckfoodItem.Items.Clear();
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
    #region Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        ClearFoodDietTime();
    }
    #endregion
    #region Submit 
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        if (btnSubmit.Text == "Submit")
        {

            InsertFoodDietTime();
        }
        else
        {
            InsertFoodDietTime();
        }

    }
    #endregion
    #region Clear
    public void ClearFoodDietTime()
    {
        chckfoodItem.ClearSelection();
        ddlmealType.ClearSelection();
        ddlmealType.Enabled = true;
        DivForm.Visible = false;
        divGv.Visible = true;
        btnSubmit.Text = "Submit";
    }
    #endregion
    #region Bind Food Diet Time
    public void BindFoodDietTime()
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
                string Endpoint = "foodDietTimeMaster?gymOwnerId=" + Session["gymOwnerId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                btnCancel.Visible = false;

                string Response;
                var Locresponse = response.Content.ReadAsStringAsync().Result;
                int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());

                if (response.IsSuccessStatusCode)
                {
                    if (statusCode == 1)
                    {
                        string ResponseMsg = JObject.Parse(Locresponse)["FoodDietTime"].ToString();

                        List<FoodDietTimeGet> FoodDietTime = JsonConvert.DeserializeObject<List<FoodDietTimeGet>>(ResponseMsg);
                        DataTable dt = ConvertToDataTable(FoodDietTime);
                        gvFoodDietTimeMstr.DataSource = FoodDietTime;
                        gvFoodDietTimeMstr.DataBind();
                        DivForm.Visible = false;
                        divGv.Visible = true;
                        btnCancel.Visible = true;
                    }
                    else
                    {
                        Response = JObject.Parse(Locresponse)["Response"].ToString();
                        DivForm.Visible = true;
                        divGv.Visible = false;
                        gvFoodDietTimeMstr.DataBind();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Response.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    Response = JObject.Parse(Locresponse)["Response"].ToString();
                    DivForm.Visible = true;
                    divGv.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Response.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
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
    #endregion
    #region Insert Function 
    public void InsertFoodDietTime()
    {
        try
        {
            int Count = 0;
            string fooditemId = string.Empty;
            for (int i = 0; i < chckfoodItem.Items.Count; i++)
            {
                if (chckfoodItem.Items[i].Selected == true)
                {
                    Count = 1;

                    fooditemId += chckfoodItem.Items[i].Value + ",";

                }
            }
            if (Count == 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Any One Food');", true);
                return;
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                InsertFoodDietTime_In Insert = new InsertFoodDietTime_In()
                {
                    lstFoodDietTime = InsertFoodDietTime(ddlmealType.SelectedValue, fooditemId.ToString().TrimEnd(','), Session["userId"].ToString(), Session["gymOwnerId"].ToString())
                };
                HttpResponseMessage response = client.PostAsJsonAsync("foodDietTimeMaster/insert", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindFoodDietTime();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert(`" + ResponseMsg.ToString().Trim() + "`);", true);
                    }
                    ClearFoodDietTime();
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
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }

    public static List<FoodDietTimelst> InsertFoodDietTime(string mealType, string foodItemId, string createdBy,string gymOwnerId)
    {
        string[] foodItemIds;

        foodItemIds = foodItemId.Split(',');
        List<FoodDietTimelst> lst = new List<FoodDietTimelst>();
        for (int i = 0; i < foodItemIds.Count(); i++)
        {
            lst.AddRange(new List<FoodDietTimelst>
            {
                new FoodDietTimelst { mealType= mealType,foodItemId=foodItemIds[i],createdBy=createdBy,gymOwnerId=gymOwnerId
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

            Label lblmealType = (Label)gvrow.FindControl("lblmealType");
            DataList dlBreakFast = (DataList)gvrow.FindControl("dlBreakFast");
            GetFoodItem();
            for (int i = 0; i < dlBreakFast.Items.Count; i++)
            {
                Label lblgvfoodItemId = dlBreakFast.Items[i].FindControl("lblgvfoodItemId") as Label;
                for (int j = 0; j < chckfoodItem.Items.Count; j++)
                {
                    if (chckfoodItem.Items[j].Value == lblgvfoodItemId.Text)
                    {
                        chckfoodItem.Items[j].Selected = true;
                    }

                }
            }
            ViewState["lblmealType"] = lblmealType.Text.Trim();
            GetMealType();
            ddlmealType.SelectedValue = lblmealType.Text.Trim();
            ddlmealType.Enabled = false;
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
    #region Gv RowData Bind
    protected void gvFoodDietTimeMstr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            var foodDietTime = e.Row.DataItem as FoodDietTimeGet;
            var dlBreakFast = e.Row.FindControl("dlBreakFast") as DataList;
            dlBreakFast.DataSource = foodDietTime.FoodDietList;
            dlBreakFast.DataBind();
        }
    }
    #endregion
    #region FoodDietTime Class
    public class InsertFoodDietTime_In
    {
        public List<FoodDietTimelst> lstFoodDietTime { get; set; }
    }
    public class FoodDietTimelst
    {

        public string mealType { get; set; }
        public string foodItemId { get; set; }
        public string createdBy { get; set; }
        public string gymOwnerId { get; set; }

    }

    public class FoodDietTimeGet
    {
        public string mealType { get; set; }
        public string mealTypeName { get; set; }
        public List<FoodDietList> FoodDietList { get; set; }

    }
    public class FoodDietList
    {
        public string uniqueId { get; set; }
        public string mealType { get; set; }
        public string foodItemId { get; set; }
        public string foodItemName { get; set; }

    }

    #endregion
}