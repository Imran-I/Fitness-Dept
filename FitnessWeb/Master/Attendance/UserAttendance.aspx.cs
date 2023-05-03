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


public partial class Master_Attendance_UserAttendance : System.Web.UI.Page
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
            BindUserAttendance();
            GetUsers();
        }
    }


    #region Bind User Attendance
    public void BindUserAttendance()
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
                string Endpoint = "UserAttendance?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                    "&branchId=" + Session["branchId"].ToString() + "&trainerId=" +  Session["userId"].ToString();
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        gvUser.DataSource = dt;
                        gvUser.DataBind();
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

    #region Get Users
    public void GetUsers()
    {
        try
        {
            ddlUserList.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "UserAttendance/GetTrainerBasedUsers?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "&trainerId=" + Session["userId"].ToString();
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
                            ddlUserList.DataSource = dt;
                            ddlUserList.DataTextField = "UserName";
                            ddlUserList.DataValueField = "userId";
                            ddlUserList.DataBind();
                        }
                        else
                        {
                            ddlUserList.DataBind();
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlUserList.Items.Insert(0, new ListItem("Users Name *", "0"));
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
        Label lblUserId = (Label)gvrow.FindControl("lblUserId");
        Label lblOutTime = (Label)gvrow.FindControl("lblOutTime");
        Label lblinTime = (Label)gvrow.FindControl("lblinTime");
        ViewState["UserId"] = lbluniqueId.Text.Trim();
        ddlUserList.SelectedValue = lblUserId.Text.Trim();
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
        divGv.Visible = false;
        DivForm.Visible = true;
        divlogdate.Visible = false;
        divintime.Visible = true;
       
        btnSubmit.Text = "Update";
        ddlUserList.Enabled= false;

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        if (btnSubmit.Text == "Submit")
        {

            InsertUser();
        }
        else
        {
            UpdateUser();
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        UserClear();
    }

    #region Insert Function 
    public void InsertUser()
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
                var Insert = new UserInsert()
                {
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    userId = ddlUserList.SelectedValue,
                    logDate = txtlogDate.Text,
                    inTime = txtShiftStartTime.Text,
                    createdBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("UserAttendance/insert", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindUserAttendance();

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
                UserClear();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region Update Function 
    public void UpdateUser()
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
                var Insert = new UserInsert()
                {
                    UniqueId = ViewState["UserId"].ToString(),
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    userId = ddlUserList.SelectedValue,
                    OutTime = txtShiftEndTime.Text,
                    inTime = txtShiftStartTime.Text,
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("UserAttendance/update", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindUserAttendance();

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
                UserClear();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region User Insert
    public class UserInsert
    {
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string UniqueId { get; set; }
        public string userId { get; set; }
        public string logDate { get; set; }
        public string inTime { get; set; }
        public string OutTime { get; set; }
        public string bookingId { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
    }
    #endregion

    public void UserClear()
    {
        ddlUserList.ClearSelection();
        txtShiftStartTime.Text = string.Empty;
        txtShiftEndTime.Text = string.Empty;
        txtlogDate.Text = string.Empty;
        DivForm.Visible = false;
        divGv.Visible = true;
        btnSubmit.Text = "Submit";
        divlogdate.Visible = true;
        divintime.Visible = true;
        divouttime.Visible = false;
        ddlUserList.Enabled = true;

    }
}