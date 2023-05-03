using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FitnessMstr : System.Web.UI.MasterPage
{
    readonly Helper Helper = new Helper();
    readonly private string BaseUri;
    readonly private string GetSessionIdUri;
    readonly private string GetFormAccessRightUri;
    readonly private string LogoutUri;
    private string Token;
    public FitnessMstr()
    {
        BaseUri = $"{ConfigurationManager.AppSettings["BaseUrl"].Trim()}";
        GetSessionIdUri = $"{BaseUri}/login/GetSessionId";
        LogoutUri = $"{ConfigurationManager.AppSettings["LogoutUrl"].Trim()}";
        GetFormAccessRightUri = $"{BaseUri}mstrFormAccessRights/getFormAccessRights";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Token = Session["APIToken"].ToString();
            if (!IsPostBack)
            {
                lblBranchName.Text = Session["branchName"].ToString();
                HideAllForms();
                if (Session["userRole"].ToString().Trim() == "Sadmin" & Session["branchId"].ToString().Trim() == "0")
                {
                    Logotext.Visible= false;
                    Logoimg.Visible = false;
                    SadminBeforeLogin();
                    DivbranchName.Visible = false;
                }
                else if (Session["userRole"].ToString().Trim() == "Sadmin")
                {
                    GetGymownerLogo();
                    SadminAfterLogin();
                }
                else if (Session["userRole"].ToString().Trim() == "GymOwner" & Session["branchId"].ToString().Trim() == "0")
                {
                    GetGymownerLogo();
                    OwnerBeforeBranchLogin();
                    DivbranchName.Visible = false;
                }
                else if (Session["userRole"].ToString().Trim() == "GymOwner")
                {
                    GetGymownerLogo();
                    OwnerAfterBranchLogin();
                }
                //else if (Session["userRole"].ToString().Trim() == "Employee" || Session["userRole"].ToString().Trim() == "Admin")
                //{
                //    GetGymownerLogo();
                //    GetFormAccessrights();
                //    AdminLoginRights();
                //}
                else if (Session["userRole"].ToString().Trim() == "Admin")
                {
                    GetGymownerLogo();
                    GetFormAccessrights();
                    AdminLoginRights();
                }
                else if (Session["userRole"].ToString().Trim() == "Trainer")
                {
                    GetGymownerLogo();
                    GetFormAccessrights();
                    TrainerLogin();
                }

            }
            //LogoutAlreadyLoggedInUser();          
        }
        catch (Exception Ex)
        {
            ShowErrorPopup(Ex);
        }
    }
    #region Hide All Forms
    private void HideAllForms()
    {
        try
        {
            username.Text = Session["userName"].ToString().Trim()  + "-" + Session["userRole"].ToString().Trim();

            //Dashboard
            NavDashboard.Visible = false;

            //Common Setup 
            NavCommonSetup.Visible = false;

            CommonSetup_FrmConfigType.Visible = false;
            CommonSetup_FrmConfigMaster.Visible = false;

            CommonSetup_HdgFoodMenuSetup.Visible = false;

            CommonSetup_FrmDietTypeMaster.Visible = false;
            CommonSetup_FrmFoodItemMaster.Visible = false;
            CommonSetup_FrmFoodDietTime.Visible = false;
            CommonSetup_FrmMealTimeConfig.Visible = false;

            //--------Gym Setup-----------//
            NavGymSetup.Visible = false;

            //Gym Owner
            DivGymSetup_GymOwner.Visible = false;

            GymSetup_HdgGymOwner.Visible = false;
            GymSetup_FrmownerMaster.Visible = false;

            //Branch SetUp
            DivGymSetup_Branchsetup.Visible = false;

            GymSetup_HdgBranchSetup.Visible = false;
            GymSetup_FrmBranchMaster.Visible = false;
            GymSetup_FrmBranchGallery.Visible = false;
            GymSetup_FrmBranchWorkingDays.Visible = false;
            GymSetup_FrmWorkOutType.Visible = false;
            GymSetup_FrmTrainingType.Visible = false;
            GymSetup_FrmOfferMaster.Visible = false;
            GymSetup_FrmOfferMapping.Visible = false;
            GymSetup_FrmSubscriptionPlan.Visible = false;
            GymSetup_FrmTaxMaster.Visible = false;
            GymSetup_FrmFooter.Visible = false;

            //Fitness Plan Setup
            DivGymSetup_FitnessCategory.Visible = false;

            GymSetup_HdgFitnessPlanSetup.Visible = false;
            GymSetup_FrmCategoryMaster.Visible = false;
            GymSetup_FrmCategoryPrice.Visible = false;
			GymSetup_FrmCategoryDietPlan.Visible = false;
			GymSetup_FrmCategoryWorkOutPlan.Visible = false;

			//Employee Setup
			DivGymSetup_Employeesetup.Visible = false;

            GymSetup_FrmEmployeeMaster.Visible = false;
            GymSetup_FrmShiftMaster.Visible = false;
            GymSetup_FrmMenuAccessRights.Visible = false;

            //Menu Options
            GymSetup_FrmMenuOptions.Visible = false;

            //Other Setup
            NavOtherSetup.Visible = false;
            OtherSetup_FrmFAQMaster.Visible = false;
            OtherSetup_FrmMessageTemplates.Visible = false;
            OtherSetup_FrmAppsetting.Visible = false;

            //Enrollment
            NavUserEntroll.Visible = false;
            Enrollment_FrmNewEnrollment.Visible = false;
            Enrollment_FrmManageLeads.Visible = false;
            Enrollment_FrmExcelBasedEnroll.Visible = false;
            Enrollment_FrmFollowUp.Visible = false;
            Enrollment_FrmYouTubeLive.Visible = false;
            //Tools
            NavTools.Visible = false;
            Tools_FrmDietandWorkOutPlan.Visible = false;
            Tools_FrmTools.Visible = false;
            //Report
            NavReports.Visible = false;

            //Admin Login
            AdminLogin.Visible = false;

            //Back To Admin Login
            BackAdminLogin.Visible = false;

            //Owner Login
            ownerLogin.Visible = false;

            //Back To Owner Login
            BackTownerLogin.Visible = false;

            //Newforms
            Div_Newforms.Visible = false;
            Newforms_Slot.Visible = false;
            Newforms_DeactivateSlot.Visible = false;
            Newforms_AssignTrainer.Visible = false;
            Newforms_Trainer.Visible = false;
            Newforms_TrainerDetails.Visible = false;
            //OtherSetup_TrainerAttendance.Visible = false;
            //Newforms_UserAttendance.Visible = false;
            Newforms_UserSlotSwapping.Visible = false;
            //Newforms_TrainerTrackinguser.Visible = false;
            //Newforms_TrainerReassign.Visible = false;
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion
    #region Form Rights For Sadmin Before Login Into Owner
    private void SadminBeforeLogin()
    {
        try
        {
            //Common Setup 
            NavCommonSetup.Visible = true;

            CommonSetup_FrmConfigType.Visible = true;
            CommonSetup_FrmConfigMaster.Visible = true;

            CommonSetup_HdgFoodMenuSetup.Visible = true;

            CommonSetup_FrmDietTypeMaster.Visible = true;
            //CommonSetup_FrmFoodItemMaster.Visible = false;
            //CommonSetup_FrmFoodDietTime.Visible = false;
            //CommonSetup_FrmMealTimeConfig.Visible = false;


            //--------Gym Setup-----------//
            NavGymSetup.Visible = true;

            //Gym Owner
            DivGymSetup_GymOwner.Visible = true;

            GymSetup_HdgGymOwner.Visible = true;
            GymSetup_FrmownerMaster.Visible = true;

            //Owner Branch
            DivGymSetup_Branchsetup.Visible = true;

            GymSetup_HdgBranchSetup.Visible = true;
            GymSetup_FrmBranchMaster.Visible = true;

            //Other Setup
            NavOtherSetup.Visible = true;
            OtherSetup_FrmFAQMaster.Visible = false;
            OtherSetup_FrmMessageTemplates.Visible = true;
            OtherSetup_FrmAppsetting.Visible = true;

            //Menu Options
            GymSetup_FrmMenuOptions.Visible = true;

            //Admin Login
            AdminLogin.Visible = true;
           

        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion
    #region Form Rights For Sadmin After Login Into Owner
    private void SadminAfterLogin()
    {
        try
        {
            //--------Gym Setup-----------//
            NavDashboard.Visible= true;
            NavGymSetup.Visible = true;

            //Branch SetUp
            DivGymSetup_Branchsetup.Visible = true;

            GymSetup_HdgBranchSetup.Visible = true;
            GymSetup_FrmBranchGallery.Visible = true;
            GymSetup_FrmBranchWorkingDays.Visible = true;
            GymSetup_FrmWorkOutType.Visible = true;
            GymSetup_FrmTrainingType.Visible = true;
            GymSetup_FrmOfferMaster.Visible = true;
            GymSetup_FrmOfferMapping.Visible = true;
            GymSetup_FrmSubscriptionPlan.Visible = true;
            GymSetup_FrmTaxMaster.Visible = true;
            GymSetup_FrmFooter.Visible = true;

            //Fitness Plan Setup
            DivGymSetup_FitnessCategory.Visible = true;


            GymSetup_HdgFitnessPlanSetup.Visible = true;
            GymSetup_FrmCategoryMaster.Visible = true;
            GymSetup_FrmCategoryPrice.Visible = true;
			GymSetup_FrmCategoryDietPlan.Visible = true;
			GymSetup_FrmCategoryWorkOutPlan.Visible = true;

			//Employee Setup
			DivGymSetup_Employeesetup.Visible = true;

            GymSetup_FrmEmployeeMaster.Visible = true;
            GymSetup_FrmShiftMaster.Visible = true;
            GymSetup_FrmMenuAccessRights.Visible = true;

            //Other Setup
            NavOtherSetup.Visible = true;
            OtherSetup_FrmFAQMaster.Visible = true;
            OtherSetup_FrmMessageTemplates.Visible = false;
            OtherSetup_FrmAppsetting.Visible = false;

            //Enrollment
            NavUserEntroll.Visible = true;
            Enrollment_FrmNewEnrollment.Visible = true;
            Enrollment_FrmManageLeads.Visible = true;
            Enrollment_FrmExcelBasedEnroll.Visible = true;
            Enrollment_FrmFollowUp.Visible = true;
            Enrollment_FrmYouTubeLive.Visible = true;
            //Tools
            NavTools.Visible = true;
            Tools_FrmDietandWorkOutPlan.Visible = true;
            Tools_FrmTools.Visible = true;
            //Back To Admin Login
            BackAdminLogin.Visible = true;


            Div_Newforms.Visible = true;
            CommonSetup_FrmFoodItemMaster.Visible = true;
            CommonSetup_FrmFoodDietTime.Visible = true;
            CommonSetup_FrmMealTimeConfig.Visible = true;

            //Newforms
            Newforms_Slot.Visible = true;
            Newforms_DeactivateSlot.Visible = true;
            Newforms_AssignTrainer.Visible = true;
            Newforms_Trainer.Visible = true;
            //OtherSetup_TrainerAttendance.Visible = true;
            //Newforms_TrainerReassign.Visible = true;
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion
    #region Form Rights For Owner Before Login Into Branch
    private void OwnerBeforeBranchLogin()
    {
        try
        {
            //--------Gym Setup-----------//
            NavGymSetup.Visible = true;

            //Owner Branch
            DivGymSetup_Branchsetup.Visible = true;

            GymSetup_HdgBranchSetup.Visible = true;
            GymSetup_FrmBranchMaster.Visible = true;

            //Owner Login
            ownerLogin.Visible = true;

            //Other Setup
            NavOtherSetup.Visible = true;
            OtherSetup_FrmFAQMaster.Visible = true;
            OtherSetup_FrmMessageTemplates.Visible = false;
            OtherSetup_FrmAppsetting.Visible = false;
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion
    #region Form Rights For Owner After Login Into Branch
    private void OwnerAfterBranchLogin()
    {
        try
        {
            //Dashboard
            NavDashboard.Visible = true;

            //--------Gym Setup-----------//
            NavGymSetup.Visible = true;

            //Branch SetUp
            DivGymSetup_Branchsetup.Visible = true;

            GymSetup_HdgBranchSetup.Visible = true;
            GymSetup_FrmBranchGallery.Visible = true;
            GymSetup_FrmBranchWorkingDays.Visible = true;
            GymSetup_FrmWorkOutType.Visible = true;
            GymSetup_FrmTrainingType.Visible = true;
            GymSetup_FrmOfferMaster.Visible = true;
            GymSetup_FrmOfferMapping.Visible = true;
            GymSetup_FrmSubscriptionPlan.Visible = true;
            GymSetup_FrmTaxMaster.Visible = true;
            GymSetup_FrmFooter.Visible = true;

            //Fitness Plan Setup
            DivGymSetup_FitnessCategory.Visible = true;

            GymSetup_HdgFitnessPlanSetup.Visible = true;
            GymSetup_FrmCategoryMaster.Visible = true;
            GymSetup_FrmCategoryPrice.Visible = true;
			GymSetup_FrmCategoryDietPlan.Visible = true;
			GymSetup_FrmCategoryWorkOutPlan.Visible = true;

			//Employee Setup
			DivGymSetup_Employeesetup.Visible = true;

            GymSetup_FrmEmployeeMaster.Visible = true;
            GymSetup_FrmShiftMaster.Visible = true;
            GymSetup_FrmMenuAccessRights.Visible = true;


            //Enrollment
            NavUserEntroll.Visible = true;
            Enrollment_FrmNewEnrollment.Visible = true;
            Enrollment_FrmManageLeads.Visible = true;
            Enrollment_FrmExcelBasedEnroll.Visible = true;
            Enrollment_FrmFollowUp.Visible = true;
            Enrollment_FrmYouTubeLive.Visible = true;
            //Tools
            NavTools.Visible = true;
            Tools_FrmDietandWorkOutPlan.Visible = true;
            Tools_FrmTools.Visible = true;
            //Back To Owner Login
            BackTownerLogin.Visible = true;


            Div_Newforms.Visible = true;
            CommonSetup_FrmFoodItemMaster.Visible = true;
            CommonSetup_FrmFoodDietTime.Visible = true;
            CommonSetup_FrmMealTimeConfig.Visible = true;

            //Newforms
            Newforms_Slot.Visible = true;
            Newforms_DeactivateSlot.Visible = true;
            Newforms_AssignTrainer.Visible = true;
            Newforms_Trainer.Visible = true;
            //OtherSetup_TrainerAttendance.Visible = true;
            //Newforms_TrainerReassign.Visible = true;
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
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert(`" + Message.Trim() + "`);", true);
    }
    public void ShowInfoPopup(string Message)
    {
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert(`" + Message.Trim() + "`);", true);
    }
    public void ShowErrorPopup(Exception Ex)
    {
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert(`" + Ex.Message.Trim() + "`);", true);
    }
    #endregion

    #region AdminLogin
    protected void lbtnAdminLogin_Click(object sender, EventArgs e)
    {
        Session["branchId"] = "0";
        Response.Redirect("~/AdminLogin.aspx", false);

    }
    #endregion

    #region GetFormsAccessRights()
    private void GetFormAccessrights()
    {
        try
        {
            string GetFormAccessRightFullUri = $"{GetFormAccessRightUri}?branchId={Session["branchId"]}&gymOwnerId={Session["gymOwnerId"]}&roleId={Session["roleId"]}&userId={Session["userId"]}";

            Helper.APIGet(GetFormAccessRightFullUri, Token, out DataTable dt, out int StatusCode, out string Response);

            //Dashboard
            NavDashboard.Visible = true;

            if (StatusCode == 1)
            {
                if (dt.Rows.Count > 0)
                {
                    ViewState["FormAccessRights"] = dt;

                 

                    bool CheckAtleastOneBranchSetupAvailable = false;
                    bool CheckAtleastOneFitnessPlanAvailable = false;
                    bool CheckAtleastOneEnrollmentAvailable = false;
                    bool CheckAtleastOneOtherSetupAvailable = false;
                    //bool CheckAtleastOneEmployeeSetupAvailable = false;
                    //bool CheckAtleastOneToolsAvailable = false;

                    //Branch SetUp
                    if (IsFormExists("Branch Gallery"))
                    {
                        CheckAtleastOneBranchSetupAvailable = true;
                        GymSetup_FrmBranchGallery.Visible = true;
                    }
                    else
                    {
                        GymSetup_FrmBranchGallery.Visible = false;
                    }

                    if (IsFormExists("Branch Working Days"))
                    {
                        CheckAtleastOneBranchSetupAvailable = true;
                        GymSetup_FrmBranchWorkingDays.Visible = true;
                    }
                    else
                    {
                        GymSetup_FrmBranchWorkingDays.Visible = false;
                    }

                    if (IsFormExists("Workout Type"))
                    {
                        CheckAtleastOneBranchSetupAvailable = true;
                        GymSetup_FrmWorkOutType.Visible = true;
                    }
                    else
                    {
                        GymSetup_FrmWorkOutType.Visible = false;
                    }

                    if (IsFormExists("Training Type"))
                    {
                        CheckAtleastOneBranchSetupAvailable = true;
                        GymSetup_FrmTrainingType.Visible = true;
                    }
                    else
                    {
                        GymSetup_FrmTrainingType.Visible = false;
                    }

                    if (IsFormExists("Offer Mapping"))
                    {
                        CheckAtleastOneBranchSetupAvailable = true;
                        GymSetup_FrmOfferMapping.Visible = true;
                    }
                    else
                    {
                        GymSetup_FrmOfferMapping.Visible = false;
                    }


                    if (IsFormExists("Subscription"))
                    {
                        CheckAtleastOneBranchSetupAvailable = true;
                        GymSetup_FrmSubscriptionPlan.Visible = true;
                    }
                    else
                    {
                        GymSetup_FrmSubscriptionPlan.Visible = false;
                    }

                    if (IsFormExists("Tax Master"))
                    {
                        CheckAtleastOneBranchSetupAvailable = true;
                        GymSetup_FrmTaxMaster.Visible = true;
                    }
                    else
                    {
                        GymSetup_FrmTaxMaster.Visible = false;
                    }

                    if (CheckAtleastOneBranchSetupAvailable)
                    {
                        DivGymSetup_Branchsetup.Visible = true;
                        GymSetup_HdgBranchSetup.Visible = true;
                        NavGymSetup.Visible = true;
                    }



                    //Fitness Plan Setup
                    if (IsFormExists("Category Master"))
                    {
                        CheckAtleastOneFitnessPlanAvailable = true;
                        GymSetup_FrmCategoryMaster.Visible = true;
                    }
                    else
                    {
                        GymSetup_FrmCategoryMaster.Visible = false;
                    }

                    if (IsFormExists("Category Price"))
                    {
                        CheckAtleastOneFitnessPlanAvailable = true;
                        GymSetup_FrmCategoryPrice.Visible = true;
                    }
                    else
                    {
                        GymSetup_FrmCategoryPrice.Visible = false;
                    }
                    if (IsFormExists("CategoryDiet"))
                    {
                        CheckAtleastOneFitnessPlanAvailable = true;
                        GymSetup_FrmCategoryDietPlan.Visible = true;
                    }
                    else
                    {
                        GymSetup_FrmCategoryDietPlan.Visible = false;
                    }
                    if (IsFormExists("CategoryWorkOut"))
                    {
                        CheckAtleastOneFitnessPlanAvailable = true;
                        GymSetup_FrmCategoryWorkOutPlan.Visible = true;
                    }
                    else
                    {
                        GymSetup_FrmCategoryWorkOutPlan.Visible = false;
                    }

                    if (CheckAtleastOneFitnessPlanAvailable)
                    {
                        DivGymSetup_FitnessCategory.Visible = true;
                        GymSetup_HdgFitnessPlanSetup.Visible = true;
                    }

                    //Other Setup

                    if (IsFormExists("FAQ"))
                    {
                        CheckAtleastOneOtherSetupAvailable = true;
                        OtherSetup_FrmFAQMaster.Visible = true;
                    }
                    else
                    {
                        OtherSetup_FrmFAQMaster.Visible = false;
                    }

                    if (IsFormExists("FAQ"))
                    {
                        CheckAtleastOneOtherSetupAvailable = true;
                        OtherSetup_FrmFAQMaster.Visible = true;
                    }
                    else
                    {
                        OtherSetup_FrmFAQMaster.Visible = false;
                    }
                    if (CheckAtleastOneOtherSetupAvailable)
                    {
                        NavOtherSetup.Visible = true;
                    }

                    //Enrollment
                    if (IsFormExists("New Enrollment"))
                    {
                        CheckAtleastOneEnrollmentAvailable = true;
                        Enrollment_FrmNewEnrollment.Visible = true;
                    }
                    else
                    {
                        Enrollment_FrmNewEnrollment.Visible = false;
                    }

                    if (IsFormExists("Manage Leads"))
                    {
                        CheckAtleastOneEnrollmentAvailable = true;
                        Enrollment_FrmManageLeads.Visible = true;
                    }
                    else
                    {
                        Enrollment_FrmManageLeads.Visible = false;
                    }

                    if (IsFormExists("Follow Up"))
                    {
                        CheckAtleastOneEnrollmentAvailable = true;
                        Enrollment_FrmFollowUp.Visible = true;
                    }
                    else
                    {
                        Enrollment_FrmFollowUp.Visible = false;
                    } 
                    if (IsFormExists("LiveConfig"))
                    {
                        CheckAtleastOneEnrollmentAvailable = true;
                        Enrollment_FrmYouTubeLive.Visible = true;
                    }
                    else
                    {
                        Enrollment_FrmYouTubeLive.Visible = false;
                    }

                    if (CheckAtleastOneEnrollmentAvailable)
                    {
                        NavUserEntroll.Visible = true;
                    }
                    
                    //Tools
                    NavTools.Visible = true;
                    Tools_FrmDietandWorkOutPlan.Visible = true;
                    Tools_FrmTools.Visible = true;
                }
            }

        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region Check If The Form Present in Table
    private bool IsFormExists(string FormName)
    {
        try
        {
            var dt = (DataTable)ViewState["FormAccessRights"];
            var CheckForm = dt.AsEnumerable().Where(dr => dr["optionName"].ToString() == FormName);

            if (CheckForm.Count() > 0)
            {
                var Filteredt = CheckForm.CopyToDataTable();
                return Filteredt.Rows[0]["activeStatus"].ToString() == "A" ? true : false;
            }
            return false;
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region OwnerLogin
    protected void lbtnownerLogin_Click(object sender, EventArgs e)
    {
        Session["branchId"] = "0";
        Response.Redirect("~/OwnerLogin.aspx", false);
    }
    #endregion
    #region logOut Already Logged In User
    private void LogoutAlreadyLoggedInUser()
    {
        try
        {
            string GetSessionIdFullUri = $"{GetSessionIdUri}?UserId={Session["UserId"]}&SessionId={Session.SessionID.Trim()}";
            Helper.APIGet(GetSessionIdFullUri, Token, out DataTable dt, out int StatusCode, out string Response);

            if (StatusCode == 1)
            {
                if (dt.Rows.Count == 0)
                {
                    Session.Clear();
                    Page.Response.Redirect(LogoutUri, true);
                }
            }
            else
            {
                Page.Response.Redirect(LogoutUri, true);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region Get GymownerLogo
    public string GetGymownerLogo()
    {
        string logourl = string.Empty;
        try
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                string Endpoint = "ownerMaster/IndividualOwner?gymOwnerId=" + Session["gymOwnerId"] + "";
                HttpResponseMessage response = client.GetAsync(Endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        //btnHomeFromDashBoard.ImageUrl = dt.Rows[0]["logoUrl"].ToString();
                        Logotext.Visible = true;
                        Logoimg.Visible = true;
                        Logotext.Text = dt.Rows[0]["gymName"].ToString();
                        Logoimg.ImageUrl =   dt.Rows[0]["logoUrl"].ToString();

                    }
                    else
                    {
                        logourl = "../images/master/logoFitness.svg";
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
        return logourl;
    }
    #endregion

    #region Form Rights For Trainer Login
    private void TrainerLogin()
    {
        try
        {
            NavGymSetup.Visible = true;
            Div_Newforms.Visible = true;
            //Newforms
            Newforms_TrainerDetails.Visible = true;
            //Newforms_UserAttendance.Visible = true;
            Newforms_UserSlotSwapping.Visible = true;
            //Newforms_TrainerTrackinguser.Visible = true;
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region Form Rights For Admin Login
    private void AdminLoginRights()
    {
        try
        {
            NavGymSetup.Visible = true;
            Div_Newforms.Visible = true;
            CommonSetup_FrmFoodItemMaster.Visible = true;
            CommonSetup_FrmFoodDietTime.Visible = true;
            CommonSetup_FrmMealTimeConfig.Visible = true;

            //Newforms
            Newforms_Slot.Visible = true;
            Newforms_DeactivateSlot.Visible = true;
            Newforms_AssignTrainer.Visible = true;
            Newforms_Trainer.Visible = true;
            //OtherSetup_TrainerAttendance.Visible = true;
            //Newforms_TrainerReassign.Visible = true;

            //Enrollment
            NavUserEntroll.Visible = true;
            Enrollment_FrmNewEnrollment.Visible = true;
            Enrollment_FrmManageLeads.Visible = true;
            Enrollment_FrmExcelBasedEnroll.Visible = true;
            Enrollment_FrmFollowUp.Visible = true;
            Enrollment_FrmYouTubeLive.Visible = true;

        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region Dashboard based on Login
    protected void lbtnDashboard_Click(object sender, EventArgs e)
    {
        //if (Session["userRole"].ToString().Trim() == "GymOwner")
        //{
        //    Response.Redirect("~/OwnerLogin.aspx", false);
        //}
        // if (Session["userRole"].ToString().Trim() == "Employee")
        //{
        //    Response.Redirect("~/AdminDashBoard.aspx", false);
        //}
        if (Session["userRole"].ToString().Trim() == "Trainer")
        {
            Response.Redirect("~/DashBoard.aspx", false);
        }
        else if (Session["userRole"].ToString().Trim() == "Admin" || (Session["userRole"].ToString().Trim() == "Sadmin" & Session["branchId"].ToString().Trim() != "0") 
            || (Session["userRole"].ToString().Trim() == "GymOwner" & Session["branchId"].ToString().Trim() != "0")) 
        {
            Response.Redirect("~/AdminDashBoard.aspx", false);
        }
    }
    #endregion
}
