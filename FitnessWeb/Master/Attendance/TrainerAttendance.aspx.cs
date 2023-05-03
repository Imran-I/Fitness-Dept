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

public partial class Master_Attendance_TrainerAttendance : System.Web.UI.Page
{
    Helper helper = new Helper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null && Session["userRole"] == null)
        {
            Session.Clear();
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["LogoutUrl"].Trim(), true);
        }
        if (!IsPostBack)
        {
            BindTrainnerAttendance();
            GetTrainers();
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
                    "&branchId=" + Session["branchId"].ToString() + "";
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
                        divGv.Visible = true;
                        DivForm.Visible = false;
                        btnCancel.Visible = true;
                    }
                    else
                    {
                        divGv.Visible = false;
                        DivForm.Visible = true;
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divGv.Visible = false;
        DivForm.Visible = true;
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
        ddlTrainerList.SelectedValue = lblempId.Text.Trim();
        divGv.Visible = false;
        DivForm.Visible = true;
        divlogdate.Visible= false;
        divintime.Visible= true;
        
        btnSubmit.Text = "Update";
       ddlTrainerList.Enabled= false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
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
                    empId = ddlTrainerList.SelectedValue,
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
                        BindTrainnerAttendance();

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
                    empId = ddlTrainerList.SelectedValue,
                    inTime= txtShiftStartTime.Text,
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
                        BindTrainnerAttendance();

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
        ddlTrainerList.ClearSelection();
        txtShiftStartTime.Text = string.Empty;
        txtShiftEndTime.Text = string.Empty;
        txtlogDate.Text = string.Empty;
        DivForm.Visible = false;
        divGv.Visible = true;
        btnSubmit.Text = "Submit";
        divlogdate.Visible = true;
        divintime.Visible = true;
        divouttime.Visible = false;
        ddlTrainerList.Enabled = true;

    }
}