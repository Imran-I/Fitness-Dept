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


public partial class Master_Branch_BranchGalleryMstr : System.Web.UI.Page
{
    Helper helper = new Helper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null && Session["userRole"] == null)
        {
            Session.Clear();
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["LogoutUrl"].Trim(), true);
        }
        if(!IsPostBack)
        {
            GetBranchGallery();
        }

    }
    #region Add Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divGv.Visible = false;
        DivForm.Visible = true;
        Fuimage.Dispose();
        imgGalleryPhotoPrev.ImageUrl = "~/img/Defaultupload.png";
    }
    #endregion
    #region Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        BranchGalleryClear();
    }
    #endregion   
    #region Submit 
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        divGv.Visible = true;
        DivForm.Visible = false;
        if (btnSubmit.Text == "Submit")
        {
            InsertImageType();
        }
        else
        {
            UpdateImageType();
        }

    }
    #endregion
    #region Get Branch Gallery
    public void GetBranchGallery()
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
                string URl = Session["BaseUrl"].ToString().Trim() + "branchGallery?branchId=" + Session["branchId"].ToString().Trim() + "";
                HttpResponseMessage response = client.GetAsync(URl).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Locresponse = response.Content.ReadAsStringAsync().Result;
                    int statusCode = Convert.ToInt32(JObject.Parse(Locresponse)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Locresponse)["Response"].ToString();
                    if (statusCode == 1)
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(ResponseMsg);
                        gvBranchGalleryMstr.DataSource = dt;
                        gvBranchGalleryMstr.DataBind();
                        divGridView.Visible = true;
                        divGv.Visible = true;
                    }
                    else
                    {
                        gvBranchGalleryMstr.DataSource = null;
                        gvBranchGalleryMstr.DataBind();
                        divGridView.Visible = false;
                        divGv.Visible = false;
                        DivForm.Visible = true;
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
    #region Insert Function 
    public void InsertImageType()
    {
        try
        {
            int StatusCodes;
            string ImageUrl;
            if (Fuimage.HasFile)
            {
                helper.UploadImage(Fuimage, Session["BaseUrl"].ToString().Trim() + "UploadImage", out StatusCodes, out ImageUrl);
            }
            else
            {
                if (imgGalleryPhotoPrev.ImageUrl != ""  && imgGalleryPhotoPrev.ImageUrl != "~/img/Defaultupload.png")
                {
                    ImageUrl = imgGalleryPhotoPrev.ImageUrl;
                }

                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Upload File');", true);
                    return;
                }
              
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new BranchGallery()
                {
                    branchId = Session["branchId"].ToString(),
                    imageUrl = ImageUrl,
                    createdBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("branchGallery/insert", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    GetBranchGallery();
                    BranchGalleryClear();
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
    public void UpdateImageType()
    {
        try
        {
            int StatusCodes;
            string ImageUrl;
            if (Fuimage.HasFile)
            {
                helper.UploadImage(Fuimage, Session["BaseUrl"].ToString().Trim() + "UploadImage", out StatusCodes, out ImageUrl);
            }
            else
            {
                if (imgGalleryPhotoPrev.ImageUrl != "" && imgGalleryPhotoPrev.ImageUrl != "~/img/Defaultupload.png")
                {
                    ImageUrl = imgGalleryPhotoPrev.ImageUrl;
                }
                
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Upload File');", true);
                    return;
                }
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());
                var Insert = new BranchGallery()
                {
                    branchId=Session["branchId"].ToString(),
                    imageId = ViewState["imageId"].ToString(),
                    imageUrl = ImageUrl,
                    updatedBy = Session["userId"].ToString()

                };
                HttpResponseMessage response = client.PostAsJsonAsync("branchGallery/update", Insert).Result;

                if (response.IsSuccessStatusCode)
                {
                    var FinessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FinessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FinessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    GetBranchGallery();
                    BranchGalleryClear();
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

            Label lblimageId = (Label)gvrow.FindControl("lblimageId");
            Label lblimageUrl = (Label)gvrow.FindControl("lblimageUrl");

            imgGalleryPhotoPrev.ImageUrl = lblimageUrl.Text.Trim();
            if (imgGalleryPhotoPrev.ImageUrl == "")
            {
                imgGalleryPhotoPrev.ImageUrl = "~/img/Defaultupload.png";
            }

            ViewState["imageId"] = lblimageId.Text.Trim();
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
    #region Clear
    public void BranchGalleryClear()
    {
        Fuimage.Dispose();
        imgGalleryPhotoPrev.ImageUrl = "~/img/Defaultupload.png";
        DivForm.Visible = false;
        divGv.Visible = true;
        btnSubmit.Text = "Submit";
    }
    #endregion
    #region Active or Inactive  Click Event
    protected void lnkActiveOrInactive_Click(object sender, EventArgs e)
    {

        try
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            Label lblimageId = (Label)gvrow.FindControl("lblimageId");

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
                var activeOrInActive = new BranchGallery()
                {
                    queryType = QueryType.Trim(),
                    imageId = lblimageId.Text.Trim(),
                    updatedBy = Session["userId"].ToString()
                };
                HttpResponseMessage response = client.PostAsJsonAsync("branchGallery/activeOrInActive", activeOrInActive).Result;
                if (response.IsSuccessStatusCode)
                {
                    var Fitness = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(Fitness)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(Fitness)["Response"].ToString();

                    if (StatusCode == 1)
                    {

                        GetBranchGallery();
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
    #region BranchGallery Class
    public class BranchGallery
    {
        public string queryType { get; set; }
        public string imageId { get; set; }
        public string branchId { get; set; }
        public string imageUrl { get; set; }
        public string activeStatus { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
    }
    #endregion
}