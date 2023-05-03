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

public partial class Master_AppSetting : System.Web.UI.Page
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
            BindOwner();
            GetAllAppSetting();
        }
    }
    #endregion

    #region Bind Owner List
    public void BindOwner()
    {
        try
        {
            ddlOwnerList.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sUrl = Session["BaseUrl"].ToString().Trim() + "ownerMaster/Getddlowner";
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
                            ddlOwnerList.DataSource = dt;
                            ddlOwnerList.DataTextField = "gymOwnerName";
                            ddlOwnerList.DataValueField = "gymOwnerId";
                          
                            if (Session["userRole"].ToString().Trim() == "Employee" || Session["userRole"].ToString().Trim() == "Admin")
                            {
                                var firstitem = ddlOwnerList.Items.FindByValue(Session["gymOwnerId"].ToString());
                                ddlOwnerList.Items.Clear();
                                ddlOwnerList.Items.Add(firstitem);
                                ddlOwnerList.SelectedValue = firstitem.Value;
                                ddlOwnerList.Enabled = false;
                            }
                        }
                        else
                        {
                            ddlOwnerList.DataBind();                           
                        }

                    }
                    else
                    {
                        ddlOwnerList.DataBind();                       
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlOwnerList.Items.Insert(0, new ListItem("Gym Owner *", "0"));
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

    #region Grid AppSetting
    public void GetAllAppSetting()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "appSettings/GetAllMstrAppSettings";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        divGv.Visible = true;
                        DivForm.Visible = false;
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        gvAppSettingMstr.DataSource = dt;
                        gvAppSettingMstr.DataBind();
                    }
                    else
                    {
                        gvAppSettingMstr.DataBind();
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
        BindOwner();
    }
    #endregion

    #region Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        clear();
    }
    #endregion

    #region BtnSubmit
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubmit.Text == "Submit")
        {
            InsertAppSetting();
        }
    }
    #endregion

    #region Insert App Setting
    public void InsertAppSetting()
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
                var Insert = new AppSetting()
                {
                    gymOwnerId = ddlOwnerList.SelectedValue,
                    appType = ddlappType.SelectedValue,
                    packageName = txtpackageName.Text,
                    appVersion = txtappVersion.Text,
                    versionChanges = txtversionChanges.Text,
                    createdBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("appSettings/insert", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        clear();
                        GetAllAppSetting();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        divGv.Visible = true;
                        DivForm.Visible = false;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
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
    public void clear()
    {
        ddlOwnerList.ClearSelection();
        ddlappType.ClearSelection();
        txtappVersion.Text = "";
        txtpackageName.Text = "";
        txtversionChanges.Text = "";
    }
    #endregion

    #region AppSetting Class 
    public class AppSetting
    {
        public int uniqueId { get; set; }
        public string gymOwnerId { get; set; }
        public string gymOwnerName { get; set; }
        public string appType { get; set; }
        public string appVersion { get; set; }
        public string packageName { get; set; }
        public string versionChanges { get; set; }
        public string activeStatus { get; set; }
        public string createdBy { get; set; }
    }

    #endregion
}