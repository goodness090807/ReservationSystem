using Newtonsoft.Json;
using ReservationSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ReservationSystem.Service
{
    public class SharedService
    {
        private readonly string GetProfileUrl = "https://api.line.me/v2/profile";

        public UserInfoViewModel GetUserInfoWithToken(string TokenID)
        {
            string result = string.Empty;
            WebRequest webRequest = HttpWebRequest.Create(GetProfileUrl);

            webRequest.Method = "GET";
            webRequest.Headers.Add("Authorization", $"Bearer {TokenID}");

            using (var httpResponse = (HttpWebResponse)webRequest.GetResponse())
            //使用 GetResponseStream 方法從 server 回應中取得資料，stream 必需被關閉
            //使用 stream.close 就可以直接關閉 WebResponse 及 stream，但同時使用 using 或是關閉兩者並不會造成錯誤，養成習慣遇到其他情境時就比較不會出錯
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            UserInfoViewModel userInfo = JsonConvert.DeserializeObject<UserInfoViewModel>(result);

            return userInfo;
        }

    }
}
