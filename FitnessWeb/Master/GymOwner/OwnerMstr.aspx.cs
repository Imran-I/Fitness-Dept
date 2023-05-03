using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_OwnerMstr : System.Web.UI.Page
{
    readonly Helper helper = new Helper();
    readonly string BaseUri;
    readonly string BindGridViewUri;
    readonly string UploadImageUri;
    readonly string InsertOwnerUri;
    readonly string UpdateOwnerUri;
    readonly string ActiveOrInactiveOwnerUri;
    string Token;
    public Master_OwnerMstr()
    {
        BaseUri = $"{ConfigurationManager.AppSettings["BaseUrl"].Trim()}";
        BindGridViewUri = $"{BaseUri}ownerMaster";
        UploadImageUri = $"{BaseUri}UploadImage";
        InsertOwnerUri = $"{BaseUri}ownerMaster/insert";
        UpdateOwnerUri = $"{BaseUri}ownerMaster/update";
        ActiveOrInactiveOwnerUri = $"{BaseUri}ownerMaster/activeOrInActive";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["userId"] == null && Session["userRole"] == null)
            {
                Session.Clear();
                Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["LogoutUrl"].Trim(), true);
            }
            Token = Session["APIToken"].ToString();
            if (!IsPostBack)
            {
                if (Session["userRole"].ToString().Trim() == "Sadmin" & Session["branchId"].ToString().Trim() == "0")
                {
                    BingGridView();
                }
                else if (Session["userRole"].ToString().Trim() == "Sadmin" || Session["userRole"].ToString().Trim() == "GymOwner")
                {
                    BingIndividualGridView();
                }

            }
        }
        catch (Exception Ex)
        {
            ShowErrorPopup(Ex);
        }
    }
    #region Bind Grid
    public void BingGridView()
    {
        try
        {

            helper.APIGet(BindGridViewUri, Token, out DataTable dt, out int StatusCode, out string Response);
            if (StatusCode == 1)
            {
                gvGymOwner.DataSource = dt;
                gvGymOwner.DataBind();

                if (dt.Rows.Count == 0)
                {
                    ShowGridOrFields("F");
                }
                else
                {
                    ShowGridOrFields("G");
                }
            }
            else
            {
                ShowInfoPopup(Response);
                divGv.Visible = false;
                DivForm.Visible = true;
            }
        }
        catch (Exception Ex)
        {
            ShowErrorPopup(Ex);
        }
    }

    public void BingIndividualGridView()
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
                string sUrl = string.Empty;
                sUrl = Session["BaseUrl"].ToString().Trim() + "ownerMaster/IndividualOwner?gymOwnerId=" + Session["gymOwnerId"].ToString().Trim() + "";


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
                            gvGymOwner.DataSource = dt;
                            gvGymOwner.DataBind();
                            divGv.Visible = true;
                            DivForm.Visible = false;

                        }
                        else
                        {
                            gvGymOwner.DataSource = dt;
                            gvGymOwner.DataBind();
                            divGv.Visible = false;
                            DivForm.Visible = true;
                        }

                    }
                    else
                    {
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

    #region Insert and Update Owner
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int StatusCode = 0;
            string Response = "";
            int ImageStatusCode = 0;
            string ImageResponse = "";

            if (btnSubmit.Text.Trim() == "Submit")
            {
                if (fuimage.HasFile)
                {
                    helper.UploadImage(fuimage, UploadImageUri, out ImageStatusCode, out ImageResponse);

                    if (ImageStatusCode == 0)
                    {
                        ShowInfoPopup(ImageResponse);
                        return;
                    }

                }
                else
                {
                    ImageResponse = null;
                }

                InsertOwner InsertOwner = new InsertOwner()
                {
                    gymName = txtGymName.Text.Trim(),
                    shortName = txtShortName.Text.Trim(),
                    gymOwnerName = txtUserName.Text.Trim(),
                    mobileNumber = txtMobileNo.Text.Trim(),
                    mailId = txtEmailId.Text.Trim(),
                    passWord = txtPassWord.Text.Trim(),
                    logoUrl = ImageResponse,
                    websiteUrl = txtWebSiteUrl.Text.Trim(),
                    createdBy = Convert.ToInt32(Session["userId"])
                };
                helper.APIpost<InsertOwner>(InsertOwnerUri, Token, InsertOwner, out StatusCode, out Response);
            }
            else
            {
                if (fuimage.HasFile)
                {
                    helper.UploadImage(fuimage, UploadImageUri, out ImageStatusCode, out ImageResponse);

                    if (ImageStatusCode == 0)
                    {
                        ShowInfoPopup(ImageResponse);
                        return;
                    }
                }
                else
                {
                    ImageResponse = imgpreview.Src;
                }

                UpdateOwner UpdateOwner = new UpdateOwner()
                {
                    gymOwnerId = Convert.ToInt32(ViewState["gymOwnerId"]),
                    gymName = txtGymName.Text.Trim(),
                    shortName = txtShortName.Text.Trim(),
                    gymOwnerName = txtUserName.Text.Trim(),
                    mobileNumber = txtMobileNo.Text.Trim(),
                    mailId = txtEmailId.Text.Trim(),
                    passWord = txtPassWord.Text.Trim(),
                    logoUrl = ImageResponse.Trim(),
                    websiteUrl = txtWebSiteUrl.Text.Trim(),
                    updatedBy = Convert.ToInt32(Session["userId"])
                };
                helper.APIpost<UpdateOwner>(UpdateOwnerUri, Token, UpdateOwner, out StatusCode, out Response);
            }

            if (StatusCode == 1)
            {
                BingGridView();
                Clear();
                ShowSuccessPopup(Response);
                btnSubmit.Text = "Submit";
            }
            else
            {
                ShowInfoPopup(Response);
            }
        }
        catch (Exception Ex)
        {
            ShowErrorPopup(Ex);
        }
    }
    #endregion


    #region Edit
    protected void LnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton ImgBtn = sender as ImageButton;
            GridViewRow GvRow = (GridViewRow)ImgBtn.NamingContainer;

            Label lblGvOwnername = (Label)GvRow.FindControl("gvLblgymOwnerName");
            Label lblGvgymName = (Label)GvRow.FindControl("gvLblgymName");
            Label lblGvshortName = (Label)GvRow.FindControl("gvLblshortName");

            string websiteUrl, logoUrl, passWord, mailId, mobileNumber;
            ViewState["gymOwnerId"] = gvGymOwner.DataKeys[GvRow.RowIndex]["gymOwnerId"].ToString().Trim();
            websiteUrl = gvGymOwner.DataKeys[GvRow.RowIndex]["websiteUrl"].ToString().Trim();
            logoUrl = gvGymOwner.DataKeys[GvRow.RowIndex]["logoUrl"].ToString().Trim();
            passWord = gvGymOwner.DataKeys[GvRow.RowIndex]["passWord"].ToString().Trim();
            mailId = gvGymOwner.DataKeys[GvRow.RowIndex]["mailId"].ToString().Trim();
            mobileNumber = gvGymOwner.DataKeys[GvRow.RowIndex]["mobileNumber"].ToString().Trim();


            txtGymName.Text = lblGvgymName.Text.Trim();
            txtUserName.Text = lblGvOwnername.Text.Trim();
            txtShortName.Text = lblGvshortName.Text.Trim();
            txtPassWord.Text = passWord;
            txtMobileNo.Text = mobileNumber;
            txtMobileNo.Attributes.Add("readOnly", "readOnly");
            txtEmailId.Text = mailId;
            txtWebSiteUrl.Text = websiteUrl;
            if (!string.IsNullOrEmpty(logoUrl))
            {
                imgpreview.Src = logoUrl;
            }

            ShowGridOrFields("F");

            btnSubmit.Text = "Update";
        }
        catch (Exception Ex)
        {
            ShowErrorPopup(Ex);
        }
    }
    #endregion

    #region Active Or Inactive
    protected void lnkActiveOrInactive_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkBtn = sender as LinkButton;
            GridViewRow GvRow = (GridViewRow)lnkBtn.NamingContainer;

            int gymOwnerId;
            string ActiveOrInactive;
            gymOwnerId = Convert.ToInt32(gvGymOwner.DataKeys[GvRow.RowIndex]["gymOwnerId"].ToString().Trim());
            ActiveOrInactive = gvGymOwner.DataKeys[GvRow.RowIndex]["activeStatus"].ToString().Trim();

            int StatusCode;
            string Response;
            ActiveOrInactive ActiveOrInactiveOwner;

            if (ActiveOrInactive == "A")
            {
                ActiveOrInactiveOwner = new ActiveOrInactive()
                {
                    queryType = "inActive",
                    gymOwnerId = gymOwnerId,
                    updatedBy = Convert.ToInt32(Session["userId"])
                };

                helper.APIpost<ActiveOrInactive>(ActiveOrInactiveOwnerUri, Session["APIToken"].ToString(), ActiveOrInactiveOwner, out StatusCode, out Response);
            }
            else
            {
                ActiveOrInactiveOwner = new ActiveOrInactive()
                {
                    queryType = "active",
                    gymOwnerId = gymOwnerId,
                    updatedBy = Convert.ToInt32(Session["userId"])
                };

                helper.APIpost<ActiveOrInactive>(ActiveOrInactiveOwnerUri, Session["APIToken"].ToString(), ActiveOrInactiveOwner, out StatusCode, out Response);
            }

            if (StatusCode == 1)
            {
                ShowSuccessPopup(Response);
                BingGridView();
            }
            else
            {
                ShowInfoPopup(Response);
            }
        }
        catch (Exception Ex)
        {
            ShowErrorPopup(Ex);
        }
    }
    #endregion


    #region Add Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            ShowGridOrFields("F");
        }
        catch (Exception Ex)
        {
            ShowErrorPopup(Ex);
        }

    }
    #endregion

    #region Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ShowGridOrFields("G");
        Clear();
    }
    #endregion

    #region Show Grid Or Field
    private void ShowGridOrFields(string GridOrFields)
    {
        if (GridOrFields == "G")
        {
            divGv.Visible = true;
            DivForm.Visible = false;
        }
        else
        {
            divGv.Visible = false;
            DivForm.Visible = true;
        }
    }
    #endregion

    #region Clear
    private void Clear()
    {
        txtEmailId.Text = string.Empty;
        txtGymName.Text = string.Empty;
        txtMobileNo.Text = string.Empty;
        txtPassWord.Text = string.Empty;
        txtShortName.Text = string.Empty;
        txtUserName.Text = string.Empty;
        txtWebSiteUrl.Text = string.Empty;
        txtMobileNo.Attributes.Remove("readOnly");
        imgpreview.Src = "~/img/User.png";
        btnSubmit.Text = "Submit";
    }
    #endregion

    #region Alerts
    public void ShowSuccessPopup(string Message)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert(`" + Message.Trim() + "`);", true);
    }
    public void ShowInfoPopup(string Message)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert(`" + Message.Trim() + "`);", true);
    }
    public void ShowErrorPopup(Exception Ex)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert(`" + Ex.Message.Trim() + "`);", true);
    }
    #endregion

    public class ResponseMessage
    {
        public int StatusCode { get; set; }
        public string Response { get; set; }
    }
    public class InsertOwner
    {
        public string gymName { get; set; }
        public string shortName { get; set; }
        public string gymOwnerName { get; set; }
        public string mobileNumber { get; set; }
        public string passWord { get; set; }
        public string mailId { get; set; }
        public string logoUrl { get; set; }
        public string websiteUrl { get; set; }
        public int createdBy { get; set; }
    }

    public class UpdateOwner
    {
        public int gymOwnerId { get; set; }
        public string gymName { get; set; }
        public string shortName { get; set; }
        public string gymOwnerName { get; set; }
        public string mobileNumber { get; set; }
        public string passWord { get; set; }
        public string mailId { get; set; }
        public string logoUrl { get; set; }
        public string websiteUrl { get; set; }
        public int updatedBy { get; set; }
    }

    public class ActiveOrInactive
    {
        public string queryType { get; set; }
        public int gymOwnerId { get; set; }
        public int updatedBy { get; set; }
    }
}