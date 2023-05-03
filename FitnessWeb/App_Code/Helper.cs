using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

/// <summary>
/// this class contains common methods 
/// </summary>
public class Helper
{
    /// <summary>
    /// this method is used to upload image
    /// </summary>
    /// <param name="File">Input file</param>
    /// <param name="requestUri">requestUri</param>
    /// <param name="StatusCode">int</param>
    /// <param name="Response">string</param>
    public void UploadImage(FileUpload File, string requestUri, out int StatusCode, out string Response)
    {
        try
        {
            ResponseMessage OutResponse = new ResponseMessage();
            if (File.HasFile)
            {
                HttpClient Client = new HttpClient();

                var content = new StreamContent(File.FileContent);
                var mpcontent = new MultipartFormDataContent();

                content.Headers.ContentType = new MediaTypeHeaderValue($"image/{File.FileName.Split('.')[1]}");
                mpcontent.Add(content, "Image", File.FileName);
                HttpResponseMessage response = Client.PostAsync(requestUri, mpcontent).Result;

                JavaScriptSerializer JsSerializer = new JavaScriptSerializer();

                string ResponseContent = response.Content.ReadAsStringAsync().Result;

                OutResponse = JsSerializer.Deserialize<ResponseMessage>(ResponseContent);

                StatusCode = OutResponse.StatusCode;
                Response = OutResponse.Response;
            }
            else
            {
                StatusCode = 0;
                Response = "No Image Found !!!.";
            }
        }
        catch (Exception)
        {
            throw;
        }
    }


    /// <summary>
    /// this method is used to upload image
    /// </summary>
    /// <param name="File">Input file</param>
    /// <param name="requestUri">requestUri</param>
    /// <param name="StatusCode">int</param>
    /// <param name="Response">string</param>
    public void UploadExcel(FileUpload File, string requestUri, string RoleId, string CreatedBy, string GymOwnerId,
     string BranchId, out int StatusCode, out string Response)
    {
        try
        {
            ResponseMessage OutResponse = new ResponseMessage();
            if (File.HasFile)
            {
                HttpClient Client = new HttpClient();
                // Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                var content = new StreamContent(File.FileContent);
                var mpcontent = new MultipartFormDataContent();

                content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = File.FileName
                };
                var values = new[]
                  {
                            new KeyValuePair<string, string>("RoleId", RoleId),
                            new KeyValuePair<string, string>("CreatedBy", CreatedBy),
                            new KeyValuePair<string, string>("GymOwnerId", GymOwnerId),
                            new KeyValuePair<string, string>("BranchId", BranchId),

                  };

                foreach (var keyValuePair in values)
                {
                    mpcontent.Add(new StringContent(keyValuePair.Value),
                        String.Format("\"{0}\"", keyValuePair.Key));
                }

                mpcontent.Add(content, "Excel", File.FileName);
                HttpResponseMessage response = Client.PostAsync(requestUri, mpcontent).Result;

                JavaScriptSerializer JsSerializer = new JavaScriptSerializer();

                string ResponseContent = response.Content.ReadAsStringAsync().Result;

                OutResponse = JsSerializer.Deserialize<ResponseMessage>(ResponseContent);

                StatusCode = OutResponse.StatusCode;
                Response = OutResponse.Response;
            }
            else
            {
                StatusCode = 0;
                Response = "No Image Found !!!.";
            }
        }
        catch (Exception)
        {
            throw;
        }
    }


    /// <summary>
    /// this method is used to call get method of API
    /// </summary>
    /// <param name="requestUri">requestUri</param>
    /// <param name="token">token</param>
    /// <param name="Dt">DataTable</param>
    /// <param name="StatusCode">int</param>
    /// <param name="Response">string</param>
    public void APIGet(string requestUri, string Token, out DataTable Dt, out int StatusCode, out string Response)
    {
        try
        {
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token.ToString());
            HttpResponseMessage response = Client.GetAsync(requestUri).Result;

            string Content = response.Content.ReadAsStringAsync().Result;
            string JSONResponse = JObject.Parse(Content)["Response"].ToString();
            StatusCode = Convert.ToInt32(JObject.Parse(Content)["StatusCode"]);


            if (StatusCode == 1)
            {
                Dt = JsonConvert.DeserializeObject<DataTable>(JSONResponse);
                Response = "";
            }
            else
            {
                Dt = null;
                Response = JSONResponse;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void APIpost<T>(string requestUri, string Token, T InputObject, out int StatusCode, out string Response)
    {
        try
        {
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token.ToString());
            HttpResponseMessage response = Client.PostAsJsonAsync<T>(requestUri, InputObject).Result;

            string Content = response.Content.ReadAsStringAsync().Result;

            Response = JObject.Parse(Content)["Response"].ToString();
            StatusCode = Convert.ToInt32(JObject.Parse(Content)["StatusCode"]);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public class ResponseMessage
    {
        public int StatusCode { get; set; }
        public string Response { get; set; }
    }


    /// <summary>
    /// this method is used to Insert Data By  uploading Excel
    /// </summary>
    public void UploadExcel(FileUpload File, string requestUri, dynamic InputObj, string Token, out int StatusCode, out string Response)
    {
        try
        {
            ResponseMessage OutResponse = new ResponseMessage();
            if (File.HasFile)
            {
                HttpClient Client = new HttpClient();
                Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                using (var content = new StreamContent(File.FileContent))
                {
                    var mpcontent = new MultipartFormDataContent();

                    content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                    var values = new[]
                      {
                            new KeyValuePair<string, string>("RoleId", InputObj.RoleId),
                            new KeyValuePair<string, string>("CreatedBy", InputObj.CreatedBy),
                            new KeyValuePair<string, string>("GymOwnerId", InputObj.GymOwnerId),
                            new KeyValuePair<string, string>("BranchId", InputObj.BranchId),
                  };

                    foreach (var keyValuePair in values)
                    {
                        mpcontent.Add(new StringContent(keyValuePair.Value),
                            String.Format("\"{0}\"", keyValuePair.Key));
                    }

                    mpcontent.Add(content, "Excel", File.FileName);

                    HttpResponseMessage response = Client.PostAsync(requestUri, mpcontent).Result;

                    JavaScriptSerializer JsSerializer = new JavaScriptSerializer();

                    string ResponseContent = response.Content.ReadAsStringAsync().Result;

                    OutResponse = JsSerializer.Deserialize<ResponseMessage>(ResponseContent);

                    StatusCode = OutResponse.StatusCode;
                    Response = OutResponse.Response;
                }
            }
            else
            {
                StatusCode = 0;
                Response = "No Image Found !!!.";
            }
        }
        catch (Exception)
        {
            throw;
        }
    }


}