using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Text;
using System.Configuration;

public partial class Login : System.Web.UI.Page
{
    readonly Helper Helper = new Helper();
    readonly private string BaseUri;
    readonly private string CheckLogin;
    readonly private string CheckReLogin;
    private string Token;
    public Login()
    {
        BaseUri = $"{ConfigurationManager.AppSettings["BaseUrl"].Trim()}";
        CheckLogin = $"{BaseUri}login/checkLogin";
        CheckReLogin = $"{BaseUri}login/checkReLogin";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Clear();
            Session["BaseUrl"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"].Trim();
            Session["BaseUrlToken"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrlToken"].Trim();
            GetApiToken();
        }
        Token = Session["APIToken"].ToString();
    }
    #region GetApiToken
    public void GetApiToken()
    {
        try
        {
            HttpClient client = new HttpClient();
            // client.BaseAddress = new Uri("http://localhost:44385/");
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Session["BaseUrlToken"].ToString() + "token");
            string value = "username=Fitness@gmail.com&password=Fitness@123&grant_type=password";
            request.Content = new StringContent(value,
                                                Encoding.UTF8,
                                                "application/x-www-form-urlencoded");//CONTENT-TYPE header

            HttpResponseMessage response = client.SendAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {
                var Token = response.Content.ReadAsStringAsync().Result;
                string ResponseMsg = JObject.Parse(Token)["access_token"].ToString();

                Session["APIToken"] = ResponseMsg;
            }
        }

        catch (Exception ex)
        {

        }
    }
    #endregion
    #region Login Click
    protected void btnSendOTP_Click(object sender, EventArgs e)
    {
        txtMobileNo.Enabled = false;
        SendOtp();
    }
    #endregion
    #region Get UserDetails
    public void GetUserDetails(string LoginOrReLogin = "Login")
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
                HttpResponseMessage response;
                if (txtMobileNo.Text != "")
                {
                    string sUrl = Session["BaseUrl"].ToString().Trim()
                         + "signIn?mobileNo=" + txtMobileNo.Text + "";

                    response = client.GetAsync(sUrl).Result;
                }
                else
                {
                    string sUrl = Session["BaseUrl"].ToString().Trim()
                        + "signIn?mobileNo=" + txtMail.Text + "";

                    response = client.GetAsync(sUrl).Result;
                }

                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);

                        if (!CheckLoginOrReLogin(LoginOrReLogin, dt.Rows[0]["userId"].ToString()))
                        {
                            ShowSinglLoginPopUp("Show");
                            return;
                        }

                        Session["userId"] = dt.Rows[0]["userId"].ToString();
                        Session["roleId"] = dt.Rows[0]["roleId"].ToString();
                        Session["userRole"] = dt.Rows[0]["roleName"].ToString();
                        Session["userName"] = dt.Rows[0]["UserName"].ToString();
                        Session["mailId"] = dt.Rows[0]["mailId"].ToString();
                        Session["mobileNo"] = dt.Rows[0]["mobileNo"].ToString();
                        Session["branchName"] = dt.Rows[0]["branchName"].ToString();

                        if (Session["userRole"].ToString().Trim() == "Sadmin")
                        {
                            Session["approvalStatus"] = "A";
                            Session["gymOwnerId"] = 0;
                            Session["branchId"] = 0;
                            Session["branchName"] = "";
                            // Response.Redirect("~/AdminLogin.aspx", false);
                            Response.Redirect("~/index.aspx?qUserId=" + Session["userId"].ToString() + "&qUserRole=" + Session["userRole"].ToString() +
                                "&qUserName=" + Session["userName"].ToString() + "&qroleId=" + Session["roleId"].ToString() + "&qmailId=" + Session["mailId"].ToString() +
                                "&qbranchName=" + Session["branchName"].ToString() + "&qapprovalStatus=" + Session["approvalStatus"].ToString() + "&qbranchId=" + Session["branchId"].ToString() +
                                 "&qgymOwnerId=" + Session["gymOwnerId"].ToString() + "&qAPIToken=" + Session["APIToken"].ToString() + "&qmobileNo=" + Session["mobileNo"].ToString() + "", false);
                        }
                        else if (Session["userRole"].ToString().Trim() == "GymOwner")
                        {
                            Session["approvalStatus"] = "A";
                            Session["gymOwnerId"] = dt.Rows[0]["gymOwnerId"].ToString();
                            Session["branchId"] = 0;
                            Session["branchName"] = "";
                            //Response.Redirect("~/OwnerLogin.aspx", false);
                            Response.Redirect("~/index.aspx?qUserId=" + Session["userId"].ToString() + "&qUserRole=" + Session["userRole"].ToString() +
                               "&qUserName=" + Session["userName"].ToString() + "&qroleId=" + Session["roleId"].ToString() + "&qmailId=" + Session["mailId"].ToString() +
                               "&qbranchName=" + Session["branchName"].ToString() + "&qapprovalStatus=" + Session["approvalStatus"].ToString() + "&qbranchId=" + Session["branchId"].ToString() +
                                "&qgymOwnerId=" + Session["gymOwnerId"].ToString() + "&qAPIToken=" + Session["APIToken"].ToString() + "&qmobileNo=" + Session["mobileNo"].ToString() + "", false);
                        }
                        //else if (Session["userRole"].ToString().Trim() == "Employee" || Session["userRole"].ToString().Trim() == "Admin")
                        //{
                        else if (Session["userRole"].ToString().Trim() == "Admin")
                        {
                            Session["approvalStatus"] = "W";
                            Session["gymOwnerId"] = dt.Rows[0]["gymOwnerId"].ToString();
                            Session["branchId"] = dt.Rows[0]["branchId"].ToString();
                            //Response.Redirect("~/DashBoard.aspx", false);
                            Response.Redirect("~/index.aspx?qUserId=" + Session["userId"].ToString() + "&qUserRole=" + Session["userRole"].ToString() +
                              "&qUserName=" + Session["userName"].ToString() + "&qroleId=" + Session["roleId"].ToString() + "&qmailId=" + Session["mailId"].ToString() +
                              "&qbranchName=" + Session["branchName"].ToString() + "&qapprovalStatus=" + Session["approvalStatus"].ToString() + "&qbranchId=" + Session["branchId"].ToString() +
                               "&qgymOwnerId=" + Session["gymOwnerId"].ToString() + "&qAPIToken=" + Session["APIToken"].ToString() + "&qmobileNo=" + Session["mobileNo"].ToString() + "", false);
                        }
                        else if (Session["userRole"].ToString().Trim() == "User")
                        {
                            Session["gymOwnerId"] = 0;
                            Session["branchId"] = 0;
                            txtotp.Visible = false;
                            btnCfmOtp.Visible = false;
                            btnResend.Visible = false;
                            lblOtp.Visible = false;
                            txtMobileNo.Text = string.Empty;
                            txtMail.Text = string.Empty;
                            txtotp.Text = string.Empty;
                            txtMobileNo.Enabled = true;
                            //Response.Redirect("~/Master/Configuration/ConfigMstr.aspx", false);
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('User Module Coming Soon !');", true);
                        }
                        else if (Session["userRole"].ToString().Trim() == "Trainer")
                        {
                            Session["approvalStatus"] = "W";
                            Session["gymOwnerId"] = dt.Rows[0]["gymOwnerId"].ToString();
                            Session["branchId"] = dt.Rows[0]["branchId"].ToString();
                            //Response.Redirect("~/DashBoard.aspx", false);
                            Response.Redirect("~/index.aspx?qUserId=" + Session["userId"].ToString() + "&qUserRole=" + Session["userRole"].ToString() +
                              "&qUserName=" + Session["userName"].ToString() + "&qroleId=" + Session["roleId"].ToString() + "&qmailId=" + Session["mailId"].ToString() +
                              "&qbranchName=" + Session["branchName"].ToString() + "&qapprovalStatus=" + Session["approvalStatus"].ToString() + "&qbranchId=" + Session["branchId"].ToString() +
                               "&qgymOwnerId=" + Session["gymOwnerId"].ToString() + "&qAPIToken=" + Session["APIToken"].ToString() + "&qmobileNo=" + Session["mobileNo"].ToString() + "", false);
                        }
                    }
                    else
                    {
                        Signup();
                    }
                }
                else
                {
                    txtMobileNo.Enabled = true;
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
    #region SendOtp
    public void SendOtp()
    {
        try
        {
            using (var client = new HttpClient())
            {
                // hfMobileNo.Value = txtMobileNo.Text;
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new SendOTPClass()
                {
                    queryType = "sendSignInOtp",
                    mobileNo = txtMobileNo.Text

                };
                HttpResponseMessage response = client.PostAsJsonAsync("sendOtp", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "SendOtp", "SendOtp();", true);
                        btnCfmOtp.Visible = true;
                        btnResend.Visible = true;
                        txtotp.Visible = true;
                        lblOtp.Visible = true;
                        string[] Response = ResponseMsg.Split('~');
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Please Check the Mobile Number!');", true);
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
    #region VerifyOtp
    public void VerifyOtp()
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
                var Insert = new VerifyOTPClass()
                {
                    mobileNo = txtMobileNo.Text,
                    otp = txtotp.Text
                };
                HttpResponseMessage response = client.PostAsJsonAsync("verifyOtp", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        GetUserDetails();
                    }
                    else
                    {
                        txtotp.Text = string.Empty;
                        btnResend.Text = "Resend OTP";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Please Check the OTP');", true);
                    }
                }
                else
                {
                    var Errorresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Errorresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Errorresponse)["Response"].ToString();
                    if (statusCode == 0)
                    {
                        txtotp.Text = string.Empty;
                        btnResend.Text = "Resend OTP";
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
    #region VerifyOTP Class
    public class VerifyOTPClass
    {
        public string mobileNo { get; set; }
        public string otp { get; set; }
    }
    #endregion
    #region SendOTP Class
    public class SendOTPClass
    {
        public string queryType { get; set; }
        public string mobileNo { get; set; }
    }
    #endregion
    #region Signup Class
    public class SignupClass
    {
        public string mobileNo { get; set; }
        public string mailId { get; set; }
        public string passWord { get; set; }
    }
    #endregion
    #region Signup
    public void Signup()
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
                HttpResponseMessage response;
                string email;
                if (txtMobileNo.Text != "")
                {
                    email = txtMobileNo.Text;
                }
                else
                {
                    email = txtMail.Text;
                }

                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                if (match.Success)
                {
                    var Insert = new SignupClass()
                    {
                        mailId = txtMail.Text,
                        mobileNo = ""
                    };
                    response = client.PostAsJsonAsync("signUp", Insert).Result;

                }
                else
                {
                    var Insert = new SignupClass()
                    {
                        mobileNo = txtMobileNo.Text,
                        mailId = ""
                    };
                    response = client.PostAsJsonAsync("signUp", Insert).Result;
                }
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        GetUserDetails();
                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    else
                    {
                        txtotp.Visible = false;
                        btnCfmOtp.Visible = false;
                        btnResend.Visible = false;
                        lblOtp.Visible = false;
                        txtMobileNo.Text = string.Empty;
                        txtMail.Text = string.Empty;
                        txtotp.Text = string.Empty;
                        txtMobileNo.Enabled = true;
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Please Check the Mobile Number!');", true);
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
    #region Resend
    protected void btnResend_Click(object sender, EventArgs e)
    {
        SendOtp();
    }
    #endregion
    #region CfmOtp
    protected void btnCfmOtp_Click(object sender, EventArgs e)
    {
        if (txtotp.Text != "")
        {
            VerifyOtp();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "SendOtp", "SendOtp();", true);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Enter OTP !');", true);
        }

    }
    #endregion
    #region txtMail
    protected void txtMail_TextChanged(object sender, EventArgs e)
    {
        GetUserDetails();
    }
    #endregion

    #region SingleLogin
    /// <summary>
    /// this method is used to close the single login popup
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LnkbtnSingleLoginClose_Click(object sender, EventArgs e)
    {
        try
        {
            ShowSinglLoginPopUp(ShowOrHide: "Hide");
        }
        catch (Exception Ex)
        {
            ShowErrorPopup(Ex);
        }
    }
    /// <summary>
    /// this method is used relogin
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSingleLoginPopUpOk_Click(object sender, EventArgs e)
    {
        try
        {
            GetUserDetails(LoginOrReLogin: "ReLogin");
        }
        catch (Exception Ex)
        {
            ShowErrorPopup(Ex);
        }
    }
    /// <summary>
    /// this method is used to show or hide single login popup
    /// </summary>
    /// <param name="ShowOrHide">pass "Show" or "Hide"</param>
    private void ShowSinglLoginPopUp(string ShowOrHide)
    {
        if (ShowOrHide == "Show")
        {
            SingleLogin_Overlay.Attributes.Add("class", "SingleLogin_Overlay d-block");
            SingleLogin_Popup.Attributes.Add("class", "SingleLogin_Popup d-block");
        }
        else
        {
            SingleLogin_Overlay.Attributes.Add("class", "SingleLogin_Overlay d-none");
            SingleLogin_Popup.Attributes.Add("class", "SingleLogin_Popup d-none");
        }
    }
    /// <summary>
    /// this method is used to check if the user is already logged in
    /// </summary>
    private bool CheckLoginOrReLogin(string LoginOrReLogin, string UserId)
    {
        try
        {
            string SessionId = Session.SessionID.Trim();
            int StatusCode = 0;
            string Response = null;
            if (LoginOrReLogin == "Login")
            {
                UpdateSingleLogin_In Input = new UpdateSingleLogin_In()
                {
                    SessionId = SessionId,
                    UserId = int.Parse(UserId.Trim())
                };
                Helper.APIpost<UpdateSingleLogin_In>(CheckLogin, Token, Input, out StatusCode, out Response);
            }
            else
            {
                UpdateSingleReLogin_In Input = new UpdateSingleReLogin_In()
                {
                    SessionId = SessionId,
                    UserId = int.Parse(UserId.Trim())
                };
                Helper.APIpost<UpdateSingleReLogin_In>(CheckReLogin, Token, Input, out StatusCode, out Response);
            }

            if (StatusCode == 1)
            {
                if (SessionId == Response.Trim())
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            throw;
        }
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

    #region Login And ReLogin Input Class
    public class UpdateSingleLogin_In
    {
        public int UserId { get; set; }
        public string SessionId { get; set; }
    }

    public class UpdateSingleReLogin_In
    {
        public int UserId { get; set; }
        public string SessionId { get; set; }
    }
    #endregion
}