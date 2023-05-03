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

public partial class Master_WorkOutPlan_UserBookedList : System.Web.UI.Page
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
            DateTime today = DateTime.Now;
            txtFromDate.Text = today.ToString("yyyy-MM-dd");
            ddlGenerateType.SelectedValue = "N";
            BindUserBookedList(txtFromDate.Text, ddlGenerateType.SelectedValue);
        }

    }
    #region Bind GridView
    //public void BindUserBookedList()
    //{
    //    try
    //    {

    //        using (var client = new HttpClient())
    //        {
    //            client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
    //            client.DefaultRequestHeaders.Clear();
    //            client.DefaultRequestHeaders.Accept.Clear();
    //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    //           client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

    //            string sUrl = Session["BaseUrl"].ToString().Trim() + "UserDietPlanandWorkoutplan?gymOwnerId=" + Session["gymOwnerId"].ToString() + "&branchId=" + Session["branchId"].ToString() + "";
    //            HttpResponseMessage response = client.GetAsync(sUrl).Result;

    //            if (response.IsSuccessStatusCode)
    //            {
    //                var FinessList = response.Content.ReadAsStringAsync().Result;
    //                int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
    //                string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

    //                if (StatusCode == 1)
    //                {
    //                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
    //                    if (dt.Rows.Count > 0)
    //                    {
    //                        gvUserBookedList.DataSource = dt;
    //                        gvUserBookedList.DataBind();
    //                    }
    //                    else
    //                    {
    //                        gvUserBookedList.DataBind();
    //                    }
    //                }
    //                else
    //                {
    //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
    //                }
    //            }

    //            else
    //            {
    //                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + response.ReasonPhrase.ToString().Trim() + "');", true);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
    //    }
    //}

    public void BindUserBookedList(string Date,string Value)
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "GetUserBookingDetailsBasedOnType?"
                    +"gymOwnerId=" + Session["gymOwnerId"].ToString() + "&branchId=" + Session["branchId"].ToString() + ""
                    +"&date="+ Date.Trim()+"&type="+ Value.Trim()+"";
                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        if (dt.Rows.Count > 0)
                        {
                            gvUserBookedList.DataSource = dt;
                            gvUserBookedList.DataBind();
                        }
                        else
                        {
                            gvUserBookedList.DataBind();
                        }
                    }
                    else
                    {
                        gvUserBookedList.DataBind();
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

    protected void btngenerate_Click(object sender, EventArgs e)
    {
        Button lnkbtn = sender as Button;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        Label lbluserId = (Label)gvrow.FindControl("lbluserId");
        Label lblbookingId = (Label)gvrow.FindControl("lblbookingId");
        Label lblfromDate = (Label)gvrow.FindControl("lblfromDate");
        Label lbltoDate = (Label)gvrow.FindControl("lbltoDate");
        Label lblTDEE = (Label)gvrow.FindControl("lblTDEE");
        Label lblapprovedStatus = (Label)gvrow.FindControl("lblapprovedStatus");
        //DateTime FromDate = Convert.ToDateTime(lblfromDate.Text);
        //DateTime ToDate = Convert.ToDateTime(lbltoDate.Text);
        Session["UserIdWorkOutplan"] = lbluserId.Text.Trim();
        Session["fromDateWorkOutplan"] = lblfromDate.Text.Trim();
        Session["toDateWorkOutplan"] = lbltoDate.Text.Trim();
        //Session["fromDateWorkOutplan"] = FromDate.ToString("dd-MM-yyyy");
        //Session["toDateWorkOutplan"] = ToDate.ToString("dd-MM-yyyy");

        Session["BookingIDWorkOutplan"] = lblbookingId.Text.Trim();
        Session["TDEEWorkOutplan"] = lblTDEE.Text.Trim();
        Session["ApprovedStatus"] = lblapprovedStatus.Text.Trim();
        Response.Redirect("TranWorkoutPlan_New.aspx");

    }

    protected void btnSendSmsandMail_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkbtn = sender as ImageButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        Label lbluserId = (Label)gvrow.FindControl("lbluserId");
        Label lblPhoneNumber = (Label)gvrow.FindControl("lblPhoneNumber");
        Label lblMailId = (Label)gvrow.FindControl("lblMailId");
        SendSmsAndMail(lblPhoneNumber.Text, lblMailId.Text, lbluserId.Text);
    }
    public void SendSmsAndMail(string MobileNo,string MailId,string userId)
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
                var Send= new SendSms()
                { 
                    queryType= "SendSMSAndMailForPlan",
                    mobileNo= MobileNo,
                    mailId= MailId,
                    userId=userId
                };
              
                HttpResponseMessage response = client.PostAsJsonAsync("sendSmsandMail", Send).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindUserBookedList(txtFromDate.Text,ddlGenerateType.SelectedValue);
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
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    public class SendSms
    {
        public string queryType { get; set; }
        public string mobileNo { get; set; }
        public string mailId { get; set; }
        public string userId { get; set; }

    }


    protected void gvUserBookedList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblapprovedStatus = (Label)e.Row.FindControl("lblapprovedStatus");
            Label lblPlanGenearetedDiet = (Label)e.Row.FindControl("lblPlanGenearetedDiet");
            Label lblPlanGeneareted = (Label)e.Row.FindControl("lblPlanGeneareted");
            Label lblGeneareted = (Label)e.Row.FindControl("lblGeneareted");
            Label lblApproved = (Label)e.Row.FindControl("lblApproved");
            Button btngenerate = (Button)e.Row.FindControl("btngenerate");
            Button btnApprove = (Button)e.Row.FindControl("btnApprove");
          
            LinkButton LnkView = (LinkButton)e.Row.FindControl("LnkView");
            if (lblapprovedStatus.Text == "N")
            {
                if(lblPlanGenearetedDiet.Text == "Y" && lblPlanGeneareted.Text == "Y")
                {
                    btnApprove.Visible = true;
                    btngenerate.Visible = false;
                    lblGeneareted.Visible = true;
                }
                else
                {
                    btnApprove.Visible = false;
                    btngenerate.Visible = true;
                }
                lblApproved.Visible = false;

                LnkView.Visible = false;
               
                
            }
            if (lblapprovedStatus.Text == "A")
            {
                lblGeneareted.Visible = true;
                lblApproved.Visible = true;
                btngenerate.Visible = false;
                btnApprove.Visible = false;
                LnkView.Visible = true;
            }
        }
    }
    #region Update Approval Status
   
    public class UpdateApproval
    {
        public string bookingId { get; set; }
        public string userId { get; set; }
        public string updatedBy { get; set; }



    }
    #endregion
    protected void LnkView_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        Label lbluserId = (Label)gvrow.FindControl("lbluserId");
        Label lblbookingId = (Label)gvrow.FindControl("lblbookingId");
        Label lblfromDate = (Label)gvrow.FindControl("lblfromDate");
        Label lbltoDate = (Label)gvrow.FindControl("lbltoDate");
        Label lblTDEE = (Label)gvrow.FindControl("lblTDEE");
        Label lblapprovedStatus = (Label)gvrow.FindControl("lblapprovedStatus");
        //DateTime FromDate = Convert.ToDateTime(lblfromDate.Text);
        //DateTime ToDate = Convert.ToDateTime(lbltoDate.Text);
        //Session["fromDateWorkOutplan"] = FromDate.ToString("dd-MM-yyyy");
        //Session["toDateWorkOutplan"] = ToDate.ToString("dd-MM-yyyy");
        Session["UserIdWorkOutplan"] = lbluserId.Text.Trim();
        Session["fromDateWorkOutplan"] = lblfromDate.Text;
        Session["toDateWorkOutplan"] = lbltoDate.Text;
      
        Session["BookingIDWorkOutplan"] = lblbookingId.Text.Trim();
        Session["TDEEWorkOutplan"] = lblTDEE.Text.Trim();
        Session["ApprovedStatus"] = lblapprovedStatus.Text.Trim();
        Response.Redirect("TranWorkoutPlan_New.aspx");
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        Button lnkbtn = sender as Button;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        Label lbluserId = (Label)gvrow.FindControl("lbluserId");
        Label lblbookingId = (Label)gvrow.FindControl("lblbookingId");
       
        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                UpdateApproval Update = new UpdateApproval()
                {
                    bookingId = lblbookingId.Text,
                    userId = lbluserId.Text,
                    updatedBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("UserWorkOutPlan/UpdateApproveStatus", Update).Result;
                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        BindUserBookedList(txtFromDate.Text, ddlGenerateType.SelectedValue);
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
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }

    protected void ddlGenerateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlGenerateType.SelectedIndex!=0)
        {
            BindUserBookedList(txtFromDate.Text, ddlGenerateType.SelectedValue);
        }
        
    }
}