using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_ExcelUpload_ExcelUploadForEnroll : System.Web.UI.Page
{
    readonly Helper helper = new Helper();
    readonly private string BaseUri;
    private string Token;
    readonly private string GetExcelUri;
    readonly private string UploadExcelUri;
    public Master_ExcelUpload_ExcelUploadForEnroll()
    {
        BaseUri = $"{ConfigurationManager.AppSettings["BaseUrl"].Trim()}";
        UploadExcelUri = $"{BaseUri}SignupExcel/Insert";
        GetExcelUri = $"{BaseUri}SignupExcel";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Token = Session["APIToken"].ToString();

            if (!IsPostBack)
            {
                HyperLnkDownLoad.NavigateUrl= GetExcelUri;
            }
        }
        catch (Exception Ex)
        {
            ShowErrorPopup(Ex);
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        UploadExcel();
    }
    private void UploadExcel()
    {
        try
        {
            UploadExcel_In Input = new UploadExcel_In()
            {
                RoleId = Session["roleId"].ToString(),
                BranchId = Session["branchId"].ToString(),
                GymOwnerId = Session["gymOwnerId"].ToString(),
                CreatedBy = Session["gymOwnerId"].ToString(),
            };

            if (FuExcel.HasFile)
            {
                helper.UploadExcel(FuExcel, UploadExcelUri, Input, Token, out int StatusCode, out string Response);

                if (StatusCode == 1)
                {
                    ShowSuccessPopup(Response);
                }
                else
                {
                    ShowInfoPopup(Response);
                }
            }
            else
            {
                ShowInfoPopup("Please upload a excel.");
            }
        }
        catch (Exception Ex)
        {
            ShowErrorPopup(Ex);
        }
    }

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

    #region Input Class
    public class UploadExcel_In
    {
        public string RoleId { get; set; }
        public string BranchId { get; set; }
        public string CreatedBy { get; set; }
        public string GymOwnerId { get; set; }
    }
    #endregion
}