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

public partial class Master_Branch_TrainerUserAssign : System.Web.UI.Page
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
            
            GetTrainerReassignDetails();
            ddlCategory.Items.Insert(0, new ListItem("Category *", "0"));
            ddlTrainingType.Items.Insert(0, new ListItem("TrainingType *", "0"));
            ddlNewTrainer.Items.Insert(0, new ListItem("NewTrainers *", "0"));

        }

    }

    #region Add Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        // ViewSlotList.Visible = false;
        divGv.Visible = false;
        DivForm.Visible = true;
        GetTrainer();
    }
    #endregion
    #region Get Trainer
    public void GetTrainerReassignDetails()
    {
        try
        {
            ddlOldTrainer.Items.Clear();
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
                            gvTrainerReassign.DataSource = dt;
                            gvTrainerReassign.DataBind();
                        }
                        else
                        {
                            gvTrainerReassign.DataBind();
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
            ddlOldTrainer.Items.Clear();
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
                            ddlOldTrainer.DataSource = dt;
                            ddlOldTrainer.DataTextField = "trainerName";
                            ddlOldTrainer.DataValueField = "trainerId";
                            ddlOldTrainer.DataBind();
                        }
                        else
                        {
                            ddlOldTrainer.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlOldTrainer.Items.Insert(0, new ListItem("Trainer *", "0"));
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
            ddlCategory.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "trainerReassign/getddlDetails?queryType=GetAbsentSpecialist&gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "&trainerId="+ddlOldTrainer.SelectedValue+"";
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
                            ddlCategory.DataSource = dt;
                            ddlCategory.DataTextField = "categoryName";
                            ddlCategory.DataValueField = "categoryId";
                            ddlCategory.DataBind();
                        }
                        else
                        {
                            ddlCategory.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlCategory.Items.Insert(0, new ListItem("Category *", "0"));
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
        GetTrainingType();

    }
    #region Get TrainingType
    public void GetTrainingType()
    {
        try
        {
            ddlTrainingType.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "trainerReassign/getddlDetails?queryType=GetAbsentTrainingType&gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "&trainerId=" + ddlOldTrainer.SelectedValue + " " +
                   "&categoryId="+ddlCategory.SelectedValue+"";
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
                            ddlTrainingType.DataSource = dt;
                            ddlTrainingType.DataTextField = "trainingTypeName";
                            ddlTrainingType.DataValueField = "trainingTypeId";
                            ddlTrainingType.DataBind();
                        }
                        else
                        {
                            ddlTrainingType.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlTrainingType.Items.Insert(0, new ListItem("TrainingType *", "0"));
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
        GetSlot();
        divSlotList.Visible = true;
    }
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
                string Endpoint = "trainerReassign/getddlDetails?queryType=GetAbsentTrainerSlot&gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "&trainerId=" + ddlOldTrainer.SelectedValue + " " +
                   "&categoryId=" + ddlCategory.SelectedValue + "&trainingTypeId="+ddlTrainingType.SelectedValue+"";
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
            for(int i=0;i<chkSlotList.Items.Count;i++)
            {
                if (chkSlotList.Items[i].Selected == true)
                {
                    SlotId += chkSlotList.Items[i].Value + ',';
                }
            }
            ddlNewTrainer.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "trainerReassign/getddlDetails?queryType=GetNewTrainerForAssign&gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "&trainerId=" + ddlOldTrainer.SelectedValue + " " +
                   "&categoryId=" + ddlCategory.SelectedValue + "&trainingTypeId=" + ddlTrainingType.SelectedValue + "" +
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
                            ddlNewTrainer.DataSource = dt;
                            ddlNewTrainer.DataTextField = "trainerName";
                            ddlNewTrainer.DataValueField = "trainerId";
                            ddlNewTrainer.DataBind();
                        }
                        else
                        {
                            ddlNewTrainer.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlNewTrainer.Items.Insert(0, new ListItem("New Trainer *", "0"));
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
    protected void btnSubmit_Click(object sender, EventArgs e)
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
                    ddlNewTrainer.SelectedValue, ddlOldTrainer.SelectedValue, ddlCategory.SelectedValue, ddlTrainingType.SelectedValue,
                    Session["userId"].ToString(), txtFromDate.Text, txtToDate.Text)
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
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
        divGv.Visible = true;
        DivForm.Visible = false; 
        ddlOldTrainer.ClearSelection();
        ddlNewTrainer.ClearSelection();
        ddlTrainingType.ClearSelection();
        ddlCategory.ClearSelection();
        chkSlotList.ClearSelection();
        divSlotList.Visible = false;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        ddlOldTrainer.Enabled = true;
        ddlNewTrainer.Enabled = true;
        ddlTrainingType.Enabled = true;
        ddlCategory.Enabled = true;
        btnSubmit.Text = "Submit";
    }
    #endregion
}