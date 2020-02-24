using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace MVCJob.Models.Jobs
{
    public class CallApiJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            HttpContent postContent = null;
            var headerStr = dataMap.GetString("Header");
            JObject header = (JObject)JsonConvert.DeserializeObject(headerStr);
            var dataStr = dataMap.GetString("Data");
            JObject data = (JObject)JsonConvert.DeserializeObject(dataStr);
            if (string.IsNullOrEmpty(header["Content-Type"].ToString())|| header["Content-Type"].ToString()== "application/x-www-form-urlencoded")
            {
                var param = JsonConvert.DeserializeObject<Dictionary<string, string>>(dataStr);
                postContent = new FormUrlEncodedContent(param);
            }
            else
            {
                postContent = new StringContent(dataStr);
                postContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                postContent.Headers.ContentType.CharSet = "utf-8";
            }
            var verify = JsonConvert.DeserializeObject<Dictionary<string, string>>(headerStr);
            verify.Remove("Content-Type");
            foreach(var item in verify)
            {
                postContent.Headers.Add(item.Key, item.Value);
            }
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.PostAsync(dataMap.GetString("Url"), postContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    var t = response.Content.ReadAsStringAsync();
                    string s = t.Result;
                    //string json = JsonConvert.DeserializeObject(s).ToString();
                    //var result = JsonConvert.DeserializeObject<T>(json);
                }
            }
        }
    }
}