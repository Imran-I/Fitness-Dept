using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Master_MenuAccessRights_MenuOptionAccess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRole();
            BindOptionListForGrid();
        }

    }
    #region Bind Role
    public void BindRole()
    {
        try
        {
            ddlRoles.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sUrl = Session["BaseUrl"].ToString().Trim() + "userMenuAccess/getRoles";
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
                            ddlRoles.DataSource = dt;
                            ddlRoles.DataTextField = "roleName";
                            ddlRoles.DataValueField = "RoleId";
                            ddlRoles.DataBind();
                        }
                        else
                        {
                            ddlRoles.DataBind();
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlRoles.Items.Insert(0, new ListItem("Role *", "0"));
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
    #region Bind User
    public void BindUser()
    {
        try
        {
            ddlUserName.Items.Clear();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Session["BaseUrl"].ToString().Trim());
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["APIToken"].ToString());

                string sUrl = Session["BaseUrl"].ToString().Trim() + "userMenuAccess/getEmployee?roleId=" + ddlRoles.SelectedValue + "&branchId=" + Session["branchId"].ToString() + "";
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
                            ddlUserName.DataSource = dt;
                            ddlUserName.DataTextField = "fullName";
                            ddlUserName.DataValueField = "empId";
                            ddlUserName.DataBind();
                        }
                        else
                        {
                            ddlUserName.DataBind();
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                    ddlUserName.Items.Insert(0, new ListItem("User *", "0"));
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
    #region User Selected Index Changed
    protected void ddlUserName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUserName.SelectedIndex != 0)
        {

            BindOptionListForAdd();
        }

    }
    #endregion
    #region Role Selected Index Changed
    protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRoles.SelectedIndex != 0)
        {
            BindUser();
        }
    }
    #endregion
    #region Bind Option List   For Add
    public void BindOptionListForAdd()
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

                sUrl = Session["BaseUrl"].ToString().Trim() + "userMenuAccess/getMenuOptions?roleId=" + ddlRoles.SelectedValue + ""
                    + "&empId=" + ddlUserName.SelectedValue + "&gymOwnerId=" + Session["gymOwnerId"].ToString() + "&branchId=" + Session["branchId"].ToString() + "";

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
                            gvOption.DataSource = dt;
                            gvOption.DataBind();
                            divPages.Visible = true;
                            gvOption.Visible = true;
                            GvoptionEdit.Visible = false;
                        }
                        else
                        {
                            gvOption.DataSource = null;
                            gvOption.DataBind();
                            divPages.Visible = false;
                            gvOption.Visible = false;
                            GvoptionEdit.Visible = false;
                        }

                    }
                    else
                    {
                        gvOption.DataSource = null;
                        gvOption.DataBind();
                        divPages.Visible = false;
                        gvOption.Visible = false;
                        GvoptionEdit.Visible = false;
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
    #region Add Click Fucntion
    public void ADD()
    {
        ddlUserName.Items.Clear();
        divGv.Visible = false;
        divForm.Visible = true;
        divPages.Visible = false;
        GvoptionEdit.Visible = false;
        ddlUserName.Enabled = true;
        ddlRoles.Enabled = true;
        btnSubmit.Text = "Submit";

    }
    #endregion
    #region ADD Click
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //spAddorEdit.InnerText = "Add ";
        ADD();
    }
    #endregion
    #region Submit Click
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubmit.Text == "Submit")
        {
            Insertmenu();
        }
        else
        {
            UpdateMenu();
        }
    }
    #endregion
    #region Insert Function
    public void Insertmenu()
    {

        string optionId = string.Empty;
        string Activestatus = string.Empty;
        string AddRights = string.Empty;
        string ViewRights = string.Empty;
        string EditRights = string.Empty;
        string DeleteRights = string.Empty;
        int Fi = 0, Ai = 0, Ei = 0, Di = 0, Vi = 0;
        foreach (GridViewRow item in gvOption.Rows)
        {
            Label lbloptionId = item.FindControl("lbloptionId") as Label;
            CheckBox CheckBox1 = item.FindControl("CheckBox1") as CheckBox;

            if (CheckBox1.Checked == true)
            {
                Fi = 1;
                Activestatus += "A" + ',';
            }
            else
            {
                Activestatus += "D" + ',';
            }
            CheckBox AddCheckBox1 = item.FindControl("AddCheckBox1") as CheckBox;
            if (AddCheckBox1.Checked == true)
            {
                Ai = 1;
                AddRights += "Y" + ',';
            }
            else
            {
                AddRights += "N" + ',';
            }
            CheckBox ViewCheckBox1 = item.FindControl("ViewCheckBox1") as CheckBox;
            if (ViewCheckBox1.Checked == true)
            {
                Vi = 1;
                ViewRights += "Y" + ',';
            }
            else
            {
                ViewRights += "N" + ',';
            }
            CheckBox EditCheckBox1 = item.FindControl("EditCheckBox1") as CheckBox;
            if (EditCheckBox1.Checked == true)
            {
                Ei = 1;
                EditRights += "Y" + ',';
            }
            else
            {
                EditRights += "N" + ',';
            }
            CheckBox DeleteCheckBox1 = item.FindControl("DeleteCheckBox1") as CheckBox;
            if (DeleteCheckBox1.Checked == true)
            {
                Di = 1;
                DeleteRights += "Y" + ',';
            }
            else
            {
                DeleteRights += "N" + ',';
            }
            optionId += lbloptionId.Text + ',';

        }
        if (Fi == 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Check Any One Form Rights ');", true);
            return;
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
                InsertmenuOption Insert = new InsertmenuOption()
                {
                    ListOfInsertMenuAccess = GetOptionDetails(Session["gymOwnerId"].ToString(), Session["branchId"].ToString(),
                    ddlUserName.SelectedValue, ddlRoles.SelectedValue, optionId.ToString().TrimEnd(','), Activestatus.ToString().TrimEnd(','),
                AddRights.ToString().TrimEnd(','), EditRights.ToString().TrimEnd(','), ViewRights.ToString().TrimEnd(','),
                DeleteRights.ToString().TrimEnd(','), Session["userId"].ToString()),

                };

                HttpResponseMessage response = client.PostAsJsonAsync("userMenuAccess/insert", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        Clear();
                        BindOptionListForGrid();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
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
    public static List<InsertmenuOptionAccess> GetOptionDetails(string GymOwnerId, string BranchId, string EmpId,
     string RoleId, string OptionId, string ActiveStatus,
        string AddRights, string EditRights, string ViewRights, string DeleteRights, string CreatedBy)
    {

        string[] optionIds;
        string[] activeStatuss;
        string[] AddRightss;
        string[] EditRightss;
        string[] ViewRightss;
        string[] DeleteRightss;
        AddRightss = AddRights.Split(',');
        EditRightss = EditRights.Split(',');
        ViewRightss = ViewRights.Split(',');
        DeleteRightss = DeleteRights.Split(',');
        optionIds = OptionId.Split(',');
        activeStatuss = ActiveStatus.Split(',');
        List<InsertmenuOptionAccess> lst = new List<InsertmenuOptionAccess>();
        for (int i = 0; i < optionIds.Count(); i++)
        {
            lst.AddRange(new List<InsertmenuOptionAccess>
            {
                new InsertmenuOptionAccess {GymOwnerId=GymOwnerId,BranchId=BranchId,EmpId=EmpId,RoleId=RoleId,
                OptionId=optionIds[i] ,ViewRights=ViewRightss[i],AddRights=AddRightss[i],EditRights=EditRightss[i],
               DeleteRights=DeleteRightss[i] ,ActiveStatus=activeStatuss[i],CreatedBy=CreatedBy}

            });
        }

        return lst;

    }
    #endregion

    #region Bind Option List   For GRID
    public void BindOptionListForGrid()
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

                sUrl = Session["BaseUrl"].ToString().Trim() + "userMenuAccess/getEmpNameForGV?"
                    + "gymOwnerId=" + Session["gymOwnerId"].ToString() + "&branchId=" + Session["branchId"].ToString() + "";

                HttpResponseMessage response = client.GetAsync(sUrl).Result;

                btnCancel.Visible = false;

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
                            gvMenuAccess.DataSource = dt;
                            gvMenuAccess.DataBind();
                            gvMenuAccess.Visible = true;

                            divGv.Visible = true;
                            divForm.Visible = false;
                            btnCancel.Visible = true;
                        }
                        else
                        {
                            gvOption.DataSource = null;
                            gvOption.DataBind();
                            gvMenuAccess.Visible = false;

                            divGv.Visible = false;
                            divForm.Visible = true;
                        }
                    }
                    else
                    {
                        gvOption.DataSource = null;
                        gvOption.DataBind();
                        gvMenuAccess.Visible = false;

                        divGv.Visible = false;
                        divForm.Visible = true;
                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
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

    protected void LnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkbtn = sender as ImageButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        Label lblgvempId = gvrow.FindControl("lblgvempId") as Label;
        Label lblgvroldId = gvrow.FindControl("lblgvroldId") as Label;

        BindRole();
        ddlRoles.SelectedValue = lblgvroldId.Text;
        ddlRoles.Enabled = false;
        BindUser();
        ddlUserName.SelectedValue = lblgvempId.Text;
        ddlUserName.Enabled = false;
        BindOptionListForEdit();
        btnSubmit.Text = "Update";
        divGv.Visible = false;
        divForm.Visible = true;
    }
    #region Bind Option List   For Edit
    public void BindOptionListForEdit()
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

                sUrl = Session["BaseUrl"].ToString().Trim() + "userMenuAccess/getMenuOptionsForUpdate?roleId=" + ddlRoles.SelectedValue + ""
                    + "&empId=" + ddlUserName.SelectedValue + "&gymOwnerId=" + Session["gymOwnerId"].ToString() + "&branchId=" + Session["branchId"].ToString() + "";

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
                            GvoptionEdit.DataSource = dt;
                            GvoptionEdit.DataBind();
                            GvoptionEdit.Visible = true;
                            gvOption.Visible = false;
                            divPages.Visible = true;

                        }
                        else
                        {
                            GvoptionEdit.DataSource = null;
                            GvoptionEdit.DataBind();
                            GvoptionEdit.Visible = false;
                            gvOption.Visible = false;
                            divPages.Visible = false;
                        }

                    }
                    else
                    {
                        divPages.Visible = false;
                        GvoptionEdit.DataSource = null;
                        GvoptionEdit.DataBind();
                        GvoptionEdit.Visible = false;
                        gvOption.Visible = false;
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
    #region Update Function
    public void UpdateMenu()
    {
        string optionId = string.Empty;
        string MenuOptionAcessId = string.Empty;
        string Activestatus = string.Empty;
        string AddRights = string.Empty;
        string ViewRights = string.Empty;
        string EditRights = string.Empty;
        string DeleteRights = string.Empty;
        int Fi = 0, Ai = 0, Ei = 0, Di = 0, Vi = 0;
        foreach (GridViewRow item in GvoptionEdit.Rows)
        {
            Label lbloptionId = item.FindControl("lbloptionId") as Label;
            Label lblMenuOptionAcessId = item.FindControl("lblMenuOptionAcessId") as Label;
            CheckBox CheckBox1 = item.FindControl("CheckBox1") as CheckBox;

            if (CheckBox1.Checked == true)
            {
                Fi = 1;
                Activestatus += "A" + ',';
            }
            else
            {
                Activestatus += "D" + ',';
            }
            CheckBox AddCheckBox1 = item.FindControl("AddCheckBox1") as CheckBox;
            if (AddCheckBox1.Checked == true)
            {
                Ai = 1;
                AddRights += "Y" + ',';
            }
            else
            {
                AddRights += "N" + ',';
            }
            CheckBox ViewCheckBox1 = item.FindControl("ViewCheckBox1") as CheckBox;
            if (ViewCheckBox1.Checked == true)
            {
                Vi = 1;
                ViewRights += "Y" + ',';
            }
            else
            {
                ViewRights += "N" + ',';
            }
            CheckBox EditCheckBox1 = item.FindControl("EditCheckBox1") as CheckBox;
            if (EditCheckBox1.Checked == true)
            {
                Ei = 1;
                EditRights += "Y" + ',';
            }
            else
            {
                EditRights += "N" + ',';
            }
            CheckBox DeleteCheckBox1 = item.FindControl("DeleteCheckBox1") as CheckBox;
            if (DeleteCheckBox1.Checked == true)
            {
                Di = 1;
                DeleteRights += "Y" + ',';
            }
            else
            {
                DeleteRights += "N" + ',';
            }
            optionId += lbloptionId.Text + ',';
            MenuOptionAcessId += lblMenuOptionAcessId.Text + ',';

        }
        if (Fi == 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Check Any One Form Rights ');", true);
            return;
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
                UpdatemenuOption Insert = new UpdatemenuOption()
                {
                    listOfUpdateMenuAccess = GetOptionDetailss(MenuOptionAcessId.ToString().TrimEnd(','), Session["gymOwnerId"].ToString(), Session["branchId"].ToString(), ddlUserName.SelectedValue, ddlRoles.SelectedValue, optionId.ToString().TrimEnd(','), Activestatus.ToString().TrimEnd(','),
                     AddRights.ToString().TrimEnd(','), EditRights.ToString().TrimEnd(','), ViewRights.ToString().TrimEnd(','),
                      DeleteRights.ToString().TrimEnd(','), Session["userId"].ToString()),

                };

                HttpResponseMessage response = client.PostAsJsonAsync("userMenuAccess/update", Insert).Result;
                if (response.IsSuccessStatusCode)
                {
                    var FitnessList = response.Content.ReadAsStringAsync().Result;
                    int StatusCode = Convert.ToInt32(JObject.Parse(FitnessList)["StatusCode"].ToString());
                    string ResponseMsg = JObject.Parse(FitnessList)["Response"].ToString();

                    if (StatusCode == 1)
                    {
                        Clear();
                        BindOptionListForGrid();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "successalert('" + ResponseMsg.ToString().Trim() + "');", true);

                    }
                    else
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
    public static List<InsertmenuOptionAccess> GetOptionDetailss(string MenuOptionAcessId, string GymOwnerId, string BranchId, string EmpId,
     string RoleId, string OptionId, string ActiveStatus,
        string AddRights, string EditRights, string ViewRights, string DeleteRights, string UpdatedBy)
    {
        string[] optionIds; string[] MenuOptionAcessIds;
        string[] activeStatuss;
        string[] AddRightss;
        string[] EditRightss;
        string[] ViewRightss;
        string[] DeleteRightss;
        AddRightss = AddRights.Split(',');
        MenuOptionAcessIds = MenuOptionAcessId.Split(',');
        EditRightss = EditRights.Split(',');
        ViewRightss = ViewRights.Split(',');
        DeleteRightss = DeleteRights.Split(',');
        optionIds = OptionId.Split(',');
        activeStatuss = ActiveStatus.Split(',');
        List<InsertmenuOptionAccess> lst = new List<InsertmenuOptionAccess>();
        for (int i = 0; i < optionIds.Count(); i++)
        {
            lst.AddRange(new List<InsertmenuOptionAccess>
            {
                new InsertmenuOptionAccess {MenuOptionAcessId=MenuOptionAcessIds[i],GymOwnerId=GymOwnerId,BranchId=BranchId,EmpId=EmpId,RoleId=RoleId,
                OptionId=optionIds[i] ,ViewRights=ViewRightss[i],AddRights=AddRightss[i],EditRights=EditRightss[i],
               DeleteRights=DeleteRightss[i] ,ActiveStatus=activeStatuss[i],UpdatedBy=UpdatedBy}

            });
        }

        return lst;

    }
    #endregion
    #region Clear
    public void Clear()
    {
        divForm.Visible = false;
        divGv.Visible = true;
        ddlRoles.ClearSelection();
        ddlUserName.ClearSelection();
        ddlRoles.Enabled = true;
        ddlUserName.Enabled = true;
    }
    #endregion

    #region Cancel Click
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    #endregion

    #region Insert menu Option Access
    public class InsertmenuOptionAccess
    {
        public string GymOwnerId { get; set; }
        public string MenuOptionAcessId { get; set; }
        public string BranchId { get; set; }
        public string EmpId { get; set; }
        public string RoleId { get; set; }
        public string OptionId { get; set; }
        public string ActiveStatus { get; set; }
        public string AddRights { get; set; }
        public string EditRights { get; set; }
        public string ViewRights { get; set; }
        public string DeleteRights { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
    public class InsertmenuOption
    {
        public List<InsertmenuOptionAccess> ListOfInsertMenuAccess { get; set; }
    }
    public class UpdatemenuOption
    {
        public List<InsertmenuOptionAccess> listOfUpdateMenuAccess { get; set; }
    }

    #endregion

}