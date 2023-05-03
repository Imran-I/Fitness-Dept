using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class OwnerLogin : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBranch();
            BindBranchDetails();
        }
    }
    #endregion
    #region Bind DropDown Branch
    public void BindBranch()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "branch/GetDropDownDetails?queryType=ddlBranchMstrOwner&gymOwnerId="
                             + Session["userId"].ToString().Trim();
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
                            ddlBranch.DataSource = dt;
                            ddlBranch.DataTextField = "branchName";
                            ddlBranch.DataValueField = "branchId";
                            ddlBranch.DataBind();
                        }
                        else
                        {
                            ddlBranch.DataBind();
                        }
                        ddlBranch.Items.Insert(0, new ListItem("Branch *", "0"));
                    }
                    else
                    {
                        ddlBranch.Items.Clear();
                        ddlBranch.Items.Insert(0, new ListItem("Branch *", "0"));
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('" + ResponseMsg.ToString().Trim() + "');", true);
                    }
                }
                else
                {
                    ddlBranch.Items.Clear();
                    ddlBranch.Items.Insert(0, new ListItem("Branch *", "0"));
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

    #region Bind Branch Details to Owner
    public void BindBranchDetails()
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

                string sUrl = Session["BaseUrl"].ToString().Trim() + "branch/GetOwnerCurrentDayDetail?queryType=ddlBranchMstrOwner&gymOwnerId="
                             + Session["userId"].ToString().Trim();
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
                            UserCount.DataSource = dt;
                            UserCount.DataBind();
                            divUserCount.DataSource = dt;
                            divUserCount.DataBind();

                            decimal totalamount = 0;
                            int totaluser= 0;

                            // Replace "myList" with the ID of your DataList control
                            foreach (DataListItem item in UserCount.Items)
                            {
                                int valueuser;
                                if (Int32.TryParse(((Label)item.FindControl("lblusers")).Text, out valueuser))
                                {
                                    totaluser += valueuser;
                                }
                            }
                            foreach (DataListItem item in divUserCount.Items)
                            {
                                Label myLabel = (Label)item.FindControl("lbltotalamount") as Label;
                                string lbltotalamount = myLabel.Text;
                                string[] myArray = lbltotalamount.Split(new char[] { '₹' });
                               
                                // Replace "columnIndex" with the index of the column you want to sum
                                decimal value;
                                if (decimal.TryParse(myArray[1], out value))
                                {
                                    totalamount += value;
                                }
                            }
                            string myamount = totalamount.ToString();
                            string myusers = totaluser.ToString();
                            lbluseramount.Text = '₹'+ myamount;
                            lblusercounts.Text = myusers;

                        }
                        else
                        {
                            UserCount.DataBind();
                            divUserCount.DataBind();
                        }
                       
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
    #endregion

    #region  DropDown gymowner Index
    protected void ddlgymowner_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBranch();
    }
    #endregion
    #region Submit
    protected void btnSubmit_Click(object sender, EventArgs e)
    {       
        Session["branchId"] = ddlBranch.SelectedValue;
        Session["branchName"] = ddlBranch.SelectedItem.Text;
        Response.Redirect("~/AdminDashBoard.aspx", false);

    }
    #endregion
    #region Cancel
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlBranch.SelectedValue = "0";
        //ddlBranch.Items.Clear();
        //ddlBranch.Items.Add(new ListItem("Branch * ", "0"));
    }
    #endregion
}