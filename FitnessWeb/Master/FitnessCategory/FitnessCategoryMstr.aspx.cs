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

public partial class Master_Configuration_FitnessCategoryMstr : System.Web.UI.Page
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
            BindCategory();
        }
    }
    #endregion

    #region Bind Category
    public void BindCategory()
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
                string Endpoint = "categoryMaster?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
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
                        gvCategory.DataSource = dt;
                        gvCategory.DataBind();
                        divGv.Visible = true;
                        DivForm.Visible = false;
                        DivSubForm.Visible = false;
                        btnCancel.Visible = true;
                    }
                    else
                    {
                        divGv.Visible = false;
                        DivForm.Visible = true;
                        DivSubForm.Visible = false;
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

    #region Add Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divGv.Visible = false;
        DivForm.Visible = true;
        DivSubForm.Visible = false;
    }
    #endregion
      
    #region Submit 
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        if (btnSubmit.Text == "Submit")
        {

            InsertCategory();
        }
        else
        {
            UpdateCategory();
        }

    }
    #endregion

    #region Insert Function 
    public void InsertCategory()
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
                var Insert = new Category()
                {
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    categoryName = txtCategoryName.Text,
                    description = txtDescription.Text,
                    imageUrl = Response,
                    createdBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("categoryMaster/insert", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindCategory();
                      
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
                CategoryClear();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region Update Function 
    public void UpdateCategory()
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
                var Insert = new Category()
                {
                    categoryId = ViewState["categoryId"].ToString(),
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    categoryName = txtCategoryName.Text,
                    description = txtDescription.Text,
                    imageUrl = Response,
                    updatedBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("categoryMaster/update", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindCategory();
                        
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
                CategoryClear();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region Btn Edit Click Event
    protected void LnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DivSubForm.Visible = false;
            ImageButton lnkbtn = sender as ImageButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            Label lblcategoryId = (Label)gvrow.FindControl("lblcategoryId");
            Label lblcategoryName = (Label)gvrow.FindControl("lblcategoryName");
            Label lbldescription = (Label)gvrow.FindControl("lbldescription");
            Label lblimageUrl = (Label)gvrow.FindControl("lblimageUrl");
            ViewState["categoryId"] = lblcategoryId.Text.Trim();
            txtCategoryName.Text = lblcategoryName.Text.Trim();
            txtCategoryName.Enabled = false;
            txtDescription.Text = lbldescription.Text.Trim();
            imgpreview.Src = lblimageUrl.Text.Trim();
            if (imgpreview.Src == "")
            {
                imgpreview.Src = "~/img/Defaultupload.png";
            }
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
            Label lblcategoryId = (Label)gvrow.FindControl("lblcategoryId");
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
                var activeOrInActive = new Category()
                {
                    queryType = QueryType.Trim(),
                    categoryId = lblcategoryId.Text.Trim(),
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("categoryMaster/activeOrInActive", activeOrInActive).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindCategory();
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

    #region Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        CategoryClear();
        DivForm.Visible = false;
    }
    #endregion

    #region Clear
    public void CategoryClear()
    {
        txtCategoryName.Enabled = true;
        txtCategoryName.Text = string.Empty;
        txtDescription.Text = string.Empty;
        imgpreview.Src = "~/img/Defaultupload.png";
        DivForm.Visible = false;
        divGv.Visible = true;
        btnSubmit.Text = "Submit";
    }
    #endregion

    //Benefits
    #region Get Category Benefits
    public void BindCategoryBenefits()
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
                string Endpoint = "categoryBenefitMaster/GetCategoryBenefitDep?categoryId=" + ViewState["categoryId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        gvCatBenefitsMstr.DataSource = dt;
                        gvCatBenefitsMstr.DataBind();
                    }
                    else
                    {
                        gvCatBenefitsMstr.DataSource = null;
                        gvCatBenefitsMstr.DataBind();

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

    #region Add Benefits Click
    protected void linkAddDetails_Click(object sender, EventArgs e)
    {
        DivSubForm.Visible = true;
        LinkButton lnkbtn = sender as LinkButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        Label lblcategoryId = (Label)gvrow.FindControl("lblcategoryId");
        Label lblcategoryName = (Label)gvrow.FindControl("lblcategoryName");
        ViewState["categoryId"] = lblcategoryId.Text.Trim();
        lblCategoryName.Text = lblcategoryName.Text.Trim();
        BenefitsClear();
        BindBenefitType();
        BindCategoryBenefits();
    }
    #endregion
   
    #region Bind Benefit Type
    public void BindBenefitType()
    {
        try

        {
            ddlType.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sUrl = Session["BaseUrl"].ToString().Trim() + "configMaster/getDropDownDetails?typeId=15";
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
                            ddlType.DataSource = dt;
                            ddlType.DataTextField = "configName";
                            ddlType.DataValueField = "configId";
                            ddlType.DataBind();
                        }
                        else
                        {
                            ddlType.DataBind();

                        }
                        ddlType.Items.Insert(0, new ListItem("Type *", "0"));
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

    #region Submit 
    protected void btnSubSubmit_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        if (btnSubSubmit.Text == "Submit")
        {
            InsertBenefit();
        }
        else
        {
            UpdateBenefit();
        }

    }
    #endregion
  
    #region Insert Function 
    public void InsertBenefit()
    {
        try
        {
            int StatusCodes;
            string ImageUrl;
            if (FuSubimage.HasFile)
            {
                helper.UploadImage(FuSubimage, Session["BaseUrl"].ToString().Trim() + "UploadImage", out StatusCodes, out ImageUrl);
            }
            else
            {
                ImageUrl = "";
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new CategoryBenefits()
                {
                    description = txtSubDescription.Text,
                    type = ddlType.SelectedValue,
                    categoryId = ViewState["categoryId"].ToString(),
                    imageUrl = ImageUrl,
                    createdBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("categoryBenefitMaster/insert", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindCategoryBenefits();
                        
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
                BenefitsClear();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region Update Function 
    public void UpdateBenefit()
    {
        try
        {
            int StatusCodes;
            string ImageUrl;
            if (FuSubimage.HasFile)
            {
                helper.UploadImage(FuSubimage, Session["BaseUrl"].ToString().Trim() + "UploadImage", out StatusCodes, out ImageUrl);
            }
            else
            {
                if (imgBeneFitsPhotoPrev.ImageUrl != "")
                {
                    ImageUrl = imgBeneFitsPhotoPrev.ImageUrl;
                }

                else
                {
                    ImageUrl = "";
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Update = new CategoryBenefits()
                {
                    uniqueId = ViewState["uniqueId"].ToString(),
                    description = txtSubDescription.Text,
                    type = ddlType.SelectedValue,
                    categoryId = ViewState["categoryId"].ToString(),
                    imageUrl = ImageUrl,
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("categoryBenefitMaster/update", Update).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindCategoryBenefits();
                      
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
                BenefitsClear();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region LnkSubEdit Click Event
    protected void LnkSubEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton lnkbtn = sender as ImageButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;

            Label lbluniqueId = (Label)gvrow.FindControl("lbluniqueId");
            Label lblcategoryId = (Label)gvrow.FindControl("lblcategoryId");
            Label lbltype = (Label)gvrow.FindControl("lbltype");
            ddlType.SelectedValue = lbltype.Text;
            ddlType.Enabled = false;
            Label lbldescription = (Label)gvrow.FindControl("lbldescription");
            txtSubDescription.Text = lbldescription.Text;
            Label lblimageUrl = (Label)gvrow.FindControl("lblimageUrl");

            imgBeneFitsPhotoPrev.ImageUrl = lblimageUrl.Text.Trim();

            if (imgBeneFitsPhotoPrev.ImageUrl == "")
            {
                imgBeneFitsPhotoPrev.ImageUrl = "~/img/Defaultupload.png";
            }

            ViewState["uniqueId"] = lbluniqueId.Text.Trim();
            btnSubSubmit.Text = "Update";
            gvCatBenefitsMstr.Visible = false;
           
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }

    }
    #endregion

    #region Cancel Click
    protected void btnSubCancel_Click(object sender, EventArgs e)
    {
        DivSubForm.Visible = false;
        BenefitsClear();
    }
    #endregion

    #region Clear
    public void BenefitsClear()
    {
        txtSubDescription.Text = string.Empty;
        ddlType.Enabled=true;
        ddlType.ClearSelection();
        FuSubimage.Dispose();
        imgBeneFitsPhotoPrev.ImageUrl = "~/img/Defaultupload.png";
        DivForm.Visible = false;
        divGv.Visible = true;
        btnSubSubmit.Text = "Submit";
        gvCatBenefitsMstr.Visible = true;
    }
    #endregion

    #region Active or Inactive  Click Event
    protected void lnkActiveOrInactiveSub_Click(object sender, EventArgs e)
    {

        try
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            Label lbluniqueId = (Label)gvrow.FindControl("lbluniqueId");

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
                var activeOrInActive = new CategoryBenefits()
                {
                    queryType = QueryType.Trim(),
                    uniqueId = lbluniqueId.Text.Trim(),
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("categoryBenefitMaster/activeOrInActive", activeOrInActive).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindCategoryBenefits();
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
                BenefitsClear();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region Category Class
    public class Category
    {
        public string queryType { get; set; }
        public string categoryId { get; set; }
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string categoryName { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
    }
    #endregion
    #region Category Benefits Class
    public class CategoryBenefits
    {
        public string queryType { get; set; }
        public string uniqueId { get; set; }
        public string categoryId { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
    }
    #endregion
}