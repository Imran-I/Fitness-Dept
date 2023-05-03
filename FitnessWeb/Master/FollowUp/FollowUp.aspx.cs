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

public partial class Master_FollowUp_FollowUp : System.Web.UI.Page
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
            Session["BaseUrl"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"].Trim();
            gvFollowUp.Visible = true;

            txtFromdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtTodate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }
    }
    #endregion
    #region Search Click 
    protected void btnSearch_Click(object sender, EventArgs e)
    {
		BindFollowUp();
        DivView.Visible = false;
        gvFollowUp.Visible = true;
    }
    #endregion
    #region Bind Follow Up
    public void BindFollowUp()
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
                string Endpoint = "GetFollowupTrackingBooking?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                                  "&branchId=" + Session["branchId"].ToString() + "" +
                                  "&fromDate=" + txtFromdate.Text + "&toDate=" + txtTodate.Text + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                string Response;
                var Locresponse = response.Content.ReadAsStringAsync().Result;
                int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                if (response.IsSuccessStatusCode)
                {
                    if (statusCode == 1)
                    {
                        string ResponseMsg = JObject.Parse(Locresponse)["BookingFollowupDetails"].ToString();
                        List<followUp> FollowupDetails = JsonConvert.DeserializeObject<List<followUp>>(ResponseMsg);
                        Session["followUp"] = FollowupDetails;
                        DataTable dt = ConvertToDataTable(FollowupDetails);
                        gvFollowUp.DataSource = FollowupDetails;
                        gvFollowUp.DataBind();
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
    DataTable ConvertToDataTable<TSource>(IEnumerable<TSource> source)
    {
        var props = typeof(TSource).GetProperties();

        var dt = new DataTable();
        dt.Columns.AddRange(
          props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray()
        );

        source.ToList().ForEach(
          i => dt.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray())
        );

        return dt;
    }
    #endregion
    #region Follow Up Class
    public class followUp
    {
        public string bookingId { get; set; }
        public string gymOwnerId { get; set; }
        public string branchId { get; set; }
        public string userId { get; set; }
        public string UserName { get; set; }
        public string totalAmount { get; set; }
        public string categoryName { get; set; }
        public string trainingTypeName { get; set; }
        public string PlaneDurationMonth { get; set; }
        public List<FollowupDetails> FollowupDetails { get; set; }
    }

    public class FollowupDetails
    {
        public string DateOfDay { get; set; }
        public string DaysName { get; set; }
        public string TotalCalories { get; set; }
        public string userId { get; set; }
        public string CompletedActivity { get; set; }
        public string ConsumedCalories { get; set; }
    }
    #endregion
    #region List Bind
    protected void gvFollowUp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            var followUp = e.Row.DataItem as followUp;
            var dlFollowupDetails = e.Row.FindControl("dlFollowupDetails") as DataList;
            dlFollowupDetails.DataSource = followUp.FollowupDetails;
            dlFollowupDetails.DataBind();
        }
    }
    #endregion
    #region View Bind
    protected void LnkView_Click(object sender, EventArgs e)
    {

        DivView.Visible = true;
        DivGvViewUser.Visible = true;
        gvFollowUp.Visible = false;
        divMain.Visible = false;
		DataTable dt = new DataTable();
        LinkButton lnkbtn = sender as LinkButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        Label lblgvuserIds = (Label)gvrow.FindControl("lblgvuserIds");
        Label lblUserName = (Label)gvrow.FindControl("lblUserName");
        lblUserames.Text = lblUserName.Text;
        List<followUp> follows = new List<followUp>();
        List<followUp> userfollows = new List<followUp>();
        follows = (List<followUp>)Session["followUp"];
        userfollows = follows.Where(x => x.userId == lblgvuserIds.Text).Select(x => x).ToList();
        var item = userfollows.ElementAt(0);
        gvUserView.DataSource = item.FollowupDetails;
        gvUserView.DataBind();
        hfListClose.Value = "O";
    }
    #endregion
    #region View Close 
    protected void btnClose_Click(object sender, EventArgs e)
    {  if(hfListClose.Value == "O")
        {
			DivView.Visible = false;
			DivGvViewUser.Visible = false;
			DivGvFoodList.Visible = false;
			DivGvWorkoutList.Visible = false;
			divMain.Visible = true;
			gvFollowUp.Visible = true;
        }
        else
        {
			DivView.Visible = true;
            DivGvViewUser.Visible = true;
			DivGvFoodList.Visible = false;
			DivGvWorkoutList.Visible = false;
            hfListClose.Value = "O";
		}

    }
	#endregion
	#region View Food and Workout Click
	protected void lblDate_Click(object sender, EventArgs e)
	{
		LinkButton lnkbtn = sender as LinkButton;
		GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
		LinkButton lblDate = (LinkButton)gvrow.FindControl("lblDate");
		Label lbluserId = (Label)gvrow.FindControl("lbluserId");
		Label lblDaysName = (Label)gvrow.FindControl("lblDaysName");
		Label lblDateOfDay = (Label)gvrow.FindControl("lblDateOfDay");
        string Days;

        
		Days = lblDaysName.Text.Remove(2);

		lblFDate.Text = lblDateOfDay.Text;
		DivGvViewUser.Visible = false;
		DivGvFoodList.Visible = true;
		DivGvWorkoutList.Visible = true;
		BindFoodList(lblDate.Text, lbluserId.Text);
		BindWorkoutList(lblDate.Text, Days, lbluserId.Text);
		hfListClose.Value = "C";
	}
	#endregion
	#region Get Food List 
	public void BindFoodList( string Date ,string userId)
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
                string Endpoint = "tranUserFoodTracking/userConsumingDietList?gymOwnerId=" + Session["gymOwnerId"].ToString() + "" +
                                  "&date=" + Date.ToString() + "" +
                                  "&userId=" + userId + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
				string ResponseMsg;
				var Locresponse = response.Content.ReadAsStringAsync().Result;
                int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
				if (response.IsSuccessStatusCode)
                {
                    if (statusCode == 1)
					{
						ResponseMsg = JObject.Parse(Locresponse)["UserFoodMenu"].ToString();
						DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        GvFoodList.DataSource = dt;
						GvFoodList.DataBind();
                    }
                    else
					{
						DivGvViewUser.Visible = true;
						DivGvFoodList.Visible = false;
						DivGvWorkoutList.Visible = false;
						ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('No Record Found');", true);
                    }
                }
                else
				{
					DivGvViewUser.Visible = true;
					DivGvFoodList.Visible = false;
					DivGvWorkoutList.Visible = false;
					ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
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
	#region Get Workout List 
	public void BindWorkoutList(string Date, string day, string userId)
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
				string Endpoint = "UserWorkOutTracking/GetTranUserWorkOutTrackingBasedonDateDay?day=" + day.ToString() + "" +
								  "&date=" + Date.ToString() + "" +
								  "&userId=" + userId + "";
				HttpResponseMessage response = client.GetAsync(Endpoint).Result;
				string Response;
				var Locresponse = response.Content.ReadAsStringAsync().Result;
				int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
				if (response.IsSuccessStatusCode)
				{
					if (statusCode == 1)
					{
						Response = JObject.Parse(Locresponse)["UserFoodMenu"].ToString();
						DataTable dt = JsonConvert.DeserializeObject<DataTable>(Response);
						GvFoodList.DataSource = dt;
						GvFoodList.DataBind();
					}
					else
					{
						Response = JObject.Parse(Locresponse)["Response"].ToString();
						ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "infoalert('"+ Response.ToString().Trim() + "');", true);
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

}