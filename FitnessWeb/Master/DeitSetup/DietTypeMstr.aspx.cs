using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_DietTypeMstr : System.Web.UI.Page
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
            BindDietType();
            BindDietTypeddl();
        }
    }

    #region Bind Diet Type ddl
    public void BindDietTypeddl()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "configMaster/getDropDownDetails?typeId=14";
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
                            ddlDietType.DataTextField = "configName";
                            ddlDietType.DataValueField = "configId";
                            ddlDietType.DataBind();
                        }
                        else
                        {
                            ddlDietType.DataBind();
                        }
                        
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlDietType.Items.Insert(0, new ListItem("Diet Type *", "0"));
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
    #region Add Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divGv.Visible = false;
        DivForm.Visible = true;
    }
    #endregion

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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "dietTypeMaster";
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
                            gvDietMstr.DataSource = dt;
                            gvDietMstr.DataBind();
                            divGv.Visible = true;
                            DivForm.Visible = false;
                        }
                        else
                        {
                            gvDietMstr.DataSource = null;
                            gvDietMstr.DataBind();
                            divGv.Visible = false;
                            DivForm.Visible = true;
                        }
                       
                    }
                    else
                    {
                        gvDietMstr.DataSource = null;
                        gvDietMstr.DataBind();
                        divGv.Visible = false;
                        DivForm.Visible = true;
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

    #region Insert Diet Type
    public void InsertDiet()
    {
        int StatusCodes;
        string ImageUrl;
        if (Fuimage.HasFile)
        {
            helper.UploadImage(Fuimage, Session["BaseUrl"].ToString().Trim() + "UploadImage", out StatusCodes, out ImageUrl);
        }
        else
        {
            ImageUrl = "";
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
                var Insert = new InsertDietType_In()
                {
                    dietTypeNameId = ddlDietType.SelectedValue,
                    description = txtDescription.Text.Trim(),
                    typeIndicationImageUrl = txtTypeIndicationUrl.Text.Trim(),
                    imageUrl = ImageUrl,
                    createdBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("dietTypeMaster/insert", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {                       
                        BindDietType();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    DietClear();
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region Update Diet Type
    public void UpdateDiet()
    {
        
        int StatusCodes;
        string ImageUrl;
        if (Fuimage.HasFile)
        {
                helper.UploadImage(Fuimage, Session["BaseUrl"].ToString().Trim() + "UploadImage", out StatusCodes, out ImageUrl);
        }
        else
        {
           ImageUrl = hfImageUrl.Value;
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
                var Update = new InsertDietType_In()
                {
                    dietTypeId=ViewState["dietTypeId"].ToString(),
                    dietTypeNameId = ddlDietType.SelectedValue,
                    description = txtDescription.Text.Trim(),
                    typeIndicationImageUrl = txtTypeIndicationUrl.Text.Trim(),
                    imageUrl = ImageUrl,
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("dietTypeMaster/update", Update).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindDietType();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    DietClear();

                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region Btnsubmit Click Event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if(btnSubmit.Text=="Submit")
        {
            InsertDiet();
        }
        else
        {
            UpdateDiet();
        }
    }
    #endregion

    #region Btn Edit Click Event
    protected void LnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton lnkbtn = sender as ImageButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            
            Label lbldietTypeId = (Label)gvrow.FindControl("lbldietTypeId");
            Label lbldietTypeNameId = (Label)gvrow.FindControl("lbldietTypeNameId");
            BindDietTypeddl();
            ddlDietType.SelectedValue = lbldietTypeNameId.Text.Trim();
            ddlDietType.Enabled = false;
            Label lbldescription = (Label)gvrow.FindControl("lbldescription");
            txtDescription.Text = lbldescription.Text.Trim();
            Label lblimageUrl = (Label)gvrow.FindControl("lblimageUrl");
            imgDietPhotoPrev.ImageUrl = lblimageUrl.Text.Trim();
            
            if (imgDietPhotoPrev.ImageUrl == "")
            {
                imgDietPhotoPrev.ImageUrl = "~/img/Defaultupload.png";
            }

            hfImageUrl.Value = imgDietPhotoPrev.ImageUrl;
            
            Label lbltypeIndicationImageUrl = (Label)gvrow.FindControl("lbltypeIndicationImageUrl");
            txtTypeIndicationUrl.Text = lbltypeIndicationImageUrl.Text.Trim();
            
            ViewState["dietTypeId"] = lbldietTypeId.Text.Trim();
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

    #region Active or Inactive  Click Event
    protected void lnkActiveOrInactive_Click(object sender, EventArgs e)
    {
      
        try
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            Label lbldietTypeId = (Label)gvrow.FindControl("lbldietTypeId");
            
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
                var activeOrInActive = new InsertDietType_In()
                {
                    queryType= QueryType.Trim(),
                    dietTypeId = lbldietTypeId.Text.Trim(),
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("dietTypeMaster/activeOrInActive", activeOrInActive).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {                        
                        BindDietType();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    DietClear();
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        DietClear();
    }
    #endregion

    #region Clear
    public void DietClear()
    {
        ddlDietType.ClearSelection();
        ddlDietType.Enabled = true;
        txtDescription.Text = string.Empty;
        txtTypeIndicationUrl.Text = string.Empty;
        hfImageUrl.Value = string.Empty;
        imgDietPhotoPrev.ImageUrl = "~/img/Defaultupload.png";
        DivForm.Visible = false;
        divGv.Visible = true;
        btnSubmit.Text = "Submit";       
    }
    #endregion

    #region Diet Type Class
    public class InsertDietType_In
    {
        public string queryType { get; set; }
        public string dietTypeId { get; set; }
        public string dietTypeNameId { get; set; }
        public string dietTypeName { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }
        public string typeIndicationImageUrl { get; set; }
        public string activeStatus { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }

    }
    #endregion



    
}