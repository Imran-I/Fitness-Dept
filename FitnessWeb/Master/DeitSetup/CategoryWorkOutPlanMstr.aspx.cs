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

public partial class Master_DeitSetup_CategoryWorkOutPlanMstr : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCategoryWorkOutPlan();

        }
    }
    #region Bind Category WorkOut Plan GridView
    public void BindCategoryWorkOutPlan()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "CategoryWorkOutPlan/getCategoryWorkOut?branchId=" + Session["branchId"].ToString() + ""
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
                            gvCategoryWorkOutPlanMstr.DataSource = dt;
                            gvCategoryWorkOutPlanMstr.DataBind();

                        }
                        else
                        {
                            DivForm.Visible = true;
                            divGv.Visible = false;
                            divEditDtl.Visible = false;
                            divAddDtl.Visible = true;
                            BindCategory();
                            BindWorkOutList();
                            gvCategoryWorkOutPlanMstr.DataBind();
                        }

                    }
                    else
                    {
                        DivForm.Visible = true;
                        divGv.Visible = false;
                        divEditDtl.Visible = false;
                        divAddDtl.Visible = true;
                        BindCategory();
                        BindWorkOutList();
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
        BindWorkOutList();
    }
    #endregion
    #region Bind Food Item
    public void BindWorkOutList()
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
                string sUrl = Session["BaseUrl"].ToString().Trim() + "CategoryWorkOutPlan/getMstrWorkOut?branchId=" + Session["branchId"].ToString() + ""
                + "&gymOwnerId=" + Session["gymOwnerId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;
                btnCancel.Visible = false;

                string Response;
                var Locresponse = response.Content.ReadAsStringAsync().Result;
                int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());

                if (response.IsSuccessStatusCode)
                {
                    if (statusCode == 1)
                    {
                        string ResponseMsg = JObject.Parse(Locresponse)["CategoryWorkOutPlan"].ToString();

                        List<WorkItemGet> WorkItemGet = JsonConvert.DeserializeObject<List<WorkItemGet>>(ResponseMsg);
                        DataTable dt = ConvertToDataTable(WorkItemGet);
                        dtlWorkOut.DataSource = WorkItemGet;
                        dtlWorkOut.DataBind();
                        btnCancel.Visible = true;
                    }
                    else
                    {
                        Response = JObject.Parse(Locresponse)["Response"].ToString();
                        DivForm.Visible = true;
                        divGv.Visible = false;
                        dtlWorkOut.DataBind();
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
            var Work = e.Item.DataItem as WorkItemGet;
            var dtlWorkOutList = e.Item.FindControl("dtlWorkOutList") as DataList;

            dtlWorkOutList.DataSource = Work.WorkOutList;
            dtlWorkOutList.DataBind();
            CheckBoxList WorkChk = dtlWorkOutList.Items[0].FindControl("chckWorKout") as CheckBoxList;
            WorkChk.DataSource = Work.WorkOutList;
            WorkChk.DataTextField = "workoutType";
            WorkChk.DataValueField = "workoutTypeId";
            WorkChk.DataBind();

        }
    }
    #endregion
    #region  InsertFoodItem
    public void InsertFoodItem()
    {
        try
        {
            int Count = 0;
            string workoutTypeId = string.Empty;
            string workoutCatTypeId = string.Empty;
          
            string activeStatus = string.Empty;
            for (int i = 0; i < dtlWorkOut.Items.Count; i++)
            {
                DataList dtlWorkOutList = dtlWorkOut.Items[i].FindControl("dtlWorkOutList") as DataList;
                Label lblworkoutCatTypeId = dtlWorkOut.Items[i].FindControl("lblworkoutCatTypeId") as Label;

                for (int j = 0; j < dtlWorkOutList.Items.Count; j++)
                {
                    CheckBoxList chckWorKout = dtlWorkOutList.Items[j].FindControl("chckWorKout") as CheckBoxList;

                    for (int m = 0; m < chckWorKout.Items.Count; m++)
                    {
                        workoutTypeId += chckWorKout.Items[m].Value + ",";
                        workoutCatTypeId += lblworkoutCatTypeId.Text + ',';
                       
                        if (chckWorKout.Items[m].Selected == true)
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
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Any One WorkOut');", true);
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
                    lstWorkOutFoodMenu = InsertWorkOutPlanMenu(workoutCatTypeId.ToString().TrimEnd(','), workoutTypeId.ToString().TrimEnd(','),
                           ddlWorkOutType.SelectedValue, activeStatus.ToString().TrimEnd(','),
                           Session["userId"].ToString(), Session["branchId"].ToString(), Session["gymOwnerId"].ToString())
                };
                HttpResponseMessage response = client.PostAsJsonAsync("CategoryWorkOutPlan/insert", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindCategoryWorkOutPlan();
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

        BindFindWorkOutPlanList(lblcategoryId.Text);
        btnSubmit.Text = "Update";
        divGv.Visible = false;
        DivForm.Visible = true;
        divEditDtl.Visible = true;
        divAddDtl.Visible = false;
    }
    #endregion

    #region Bind Edit Food item List
    public void BindFindWorkOutPlanList(string categoryId)
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
                string sUrl = Session["BaseUrl"].ToString().Trim() + "CategoryWorkOutPlan?branchId=" + Session["branchId"].ToString() + ""
                   + "&gymOwnerId=" + Session["gymOwnerId"].ToString() + "&categoryId=" + categoryId.Trim() + "";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;
                string Response;
                var Locresponse = response.Content.ReadAsStringAsync().Result;
                int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());

                if (response.IsSuccessStatusCode)
                {
                    if (statusCode == 1)
                    {
                        string ResponseMsg = JObject.Parse(Locresponse)["CategoryWorkOutPlan"].ToString();

                        List<CategoryWorkPlanGet> FoodList = JsonConvert.DeserializeObject<List<CategoryWorkPlanGet>>(ResponseMsg);
                        DataTable dt = ConvertToDataTable(FoodList);
                        dtlEditWorkOutList.DataSource = FoodList;
                        dtlEditWorkOutList.DataBind();
                    }
                    else
                    {
                        Response = JObject.Parse(Locresponse)["Response"].ToString();
                        //DivForm.Visible = true;
                        //divGv.Visible = false;
                        dtlEditWorkOutList.DataBind();
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
            var WorkOut = e.Item.DataItem as CategoryWorkPlanGet;
            var dtlWorkOutLists = e.Item.FindControl("dtlWorkOutLists") as DataList;

            dtlWorkOutLists.DataSource = WorkOut.WorkOutList;
            dtlWorkOutLists.DataBind();
            CheckBoxList Work = dtlWorkOutLists.Items[0].FindControl("chckWorKout") as CheckBoxList;

            Work.DataSource = WorkOut.WorkOutList;
            Work.DataTextField = "workoutType";
            Work.DataValueField = "workoutTypeId";
            Work.DataBind();
            for (int i = 0; i < Work.Items.Count; i++)
            {
                Label lblactiveStatus = dtlWorkOutLists.Items[i].FindControl("lblactiveStatus") as Label;
                if (lblactiveStatus.Text == "A")
                {
                    Work.Items[i].Selected = true;
                }
                else
                {
                    Work.Items[i].Selected = false;
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
            string workoutTypeId = string.Empty;
            string workoutCatTypeId = string.Empty;
            string activeStatus = string.Empty;
            string uniqueId = string.Empty;
            for (int i = 0; i < dtlEditWorkOutList.Items.Count; i++)
            {
                DataList dtlWorkOutLists = dtlEditWorkOutList.Items[i].FindControl("dtlWorkOutLists") as DataList;
                Label lblworkoutCatTypeId = dtlEditWorkOutList.Items[i].FindControl("lblworkoutCatTypeId") as Label;

                for (int j = 0; j < dtlWorkOutLists.Items.Count; j++)
                {
                    CheckBoxList chckWorKout = dtlWorkOutLists.Items[j].FindControl("chckWorKout") as CheckBoxList;


                    for (int m = 0; m < chckWorKout.Items.Count; m++)
                    {
                        Label lbluniqueId = dtlWorkOutLists.Items[m].FindControl("lbluniqueId") as Label;
                        workoutTypeId += chckWorKout.Items[m].Value + ",";
                        workoutCatTypeId += lblworkoutCatTypeId.Text + ',';
                     
                        uniqueId += lbluniqueId.Text + ',';
                        if (chckWorKout.Items[m].Selected == true)
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
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Any One WorkOut');", true);
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
                    lstWorkOutFoodMenu = UpdateWorkOutPlanMenu(workoutCatTypeId.ToString().TrimEnd(','), workoutTypeId.ToString().TrimEnd(','),
                           ddlWorkOutType.SelectedValue,  activeStatus.ToString().TrimEnd(','),
                           Session["userId"].ToString(), uniqueId.ToString().TrimEnd(','), Session["branchId"].ToString(), Session["gymOwnerId"].ToString())
                };
                HttpResponseMessage response = client.PostAsJsonAsync("CategoryWorkOutPlan/update", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindCategoryWorkOutPlan();
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
    #region Sumbmit Click Button
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
    public static List<InsertWorkOutMenu> InsertWorkOutPlanMenu(string workoutCatTypeId, string workoutTypeId, string WorKoutCatId,
         string activeStatus, string createdBy, string branchId, string GymOwnerId)
    {
        string[] workoutTypeIds;
        string[] workoutCatTypeIds;
        string[] activeStatuss;

        workoutTypeIds = workoutTypeId.Split(',');
        workoutCatTypeIds = workoutCatTypeId.Split(',');
       
        activeStatuss = activeStatus.Split(',');

        List<InsertWorkOutMenu> lst = new List<InsertWorkOutMenu>();
        for (int i = 0; i < workoutTypeIds.Count(); i++)
        {
            lst.AddRange(new List<InsertWorkOutMenu>
            {
                new InsertWorkOutMenu  { workoutCatTypeId= workoutCatTypeIds[i],workoutTypeId=workoutTypeIds[i],
                    categoryId=WorKoutCatId,activeStatus=activeStatuss[i],
                    createdBy=createdBy,branchId=branchId,gymOwnerId=GymOwnerId
                }

            }); ;
        }
        return lst;

    }
    public static List<UpdateWorkOutMenu> UpdateWorkOutPlanMenu(string workoutCatTypeId, string workoutTypeId,
        string WorKoutCatId, string activeStatus, string UpdatedBy, string uniqueId, string branchId, string GymOwnerId)
    {
        string[] workoutTypeIds;
        string[] workoutCatTypeIds;

        string[] uniqueIds;
        string[] activeStatuss;

        workoutTypeIds = workoutTypeId.Split(',');
        workoutCatTypeIds = workoutCatTypeId.Split(',');
        uniqueIds = uniqueId.Split(',');
        activeStatuss = activeStatus.Split(',');
        List<UpdateWorkOutMenu> lst = new List<UpdateWorkOutMenu>();
        for (int i = 0; i < workoutTypeIds.Count(); i++)
        {
            lst.AddRange(new List<UpdateWorkOutMenu>
            {
                new UpdateWorkOutMenu { workoutCatTypeId= workoutCatTypeIds[i],workoutTypeId=workoutTypeIds[i],
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
        public List<InsertWorkOutMenu> lstWorkOutFoodMenu { get; set; }
    }
    public class UpdateCategoryDietPlan_In
    {
        public List<UpdateWorkOutMenu> lstWorkOutFoodMenu { get; set; }
    }
    public class InsertWorkOutMenu
    {
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string workoutCatTypeId { get; set; }
        public string workoutTypeId { get; set; }
        public string categoryId { get; set; }
        public string activeStatus { get; set; }
        public string createdBy { get; set; }

    }
    public class UpdateWorkOutMenu
    {
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string uniqueId { get; set; }
        public string workoutCatTypeId { get; set; }
        public string workoutTypeId { get; set; }
        public string categoryId { get; set; }
        public string activeStatus { get; set; }
        public string updatedBy { get; set; }

    }

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

    public class CategoryWorkPlanGet
    {
        public string workoutCatTypeId { get; set; }
        public string workoutCatTypeName { get; set; }
        public List<WorkOutLists> WorkOutList { get; set; }

    }
    public class WorkOutLists
    {
        public string uniqueId { get; set; }
        public string workoutCatTypeId { get; set; }
        public string workoutTypeId { get; set; }
        public string categoryId { get; set; }
        public string workoutType { get; set; }
        public string activeStatus { get; set; }

    }

    #endregion
}