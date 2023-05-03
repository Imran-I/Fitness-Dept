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

public partial class Master_WorkoutType : System.Web.UI.Page
{
    Helper helper = new Helper();
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
            BindWorkOutType();
            GetWorkOutType();
        }
    }
    #endregion
    #region Bind DropDown WorkOut Type
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
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            ddlWorkoutType.DataSource = dt;
                            ddlWorkoutType.DataTextField = "configName";
                            ddlWorkoutType.DataValueField = "configId";
                            ddlWorkoutType.DataBind();
                        }
                        else
                        {
                            ddlWorkoutType.DataBind();
                        }

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlWorkoutType.Items.Insert(0, new ListItem("WorkoutType *", "0"));
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
    #region Get WorkOutType
    public void GetWorkOutType()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "workoutType?gymOwnerId="
                             + Session["gymOwnerId"].ToString().Trim() + "&branchId="
                             + Session["branchId"].ToString().Trim() + "";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;
                btnCancel.Visible = false;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        gvWorkoutTypeMstr.DataSource = dt;
                        gvWorkoutTypeMstr.DataBind();
                        divGv.Visible = true;
                        DivForm.Visible = false;
                        btnCancel.Visible = true;
                    }
                    else
                    {
                        divGv.Visible = false;
                        DivForm.Visible = true;
                    }
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
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
    #region Add Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divGv.Visible = false;
        DivForm.Visible = true;
        btnSubmit.Text = "Submit";
    }
    #endregion
    #region Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        WorkOutTypeclear();
    }
    #endregion
    #region BtnSubmit
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubmit.Text == "Submit")
        {
            InsertWorkOutType();
        }
        else
        {
            UpdateWorkOutType();
        }

    }
    #endregion
    #region Insert WorkOut Type
    public void InsertWorkOutType()
    {
        try
        {
            int SCode;
            string Response;
            helper.UploadImage(fuimage, Session["BaseUrl"].ToString().Trim() + "UploadImage", out SCode, out Response);
            if (SCode == 0)
            {
                Response = null;
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new WorkoutType()
                {
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    workoutCatTypeId = ddlWorkoutType.SelectedValue,
                    workoutType = txtWorkOutType.Text,
                    description = txtDescription.Text,
                    imageUrl = Response,
                    video = txtVideoUrl.Text,
                    calories=txtcalories.Text,
                    createdBy = Session["gymOwnerId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("workoutType/insert", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        WorkOutTypeclear();
                        GetWorkOutType();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        divGv.Visible = true;
                        DivForm.Visible = false;
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
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
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
    #region Update WorkOut Type
    public void UpdateWorkOutType()
    {
        try
        {
            int SCode;
            string Response;
            if (fuimage.HasFile)
            {

                helper.UploadImage(fuimage, Session["BaseUrl"].ToString().Trim() + "UploadImage", out SCode, out Response);
                if (SCode == 0)
                {
                    Response = null;
                }
            }
            else
            {
                Response = imgpreview.Src;
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new WorkoutType()
                {
                    workoutTypeId = ViewState["workoutTypeId"].ToString(),
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    workoutCatTypeId = ddlWorkoutType.SelectedValue,
                    workoutType = txtWorkOutType.Text,
                    description = txtDescription.Text,
                    imageUrl = Response,
                    video = txtVideoUrl.Text,
                    calories = txtcalories.Text,
                    updatedBy = Session["gymOwnerId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("workoutType/update", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        WorkOutTypeclear();
                        GetWorkOutType();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        divGv.Visible = true;
                        DivForm.Visible = false;
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
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
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
    #region Clear
    public void WorkOutTypeclear()
    {
        txtDescription.Text = "";
        txtVideoUrl.Text = "";
        txtWorkOutType.Text = "";
        ddlWorkoutType.SelectedValue = "0";
        imgpreview.Src = "../../img/Defaultupload.png";
        btnSubmit.Text = "Submit";
    }
    #endregion    
    #region Btn Edit Click Event
    protected void LnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton lnkbtn = sender as ImageButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;

            Label lblworkoutTypeId = (Label)gvrow.FindControl("lblworkoutTypeId");
            Label lblworkoutCatTypeId = (Label)gvrow.FindControl("lblworkoutCatTypeId");
            Label lblworkoutTypeName = (Label)gvrow.FindControl("lblworkoutTypeName");
            Label lblworkoutType = (Label)gvrow.FindControl("lblworkoutType");
            Label lbldescription = (Label)gvrow.FindControl("lbldescription");
            Label lblimageUrl = (Label)gvrow.FindControl("lblimageUrl");
            Label lblvideo = (Label)gvrow.FindControl("lblvideo");
            Label lblcalories = (Label)gvrow.FindControl("lblcalories");
            txtDescription.Text = lbldescription.Text.Trim();
            txtVideoUrl.Text = lblvideo.Text.Trim();
            txtWorkOutType.Text = lblworkoutType.Text.Trim();
            txtcalories.Text = lblcalories.Text.Trim();
            ddlWorkoutType.SelectedValue = lblworkoutCatTypeId.Text.Trim();
            if (lblimageUrl.Text.Trim() == "")
            {
                imgpreview.Src = "../../img/Defaultupload.png";
            }
            else
            {
                imgpreview.Src = lblimageUrl.Text.Trim();
            }

            ViewState["workoutTypeId"] = lblworkoutTypeId.Text.Trim();
            btnSubmit.Text = "Update";
            divGv.Visible = false;
            DivForm.Visible = true;

        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }

    }
    #endregion
    #region Active or Inactive  Click Event
    protected void lnkActiveOrInactive_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            Label lblworkoutTypeId = (Label)gvrow.FindControl("lblworkoutTypeId");

            LinkButton lblActiveStatus = (LinkButton)lnkbtn.FindControl("lnkActiveOrInactive");
            string sActiveStatus = lblActiveStatus.Text.Trim() == "Active" ? "A" : "D";
            string QueryType = string.Empty;
            if (sActiveStatus.Trim() == "D")
            {
                QueryType = "active";
            }
            else
            {
                QueryType = "inActive";
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var activeOrInActive = new WorkoutTypeActive()
                {
                    queryType = QueryType.Trim(),
                    workoutTypeId = lblworkoutTypeId.Text.Trim(),
                    updatedBy = Session["gymOwnerId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("workoutType/activeOrInActive", activeOrInActive).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        GetWorkOutType();
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
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region WorkoutType Insert & Update Classes
    public class WorkoutType
    {
        public string workoutTypeId { get; set; }
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string workoutCatTypeId { get; set; }
        public string workoutType { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }
        public string video { get; set; }
        public string calories { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
    }
    public class WorkoutTypeActive
    {
        public string queryType { get; set; }
        public string workoutTypeId { get; set; }
        public string updatedBy { get; set; }
    }
    #endregion
}