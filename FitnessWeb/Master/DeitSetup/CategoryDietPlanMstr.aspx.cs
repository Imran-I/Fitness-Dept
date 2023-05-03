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
using System.ComponentModel.DataAnnotations;
using System.Drawing;

public partial class Master_DeitSetup_WorkOutFoodMenuMstr : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCategoryDietPlan();

        }
    }
    #region Bind Category Diet Plan GridView
    public void BindCategoryDietPlan()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "CategoryDietPlan/getCategoryDiet?branchId=" + Session["branchId"].ToString() + ""
                    + "&gymOwnerId=" + Session["gymOwnerId"].ToString() + "";
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
                            DivForm.Visible = false;
                            divGv.Visible = true;
                            gvCategoryDietPlanMstr.DataSource = dt;
                            gvCategoryDietPlanMstr.DataBind();


                        }
                        else
                        {
                            DivForm.Visible = true;
                            divGv.Visible = false;
                            divEditDtl.Visible = false;
                            divAddDtl.Visible = true;
                            BindCategory();
                            BindFoodItem();
                            gvCategoryDietPlanMstr.DataBind();
                        }

                    }
                    else
                    {
                        DivForm.Visible = true;
                        divGv.Visible = false;
                        divEditDtl.Visible = false;
                        divAddDtl.Visible = true;
                        BindCategory();
                        BindFoodItem();
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
    #region Bind Category
    public void BindCategory()
    {
        try

        {
            ddlWorkOutType.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sUrl = Session["BaseUrl"].ToString().Trim() + "categoryMaster/GetDropDownDetails?branchId=" + Session["branchId"].ToString() + ""
                    + "&gymOwnerId=" + Session["gymOwnerId"].ToString() + "";
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
                            ddlWorkOutType.DataSource = dt;
                            ddlWorkOutType.DataTextField = "categoryName";
                            ddlWorkOutType.DataValueField = "categoryId";
                            ddlWorkOutType.DataBind();
                        }
                        else
                        {
                            ddlWorkOutType.DataBind();
                        }
                        ddlWorkOutType.Items.Insert(0, new ListItem("Work Out Type *", "0"));
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
    #region btnAdd 
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DivForm.Visible = true;
        divGv.Visible = false;
        divEditDtl.Visible = false;
        divAddDtl.Visible = true;
        BindCategory();
        BindFoodItem();
    }
    #endregion
    #region Bind Food Item
    public void BindFoodItem()
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
                        dtlFoodItem.DataSource = FoodDietTime;
                        dtlFoodItem.DataBind();
                        btnCancel.Visible = true;
                    }
                    else
                    {
                        Response = JObject.Parse(Locresponse)["Response"].ToString();
                        DivForm.Visible = true;
                        divGv.Visible = false;
                        dtlFoodItem.DataBind();
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
    #endregion
    #region Food Item Item DataBound
    protected void dtlFoodItem_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            var foodDietTime = e.Item.DataItem as FoodDietTimeGet;
            var dlBreakFast = e.Item.FindControl("dtlFoodItemList") as DataList;

            dlBreakFast.DataSource = foodDietTime.FoodDietList;
            dlBreakFast.DataBind();
            CheckBoxList Food = dlBreakFast.Items[0].FindControl("chckfoodItem") as CheckBoxList;
            Food.DataSource = foodDietTime.FoodDietList;
            Food.DataTextField = "foodItemName";
            Food.DataValueField = "foodItemId";
            Food.DataBind();

        }
    }
    #endregion
    #region  InsertFoodItem
    public void InsertFoodItem()
    {
        try
        {
            int Count = 0;
            string fooditemId = string.Empty;
            string MealtypeId = string.Empty;
            string DietTimeId = string.Empty;
            string activeStatus = string.Empty;
            for (int i = 0; i < dtlFoodItem.Items.Count; i++)
            {
                DataList dtlFoodItemList = dtlFoodItem.Items[i].FindControl("dtlFoodItemList") as DataList;
                Label lblMealType = dtlFoodItem.Items[i].FindControl("lblMealType") as Label;

                for (int j = 0; j < dtlFoodItemList.Items.Count; j++)
                {
                    CheckBoxList chckfoodItem = dtlFoodItemList.Items[j].FindControl("chckfoodItem") as CheckBoxList;

                    for (int m = 0; m < chckfoodItem.Items.Count; m++)
                    {
                        Label lblDietTimeID = dtlFoodItemList.Items[m].FindControl("lblDietTimeID") as Label;
                        fooditemId += chckfoodItem.Items[m].Value + ",";
                        MealtypeId += lblMealType.Text + ',';
                        DietTimeId += lblDietTimeID.Text + ',';
                        if (chckfoodItem.Items[m].Selected == true)
                        {
                            Count = 1;
                            activeStatus += "A" + ',';
                        }
                        else
                        {
                            activeStatus += "D" + ',';
                        }
                    }
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
                InsertCategoryDietPlan_In Insert = new InsertCategoryDietPlan_In()
                {
                    lstWorkOutFoodMenu = InsertWorkOutMenu(MealtypeId.ToString().TrimEnd(','), DietTimeId.ToString().TrimEnd(','),
                           ddlWorkOutType.SelectedValue, fooditemId.ToString().TrimEnd(','), activeStatus.ToString().TrimEnd(','),
                           Session["userId"].ToString(), Session["branchId"].ToString(), Session["gymOwnerId"].ToString())
                };
                HttpResponseMessage response = client.PostAsJsonAsync("CategoryDietPlan/insert", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindCategoryDietPlan();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert(`" + ResponseMsg.ToString().Trim() + "`);", true);
                    }
                    ClearFood();
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
    #endregion
    #region Edit Click Event
    protected void LnkEdit_Click(object sender, ImageClickEventArgs e)
       {
        ImageButton lnkbtn = sender as ImageButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        Label lblcategoryId = (Label)gvrow.FindControl("lblcategoryId");
        BindCategory();
        ddlWorkOutType.SelectedValue = lblcategoryId.Text;
        ddlWorkOutType.Enabled = false;

        BindFindItemList(lblcategoryId.Text);
        btnSubmit.Text = "Update";
        divGv.Visible = false;
        DivForm.Visible = true;
        divEditDtl.Visible = true;
        divAddDtl.Visible = false;
    }
    #endregion

    #region Bind Edit Food item List
    public void BindFindItemList(string categoryId)
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
                string sUrl = Session["BaseUrl"].ToString().Trim() + "CategoryDietPlan?branchId=" + Session["branchId"].ToString() + ""
                   + "&gymOwnerId=" + Session["gymOwnerId"].ToString() + "&categoryId=" + categoryId.Trim() + "";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;
                string Response;
                var Locresponse = response.Content.ReadAsStringAsync().Result;
                int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());

                if (response.IsSuccessStatusCode)
                {
                    if (statusCode == 1)
                    {
                        string ResponseMsg = JObject.Parse(Locresponse)["CategoryDietPlan"].ToString();

                        List<CategoryDietPlanGet> FoodList = JsonConvert.DeserializeObject<List<CategoryDietPlanGet>>(ResponseMsg);
                        DataTable dt = ConvertToDataTable(FoodList);
                        dtlEditFoodItem.DataSource = FoodList;
                        dtlEditFoodItem.DataBind();
                    }
                    else
                    {
                        Response = JObject.Parse(Locresponse)["Response"].ToString();
                        //DivForm.Visible = true;
                        //divGv.Visible = false;
                        dtlEditFoodItem.DataBind();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Response.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    Response = JObject.Parse(Locresponse)["Response"].ToString();
                    //DivForm.Visible = true;
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
    protected void dtlEditFoodItem_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            var foodDietTime = e.Item.DataItem as CategoryDietPlanGet;
            var dtlFoodItemLists = e.Item.FindControl("dtlFoodItemLists") as DataList;

            dtlFoodItemLists.DataSource = foodDietTime.FoodItemList;
            dtlFoodItemLists.DataBind();
            CheckBoxList Food = dtlFoodItemLists.Items[0].FindControl("chckfoodItem") as CheckBoxList;

            Food.DataSource = foodDietTime.FoodItemList;
            Food.DataTextField = "foodItemName";
            Food.DataValueField = "foodItemId";
            Food.DataBind();
            for (int i = 0; i < Food.Items.Count; i++)
            {
                Label lblactiveStatus = dtlFoodItemLists.Items[i].FindControl("lblactiveStatus") as Label;
                if (lblactiveStatus.Text == "A")
                {
                    Food.Items[i].Selected = true;
                }
                else
                {
                    Food.Items[i].Selected = false;
                }
            }
        }
    }
    #endregion
    #region Update Food Item Function
    public void UpdateFoodItem()
    {
        try
        {
            int Count = 0;
            string fooditemId = string.Empty;
            string MealtypeId = string.Empty;
            string DietTimeId = string.Empty;
            string activeStatus = string.Empty;
            string uniqueId = string.Empty;
            for (int i = 0; i < dtlEditFoodItem.Items.Count; i++)
            {
                DataList dtlFoodItemLists = dtlEditFoodItem.Items[i].FindControl("dtlFoodItemLists") as DataList;
                Label lblMealType = dtlEditFoodItem.Items[i].FindControl("lblmealTypeId") as Label;

                for (int j = 0; j < dtlFoodItemLists.Items.Count; j++)
                {
                    CheckBoxList chckfoodItem = dtlFoodItemLists.Items[j].FindControl("chckfoodItem") as CheckBoxList;


                    for (int m = 0; m < chckfoodItem.Items.Count; m++)
                    {
                        Label lblDietTimeID = dtlFoodItemLists.Items[m].FindControl("lbldietTimeId") as Label;
                        Label lbluniqueId = dtlFoodItemLists.Items[m].FindControl("lbluniqueId") as Label;
                        fooditemId += chckfoodItem.Items[m].Value + ",";
                        MealtypeId += lblMealType.Text + ',';
                        DietTimeId += lblDietTimeID.Text + ',';
                        uniqueId += lbluniqueId.Text + ',';
                        if (chckfoodItem.Items[m].Selected == true)
                        {
                            Count = 1;
                            activeStatus += "A" + ',';
                        }
                        else
                        {
                            activeStatus += "D" + ',';
                        }
                    }
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
                UpdateCategoryDietPlan_In Insert = new UpdateCategoryDietPlan_In()
                {
                    lstWorkOutFoodMenu = UpdateWorkOutMenu(MealtypeId.ToString().TrimEnd(','), DietTimeId.ToString().TrimEnd(','),
                           ddlWorkOutType.SelectedValue, fooditemId.ToString().TrimEnd(','), activeStatus.ToString().TrimEnd(','),
                           Session["userId"].ToString(), uniqueId.ToString().TrimEnd(','), Session["branchId"].ToString(), Session["gymOwnerId"].ToString())
                };
                HttpResponseMessage response = client.PostAsJsonAsync("CategoryDietPlan/update", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindCategoryDietPlan();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert(`" + ResponseMsg.ToString().Trim() + "`);", true);
                    }
                    ClearFood();
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
    #endregion
    #region Submit Click Button
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubmit.Text == "Submit")
        {
            InsertFoodItem();
        }
        else
        {
            UpdateFoodItem();
        }
    }
    #endregion
    #region Clear
    public void ClearFood()
    {
        ddlWorkOutType.ClearSelection();
        ddlWorkOutType.Enabled = true;
        DivForm.Visible = false;
        divGv.Visible = true;
        btnSubmit.Text = "Submit";
    }
    #endregion
    #region Btn Cancel
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        ClearFood();
    }
    #endregion
    #region List Insert and Update
    public static List<InsertWorkOutFoodMenu> InsertWorkOutMenu(string mealTypeId, string dietTimeId, string WorKoutCatId,
        string foodItemId, string activeStatus, string createdBy, string branchId, string GymOwnerId)
    {
        string[] foodItemIds;
        string[] mealTypeIds;
        string[] dietTimeIds;
        string[] activeStatuss;

        foodItemIds = foodItemId.Split(',');
        mealTypeIds = mealTypeId.Split(',');
        dietTimeIds = dietTimeId.Split(',');
        activeStatuss = activeStatus.Split(',');

        List<InsertWorkOutFoodMenu> lst = new List<InsertWorkOutFoodMenu>();
        for (int i = 0; i < foodItemIds.Count(); i++)
        {
            lst.AddRange(new List<InsertWorkOutFoodMenu>
            {
                new InsertWorkOutFoodMenu { mealTypeId= mealTypeIds[i],foodItemId=foodItemIds[i],dietTimeId=dietTimeIds[i],
                    categoryId=WorKoutCatId,activeStatus=activeStatuss[i],
                    createdBy=createdBy,branchId=branchId,gymOwnerId=GymOwnerId
                }

            }); ;
        }
        return lst;

    }
    public static List<UpdateWorkOutFoodMenu> UpdateWorkOutMenu(string mealTypeId, string dietTimeId, string WorKoutCatId,
     string foodItemId, string activeStatus, string UpdatedBy, string uniqueId, string branchId, string GymOwnerId)
    {
        string[] foodItemIds;
        string[] mealTypeIds;
        string[] dietTimeIds;

        string[] uniqueIds;
        string[] activeStatuss;

        foodItemIds = foodItemId.Split(',');
        mealTypeIds = mealTypeId.Split(',');
        dietTimeIds = dietTimeId.Split(',');

        uniqueIds = uniqueId.Split(',');
        activeStatuss = activeStatus.Split(',');
        List<UpdateWorkOutFoodMenu> lst = new List<UpdateWorkOutFoodMenu>();
        for (int i = 0; i < foodItemIds.Count(); i++)
        {
            lst.AddRange(new List<UpdateWorkOutFoodMenu>
            {
                new UpdateWorkOutFoodMenu { mealTypeId= mealTypeIds[i],foodItemId=foodItemIds[i],dietTimeId=dietTimeIds[i],
                    categoryId=WorKoutCatId,uniqueId=uniqueIds[i],activeStatus=activeStatuss[i],
                    updatedBy=UpdatedBy,branchId=branchId,gymOwnerId=GymOwnerId
                }

            }); ;
        }
        return lst;

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
    #region FoodDietTime Class
    public class InsertCategoryDietPlan_In
    {
        public List<InsertWorkOutFoodMenu> lstWorkOutFoodMenu { get; set; }
    }
    public class UpdateCategoryDietPlan_In
    {
        public List<UpdateWorkOutFoodMenu> lstWorkOutFoodMenu { get; set; }
    }
    public class InsertWorkOutFoodMenu
    {
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string mealTypeId { get; set; }
        public string foodItemId { get; set; }
        public string dietTimeId { get; set; }
        public string categoryId { get; set; }
        public string activeStatus { get; set; }
        public string createdBy { get; set; }

    }
    public class UpdateWorkOutFoodMenu
    {
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string uniqueId { get; set; }
        public string mealTypeId { get; set; }
        public string foodItemId { get; set; }
        public string dietTimeId { get; set; }
        public string categoryId { get; set; }
        public string activeStatus { get; set; }
        public string updatedBy { get; set; }

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

    public class CategoryDietPlanGet
    {
        public string mealTypeId { get; set; }
        public string mealTypeName { get; set; }
        public List<FoodDietLists> FoodItemList { get; set; }

    }
    public class FoodDietLists
    {
        public string uniqueId { get; set; }
        public string mealTypeId { get; set; }
        public string foodItemId { get; set; }
        public string dietTimeId { get; set; }
        public string categoryId { get; set; }
        public string foodItemName { get; set; }
        public string activeStatus { get; set; }

    }

    #endregion

}

