using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["userId"] = "100002";
            //Session["gymOwnerId"] = "100002";
            //Session["branchId"] = "26";
            //Session["BaseUrl"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"].Trim();
            //Session["BaseUrlToken"] = System.Configuration.ConfigurationManager.AppSettings["BaseUrlToken"].Trim();
            //GetApiToken();
        }


    }
    public void GetApiToken()
    {
        try
        {
            HttpClient client = new HttpClient();
           // client.BaseAddress = new Uri("http://localhost:44385/");
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Session["BaseUrlToken"].ToString()+"token");
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

        catch (Exception )
        {
            throw;
        }
    }

}