using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Messaging;

public partial class Master_Newforms_TrainerDetails : System.Web.UI.Page
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
			BindSpecialistType();
			BindTrainerDetails();
		}
	}
	#region Add Click
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		BindSpecialistType();
		divGv.Visible = false;
		DivForm.Visible = true;
	}
	#endregion
	#region Bind Specialist Type
	public void BindSpecialistType()
	{
		try

		{
			ddlSpecialistType.Items.Clear();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

				string sUrl = Session["BaseUrl"].ToString().Trim() + "categoryMaster/GetDropDownDetails?gymOwnerId=" + Session["gymOwnerId"].ToString() + "&branchId="+Session["branchId"].ToString()+"";
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
							ddlSpecialistType.DataSource = dt;
							ddlSpecialistType.DataTextField = "categoryName";
							ddlSpecialistType.DataValueField = "categoryId";
							ddlSpecialistType.DataBind();
						}
						else
						{
							ddlSpecialistType.DataBind();
						}
					}
					else
					{
						ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
					}
						ddlSpecialistType.Items.Insert(0, new ListItem("Specialist Type *", "0"));
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
	#region Bind Trainer Details  
	public void BindTrainerDetails()
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

				//string sUrl = Session["BaseUrl"].ToString().Trim() + "trainerDetails?trainerId=101503";
				string sUrl = Session["BaseUrl"].ToString().Trim() + "trainerDetails?trainerId="+ Session["userId"].ToString();
				HttpResponseMessage response = client.GetAsync(sUrl).Result;

				if (response.IsSuccessStatusCode)
				{
					var FitnessList = response.Content.ReadAsStringAsync().Result;
					int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
					string ResponseMsg = JObject.Parse(FitnessList)["TrainerDetails"].ToString();

					if (StatusCode == 1)
					{
						DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
						if (dt.Rows.Count > 0)
						{
							gvTrainerDetails.DataSource = dt;
							gvTrainerDetails.DataBind();
							divGv.Visible = true;
							DivForm.Visible = false;
						}
						else
						{
							gvTrainerDetails.DataSource = null;
							gvTrainerDetails.DataBind();
							divGv.Visible = false;
							DivForm.Visible = true;
						}

					}
					else
					{
						gvTrainerDetails.DataSource = null;
						gvTrainerDetails.DataBind();
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
	#region Btnsubmit Click Event
	protected void btnSubmit_Click(object sender, EventArgs e)
	{
		if (btnSubmit.Text == "Submit")
		{
			InsertTrainerDetails();
		}
		else
		{
			UpdateTrainerDetails();
		}
	}
	#endregion
	#region Insert Trainer
	public void InsertTrainerDetails()
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
				var Insert = new TrainerDetails()
				{
					trainerId = Session["userId"].ToString(),
                    specialistTypeId = ddlSpecialistType.SelectedValue,
					experience = txtExperience.Text,
					qualification = txtQualification.Text,
					certificates = ImageUrl.Trim(),
					createdBy = Session["userId"].ToString()
				};
				HttpResponseMessage response = client.PostAsJsonAsync("trainerDetails/insert", Insert).Result;
				if (response.IsSuccessStatusCode)
				{
					var Fitness = response.Content.ReadAsStringAsync().Result;
					int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
					string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

					if (StatusCode == 1)
					{

						BindTrainerDetails();
						ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

					}
					else
					{
						ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
					}
					clear();
				}
			}
		}
		catch (Exception ex)
		{
			ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
		}
	}
	#endregion
	#region Update Trainer
	public void UpdateTrainerDetails()
	{

		int StatusCodes;
		string ImageUrl;
		if (Fuimage.HasFile)
		{
			helper.UploadImage(Fuimage, Session["BaseUrl"].ToString().Trim() + "UploadImage", out StatusCodes, out ImageUrl);
		}
		else
		{
			ImageUrl = imgEmpPhotoPrev.ImageUrl;
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
				var update = new TrainerDetails()
				{
					trainerId = ViewState["trainerId"].ToString(),
					specialistTypeId = ddlSpecialistType.SelectedValue,
					experience = txtExperience.Text,
					qualification = txtQualification.Text,
					certificates = ImageUrl.Trim(),
					updatedBy = Session["userId"].ToString()
				};
				HttpResponseMessage response = client.PostAsJsonAsync("trainerDetails/update", update).Result;
				if (response.IsSuccessStatusCode)
				{
					var Fitness = response.Content.ReadAsStringAsync().Result;
					int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
					string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

					if (StatusCode == 1)
					{

						BindTrainerDetails();
						ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

					}
					else
					{
						ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
					}
					clear();
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
			Label lbltrainerId = (Label)gvrow.FindControl("lbltrainerId");
			ViewState["trainerId"] = lbltrainerId.Text;
			Label lblgvspecialistTypeId = (Label)gvrow.FindControl("lblgvspecialistTypeId");
			BindSpecialistType();
			ddlSpecialistType.Text = lblgvspecialistTypeId.Text;
			Label lblexperience = (Label)gvrow.FindControl("lblexperience");
			txtExperience.Text = lblexperience.Text;
			Label lblqualification = (Label)gvrow.FindControl("lblqualification");
			txtQualification.Text = lblqualification.Text;
			Label lblcertificates = (Label)gvrow.FindControl("lblcertificates");
			imgEmpPhotoPrev.ImageUrl = lblcertificates.Text.Trim();

			if (imgEmpPhotoPrev.ImageUrl == "")
			{
				imgEmpPhotoPrev.ImageUrl = "~/img/User.png";
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
	#region Cancel Click
	protected void btnCancel_Click(object sender, EventArgs e)
	{
		clear();

	}
	#endregion
	public void clear()
	{
		divGv.Visible = true;
		DivForm.Visible = false;
		btnSubmit.Text = "Submit";
		ViewState["trainerId"] = "";
		ddlSpecialistType.ClearSelection();
		txtExperience.Text = "";
		txtQualification.Text = "";

	}
	
	#region Class 
	public class TrainerDetails
	{
		public string queryType { get; set; }
		public string trainerId { get; set; }
	
		public string specialistTypeId { get; set; }
		public string experience { get; set; }
		public string qualification { get; set; }
		public string certificates { get; set; }
		public string updatedBy { get; set; }
		public string createdBy { get; set; }


	}
	#endregion
}