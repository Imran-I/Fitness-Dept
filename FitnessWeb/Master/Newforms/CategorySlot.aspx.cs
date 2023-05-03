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

public partial class Master_FitnessCategorySlot : System.Web.UI.Page
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
            BindCategorySlot();
            GetTrainer();
            GetTrainingType();
            GetSlot();
        }
    }

    #region Add Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        // ViewSlotList.Visible = false;
        divGv.Visible = false;
        DivForm.Visible = true;
    }
    #endregion

    #region Bind CategorySlot
    public void BindCategorySlot()
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
                string Endpoint = "categorySlot?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["CategorySlot"].ToString();
                    if (StatusCode == 1)
                    {
                        List<Slots> lst = JsonConvert.DeserializeObject<List<Slots>>(ResponseMsg);

                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            gvWorkingDay.DataSource = lst;
                            gvWorkingDay.DataBind();
                        }
                        else
                        {
                            gvWorkingDay.DataBind();
                        }

                    }
                    else
                    {
                        divGv.Visible = false;
                        DivForm.Visible = true;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }

                }
                else
                {
                    divGv.Visible = false;
                    DivForm.Visible = true;
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('"+ ResponseMsg .ToString().Trim()+ "');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }

    protected void gvWorkingDay_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            var slot = e.Row.DataItem as Slots;
            DataList dt = e.Row.FindControl("dtlSlotDetails") as DataList;
            dt.DataSource = slot.CategorySlotDetails;
            dt.DataBind();
        }

    }
    #endregion

    #region Get Trainer
    public void GetTrainer()
    {
        try
        {
            ddlTrainer.Items.Clear();
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
                            ddlTrainer.DataSource = dt;
                            ddlTrainer.DataTextField = "trainerName";
                            ddlTrainer.DataValueField = "trainerId";
                            ddlTrainer.DataBind();
                        }
                        else
                        {
                            ddlTrainer.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlTrainer.Items.Insert(0, new ListItem("Trainer *", "0"));
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

    #region Get CategorySpecialist
    protected void ddlTrainer_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetSpecialIst();
    }
    public void GetSpecialIst()
    {
        try
        {
            ddlSpecialist.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "trainerDetails/getTrainerSpecialist?trainerId=" + ddlTrainer.SelectedValue + "";
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
                            ddlSpecialist.DataSource = dt;
                            ddlSpecialist.DataTextField = "specialistTypeName";
                            ddlSpecialist.DataValueField = "specialistTypeId";
                            ddlSpecialist.DataBind();
                        }
                        else
                        {
                            ddlSpecialist.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlSpecialist.Items.Insert(0, new ListItem("Specialist *", "0"));
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
    #region Get TrainingType
    public void GetTrainingType()
    {
        try
        {
            ddltrainingType.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "trainingTypeMaster/getDropDownDeatils?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
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
                            ddltrainingType.DataSource = dt;
                            ddltrainingType.DataTextField = "trainingTypeName";
                            ddltrainingType.DataValueField = "trainingTypeId";
                            ddltrainingType.DataBind();
                        }
                        else
                        {
                            ddltrainingType.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddltrainingType.Items.Insert(0, new ListItem("Training Type *", "0"));
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

    #region Get Slot
    public void GetSlot()
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
                string Endpoint = "slotMaster/GetSlotstoActivate?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
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
                            chkSlotList.DataSource = dt;
                            chkSlotList.DataTextField = "SlotTime";
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

    #region btn submit
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubmit.Text == "Submit")
        {
            InsertCategorySlot();
        }
        else
        {
            UpdateCategorySlot();
        }

    }
    #endregion
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
                    lstOfCategorySlots = InsertCategorySlot(Session["gymOwnerId"].ToString(),
                   Session["branchId"].ToString(), slotId.ToString().TrimEnd(','),
                    ddlSpecialist.SelectedValue, ddltrainingType.SelectedValue, ddlTrainer.SelectedValue,
                    Session["userId"].ToString(), txtgymStrength.Text)
                };
                HttpResponseMessage response = client.PostAsJsonAsync("categorySlot/insert", Insert).Result;
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
                        
                        if (msgs.Contains("Category Slot Details Is Already Exists !!!"))
                        {
                       
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Category Slot Details Is Already Exists !!!');", true);
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        }
                        
                    }
                    BindCategorySlot();
                    Clear();
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
        string slotId, string categoryId, string trainingTypeId, string trainerId, string createdBy, string gymStrength)
    {
        string[] slotIds;
        slotIds = slotId.Split(',');

        List<CategorySlots> lst = new List<CategorySlots>();
        for (int i = 0; i < slotIds.Count(); i++)
        {
            lst.AddRange(new List<CategorySlots>
            {
                new CategorySlots { gymOwnerId=gymOwnerId,branchId=branchId ,trainerId=trainerId,
                slotId=slotIds[i],categoryId=categoryId,trainingTypeId=trainingTypeId,createdBy=createdBy,gymStrength=gymStrength
                }

            }); ;
        }
        return lst;

    }
    #endregion
    #region Update Category Slot
    public void UpdateCategorySlot()
    {
        string slotId = string.Empty;
        string categorySlotId = string.Empty;
        string activestatus = string.Empty;
      
        int Count = 0;

        DataTable dt = (DataTable)Session["Data"];
       
        for (int i = 0; i < chkSlotList.Items.Count; i++)
        {
            DataTable dts = new DataTable();
            string lblcategorySlotId = string.Empty;
            string lblSlotId = string.Empty;
            var Cs = dt.AsEnumerable().Where(x => x.Field<Int64>("slotId") == Convert.ToInt64(chkSlotList.Items[i].Value));
            if(Cs.Any())
            {
                 dts = Cs.CopyToDataTable();
                lblcategorySlotId = dts.Rows[0]["categorySlotId"].ToString();
                lblSlotId = dts.Rows[0]["slotId"].ToString();
            }
            else
            {
                lblcategorySlotId="";
            }

            
            if (lblSlotId.ToString() == chkSlotList.Items[i].Value)
            {
                if (chkSlotList.Items[i].Selected == true)
                {
                    Count = 1;
                    categorySlotId += lblcategorySlotId.ToString() + ',';
                    slotId += chkSlotList.Items[i].Value + ",";
                    activestatus += "A" + ',';
                }
                else
                {
                    categorySlotId += lblcategorySlotId.ToString() + ',';
                    slotId += chkSlotList.Items[i].Value + ",";
                    activestatus += "D" + ',';
                }

            }
            else
            {
                if (chkSlotList.Items[i].Selected == true)
                {
                    Count = 1;
                    categorySlotId += "1" + ',';
                    slotId += chkSlotList.Items[i].Value + ",";
                    activestatus += "A" + ',';
                }
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
                UpdateCategorySlots_In Insert = new UpdateCategorySlots_In()
                {
                    lstOfCategorySlots = UpdateCategorySlot(Session["gymOwnerId"].ToString(),
                   Session["branchId"].ToString(), slotId.ToString().TrimEnd(','),
                    ddlSpecialist.SelectedValue, ddltrainingType.SelectedValue, ddlTrainer.SelectedValue,
                    Session["userId"].ToString(), txtgymStrength.Text, categorySlotId.ToString().TrimEnd(','),
                    activestatus.ToString().TrimEnd(','))
                };
                HttpResponseMessage response = client.PostAsJsonAsync("categorySlot/update", Insert).Result;
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

                        if (msgs.Contains("Category Slot Details Is Already Exists !!!"))
                        {

                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Category Slot Details Is Already Exists !!!');", true);
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        }
                    }

                    BindCategorySlot();
                    Clear();
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
    public static List<UpdateCategorySlots> UpdateCategorySlot(string gymOwnerId, string branchId,
       string slotId, string categoryId, string trainingTypeId, string trainerId, string updatedBy,
       string gymStrength,string categorySlotId,string activeStatus)
    {
        string[] activeStatuss;
        activeStatuss = activeStatus.Split(',');  
        string[] categorySlotIds;
        categorySlotIds = categorySlotId.Split(',');
        string[] slotIds;
        slotIds = slotId.Split(',');

        List<UpdateCategorySlots> lst = new List<UpdateCategorySlots>();
        for (int i = 0; i < slotIds.Count(); i++)
        {
            lst.AddRange(new List<UpdateCategorySlots>
            {
                new UpdateCategorySlots { gymOwnerId=gymOwnerId,branchId=branchId ,trainerId=trainerId,categorySlotId=categorySlotIds[i],
                slotId=slotIds[i],categoryId=categoryId,trainingTypeId=trainingTypeId,updatedBy=updatedBy,gymStrength=gymStrength,
                activeStatus=activeStatuss[i]
                }

            }); ;
        }
        return lst;

    }
    #endregion
    #region Edit Click
    protected void LnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnk = sender as ImageButton;
        GridViewRow gv = lnk.NamingContainer as GridViewRow;
        Label lbltrainerId = gv.FindControl("lbltrainerId") as Label;
        Label lblcategoryId = gv.FindControl("lblcategoryId") as Label;
        Label lbltrainingTypeId = gv.FindControl("lbltrainingTypeId") as Label;
        Label lblgymStrength = gv.FindControl("lblgymStrength") as Label;
        ddlTrainer.SelectedValue = lbltrainerId.Text;
        GetSpecialIst();
        ddlSpecialist.SelectedValue = lblcategoryId.Text;
        ddltrainingType.SelectedValue = lbltrainingTypeId.Text;
        txtgymStrength.Text = lblgymStrength.Text;
        DataList dtlSlotDetails = gv.FindControl("dtlSlotDetails") as DataList;
        DataTable dt = new DataTable();
        dt.Columns.Add("slotId",typeof(Int64));
        dt.Columns.Add("categorySlotId", typeof(Int64));
        dt.Columns.Add("activeStatus", typeof(String));
        for (int i = 0; i < dtlSlotDetails.Items.Count; i++)
        {
            Label lblcategorySlotId = dtlSlotDetails.Items[i].FindControl("lblcategorySlotId") as Label;
            Label lblslotId = dtlSlotDetails.Items[i].FindControl("lblslotId") as Label;
            Label lblactiveStatus = dtlSlotDetails.Items[i].FindControl("lblactiveStatus") as Label;
            for (int j = 0; j < chkSlotList.Items.Count; j++)
            {
                if (chkSlotList.Items[j].Value == lblslotId.Text)
                {
                    if(lblactiveStatus.Text == "A")
                    {
                        chkSlotList.Items[j].Selected = true;
                    }
                  
                }
               

            }
            dt.Rows.Add(lblslotId.Text, lblcategorySlotId.Text, lblactiveStatus.Text);
        }
        Session["Data"] = dt;
        divGv.Visible = false;
        DivForm.Visible = true;
        btnSubmit.Text = "Update";
        ddlTrainer.Enabled = false;
        ddlSpecialist.Enabled = false;
        ddltrainingType.Enabled = false;
    }
    #endregion
    #region Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();

    }
    #endregion
    #region Clear
    public void  Clear()
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        ddlTrainer.ClearSelection();
        ddlSpecialist.ClearSelection();
        ddltrainingType.ClearSelection();
        chkSlotList.ClearSelection();
        txtgymStrength.Text = string.Empty;
        ddlTrainer.Enabled = true;
        ddlSpecialist.Enabled = true;
        ddltrainingType.Enabled = true;
        btnSubmit.Text = "Submit";
    }
    #endregion
    #region Category Slot Class
    public class InsertCategorySlots
    {

        public List<CategorySlots> lstOfCategorySlots { get; set; }
    }
    public class CategorySlots
    {

        public string slotId { get; set; }

        public string gymOwnerId { get; set; }

        public string branchId { get; set; }

        public string categoryId { get; set; }

        public string trainingTypeId { get; set; }

        public string trainerId { get; set; }

        public string gymStrength { get; set; }

        public string createdBy { get; set; }
    }

    public class Slots
    {
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string categoryId { get; set; }
        public string categoryName { get; set; }
        public string trainingTypeId { get; set; }
        public string trainingTypeName { get; set; }
        public string trainerId { get; set; }
        public string trainerName { get; set; }
        public string gymStrength { get; set; }
        public List<CategorySlotDetail> CategorySlotDetails { get; set; }

    }
    public class CategorySlotDetail
    {
        public string categorySlotId { get; set; }
        public string slotId { get; set; }
        public string activeStatus { get; set; }
        public string SlotTime { get; set; }
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string categoryId { get; set; }
        public string categoryName { get; set; }
        public string trainingTypeId { get; set; }
        public string trainingTypeName { get; set; }
        public string trainerId { get; set; }
        public string trainerName { get; set; }
    }

    public class UpdateCategorySlots_In
    {
        public List<UpdateCategorySlots> lstOfCategorySlots { get; set; }
    }
    public class UpdateCategorySlots
    {
        public string categorySlotId { get; set; }
        public string activeStatus { get; set; }
        public string slotId { get; set; }
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string categoryId { get; set; }
        public string trainingTypeId { get; set; }
        public string trainerId { get; set; }
        public string gymStrength { get; set; }
        public string updatedBy { get; set; }
    }

    #endregion
}