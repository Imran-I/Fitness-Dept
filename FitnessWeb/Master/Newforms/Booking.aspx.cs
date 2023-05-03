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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Master_Booking_Booking : System.Web.UI.Page
{
    IFormatProvider obj = new System.Globalization.CultureInfo("en-GB", true);
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["BaseUrl"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"].Trim();
            GetCategory();
            GetPaymentType();
            GetSwapType();

		}

    }
    #endregion
    #region Get Category 
    public void GetCategory()
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
                string Endpoint = "categoryMaster/GetDropDownDetails?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
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
                            ddlCategoryList.DataSource = dt;
                            ddlCategoryList.DataTextField = "categoryName";
                            ddlCategoryList.DataValueField = "categoryId";
                            ddlCategoryList.DataBind();
                        }
                        else
                        {
                            ddlCategoryList.DataBind();

                        }                        
                    }
                    else
                    {
                        Clear();
                        ddlCategoryList.Items.Clear();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    ddlCategoryList.Items.Insert(0, new ListItem("Category List *", "0"));
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


    protected void ddlCategoryList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCategoryBenefits();
        if(ddlCategoryList.SelectedItem.Text == "General")
        {
            ddltrainingMode.SelectedValue = "D";
            ddltrainingMode.Enabled = false;
			GetTrainingType();

		}
        else
        {
			ddltrainingMode.SelectedValue = "0";
			ddltrainingMode.Enabled = true;
		}
        
	}

    public void GetCategoryBenefits()
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
                string Endpoint = "categoryBenefitMaster/GetCategoryBenefit?categoryId=" + ddlCategoryList.SelectedValue + "";
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
                            lblCategoryBenefit.InnerText = dt.Rows[0]["description"].ToString();
                            lblCategoryBenefit1.InnerText = dt.Rows[1]["description"].ToString();

                        }
                        else
                        {
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
	#region Get Slot 
	public void GetSlot()
	{
		try
		{
			string ddlv = ddltraningType.SelectedValue;

			string[] ddlTtv = ddlv.Split('~');
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
				string Endpoint = "GetSlotList?categoryId=" + ddlCategoryList.SelectedValue + "" +
				   "&trainingTypeId=" + ddlTtv[1] + "";
				HttpResponseMessage response = client.GetAsync(Endpoint).Result;
				if (response.IsSuccessStatusCode)
				{
					var Locresponse = response.Content.ReadAsStringAsync().Result;
					int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
					string ResponseMsg = JObject.Parse(Locresponse)["SlotList"].ToString();
					if (StatusCode == 1)
					{
						DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
						if (dt.Rows.Count > 0)
						{
							dtlSlot.DataSource = dt;
							dtlSlot.DataBind();
						}
						else
						{
							dtlSlot.DataBind();

						}
					}
					else
					{
						Clear();
						ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

					}
					//ddlSlot.Items.Insert(0, new ListItem("Slot List *", "0"));
				}
				else
				{
					Clear();
					var Locresponse = response.Content.ReadAsStringAsync().Result;
					int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
					string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
					ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
		
				}
			}
		}
		catch (Exception ex)
		{
			ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
		}
	}

	public void GetSlotGeneral()
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
				string Endpoint = "slotMaster/GetSlotstoActivate?gymOwnerId=" + Session["gymOwnerId"].ToString() +"" +
                    "&branchId="+ Session["branchId"].ToString() + "";
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
							dtlSlot.DataSource = dt;
							dtlSlot.DataBind();
						}
						else
						{
							dtlSlot.DataBind();

						}
					}
					else
					{
						Clear();
						ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

					}
					//ddlSlot.Items.Insert(0, new ListItem("Slot List *", "0"));
				}
				else
				{
					Clear();
					var Locresponse = response.Content.ReadAsStringAsync().Result;
					int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
					string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
					ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

				}
			}
		}
		catch (Exception ex)
		{
			ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
		}
	}



	#endregion
	#region Get SwapType 
	public void GetSwapType()
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
				string Endpoint = "configMaster/getDropDownDetails?typeId=30";
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
							ddlSwapType.DataSource = dt;
							ddlSwapType.DataTextField = "configName";
							ddlSwapType.DataValueField = "configId";
							ddlSwapType.DataBind();
						}
						else
						{
							ddlSwapType.DataBind();

						}
					}
					else
					{
						Clear();
						ddlSwapType.Items.Clear();
						ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

					}
					ddlSwapType.Items.Insert(0, new ListItem("Slot swapType *", "0"));
				}
				else
				{
					Clear();
					ddlSwapType.Items.Clear();
					var Locresponse = response.Content.ReadAsStringAsync().Result;
					int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
					string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
					ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

				}
			}
		}
		catch (Exception ex)
		{
			ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
		}
	}
	#endregion
	#region Slot Click 
	protected void lblFromTime_Click(object sender, EventArgs e)
	{
		LinkButton lblFromTime_Click = sender as LinkButton;
		DataListItem gvrow = lblFromTime_Click.NamingContainer as DataListItem;
		Label lblslotId = gvrow.FindControl("lblslotId") as Label;
		ViewState["SlotId"] = lblslotId.Text;
		for (int i = 0; i < dtlSlot.Items.Count; i++)
		{
			LinkButton lnk = dtlSlot.Items[i].FindControl("lblFromTime") as LinkButton;
			Label slot = dtlSlot.Items[i].FindControl("lblslotId") as Label;

			if (lblslotId.Text == slot.Text)
			{
				lnk.CssClass = "ddlSlotBtnSelect";

			}
			else
			{
				lnk.CssClass = "ddlSlotBtn";
			}
		}

        if (ddlCategoryList.SelectedItem.Text == "General")
        {
            divtrainer.Visible = false;
		}
        else
        {
			GetTrainer();
			divtrainer.Visible = true;
		}
			
	}

	#endregion
	#region Get Trainer 
	public void GetTrainer()
	{
		try
		{
			string ddlv = ddltraningType.SelectedValue;

			string[] ddlTtv = ddlv.Split('~');
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
				string Endpoint = "GetTrainerList?categoryId=" + ddlCategoryList.SelectedValue + "" +
				   "&trainingTypeId=" + ddlTtv[1] + "&slotId=" + ViewState["SlotId"].ToString() + " ";
				HttpResponseMessage response = client.GetAsync(Endpoint).Result;
				if (response.IsSuccessStatusCode)
				{
					var Locresponse = response.Content.ReadAsStringAsync().Result;
					int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
					string ResponseMsg = JObject.Parse(Locresponse)["TrainerList"].ToString();
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
						Clear();
						ddlTrainer.Items.Clear();
						ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

					}
					ddlTrainer.Items.Insert(0, new ListItem("Trainer List *", "0"));
				}
				else
				{
					Clear();
					ddlTrainer.Items.Clear();
					var Locresponse = response.Content.ReadAsStringAsync().Result;
					int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
					string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
					ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
				}
			}
		}
		catch (Exception ex)
		{
			ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
		}
	}
	#endregion
	#region Get PaymentType
	public void GetPaymentType()
    {
        try
        {
            ddlPaymentType.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "configMaster/getDropDownDetails?typeId=21";
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
                            ddlPaymentType.DataSource = dt;
                            ddlPaymentType.DataTextField = "configName";
                            ddlPaymentType.DataValueField = "configId";
                            ddlPaymentType.DataBind();
                        }
                        else
                        {
                            ddlPaymentType.DataBind();

                        }
                    }
                    else
                    {
                        ddlPaymentType.Items.Clear();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    ddlPaymentType.Items.Insert(0, new ListItem("Payment Type *", "0"));
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
    #region Get Price Details 
    #region Get Training Details
    protected void ddltrainingMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetTrainingType();
		divSlot.Visible = false;
	}
    public void GetTrainingType()
    {
        try
        {
            ddltraningType.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "fitnessCategoryPrice/GetPriceDetails?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "&categoryId=" + ddlCategoryList.SelectedValue + "" +
                   "&trainingMode=" + ddltrainingMode.SelectedValue + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                string Response;
                var Locresponse = response.Content.ReadAsStringAsync().Result;
                int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                if (response.IsSuccessStatusCode)
                {
                    
                    if (StatusCode == 1)
                    {
                        string ResponseMsg = JObject.Parse(Locresponse)["GetPriceDetails"].ToString();
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            ddltraningType.DataSource = dt;
                            dt.Columns.Add(new DataColumn("priceIds", System.Type.GetType("System.String"), "priceId + '~' + trainingTypeId"));
                            ddltraningType.DataTextField = "training";
                            ddltraningType.DataValueField = "priceIds";
                            ddltraningType.DataBind();

                           
                            //string Month = ddltraningType.SelectedItem.Text;
                            //string[] Months = Month.Split('~');
                            //SummaryHead.Text = Months[0];
                        }
                        else
                        {
                            Response = JObject.Parse(Locresponse)["Response"].ToString();
                            ddltraningType.DataBind();
                        }
                       
                    }
                    else
                    {
                        Response = JObject.Parse(Locresponse)["Response"].ToString();
                        ddltraningType.Items.Clear();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Response.ToString().Trim() + "');", true);

                    }
                    ddltraningType.Items.Insert(0, new ListItem("Training Type *", "0"));
                }
                else
                {
                    Response = JObject.Parse(Locresponse)["Response"].ToString();
                    ddltraningType.Items.Clear();
                    Clear();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Response.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }

    #endregion
    protected void ddltraningType_SelectedIndexChanged(object sender, EventArgs e)
    {
        divSlot.Visible = true;
        string Month = ddltraningType.SelectedItem.Text;
        string[] Months = Month.Split('~');
        SummaryHead.Text = Months[0];
        GetPriceDetails();
        if (ddlCategoryList.SelectedItem.Text == "General")
        {
            GetSlotGeneral();

		}
        else
        {
			GetSlot();
		}
    }
    public void GetPriceDetails()
    {
        try
        {

			string ddlv = ddltraningType.SelectedValue;

            string[] ddlTtv = ddlv.Split('~');


			using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "fitnessCategoryPrice/GetPriceDetails?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                   "&branchId=" + Session["branchId"].ToString() + "&categoryId=" + ddlCategoryList.SelectedValue + "" +
                   "&trainingMode=" + ddltrainingMode.SelectedValue + "&priceId=" + ddlTtv[0] + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                string Response;
                var Locresponse = response.Content.ReadAsStringAsync().Result;
                int StatusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                if (response.IsSuccessStatusCode)
                {
                    
                    
                    if (StatusCode == 1)
                    {
                        string ResponseMsg = JObject.Parse(Locresponse)["GetPriceDetails"].ToString();
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            decimal TotalTax = Convert.ToDecimal(dt.Rows[0]["cgstTax"]) + Convert.ToDecimal(dt.Rows[0]["sgstTax"]);
                            lblPriceAmt.Text = dt.Rows[0]["price"].ToString();
                            lblTaxamt.Text = TotalTax.ToString();
                            lblTotalAmt.Text = dt.Rows[0]["netAmount"].ToString();
                            lblFinalamt.Text = dt.Rows[0]["netAmount"].ToString();
                            hfbranchName.Value = dt.Rows[0]["branchName"].ToString();
                            hfplanDurationId.Value = dt.Rows[0]["planDurationId"].ToString();
                            hftaxId.Value = dt.Rows[0]["taxId"].ToString();
                            hftaxName.Value = dt.Rows[0]["taxName"].ToString();
                            hfpriceId.Value = dt.Rows[0]["priceId"].ToString();
                            hftrainingTypeId.Value = dt.Rows[0]["trainingTypeId"].ToString();
                        }
                        else
                        {
                            Response = JObject.Parse(Locresponse)["Response"].ToString();

                        }

                    }
                    else
                    {
                        Response = JObject.Parse(Locresponse)["Response"].ToString();

                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Response.ToString().Trim() + "');", true);

                    }
                }
                else
                {
                    Response = JObject.Parse(Locresponse)["Response"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('" + Response.ToString().Trim() + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "erroralert('" + ex + "');", true);
        }
    }
    #endregion
    #region Calculate BMI And BMR and TDEE
    #region Calculate BMI 
    public void CalBMI()
    {
        try
        {
            decimal height = Convert.ToDecimal((txtheight.Text)) / 100;
            decimal BMR = Convert.ToDecimal((txtweight.Text)) / (height * height);
            txtBMI.Text = BMR.ToString("0.00");
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region Calculate BMR and TDEE
    public void CalBMRandTDEE()
    {
        try
        {
            if (ddlGender.SelectedValue != "0")
            {
                int cal = 0;
                if (ddlGender.SelectedValue == "F")
                {
                    cal = Convert.ToInt32(655 + (Convert.ToDecimal(9.6) * Convert.ToDecimal(txtweight.Text))
                       + (Convert.ToDecimal(1.8) * Convert.ToDecimal(txtheight.Text))
                       - (Convert.ToDecimal(4.7) * Convert.ToDecimal(txtage.Text)));
                }
                else
                {
                    cal = Convert.ToInt32(66 + (Convert.ToDecimal(13.8) * Convert.ToDecimal(txtweight.Text))
                       + (Convert.ToDecimal(5.0) * Convert.ToDecimal(txtheight.Text))
                       - (Convert.ToDecimal(6.8) * Convert.ToDecimal(txtage.Text)));
                }
                if (ddlWorkOutDetails.SelectedValue != "0")
                {
                    decimal TDEE = Convert.ToInt32(Convert.ToDecimal(cal) * Convert.ToDecimal(ddlWorkOutDetails.SelectedValue));
                    txtBMR.Text = cal.ToString();
                    txtTDEE.Text = TDEE.ToString();
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select WorkOut Details');", true);
                    return;
                }

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Gender');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region Work Out Details Selected Index Changed
    protected void ddlWorkOutDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtheight.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Enter Height');", true);
            return;
        }
        if (ddlGender.SelectedValue == "0")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Gender');", true);
            return;
        }
        if (txtweight.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Enter Weight');", true);
            return;
        }
        if (txtage.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Enter Age');", true);
            return;
        }
        else
        {
            CalBMI();
            CalBMRandTDEE();
        }
    }
    #endregion
    #region Weight Changed Event
    protected void txtweight_TextChanged(object sender, EventArgs e)
    {
        if (txtage.Text != "" && txtheight.Text != "" && ddlGender.SelectedValue != "0" && txtweight.Text != "" && ddlWorkOutDetails.SelectedValue != "0")
        {
            CalBMI();
            CalBMRandTDEE();
        }

    }
    #endregion
    #region Height Changed Event
    protected void txtheight_TextChanged(object sender, EventArgs e)
    {
        if (txtage.Text != "" && txtheight.Text != "" && ddlGender.SelectedValue != "0" && txtweight.Text != "" && ddlWorkOutDetails.SelectedValue != "0")
        {
            CalBMI();
            CalBMRandTDEE();
        }

    }

    #endregion
    #region Age Changed Event
    protected void txtage_TextChanged(object sender, EventArgs e)
    {
        if (txtage.Text != "" && txtheight.Text != "" && ddlGender.SelectedValue != "0" && txtweight.Text != ""
            && ddlWorkOutDetails.SelectedValue != "0")
        {
            CalBMI();
            CalBMRandTDEE();
        }

    }
    #endregion
    #region Gender Changed Event
    protected void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtage.Text != "" && txtheight.Text != "" && ddlGender.SelectedValue != "0" && txtweight.Text != ""
          && ddlWorkOutDetails.SelectedValue != "0")
        {
            CalBMI();
            CalBMRandTDEE();
        }
    }
    #endregion
    #endregion
    #region Clear 
    public void Clear()
    {
        ddlCategoryList.ClearSelection();
        txtWakeUpTime.Text = "";
		ddlPaymentType.ClearSelection();
        ddltrainingMode.ClearSelection();
        ddltraningType.ClearSelection();
        ddlSwapType.ClearSelection();
		divSlot.Visible = false;
		divtrainer.Visible = true;
		txtage.Text = "";
        txtmobileNo.Text = "";
        txtBMI.Text = "";
        txtBMR.Text = "";
        txtDOB.Text = "";
        txtfat.Text = "";
        txtheight.Text = "";
        txtJoinDate.Text = "";
        txtname.Text = "";
        txtTDEE.Text = "";
        txtweight.Text = "";
        lblCategoryBenefit.InnerText = "";
        lblCategoryBenefit1.InnerText = "";
        lblFinalamt.Text = "";
        lblPriceAmt.Text = "";
        lblTaxamt.Text = "";
        lblTotalAmt.Text = "";
        hftaxId.Value = "";
        hftaxName.Value = "";
        hfpriceId.Value = "";
        hfbranchName.Value = "";
        hfplanDurationId.Value = "";
    }
    #endregion
    #region Sumbit Click
    protected void btnSumbit_Click(object sender, EventArgs e)
    {
        InsertUserEnroll();
    }
    #endregion
    #region Insert Function 
    public void InsertUserEnroll()
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
                EnrollClass Insert = new EnrollClass()
                {

                    firstName = txtname.Text,
                    dob = txtDOB.Text,
                    gender = ddlGender.SelectedValue,
                    WorkOutValue = ddlWorkOutDetails.SelectedValue,
                    WorkOutStatus = ddlWorkOutDetails.SelectedItem.Text,
                    age = txtage.Text,
                    date = txtJoinDate.Text,
                    weight = txtweight.Text,
                    height = txtheight.Text,
                    mobileNo = txtmobileNo.Text,
                    fatPercentage = txtfat.Text,
                    BMR = txtBMR.Text,
                    BMI = txtBMI.Text,
                    TDEE = Convert.ToInt32(txtTDEE.Text),
                    createdBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("userInBodyTest/insertDepartmemt", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    string[] uid;
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();
                    uid = ResponseMsg.Split('~');
                    string userId = uid[0].ToString();
                    if (StatusCode == 1)
                    {
                        InsertBooking(userId);
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + uid[1].ToString() + "');", true);

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


    public void InsertBooking(string userId)
    {
        try
        {
            string trainerId = string.Empty;

			if (ddlTrainer.SelectedValue == "")
            {
                trainerId = "0";

			}
            else
            {
				trainerId = ddlTrainer.SelectedValue;

			}
           

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new BookingClass()
                {

                    queryType = "insert",
                    gymOwnerId = Session["gymOwnerId"].ToString(),
                    branchId = Session["branchId"].ToString(),
                    branchName = hfbranchName.Value,
                    categoryId = ddlCategoryList.SelectedValue,
                    trainingTypeId = hftrainingTypeId.Value,
                    planDurationId = hfplanDurationId.Value,
                    traningMode = ddltrainingMode.SelectedValue,
                    phoneNumber = txtmobileNo.Text,
                    userId = userId,
                    booking = "W",
                    loginType = "U",
                    trainerId = trainerId,
					slotId = ViewState["SlotId"].ToString(),
                    wakeUpTime = txtWakeUpTime.Text,
					slotSwapType = ddlSwapType.SelectedValue,
					priceId = hfpriceId.Value,
                    price = lblPriceAmt.Text,
                    taxId = hftaxId.Value,
                    taxName = hftaxName.Value,
                    taxAmount = lblTaxamt.Text,
                    totalAmount = lblTotalAmt.Text,
                    paymentCyclesStatus = "0",
                    paymentCycles = "0",
                    paidAmount = lblFinalamt.Text,
                    paymentStatus = "P",
                    paymentType = ddlPaymentType.SelectedValue,
                    createdBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("booking", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        Clear();
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
    #region Enroll Class 

    public class EnrollClass
    {
        public string queryType { get; set; }
        public string firstName { get; set; }
        public string dob { get; set; }
        public string weight { get; set; }
        public string height { get; set; }
        public string mobileNo { get; set; }
        public string fatPercentage { get; set; }
        public string age { get; set; }
        public string BMR { get; set; }
        public string BMI { get; set; }
        public int TDEE { get; set; }
        public string date { get; set; }
        public string gender { get; set; }
        public string WorkOutValue { get; set; }
        public string WorkOutStatus { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
    }
    #endregion
    #region Booking Class 

    public class BookingClass
    {
        public string queryType { get; set; }
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string branchName { get; set; }
        public string categoryId { get; set; }
        public string booking { get; set; }
        public string trainingTypeId { get; set; }
        public string planDurationId { get; set; }
        public string traningMode { get; set; }
        public string phoneNumber { get; set; }
        public string userId { get; set; }
        public string loginType { get; set; }
        public string trainerId { get; set; }
        public string slotId { get; set; }
        public string wakeUpTime { get; set; }
        public string slotSwapType { get; set; }
        public string priceId { get; set; }
        public string price { get; set; }
        public string taxId { get; set; }
        public string taxName { get; set; }
        public string taxAmount { get; set; }
        public string totalAmount { get; set; }
        public string paidAmount { get; set; }
        public string paymentStatus { get; set; }
        public string paymentCycles { get; set; }
        public string paymentType { get; set; }
        public string paymentCyclesStatus { get; set; }
        public string createdBy { get; set; }

    }
    #endregion
    #region Age Calculation
    protected void txtDOB_TextChanged(object sender, EventArgs e)
    {
        DateTime date = DateTime.Now;
        DateTime Dob = DateTime.Parse(txtDOB.Text, obj);
        var diff = date - Dob;
        var diffs = diff.Days / 365;
        txtage.Text = diffs.ToString();
        if (txtage.Text == "0")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Enter Age');", true);
            txtage.Text = string.Empty;
        }
    }
	#endregion
}