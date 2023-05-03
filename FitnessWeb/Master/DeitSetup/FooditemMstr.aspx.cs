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

public partial class Master_FooditemMstr : System.Web.UI.Page
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
            Session["BaseUrl"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"].Trim();
            GetUnit();
            GetServingIn();
            GetDietType();
            BindFoodItem();

        }
    }
    #endregion

    #region Bind FoodItem
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
                string Endpoint = "foodItemMaster?gymOwnerId=" + Session["gymOwnerId"].ToString() + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        gvFoodItemMstr.DataSource = dt;
                        gvFoodItemMstr.DataBind();
                        DivForm.Visible = false;
                        divGv.Visible = true;
                    }
                    else
                    {
                        DivForm.Visible = true;
                        divGv.Visible = false;
                        gvFoodItemMstr.DataBind();
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
    #region Add Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divGv.Visible = false;
        DivForm.Visible = true;
    }
    #endregion

    #region Get Unit
    public void GetUnit()
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
                HttpResponseMessage response = client.GetAsync("configMaster/getDropDownDetails?typeId=17").Result;
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
                            ddlUnit.DataSource = dt;
                            ddlUnit.DataTextField = "configName";
                            ddlUnit.DataValueField = "configId";
                            ddlUnit.DataBind();
                        }
                        else
                        {
                            ddlUnit.DataBind();
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlUnit.Items.Insert(0, new ListItem("Unit  *", "0"));
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
    #region Get ServingIn
    public void GetServingIn()
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
                HttpResponseMessage response = client.GetAsync("configMaster/getDropDownDetails?typeId=18").Result;
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
                            ddlservingIn.DataSource = dt;
                            ddlservingIn.DataTextField = "configName";
                            ddlservingIn.DataValueField = "configId";
                            ddlservingIn.DataBind();
                        }
                        else
                        {
                            ddlservingIn.DataBind();
                        }

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlservingIn.Items.Insert(0, new ListItem("Serving In  *", "0"));
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
    #region Get DietType
    public void GetDietType()
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
                HttpResponseMessage response = client.GetAsync("dietTypeMaster/getDropDropDetails").Result;
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
                            ddldietType.DataSource = dt;
                            ddldietType.DataTextField = "dietTypeName";
                            ddldietType.DataValueField = "dietTypeId";
                            ddldietType.DataBind();
                        }
                        else
                        {
                            ddldietType.DataBind();
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddldietType.Items.Insert(0, new ListItem("Diet Type *", "0"));
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
    #region Submit 
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        if (btnSubmit.Text == "Submit")
        {

            InsertFoodItem();
        }
        else
        {
            UpdateFooditem();
        }

    }
    #endregion

    #region Insert Function 
    public void InsertFoodItem()
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
                var Insert = new Fooditem()
                {
                    dietTypeId = ddldietType.SelectedValue,
                    foodItemName = txtFoodItemName.Text,
                    unit = ddlUnit.SelectedValue,
                    servingIn = ddlservingIn.SelectedValue,
                    protein = txtprotein.Text,
                    carbs = txtcarbs.Text,
                    fat = txtfat.Text,
                    calories = txtcalories.Text,
                    imageUrl = Response,
                    createdBy = Session["userId"].ToString(),
                    gymOwnerId = Session["gymOwnerId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("foodItemMaster/insert", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindFoodItem();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    FoodItemClear();
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
    #region Update Function 
    public void UpdateFooditem()
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
                var Insert = new Fooditem()
                {
                    foodItemId = ViewState["foodItemId"].ToString(),
                    dietTypeId = ddldietType.SelectedValue,
                    foodItemName = txtFoodItemName.Text,
                    unit = ddlUnit.SelectedValue,
                    servingIn = ddlservingIn.SelectedValue,
                    protein = txtprotein.Text,
                    carbs = txtcarbs.Text,
                    fat = txtfat.Text,
                    calories = txtcalories.Text,
                    imageUrl = Response,
                    updatedBy = Session["userId"].ToString(),
                    gymOwnerId = Session["gymOwnerId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("foodItemMaster/update", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindFoodItem();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    FoodItemClear();
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

    #region Btn Edit Click Event
    protected void LnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton lnkbtn = sender as ImageButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            Label lblfoodItemId = (Label)gvrow.FindControl("lblfoodItemId");
            Label lbldietTypeId = (Label)gvrow.FindControl("lbldietTypeId");
            Label lblfoodItemName = (Label)gvrow.FindControl("lblfoodItemName");
            Label lblunit = (Label)gvrow.FindControl("lblunit");
            Label lblservingIn = (Label)gvrow.FindControl("lblservingIn");
            Label lblprotein = (Label)gvrow.FindControl("lblprotein");
            Label lblfat = (Label)gvrow.FindControl("lblfat");
            Label lblcalories = (Label)gvrow.FindControl("lblcalories");
            Label lblcarbs = (Label)gvrow.FindControl("lblcarbs");
            Label lblimageUrl = (Label)gvrow.FindControl("lblimageUrl");
            ViewState["foodItemId"] = lblfoodItemId.Text.Trim();
            txtFoodItemName.Text = lblfoodItemName.Text.Trim();
            txtFoodItemName.Enabled = false;
            GetUnit();
            GetServingIn();
            GetDietType();
            ddlUnit.SelectedValue = lblunit.Text.Trim();
            ddlservingIn.SelectedValue = lblservingIn.Text.Trim();
            ddldietType.SelectedValue = lbldietTypeId.Text.Trim();
            txtprotein.Text = lblprotein.Text.Trim();
            txtfat.Text = lblfat.Text.Trim();
            txtcarbs.Text = lblcarbs.Text.Trim();
            txtcalories.Text = lblcalories.Text.Trim();
            imgpreview.Src = lblimageUrl.Text.Trim();
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
            Label lblfoodItemId = (Label)gvrow.FindControl("lblfoodItemId");
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
                var activeOrInActive = new Fooditem()
                {
                    queryType = QueryType.Trim(),
                    foodItemId = lblfoodItemId.Text.Trim(),
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("foodItemMaster/activeOrInActive", activeOrInActive).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindFoodItem();
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
        FoodItemClear();
    }
    #endregion
    #region Clear
    public void FoodItemClear()
    {
        ddldietType.ClearSelection();
        ddlservingIn.ClearSelection();
        ddlUnit.ClearSelection();
        txtcalories.Text = string.Empty;
        txtcarbs.Text = string.Empty;
        txtfat.Text = string.Empty;
        txtprotein.Text = string.Empty;
        txtFoodItemName.Text = string.Empty;
        txtFoodItemName.Enabled = true;
        imgpreview.Src = "~/img/Defaultupload.png";
        DivForm.Visible = false;
        divGv.Visible = true;
        btnSubmit.Text = "Submit";
    }
    #endregion
    #region Fooditem Class
    public class Fooditem
    {
        public string queryType { get; set; }
        public string gymOwnerId { get; set; }
        public string foodItemId { get; set; }
        public string dietTypeId { get; set; }
        public string foodItemName { get; set; }
        public string unit { get; set; }
        public string servingIn { get; set; }
        public string protein { get; set; }
        public string carbs { get; set; }
        public string fat { get; set; }
        public string calories { get; set; }
        public string imageUrl { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
    }
    #endregion

}