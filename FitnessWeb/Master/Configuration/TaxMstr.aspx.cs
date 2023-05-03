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
public partial class Master_Configuration_TaxMstr : System.Web.UI.Page
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
            BindServiceName();
            BindTaxMaster();
        }

    }

    #region Bind Service Name
    public void BindServiceName()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "configMaster/getDropDownDetails?typeId=12";
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
                            ddlServiceName.DataSource = dt;
                            ddlServiceName.DataTextField = "configName";
                            ddlServiceName.DataValueField = "configId";
                            ddlServiceName.DataBind();
                        }
                        else
                        {
                            ddlServiceName.DataBind();
                        }
                       
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlServiceName.Items.Insert(0, new ListItem("Service Name *", "0"));
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
        
        BindServiceName();
        divGv.Visible = false;
        DivForm.Visible = true;
    }
    #endregion

    #region Bind Tax  
    public void BindTaxMaster()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "taxMaster?queryType=GetInsertTax&gymOwnerId=" + Session["gymOwnerId"].ToString() + "&branchId=" + Session["branchId"].ToString() + "";
      
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
                            gvTaxMaster.DataSource = dt;
                            gvTaxMaster.DataBind();
                            divGridView.Visible = true;

                        }
                        else
                        {
                            gvTaxMaster.DataSource = null;
                            gvTaxMaster.DataBind();
                            divGv.Visible = false;
                            DivForm.Visible = true;
                        }

                    }
                    else
                    {
                        gvTaxMaster.DataSource = null;
                        gvTaxMaster.DataBind();
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

    public void BindTax()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "taxMaster?queryType=GetAddTax&gymOwnerId=" + Session["gymOwnerId"].ToString() + "&branchId=" + Session["branchId"].ToString() + "";
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
                            gvTax.DataSource = dt;
                            gvTax.DataBind();
                            btnSubmit.Enabled = true;
                            ddlServiceName.Enabled = false;
                        }
                        else
                        {
                            ddlServiceName.Enabled = true;
                            gvTax.DataSource = null;
                            gvTax.DataBind();
                           
                        }

                    }
                    else
                    {
                        gvTax.DataSource = null;
                        gvTax.DataBind();
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

    #region Tax On Data Bound
    protected void gvTaxMaster_DataBound(object sender, EventArgs e)
    {
        for (int currentRowIndex = 0; currentRowIndex < gvTaxMaster.Rows.Count; currentRowIndex++)
        {
            GridViewRow currentRow = gvTaxMaster.Rows[currentRowIndex];
            CombineColumnCells(currentRow, 0, "taxId");
        }
    }


    private void CombineColumnCells(GridViewRow currentRow, int colIndex, string fieldName)
    {
        TableCell currentCell = currentRow.Cells[colIndex];

        Object currentValue = gvTaxMaster.DataKeys[currentRow.RowIndex].Values[fieldName];

        for (int nextRowIndex = currentRow.RowIndex + 1; nextRowIndex < gvTaxMaster.Rows.Count; nextRowIndex++)
        {
            Object nextValue = gvTaxMaster.DataKeys[nextRowIndex].Values[fieldName];

            if (nextValue.ToString() == currentValue.ToString())
            {
                GridViewRow nextRow = gvTaxMaster.Rows[nextRowIndex];
                TableCell nextCell = nextRow.Cells[colIndex];
                currentCell.RowSpan = Math.Max(1, currentCell.RowSpan) + 1;
                nextCell.Visible = false;
            }
            else
            {
                break;
            }
        }
    }
    #endregion

    #region  btn Add For Insert Tax Temp
    protected void btnAddTax_Click(object sender, EventArgs e)
    {
      
        if (btnAddTax.Text == "Add")
        {
            InsertTax();
        }
        else
        {
            UpdateTax();
        }
    }
    #endregion
    #region Insert Tax Type
    public void InsertTaxMaster()
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
                var Insert = new Tax()
                {
                    serviceName = ddlServiceName.SelectedValue,
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("taxMaster/insert", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
  
                        BindTaxMaster();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    TaxMasterClear();
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

    public void InsertTax()
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
                var Insert = new Tax()
                {
                    serviceName = ddlServiceName.SelectedValue,
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    taxDescription = txtTaxDesc.Text.Trim(),
                    taxPercentage = txtTaxPercentage.Text.Trim(),
                    effectiveFrom = txtEffectiveFrom.Text,
                    createdBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("taxMaster/add", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindTax();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    TaxClear();
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
                BindTax();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion

    #region Update Tax 
    public void UpdateTax()
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
                var Update = new Tax()
                {
                    uniqueId= ViewState["UniqueId"].ToString(),
                    serviceName = ddlServiceName.SelectedValue,
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    taxDescription = txtTaxDesc.Text.Trim(),
                    taxPercentage = txtTaxPercentage.Text.Trim(),
                    effectiveFrom = txtEffectiveFrom.Text,
                    createdBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("taxMaster/update", Update).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindTax();
                       
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    TaxClear();
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
       
       InsertTaxMaster();
        
    }
    #endregion

    #region Btn Edit Click Event

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;

            Label lblUniqueId = (Label)gvrow.FindControl("lblUniqueId");
            Label lblserviceId = (Label)gvrow.FindControl("lblserviceId");
            BindServiceName();
            ddlServiceName.SelectedValue = lblserviceId.Text.Trim();

            Label lbltaxDescription = (Label)gvrow.FindControl("lbltaxDescription");
            txtTaxDesc.Text = lbltaxDescription.Text.Trim();

            Label lbltaxPercentage = (Label)gvrow.FindControl("lbltaxPercentage");
            txtTaxPercentage.Text = lbltaxPercentage.Text.Trim();

            Label lbleffectiveFrom = (Label)gvrow.FindControl("lbleffectiveFrom");
            //DateTime fromDate = Convert.ToDateTime(lbleffectiveFrom.Text.Trim());
            //txtEffectiveFrom.Text = fromDate.ToString("yyyy-MM-dd").Trim();
            txtEffectiveFrom.Text = lbleffectiveFrom.Text;
            ViewState["UniqueId"] = lblUniqueId.Text.Trim();
            divGv.Visible = false;
            DivForm.Visible = true;
            btnAddTax.Text = "Update";
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }

    }
    #endregion

    #region Delete Click Event
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        try
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            Label lblUniqueId = (Label)gvrow.FindControl("lblUniqueId");

           
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var delete = new Tax()
                {
                    uniqueId= lblUniqueId.Text.Trim()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("taxMaster/delete", delete).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindTax();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    TaxClear();
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
        TaxMasterClear();
    }
    #endregion

    #region Clear
    public void TaxMasterClear()
    {
        ddlServiceName.Enabled = true;
        gvTax.DataSource = null;
        gvTax.DataBind();
        btnSubmit.Enabled = false;
        ddlServiceName.ClearSelection();
        txtTaxDesc.Text = string.Empty;
        txtTaxPercentage.Text = string.Empty;
       txtEffectiveFrom.Text = string.Empty;
        DivForm.Visible = false;
        divGv.Visible = true;
        btnAddTax.Text = "Add";
    }
    public void TaxClear()
    {
        txtTaxDesc.Text = string.Empty;
        txtTaxPercentage.Text = string.Empty;
        txtEffectiveFrom.Text = string.Empty;
        btnAddTax.Text = "Add";
    }
    #endregion

    #region Tax Class
    public class Tax
    {
        public string serviceId { get; set; }
        public string uniqueId { get; set; }
        public string taxId { get; set; }
        public string branchId { get; set; }
        public string gymOwnerId { get; set; }
        public string serviceName { get; set; }
        public string taxDescription { get; set; }
        public string taxPercentage { get; set; }
        public string effectiveFrom { get; set; }
        public string effectiveTill { get; set; }
        public string activeStatus { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }

    }
    #endregion

    


}